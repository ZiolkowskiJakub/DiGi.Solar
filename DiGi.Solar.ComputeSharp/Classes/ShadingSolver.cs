using ComputeSharp;
using DiGi.ComputeSharp.Spatial.Classes;
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Spatial;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.Solar.Classes;
using DiGi.Solar.ComputeSharp.Enums;
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
        /// Gets or sets the upper bound, in bytes, of each intersection buffer the solver allocates (the device buffer and its managed readback alike). Defaults to 256 MB.
        /// <para>Receivers are dispatched in row blocks sized to fit the budget, so a smaller value lowers peak memory at the cost of more dispatches. A single row larger than the budget still runs (one row per block).</para>
        /// </summary>
        public long MaxBufferBytes { get; set; } = 256L * 1024 * 1024;

        /// <summary>
        /// Executes the shading calculation process, utilizing GPU shaders to determine intersections and project shading results onto objects.
        /// <para>Every receiver receives one result per daytime timestamp, including fully sunlit ones (shaded area 0).</para>
        /// <para>If the merge of one receiver's shadows fails, the unmerged shadows are clipped to the receiver and used instead, capped at its area: that sample's shaded area is then overstated at worst, but it never exceeds the receiver and never reads as full sun. A sample with no receiver face to cap against gets no result at all, so TryGetShadingFactor returns false for it.</para>
        /// <para>Receiver triangles are processed in row blocks sized by <see cref="MaxBufferBytes"/>, so memory no longer grows with the square of the triangle count. The results do not depend on the block size.</para>
        /// <para>When no supported device matching <see cref="ComputeDeviceType"/> can be created (see <see cref="Create.GraphicsDevice(ComputeDeviceType)"/>), <see cref="ComputeDeviceType.Default"/> falls back to the CPU solve of the base class,
        /// while an explicit <see cref="ComputeDeviceType.Hardware"/> request returns false. The obsolete <c>ComputeDeviceType.Software</c> (WARP) never gets a device, so it always returns false (ZiolkowskiJakub/DiGi.Solar#10).</para>
        /// </summary>
        /// <returns>True if the solving operation completed successfully; otherwise, false.</returns>
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

            List<Tuple<Triangle3D, int>> tuples = [];
            List<IShadingElement> shadingElements = [];

            // Captured in lockstep with shadingElements (non-shading-only) during triangulation
            // below, so the per-element plane is read from the same clone used to triangulate
            // rather than deep-cloning each PolygonalFace3D a second time later.
            // The face is captured the same way: the shadow fallback clips and caps against it
            // when the shadow union fails, and it is the only place the receiver's 2D face is needed.
            List<Geometry.Spatial.Classes.Plane?> planes_ShadingElements = [];
            List<PolygonalFace2D?> polygonalFace2Ds_ShadingElements = [];

            List<Tuple<Triangle3D, int>> tuples_ShadingOnly = [];
            List<IShadingElement> shadingElements_ShadingOnly = [];

            int count_ShadingElement_All = shadingElements_All.Count;

            for (int i = 0; i < count_ShadingElement_All; i++)
            {
                IShadingElement shadingElement = shadingElements_All[i];
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

                List<IShadingElement> shadingElements_Temp;
                List<Tuple<Triangle3D, int>> tuples_Temp;
                if (shadingElement.ShadingOnly)
                {
                    shadingElements_Temp = shadingElements_ShadingOnly;
                    tuples_Temp = tuples_ShadingOnly;
                }
                else
                {
                    shadingElements_Temp = shadingElements;
                    tuples_Temp = tuples;
                    planes_ShadingElements.Add(polygonalFace3D?.Plane);
                    polygonalFace2Ds_ShadingElements.Add(polygonalFace3D?.Geometry2D as PolygonalFace2D);
                }

                int index = shadingElements_Temp.Count;

                shadingElements_Temp.Add(shadingElement);

                foreach (Triangle3D triangle3D in triangle3Ds)
                {
                    tuples_Temp.Add(new Tuple<Triangle3D, int>(triangle3D, index));
                }
            }

            int count_ShadingElement = shadingElements.Count;
            int count_ShadingElement_ShadingOnly = shadingElements_ShadingOnly.Count;

            if (count_ShadingElement == 0)
            {
                return true;
            }

            int count_Triangle = tuples.Count;
            int count_Triangle_ShadingOnly = tuples_ShadingOnly.Count;

            // Receivers are processed in row blocks of blockSize triangles, one block of both passes at a time,
            // so each intersection buffer holds blockSize x columns cells instead of the full N x N (or N x M)
            // matrix. One block size drives both passes; the product stays within int by the clamp.
            int columns = System.Math.Max(System.Math.Max(count_Triangle, count_Triangle_ShadingOnly), 1);
            long rows = MaxBufferBytes / ((long)Unsafe.SizeOf<Triangle3Intersection>() * columns);
            int blockSize = (int)System.Math.Clamp(rows, 1L, System.Math.Min((long)count_Triangle, int.MaxValue / columns));

            using ReadOnlyBuffer<Triangle3> readOnlyBuffer = graphicDevice.AllocateReadOnlyBuffer(tuples.ConvertAll(x => DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(x.Item1, true)).ToArray());
            using ReadWriteBuffer<Triangle3Intersection> readWriteBuffer = graphicDevice.AllocateReadWriteBuffer<Triangle3Intersection>(blockSize * count_Triangle);

            using ReadOnlyBuffer<Triangle3>? readOnlyBuffer_ShadingOnly = tuples_ShadingOnly.Count == 0 ? null : graphicDevice.AllocateReadOnlyBuffer(tuples_ShadingOnly.ConvertAll(x => DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(x.Item1, true)).ToArray());
            using ReadWriteBuffer<Triangle3Intersection>? readWriteBuffer_ShadingOnly = tuples_ShadingOnly.Count == 0 ? null : graphicDevice.AllocateReadWriteBuffer<Triangle3Intersection>(blockSize * count_Triangle_ShadingOnly);

            // Reusable CPU-side readback buffers. The GPU buffer sizes are constant across all
            // blocks and directions, so allocate the managed arrays once and copy straight into
            // them instead of allocating a new array per dispatch.
            Triangle3Intersection[] triangle3Intersections_Readback = new Triangle3Intersection[blockSize * count_Triangle];
            Triangle3Intersection[]? triangle3Intersections_Readback_ShadingOnly = count_Triangle_ShadingOnly == 0 ? null : new Triangle3Intersection[blockSize * count_Triangle_ShadingOnly];

            // Reads back the first rowCount rows of a block and appends each receiver triangle's shadow
            // triangles to triangle3DsArray[rowOffset + row]. Only rowCount x columnCount cells are read:
            // a reused buffer keeps the previous block's results beyond that range.
            Action<ReadWriteBuffer<Triangle3Intersection>, Triangle3Intersection[], int, int, int, List<Triangle3D>?[]> convert = new((readWriteBuffer_Temp, triangle3Intersections, rowOffset, rowCount, columnCount, triangle3DsArray) =>
            {
                readWriteBuffer_Temp.CopyTo(triangle3Intersections, 0, 0, rowCount * columnCount);

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        // Reference the buffer element in place; Triangle3Intersection is a large
                        // (6 x Coordinate3) struct and IsNaN() only reads Point_1, so copying the
                        // whole struct per cell (most of which are NaN) would be pure overhead.
                        ref Triangle3Intersection triangle3Intersection = ref triangle3Intersections[(i * columnCount) + j];
                        if (triangle3Intersection.IsNaN())
                        {
                            continue;
                        }

                        DiGi.ComputeSharp.Spatial.Interfaces.IGeometry3[]? geometries = triangle3Intersection.GetIntersectionGeometries();
                        if (geometries == null)
                        {
                            continue;
                        }

                        foreach (DiGi.ComputeSharp.Spatial.Interfaces.IGeometry3 geometry in geometries)
                        {
                            if (geometry is Triangle3 triangle3 && DiGi.ComputeSharp.Geometry.Spatial.Convert.ToDiGi(triangle3) is Triangle3D triangle3D)
                            {
                                (triangle3DsArray[rowOffset + i] ??= []).Add(triangle3D);
                            }
                        }
                    }
                }
            });

            List<List<IShadingSolverResult>?> shadingSolverResultsList = [.. Enumerable.Repeat<List<IShadingSolverResult>?>(null, count_ShadingElement)];

            double angleTolerance = ShadingSolverOptions.AngleTolerance;

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

            List<Tuple<Vector3D, List<DateTime>>>? tuples_DateTime = Solar.Query.GroupDirections(dictionary, angleTolerance);
            if (tuples_DateTime == null || tuples_DateTime.Count == 0)
            {
                return true;
            }

            // Pre-compute the per-element triangle-index map once, outside the direction loop;
            // it avoids scanning every triangle for every element on every direction (the
            // triangle-to-element mapping never changes between directions). The element planes
            // were already captured during triangulation in planes_ShadingElements.
            List<int>[] triangleIndices_ByElement = new List<int>[count_ShadingElement];
            for (int i = 0; i < count_ShadingElement; i++)
            {
                triangleIndices_ByElement[i] = [];
            }

            for (int i = 0; i < count_Triangle; i++)
            {
                triangleIndices_ByElement[tuples[i].Item2].Add(i);
            }

            ParallelOptions parallelOptions = Core.Create.ParallelOptions();

            foreach (Tuple<Vector3D, List<DateTime>> tuple_DateTime in tuples_DateTime)
            {
                Coordinate3 coordinate3 = DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(tuple_DateTime.Item1);

                // Shadow triangles per receiver triangle, filled lazily: most receiver triangles are hit by nothing.
                // Within each row the self-shading hits are appended before the shading-only hits, whatever the
                // block size, so the union input (and therefore every result) does not depend on the tiling.
                List<Triangle3D>?[] triangle3DsArray = new List<Triangle3D>?[count_Triangle];

                for (int rowOffset = 0; rowOffset < count_Triangle; rowOffset += blockSize)
                {
                    int rowCount = System.Math.Min(blockSize, count_Triangle - rowOffset);

                    graphicDevice.For(rowCount, count_Triangle, new Triangle3ShadingRowOffsetComputeShader(readOnlyBuffer, readWriteBuffer, coordinate3, rowOffset, tolerance));
                    convert(readWriteBuffer, triangle3Intersections_Readback, rowOffset, rowCount, count_Triangle, triangle3DsArray);

                    if (readOnlyBuffer_ShadingOnly != null && readWriteBuffer_ShadingOnly != null && triangle3Intersections_Readback_ShadingOnly != null)
                    {
                        graphicDevice.For(rowCount, count_Triangle_ShadingOnly, new Triangle3ExternalShadingRowOffsetComputeShader(readOnlyBuffer, readOnlyBuffer_ShadingOnly, readWriteBuffer_ShadingOnly, coordinate3, rowOffset, tolerance));
                        convert(readWriteBuffer_ShadingOnly, triangle3Intersections_Readback_ShadingOnly, rowOffset, rowCount, count_Triangle_ShadingOnly, triangle3DsArray);
                    }
                }

                Parallel.For(0, count_ShadingElement, parallelOptions, i =>
                {
                    Geometry.Spatial.Classes.Plane? plane = planes_ShadingElements[i];
                    if (plane == null)
                    {
                        return;
                    }

                    PolygonalFace2D? polygonalFace2D_Receiver = polygonalFace2Ds_ShadingElements[i];

                    List<Triangle3D> triangle3Ds = [];
                    foreach (int j in triangleIndices_ByElement[i])
                    {
                        List<Triangle3D>? triangle3Ds_Temp = triangle3DsArray[j];
                        if (triangle3Ds_Temp == null || triangle3Ds_Temp.Count == 0)
                        {
                            continue;
                        }

                        triangle3Ds.AddRange(triangle3Ds_Temp);
                    }

                    // A receiver reached by no shadow triangle is fully sunlit: it still gets a
                    // result (shaded area 0) so TryGetShadingFactor reports 0 instead of failing,
                    // and interpolation never bridges a sunlit gap between two shaded samples.
                    List<PolygonalFace2D>? polygonalFace2Ds = null;
                    if (triangle3Ds.Count != 0)
                    {
                        List<PolygonalFace2D> polygonalFace2Ds_Shadow = [];
                        foreach (Triangle3D triangle3D in triangle3Ds)
                        {
                            if (plane.Convert(triangle3D) is not Triangle2D triangle2D)
                            {
                                continue;
                            }

                            if (Geometry.Planar.Create.PolygonalFace2D(triangle2D) is PolygonalFace2D polygonalFace2D_Shadow)
                            {
                                polygonalFace2Ds_Shadow.Add(polygonalFace2D_Shadow);
                            }
                        }

                        // Single hole-preserving union: produces PolygonalFace2D faces directly in one
                        // NTS pass, replacing the previous Union (Polygon2D) + Create.PolygonalFace2Ds
                        // re-polygonization. Unlike the Polygon2D union it keeps interior voids, so
                        // ring-shaped shadows no longer over-count the shaded area.
                        List<PolygonalFace2D>? polygonalFace2Ds_Union = polygonalFace2Ds_Shadow.Union();

                        // The merge failed (it stays possible even after the snap-rounding retry DiGi.Geometry makes): keep the unmerged shadows clipped to the receiver and capped at its area, so a failed merge reads as shade, overstated at worst, and never as full sun. Without a receiver face there is nothing to cap against, so no result is emitted for the sample instead of a wrong one.
                        polygonalFace2Ds = polygonalFace2Ds_Union ?? Solar.Query.ShadowFaces(polygonalFace2D_Receiver, polygonalFace2Ds_Shadow);
                    }

                    polygonalFace2Ds ??= [];

                    shadingSolverResultsList[i] ??= [];

                    foreach (DateTime dateTime in tuple_DateTime.Item2)
                    {
                        if (Solar.Create.ShadingSolverResult(ShadingSolverOptions.ShadingSolverType, dateTime, plane, polygonalFace2Ds) is IShadingSolverResult shadingSolverResult)
                        {
                            shadingSolverResultsList[i]!.Add(shadingSolverResult);
                        }
                    }
                });
            }

            // Every receiver with a plane now carries a result list for each direction group it was
            // solved in, so Assign receives null only for elements without a plane; elements that
            // failed triangulation never entered shadingElements.
            for (int i = 0; i < count_ShadingElement; i++)
            {
                ShadingModel.Assign(shadingElements[i], shadingSolverResultsList[i]);
            }

            return true;
        }
    }
}