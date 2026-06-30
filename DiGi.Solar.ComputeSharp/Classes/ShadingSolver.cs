using ComputeSharp;
using DiGi.ComputeSharp.Spatial.Classes;
using DiGi.Core;
using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Planar.Interfaces;
using DiGi.Geometry.Spatial;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.Solar.Classes;
using DiGi.Solar.Interfaces;

namespace DiGi.Solar.ComputeSharp.Classes
{
    /// <summary>
    /// Provides a solver implementation to calculate shading effects on objects using ComputeSharp for GPU acceleration.
    /// </summary>
    public class ShadingSolver : IShadingObject, ISolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolver"/> class with the specified shading model and options.
        /// </summary>
        /// <param name="shadingModel">The shading model to be used for calculations.</param>
        /// <param name="shadingSolverOptions">The options that configure the solver's behavior.</param>
        public ShadingSolver(ShadingModel? shadingModel, ShadingSolverOptions? shadingSolverOptions)
        {
            ShadingModel = shadingModel;
            ShadingSolverOptions = shadingSolverOptions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolver"/> class with the specified shading model and a collection of date-times.
        /// </summary>
        /// <param name="shadingModel">The shading model to be used for calculations.</param>
        /// <param name="dateTimes">An array of date-time values for which shading should be calculated.</param>
        public ShadingSolver(ShadingModel? shadingModel, DateTime[]? dateTimes)
        {
            ShadingModel = shadingModel;
            ShadingSolverOptions = new ShadingSolverOptions();
            if (dateTimes != null)
            {
                ShadingSolverOptions.TimeSeries = new DateTimeCollection(dateTimes);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ShadingModel"/> associated with this solver.
        /// </summary>
        public ShadingModel? ShadingModel { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ShadingSolverOptions"/> that define the parameters for the solving process.
        /// </summary>
        public ShadingSolverOptions? ShadingSolverOptions { get; set; }

        /// <summary>
        /// Executes the shading calculation process, utilizing GPU shaders to determine intersections and project shading results onto objects.
        /// </summary>
        /// <returns>True if the solving operation completed successfully; otherwise, false.</returns>
        public bool Solve()
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

            GraphicsDevice graphicDevice = GraphicsDevice.GetDefault();
            if (graphicDevice == null)
            {
                return false;
            }

            double tolerance = ShadingSolverOptions.Tolerance;

            List<Tuple<Triangle3D, int>> tuples = [];
            List<IShadingElement> shadingElements = [];

            // Captured in lockstep with shadingElements (non-shading-only) during triangulation
            // below, so the per-element plane is read from the same clone used to triangulate
            // rather than deep-cloning each PolygonalFace3D a second time later.
            List<Geometry.Spatial.Classes.Plane?> planes_ShadingElements = [];

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

            using ReadOnlyBuffer<Triangle3> readOnlyBuffer = graphicDevice.AllocateReadOnlyBuffer(tuples.ConvertAll(x => DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(x.Item1, true)).ToArray());
            using ReadWriteBuffer<Triangle3Intersection> readWriteBuffer = graphicDevice.AllocateReadWriteBuffer<Triangle3Intersection>(count_Triangle * count_Triangle);

            using ReadOnlyBuffer<Triangle3>? readOnlyBuffer_ShadingOnly = tuples_ShadingOnly.Count == 0 ? null : graphicDevice.AllocateReadOnlyBuffer(tuples_ShadingOnly.ConvertAll(x => DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(x.Item1, true)).ToArray());
            using ReadWriteBuffer<Triangle3Intersection>? readWriteBuffer_ShadingOnly = tuples_ShadingOnly.Count == 0 ? null : graphicDevice.AllocateReadWriteBuffer<Triangle3Intersection>(count_Triangle * count_Triangle_ShadingOnly);

            // Reusable CPU-side readback buffers. The GPU buffer sizes are constant across all
            // directions, so allocate the managed arrays once and copy straight into them each
            // direction instead of allocating a new array plus rebuilding a List on every call
            // (the previous Create.List did both, per direction).
            Triangle3Intersection[] triangle3Intersections_Readback = new Triangle3Intersection[count_Triangle * count_Triangle];
            Triangle3Intersection[]? triangle3Intersections_Readback_ShadingOnly = count_Triangle_ShadingOnly == 0 ? null : new Triangle3Intersection[count_Triangle * count_Triangle_ShadingOnly];

            Func<ReadWriteBuffer<Triangle3Intersection>, Triangle3Intersection[], int, int, List<List<Triangle3D>>> convert = new((readWriteBuffer_Temp, triangle3Intersections, count_1, count_2) =>
            {
                readWriteBuffer_Temp.CopyTo(triangle3Intersections);

                List<List<Triangle3D>> result = [];

                for (int i = 0; i < count_1; i++)
                {
                    List<Triangle3D> triangle3Ds = [];
                    for (int j = 0; j < count_2; j++)
                    {
                        // Reference the buffer element in place; Triangle3Intersection is a large
                        // (6 x Coordinate3) struct and IsNaN() only reads Point_1, so copying the
                        // whole struct per cell (most of which are NaN) would be pure overhead.
                        ref Triangle3Intersection triangle3Intersection = ref triangle3Intersections[(i * count_2) + j];
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
                                triangle3Ds.Add(triangle3D);
                            }
                        }
                    }

                    result.Add(triangle3Ds);
                }

                return result;
            });

            List<List<IShadingSolverResult>?> shadingSolverResultsList = [.. Enumerable.Repeat<List<IShadingSolverResult>?>(null, count_ShadingElement)];

            double angleTolerance = ShadingSolverOptions.AngleTolerance;

            Dictionary<DateTime, Vector3D> dictionary = [];
            foreach (DateTime dateTime in dateTimes)
            {
                Vector3D? sunDirection = Query.SunDirection(ShadingModel, dateTime, false);
                if (sunDirection == null)
                {
                    continue;
                }

                dictionary[dateTime] = sunDirection;
            }

            List<Tuple<Vector3D, List<DateTime>>>? tuples_DateTime = Query.GroupDirections(dictionary, angleTolerance);
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

                graphicDevice.For(count_Triangle, count_Triangle, new Triangle3ShadingComputeShader(readOnlyBuffer, readWriteBuffer, coordinate3, tolerance));

                List<List<Triangle3D>> triangle3DsList = convert(readWriteBuffer, triangle3Intersections_Readback, count_Triangle, count_Triangle);

                if (readOnlyBuffer_ShadingOnly != null && readWriteBuffer_ShadingOnly != null)
                {
                    graphicDevice.For(count_Triangle, count_Triangle_ShadingOnly, new Triangle3ExternalShadingComputeShader(readOnlyBuffer, readOnlyBuffer_ShadingOnly, readWriteBuffer_ShadingOnly, coordinate3, tolerance));

                    List<List<Triangle3D>> triangle3DsList_ShadingOnly = convert(readWriteBuffer_ShadingOnly, triangle3Intersections_Readback_ShadingOnly!, count_Triangle, count_Triangle_ShadingOnly);
                    for (int i = 0; i < triangle3DsList_ShadingOnly.Count; i++)
                    {
                        List<Triangle3D> triangle3s = triangle3DsList_ShadingOnly[i];
                        if (triangle3s != null && triangle3s.Count != 0)
                        {
                            if (triangle3DsList[i] == null)
                            {
                                triangle3DsList[i] = [];
                            }

                            triangle3DsList[i].AddRange(triangle3s);
                        }
                    }
                }

                Parallel.For(0, count_ShadingElement, parallelOptions, i =>
                {
                    Geometry.Spatial.Classes.Plane? plane = planes_ShadingElements[i];
                    if (plane == null)
                    {
                        return;
                    }

                    List<Triangle3D> triangle3Ds = [];
                    foreach (int j in triangleIndices_ByElement[i])
                    {
                        List<Triangle3D> triangle3Ds_Temp = triangle3DsList[j];
                        if (triangle3Ds_Temp == null || triangle3Ds_Temp.Count == 0)
                        {
                            continue;
                        }

                        triangle3Ds.AddRange(triangle3Ds_Temp);
                    }

                    if (triangle3Ds.Count == 0)
                    {
                        return;
                    }

                    List<IPolygonalFace2D> polygonalFace2Ds_Shadow = [];
                    foreach (Triangle3D triangle3D in triangle3Ds)
                    {
                        if (plane.Convert(triangle3D) is not Triangle2D triangle2D)
                        {
                            continue;
                        }

                        if (Geometry.Planar.Create.PolygonalFace2D(triangle2D) is IPolygonalFace2D polygonalFace2D_Shadow)
                        {
                            polygonalFace2Ds_Shadow.Add(polygonalFace2D_Shadow);
                        }
                    }

                    // Single hole-preserving union: produces PolygonalFace2D faces directly in one
                    // NTS pass, replacing the previous Union (Polygon2D) + Create.PolygonalFace2Ds
                    // re-polygonization. Unlike the Polygon2D union it keeps interior voids, so
                    // ring-shaped shadows no longer over-count the shaded area.
                    List<PolygonalFace2D>? polygonalFace2Ds = polygonalFace2Ds_Shadow.Union();

                    if (shadingSolverResultsList[i] is null)
                    {
                        shadingSolverResultsList[i] = [];
                    }

                    foreach (DateTime dateTime in tuple_DateTime.Item2)
                    {
                        if (Create.ShadingSolverResult(ShadingSolverOptions.ShadingSolverType, dateTime, plane, polygonalFace2Ds) is IShadingSolverResult shadingSolverResult)
                        {
                            shadingSolverResultsList[i]!.Add(shadingSolverResult);
                        }
                    }
                });
            }

            for (int i = 0; i < count_ShadingElement; i++)
            {
                ShadingModel.Assign(shadingElements[i], shadingSolverResultsList[i]);
            }

            return true;
        }
    }
}