using ComputeSharp;
using DiGi.ComputeSharp.Spatial.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Spatial;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.Solar.Classes;
using DiGi.Solar.ComputeSharp.Enums;
using DiGi.Solar.Enums;
using DiGi.Solar.Interfaces;
using System.Runtime.CompilerServices;

namespace DiGi.Solar.ComputeSharp.Classes
{
    /// <summary>
    /// Provides a solver implementation to calculate shading effects on objects using ComputeSharp for GPU acceleration.
    /// <para>Derives from the CPU <see cref="Solar.Classes.ShadingSolver"/>, which it falls back to when <see cref="ComputeDeviceType"/> is <see cref="ComputeDeviceType.Default"/> and no supported hardware device is available.</para>
    /// </summary>
    public class ShadingSolver : Solar.Classes.ShadingSolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolver"/> class with the specified shading model and options.
        /// </summary>
        /// <param name="shadingModel">The shading model to be used for calculations.</param>
        /// <param name="shadingSolverOptions">The options that configure the solver's behavior.</param>
        public ShadingSolver(ShadingModel? shadingModel, ShadingSolverOptions? shadingSolverOptions)
            : base(shadingModel, shadingSolverOptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolver"/> class with the specified shading model and a collection of date-times.
        /// </summary>
        /// <param name="shadingModel">The shading model to be used for calculations.</param>
        /// <param name="dateTimes">An array of date-time values for which shading should be calculated.</param>
        public ShadingSolver(ShadingModel? shadingModel, DateTime[]? dateTimes)
            : base(shadingModel, dateTimes)
        {
        }

        /// <summary>
        /// Gets or sets the compute device the shaders run on. Defaults to <see cref="ComputeDeviceType.Default"/>.
        /// </summary>
        public ComputeDeviceType ComputeDeviceType { get; set; } = ComputeDeviceType.Default;

        /// <summary>
        /// Gets or sets the upper bound, in bytes, of the shadow record buffer the solver allocates (the device buffer and its managed readback alike). Defaults to 256 MB.
        /// <para>The buffer holds only the shadows found (one <see cref="ShadowPolygon2"/> per receiver and caster triangle pair that casts one), so it grows with the hits, not with receivers x triangles; it starts small and grows up to this bound.
        /// When the hits of one receiver block would exceed it, the block is split into fewer receivers, so a smaller value lowers peak memory at the cost of more dispatches.
        /// A single receiver always runs, even when its hits exceed the bound (one receiver has at most one record per caster triangle).
        /// The shadows kept for the union after the readback are not bounded by this value: they grow with the total number of hits.</para>
        /// </summary>
        public long MaxBufferBytes { get; set; } = 256L * 1024 * 1024;

        /// <summary>
        /// Executes the shading calculation, clipping and projecting every caster triangle onto every receiver on the GPU and merging the resulting shadows on the CPU.
        /// <para>For every receiver and sun direction, each triangle of the other elements (receivers and shading-only casters) is clipped to the part lying between the sun and the receiver plane and projected onto that plane along the sun direction
        /// (<see cref="Triangle3ShadowProjectionComputeShader"/>, which appends only the shadows found); the shadows are then merged and clipped to the receiver face by <see cref="Solar.Query.ShadedFaces(PolygonalFace2D?, IEnumerable{PolygonalFace2D}?)"/>,
        /// exactly as in the CPU solver, so the two solvers agree up to floating point round-off: a caster crossing the receiver plane casts only its sun-side part, and a receiver with the sun behind it is shaded by whatever lies in front of its plane.</para>
        /// <para>Every receiver receives one result per daytime timestamp, including fully sunlit ones (shaded area 0).</para>
        /// <para>If the merge of one receiver's shadows fails, the unmerged shadows are clipped to the receiver and used instead, capped at its area: that sample's shaded area is then overstated at worst, but it never exceeds the receiver and never reads as full sun.
        /// A receiver without a plane frame or a planar face gets no results, so TryGetShadingFactor returns false for it; it still casts shadows on the others.</para>
        /// <para>Receivers are dispatched in blocks whose shadows fit <see cref="MaxBufferBytes"/>; the shadows are sorted by receiver and caster triangle before the merge, so the results depend neither on the block size nor on the order the GPU appends them in.</para>
        /// <para>When no supported device matching <see cref="ComputeDeviceType"/> can be created (see <see cref="Create.GraphicsDevice(ComputeDeviceType)"/>), <see cref="ComputeDeviceType.Default"/> falls back to the CPU solve of the base class,
        /// while an explicit <see cref="ComputeDeviceType.Hardware"/> request returns false. The obsolete <c>ComputeDeviceType.Software</c> (WARP) never gets a device, so it always returns false (ZiolkowskiJakub/DiGi.Solar#10).</para>
        /// </summary>
        /// <returns>True if the solving operation completed successfully; otherwise, false, without assigning any result (also when the device reports more shadows for one receiver than it has caster triangles, which only a faulty dispatch can produce).</returns>
        public override bool Solve()
        {
            if (ShadingSolverOptions == null || ShadingModel == null)
            {
                return false;
            }

            DateTime[]? dateTimes = ShadingSolverOptions.TimeSeries.GetDateTimes();
            if (dateTimes == null || dateTimes.Length == 0)
            {
                return true;
            }

            List<IShadingElement>? shadingElements_All = ShadingModel.GetShadingElements<IShadingElement>();
            if (shadingElements_All == null || shadingElements_All.Count == 0)
            {
                return true;
            }

            GraphicsDevice? graphicDevice = Create.GraphicsDevice(ComputeDeviceType);
            if (graphicDevice == null)
            {
                return ComputeDeviceType == ComputeDeviceType.Default && base.Solve();
            }

            double tolerance = ShadingSolverOptions.Tolerance;

            // One receiver row per non-shading-only element with a plane frame and a planar face; the row index is
            // the receiver index the shader reports. The plane and the face are read from the same clone used to
            // triangulate. A non-shading-only element without them gets no row: it receives nothing and, like a
            // shading-only element, casts on every receiver.
            List<IShadingElement> shadingElements = [];
            List<Geometry.Spatial.Classes.Plane> planes = [];
            List<PolygonalFace2D> polygonalFace2Ds = [];
            List<ShadowReceiver> shadowReceivers = [];
            List<IShadingElement> shadingElements_NoRow = [];

            // Caster triangles of every element and, built in the same loop so the two can never differ in length,
            // the receiver row each belongs to (-1 when it has none), so a receiver never shades itself.
            // The shader reads elementIndexes[y] for every triangle y, and D3D12 returns 0 for an out-of-bounds read.
            List<Triangle3> triangle3s = [];
            List<int> indexes_Row = [];

            foreach (IShadingElement shadingElement in shadingElements_All)
            {
                if (shadingElement == null)
                {
                    continue;
                }

                IPolygonalFace3D? polygonalFace3D = shadingElement.PolygonalFace3D;

                List<Triangle3D>? triangle3Ds = polygonalFace3D?.Triangulate(tolerance);
                if (triangle3Ds == null || triangle3Ds.Count == 0)
                {
                    continue;
                }

                int index = -1;
                if (!shadingElement.ShadingOnly)
                {
                    Geometry.Spatial.Classes.Plane? plane = polygonalFace3D?.Plane;
                    if (plane != null && polygonalFace3D?.Geometry2D is PolygonalFace2D polygonalFace2D && DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(polygonalFace3D) is ShadowReceiver shadowReceiver)
                    {
                        index = shadowReceivers.Count;
                        shadingElements.Add(shadingElement);
                        planes.Add(plane);
                        polygonalFace2Ds.Add(polygonalFace2D);
                        shadowReceivers.Add(shadowReceiver);
                    }
                    else
                    {
                        shadingElements_NoRow.Add(shadingElement);
                    }
                }

                foreach (Triangle3D triangle3D in triangle3Ds)
                {
                    if (triangle3D == null)
                    {
                        continue;
                    }

                    triangle3s.Add(DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(triangle3D, true));
                    indexes_Row.Add(index);
                }
            }

            int count_Receiver = shadowReceivers.Count;
            if (count_Receiver == 0 && shadingElements_NoRow.Count == 0)
            {
                return true;
            }

            Dictionary<DateTime, Vector3D> dictionary = [];
            foreach (DateTime dateTime in dateTimes)
            {
                Vector3D? sunDirection = Solar.Query.SunDirection(ShadingModel, dateTime, false);
                if (sunDirection == null)
                {
                    continue;
                }

                dictionary[dateTime] = sunDirection;
            }

            List<Tuple<Vector3D, List<DateTime>>>? tuples_DateTime = Solar.Query.GroupDirections(dictionary, ShadingSolverOptions.AngleTolerance);
            if (tuples_DateTime == null || tuples_DateTime.Count == 0)
            {
                return true;
            }

            int count_Group = tuples_DateTime.Count;

            // Shadows of every direction group, sorted by receiver row and then by caster triangle: row i owns
            // shadowPolygon2sArray[g][offsetsArray[g][i] .. offsetsArray[g][i + 1]).
            ShadowPolygon2[][] shadowPolygon2sArray = new ShadowPolygon2[count_Group][];
            int[][] offsetsArray = new int[count_Group][];

            if (count_Receiver != 0)
            {
                int count_Triangle = triangle3s.Count;

                // A single receiver yields at most one record per caster triangle, so a block of one always fits.
                int recordSize = Unsafe.SizeOf<ShadowPolygon2>();
                int capacity_Max = (int)System.Math.Min(System.Math.Max(MaxBufferBytes / recordSize, count_Triangle), int.MaxValue / recordSize);
                int capacity = System.Math.Min(capacity_Max, count_Receiver);

                using ReadOnlyBuffer<ShadowReceiver> readOnlyBuffer_Receivers = graphicDevice.AllocateReadOnlyBuffer(shadowReceivers.ToArray());
                using ReadOnlyBuffer<Triangle3> readOnlyBuffer_Triangles = graphicDevice.AllocateReadOnlyBuffer(triangle3s.ToArray());
                using ReadOnlyBuffer<int> readOnlyBuffer_Rows = graphicDevice.AllocateReadOnlyBuffer(indexes_Row.ToArray());
                using ReadWriteBuffer<int> readWriteBuffer_Counter = graphicDevice.AllocateReadWriteBuffer<int>(1);

                // The counter is reset and read, and the records are read, through persistent staging buffers: a copy from or to a
                // managed array stages through a transient resource on every call and costs several times more, which dominates small models.
                using UploadBuffer<int> uploadBuffer_Counter = graphicDevice.AllocateUploadBuffer<int>(1);
                using ReadBackBuffer<int> readBackBuffer_Counter = graphicDevice.AllocateReadBackBuffer<int>(1);
                uploadBuffer_Counter.Span[0] = 0;

                ReadWriteBuffer<ShadowPolygon2> readWriteBuffer_Polygons = graphicDevice.AllocateReadWriteBuffer<ShadowPolygon2>(capacity);
                ReadBackBuffer<ShadowPolygon2> readBackBuffer_Polygons = graphicDevice.AllocateReadBackBuffer<ShadowPolygon2>(capacity);

                try
                {
                    for (int g = 0; g < count_Group; g++)
                    {
                        Coordinate3 coordinate3 = DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(tuples_DateTime[g].Item1);

                        ShadowPolygon2[] shadowPolygon2s_Group = new ShadowPolygon2[capacity];
                        int count_Group_Record = 0;

                        int rowCount = count_Receiver;
                        int rowOffset = 0;
                        while (rowOffset < count_Receiver)
                        {
                            rowCount = System.Math.Min(rowCount, count_Receiver - rowOffset);

                            // The shader appends through the counter, so it must start at 0 on every dispatch, repeated ones included.
                            readWriteBuffer_Counter.CopyFrom(uploadBuffer_Counter);
                            graphicDevice.For(rowCount, count_Triangle, new Triangle3ShadowProjectionComputeShader(readOnlyBuffer_Receivers, readOnlyBuffer_Triangles, readOnlyBuffer_Rows, readWriteBuffer_Polygons, readWriteBuffer_Counter, coordinate3, rowOffset, tolerance));
                            readWriteBuffer_Counter.CopyTo(readBackBuffer_Counter);

                            // The counter ends at the true hit count even when the buffer overflowed, in which case the records are incomplete:
                            // grow the buffer while the budget allows, otherwise split the block, and dispatch it again.
                            int count = readBackBuffer_Counter.Span[0];
                            if (count > capacity)
                            {
                                if (count <= capacity_Max)
                                {
                                    capacity = (int)System.Math.Min(capacity_Max, System.Math.Max(count, 2L * capacity));

                                    readWriteBuffer_Polygons.Dispose();
                                    readWriteBuffer_Polygons = graphicDevice.AllocateReadWriteBuffer<ShadowPolygon2>(capacity);
                                    readBackBuffer_Polygons.Dispose();
                                    readBackBuffer_Polygons = graphicDevice.AllocateReadBackBuffer<ShadowPolygon2>(capacity);
                                }
                                else if (rowCount == 1)
                                {
                                    // One receiver yields at most one record per caster triangle, which capacity_Max always holds:
                                    // a larger count is a broken counter, and splitting further would never end.
                                    return false;
                                }
                                else
                                {
                                    rowCount = System.Math.Max(1, (int)((long)rowCount * capacity_Max / count));
                                }

                                continue;
                            }

                            if (count != 0)
                            {
                                readWriteBuffer_Polygons.CopyTo(readBackBuffer_Polygons, 0, 0, count);

                                if (count_Group_Record + count > shadowPolygon2s_Group.Length)
                                {
                                    Array.Resize(ref shadowPolygon2s_Group, System.Math.Max(count_Group_Record + count, 2 * shadowPolygon2s_Group.Length));
                                }

                                readBackBuffer_Polygons.Span[..count].CopyTo(shadowPolygon2s_Group.AsSpan(count_Group_Record));
                                count_Group_Record += count;
                            }

                            rowOffset += rowCount;
                        }

                        // The append order is not deterministic: bucket by receiver row (counting sort), then order each
                        // bucket by caster triangle, which is the CPU solver's order and makes the union input, and so every
                        // result, independent of the block size and of the GPU.
                        int[] offsets = new int[count_Receiver + 1];
                        for (int k = 0; k < count_Group_Record; k++)
                        {
                            offsets[shadowPolygon2s_Group[k].ReceiverIndex + 1]++;
                        }

                        for (int i = 0; i < count_Receiver; i++)
                        {
                            offsets[i + 1] += offsets[i];
                        }

                        ShadowPolygon2[] shadowPolygon2s_Sorted = new ShadowPolygon2[count_Group_Record];
                        int[] indexes_Triangle = new int[count_Group_Record];
                        int[] positions = new int[count_Receiver];
                        Array.Copy(offsets, positions, count_Receiver);
                        for (int k = 0; k < count_Group_Record; k++)
                        {
                            ShadowPolygon2 shadowPolygon2 = shadowPolygon2s_Group[k];
                            int position = positions[shadowPolygon2.ReceiverIndex]++;
                            shadowPolygon2s_Sorted[position] = shadowPolygon2;
                            indexes_Triangle[position] = shadowPolygon2.TriangleIndex;
                        }

                        for (int i = 0; i < count_Receiver; i++)
                        {
                            int length = offsets[i + 1] - offsets[i];
                            if (length > 1)
                            {
                                Array.Sort(indexes_Triangle, shadowPolygon2s_Sorted, offsets[i], length);
                            }
                        }

                        shadowPolygon2sArray[g] = shadowPolygon2s_Sorted;
                        offsetsArray[g] = offsets;
                    }
                }
                finally
                {
                    readWriteBuffer_Polygons.Dispose();
                    readBackBuffer_Polygons.Dispose();
                }
            }

            ShadingSolverType shadingSolverType = ShadingSolverOptions.ShadingSolverType;

            List<IShadingSolverResult>?[] shadingSolverResultsArray = new List<IShadingSolverResult>?[count_Receiver];

            Parallel.For(0, count_Receiver, Core.Create.ParallelOptions(), i =>
            {
                Geometry.Spatial.Classes.Plane plane = planes[i];
                PolygonalFace2D polygonalFace2D = polygonalFace2Ds[i];

                List<IShadingSolverResult> shadingSolverResults = [];

                for (int g = 0; g < count_Group; g++)
                {
                    ShadowPolygon2[] shadowPolygon2s = shadowPolygon2sArray[g];
                    int[] offsets = offsetsArray[g];

                    List<PolygonalFace2D> polygonalFace2Ds_Shadow = [];
                    for (int k = offsets[i]; k < offsets[i + 1]; k++)
                    {
                        if (DiGi.ComputeSharp.Geometry.Planar.Convert.ToDiGi(shadowPolygon2s[k]) is PolygonalFace2D polygonalFace2D_Shadow)
                        {
                            polygonalFace2Ds_Shadow.Add(polygonalFace2D_Shadow);
                        }
                    }

                    // A receiver reached by no shadow is fully sunlit and still gets a result (shaded area 0). The receiver face is never null here.
                    List<PolygonalFace2D> polygonalFace2Ds_Result = Solar.Query.ShadedFaces(polygonalFace2D, polygonalFace2Ds_Shadow) ?? [];

                    foreach (DateTime dateTime in tuples_DateTime[g].Item2)
                    {
                        if (Solar.Create.ShadingSolverResult(shadingSolverType, dateTime, plane, polygonalFace2Ds_Result) is IShadingSolverResult shadingSolverResult)
                        {
                            shadingSolverResults.Add(shadingSolverResult);
                        }
                    }
                }

                shadingSolverResultsArray[i] = shadingSolverResults;
            });

            for (int i = 0; i < count_Receiver; i++)
            {
                ShadingModel.Assign(shadingElements[i], shadingSolverResultsArray[i]);
            }

            // Receivers without a plane frame or a planar face get null, so Assign returns false for them, as in the CPU solver.
            foreach (IShadingElement shadingElement in shadingElements_NoRow)
            {
                ShadingModel.Assign(shadingElement, null);
            }

            return true;
        }
    }
}
