using ComputeSharp;
using DiGi.ComputeSharp.Spatial.Classes;
using DiGi.Core;
using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Spatial;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.Solar.Classes;
using DiGi.Solar.Interfaces;

namespace DiGi.Solar.ComputeSharp.Classes
{
    public class ShadingSolver : IShadingObject, ISolver
    {
        public ShadingSolver(ShadingModel? shadingModel, ShadingSolverOptions? shadingSolverOptions)
        {
            ShadingModel = shadingModel;
            ShadingSolverOptions = shadingSolverOptions;
        }

        public ShadingSolver(ShadingModel? shadingModel, DateTime[]? dateTimes)
        {
            ShadingModel = shadingModel;
            ShadingSolverOptions = new ShadingSolverOptions();
            if (dateTimes != null)
            {
                ShadingSolverOptions.TimeSeries = new DateTimeCollection(dateTimes);
            }
        }

        public ShadingModel? ShadingModel { get; set; }

        public ShadingSolverOptions? ShadingSolverOptions { get; set; }

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

                List<Triangle3D>? triangle3Ds = shadingElement.PolygonalFace3D?.Triangulate(tolerance);
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

            ReadOnlyBuffer<Triangle3> readOnlyBuffer = graphicDevice.AllocateReadOnlyBuffer(tuples.ConvertAll(x => DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(x.Item1, true)).ToArray());
            ReadWriteBuffer<Triangle3Intersection> readWriteBuffer = graphicDevice.AllocateReadWriteBuffer<Triangle3Intersection>(count_Triangle * count_Triangle);

            ReadOnlyBuffer<Triangle3>? readOnlyBuffer_ShadingOnly = tuples_ShadingOnly.Count == 0 ? null : graphicDevice.AllocateReadOnlyBuffer(tuples_ShadingOnly.ConvertAll(x => DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(x.Item1, true)).ToArray());
            ReadWriteBuffer<Triangle3Intersection>? readWriteBuffer_ShadingOnly = tuples_ShadingOnly.Count == 0 ? null : graphicDevice.AllocateReadWriteBuffer<Triangle3Intersection>(count_Triangle * count_Triangle_ShadingOnly);

            Func<ReadWriteBuffer<Triangle3Intersection>, int, int, List<List<Triangle3D>>> convert = new((readWriteBuffer_Temp, count_1, count_2) =>
            {
                List<Triangle3Intersection>? triangle3Intersections = DiGi.ComputeSharp.Core.Create.List(readWriteBuffer_Temp);

                List<List<Triangle3D>> result = [];

                if (triangle3Intersections is null)
                {
                    return result;
                }

                for (int i = 0; i < count_1; i++)
                {
                    List<Triangle3D> triangle3Ds = [];
                    for (int j = 0; j < count_2; j++)
                    {
                        Triangle3Intersection triangle3Intersection = triangle3Intersections[i * count_1 + j];
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

            ParallelOptions parallelOptions = Core.Create.ParallelOptions();

            foreach (Tuple<Vector3D, List<DateTime>> tuple_DateTime in tuples_DateTime)
            {
                Coordinate3 coordinate3 = DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(tuple_DateTime.Item1);

                graphicDevice.For(count_Triangle, count_Triangle, new Triangle3ShadingComputeShader(readOnlyBuffer, readWriteBuffer, coordinate3, tolerance));

                List<List<Triangle3D>> triangle3DsList = convert(readWriteBuffer, count_Triangle, count_Triangle);

                if (readOnlyBuffer_ShadingOnly != null && readWriteBuffer_ShadingOnly != null)
                {
                    graphicDevice.For(count_Triangle, count_Triangle_ShadingOnly, new Triangle3ExternalShadingComputeShader(readOnlyBuffer, readOnlyBuffer_ShadingOnly, readWriteBuffer, coordinate3, tolerance));

                    List<List<Triangle3D>> triangle3DsList_ShadingOnly = convert(readWriteBuffer, count_Triangle, count_Triangle_ShadingOnly);
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

                //for (int i =0; i < count_ShadingElement; i ++)
                Parallel.For(0, count_ShadingElement, parallelOptions, i =>
                {
                    IPolygonalFace3D? polygonalFace3D = shadingElements[i].PolygonalFace3D;

                    Geometry.Spatial.Classes.Plane? plane = polygonalFace3D?.Plane;
                    if (plane == null)
                    {
                        return;
                    }

                    List<Triangle3D> triangle3Ds = [];
                    for (int j = 0; j < tuples.Count; j++)
                    {
                        if (tuples[j].Item2 != i)
                        {
                            continue;
                        }

                        List<Triangle3D> triangle3Ds_Temp = triangle3DsList[j];
                        if (triangle3Ds_Temp == null || triangle3Ds_Temp.Count == 0)
                        {
                            continue;
                        }

                        triangle3Ds.AddRange(triangle3Ds_Temp);
                    }

                    if (triangle3Ds == null || triangle3Ds.Count == 0)
                    {
                        return;
                    }

                    List<Polygon2D>? polygon2Ds = triangle3Ds?.ConvertAll(x => plane.Convert(x)).FilterNulls().Union();

                    List<PolygonalFace2D>? polygonalFace2Ds = Geometry.Planar.Create.PolygonalFace2Ds(polygon2Ds, tolerance);

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