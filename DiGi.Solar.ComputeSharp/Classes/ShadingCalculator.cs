using ComputeSharp;
using DiGi.ComputeSharp.Spatial.Classes;
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
    public class ShadingCalculator : IShadingObject, ICalculator
    {
        public ShadingCalculator(ShadingModel shadingModel, ShadingCalculatorOptions shadingCalculatorOptions)
        {
            ShadingModel = shadingModel;
            ShadingCalculatorOptions = shadingCalculatorOptions;
        }

        public ShadingCalculator(ShadingModel shadingModel, DateTime[] dateTimes)
        {
            ShadingModel = shadingModel;
            ShadingCalculatorOptions = new ShadingCalculatorOptions()
            {
                TimeSeries = dateTimes == null ? null : new DateTimeCollection(dateTimes)
            };
        }

        public ShadingCalculatorOptions ShadingCalculatorOptions { get; set; }

        public ShadingModel ShadingModel { get; set; }
        
        public bool Calculate()
        {
            if (ShadingCalculatorOptions == null || ShadingModel == null)
            {
                return false;
            }

            DateTime[] dateTimes = ShadingCalculatorOptions.TimeSeries.GetDateTimes();
            if (dateTimes == null || dateTimes.Length == 0)
            {
                return true;
            }

            List<IShadingElement> shadingElements_All = ShadingModel.GetShadingElements<IShadingElement>();
            if (shadingElements_All == null || shadingElements_All.Count == 0)
            {
                return true;
            }

            GraphicsDevice graphicDevice = GraphicsDevice.GetDefault();
            if (graphicDevice == null)
            {
                return false;
            }

            double tolerance = ShadingCalculatorOptions.Tolerance;

            List<Tuple<Triangle3D, int>> tuples = new List<Tuple<Triangle3D, int>>();
            List<IShadingElement> shadingElements = new List<IShadingElement>();

            List<Tuple<Triangle3D, int>> tuples_ShadingOnly = new List<Tuple<Triangle3D, int>>();
            List<IShadingElement> shadingElements_ShadingOnly = new List<IShadingElement>();

            int count_ShadingElement_All = shadingElements_All.Count;

            for (int i = 0; i < count_ShadingElement_All; i++)
            {
                IShadingElement shadingElement = shadingElements_All[i];
                if (shadingElement == null)
                {
                    continue;
                }

                List<Triangle3D> triangle3Ds = shadingElement?.PolygonalFace3D?.Triangulate(tolerance);
                if (triangle3Ds == null || triangle3Ds.Count == 0)
                {
                    continue;
                }

                List<IShadingElement> shadingElements_Temp = null;
                List<Tuple<Triangle3D, int>> tuples_Temp = null;
                if(shadingElement.ShadingOnly)
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

            int count_ShadingElement = shadingElements.Count();
            int count_ShadingElement_ShadingOnly = shadingElements_ShadingOnly.Count();

            if(count_ShadingElement == 0)
            {
                return true;
            }

            int count_Triangle = tuples.Count;
            int count_Triangle_ShadingOnly = tuples_ShadingOnly.Count;

            ReadOnlyBuffer<Triangle3> readOnlyBuffer = graphicDevice.AllocateReadOnlyBuffer(tuples.ConvertAll(x => DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(x.Item1, true)).ToArray());
            ReadWriteBuffer<Triangle3Intersection> readWriteBuffer = graphicDevice.AllocateReadWriteBuffer<Triangle3Intersection>(count_Triangle * count_Triangle);

            ReadOnlyBuffer<Triangle3> readOnlyBuffer_ShadingOnly = tuples_ShadingOnly.Count == 0 ? null : graphicDevice.AllocateReadOnlyBuffer(tuples_ShadingOnly.ConvertAll(x => DiGi.ComputeSharp.Geometry.Spatial.Convert.ToComputeSharp(x.Item1, true)).ToArray());
            ReadWriteBuffer<Triangle3Intersection> readWriteBuffer_ShadingOnly = tuples_ShadingOnly.Count == 0 ? null : graphicDevice.AllocateReadWriteBuffer<Triangle3Intersection>(count_Triangle * count_Triangle_ShadingOnly);

            Func<ReadWriteBuffer<Triangle3Intersection>, int, int, List<List<Triangle3D>>> convert = new Func<ReadWriteBuffer<Triangle3Intersection>, int, int, List<List<Triangle3D>>>((ReadWriteBuffer<Triangle3Intersection> readWriteBuffer_Temp, int count_1, int count_2) => 
            {
                List<Triangle3Intersection> triangle3Intersections = DiGi.ComputeSharp.Core.Create.List(readWriteBuffer_Temp);

                List<List<Triangle3D>> result = new List<List<Triangle3D>>();
                for (int i = 0; i < count_1; i++)
                {
                    List<Triangle3D> triangle3Ds = new List<Triangle3D>();
                    for (int j = 0; j < count_2; j++)
                    {
                        Triangle3Intersection triangle3Intersection = triangle3Intersections[i * count_1 + j];
                        if (triangle3Intersection.IsNaN())
                        {
                            continue;
                        }

                        DiGi.ComputeSharp.Spatial.Interfaces.IGeometry3[] geometries = triangle3Intersection.GetIntersectionGeometries();
                        if (geometries == null)
                        {
                            continue;
                        }

                        foreach (DiGi.ComputeSharp.Spatial.Interfaces.IGeometry3 geometry in geometries)
                        {
                            if (geometry is Triangle3)
                            {
                                triangle3Ds.Add(DiGi.ComputeSharp.Geometry.Spatial.Convert.ToDiGi((Triangle3)geometry));
                            }
                        }
                    }

                    result.Add(triangle3Ds);
                }

                return result;
            });

            List<List<IShadingCalculationResult>> shadingCalculationResultsList = Enumerable.Repeat<List<IShadingCalculationResult>>(null, count_ShadingElement).ToList();

            double angleTolerance = ShadingCalculatorOptions.AngleTolerance;

            Dictionary<DateTime, Vector3D> dictionary = new Dictionary<DateTime, Vector3D>();
            foreach (DateTime dateTime in dateTimes)
            {
                Vector3D sunDirection = Query.SunDirection(ShadingModel, dateTime, false);
                if (sunDirection == null)
                {
                    continue;
                }

                dictionary[dateTime] = sunDirection;
            }

            List<Tuple<Vector3D, List<DateTime>>> tuples_DateTime = Query.GroupDirections(dictionary, angleTolerance);
            if(tuples_DateTime == null || tuples_DateTime.Count == 0)
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
                        if(triangle3s != null && triangle3s.Count != 0)
                        {
                            if(triangle3DsList[i] == null)
                            {
                                triangle3DsList[i] = new List<Triangle3D>();
                            }

                            triangle3DsList[i].AddRange(triangle3s);
                        }
                    }

                }

                //for (int i =0; i < count_ShadingElement; i ++)
                Parallel.For(0, count_ShadingElement, parallelOptions, i => 
                {
                    IPolygonalFace3D polygonalFace3D = shadingElements[i].PolygonalFace3D;

                    Geometry.Spatial.Classes.Plane plane = polygonalFace3D?.Plane;
                    if (plane == null)
                    {
                        return;
                    }

                    List<Triangle3D> triangle3Ds = new List<Triangle3D>();
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

                    List<Polygon2D> polygon2Ds = triangle3Ds.ConvertAll(x => plane.Convert(x)).Union();

                    List<IPolygonalFace2D> polygonalFace2Ds = Geometry.Planar.Create.PolygonalFace2Ds(polygon2Ds, tolerance);

                    if (shadingCalculationResultsList[i] == null)
                    {
                        shadingCalculationResultsList[i] = new List<IShadingCalculationResult>();
                    }

                    foreach (DateTime dateTime in tuple_DateTime.Item2)
                    {
                        shadingCalculationResultsList[i].Add(Create.ShadingCalculationResult(ShadingCalculatorOptions.ShadingCalculationType, dateTime, plane, polygonalFace2Ds));
                    }
                }); 

                //List<Tuple<Triangle3D, int>> tuples_Temp = new List<Tuple<Triangle3D, int>>(tuples);


                //for (int i = count_ShadingElement - 1; i >= 0; i--)
                //{
                //    IPolygonalFace3D polygonalFace3D = shadingElements[i].PolygonalFace3D;

                //    Geometry.Spatial.Classes.Plane plane = polygonalFace3D?.Plane;
                //    if (plane == null)
                //    {
                //        result.Add(null);
                //        continue;
                //    }

                //    List<Triangle3D> triangle3Ds = new List<Triangle3D>();

                //    List<int> indexes = new List<int>();
                //    int index = tuples_Temp.FindLastIndex(x => x.Item2 == i);
                //    while (index != -1)
                //    {
                //        tuples_Temp.RemoveAt(index);
                //        List<Triangle3D> triangle3Ds_Temp = triangle3DsList[index];

                //        index = tuples_Temp.FindLastIndex(x => x.Item2 == i);

                //        if (triangle3Ds_Temp == null || triangle3Ds_Temp.Count == 0)
                //        {
                //            continue;
                //        }

                //        triangle3Ds.AddRange(triangle3Ds_Temp);
                //    }

                //    if (triangle3Ds == null || triangle3Ds.Count == 0)
                //    {
                //        result.Add(null);
                //        continue;
                //    }

                //    List<Polygon2D> polygon2Ds = triangle3Ds.ConvertAll(x => plane.Convert(x)).Union();

                //    List<IPolygonalFace2D> polygonalFace2Ds = Geometry.Planar.Create.PolygonalFace2Ds(polygon2Ds, tolerance);

                //    if (shadingCalculationResultsList[i] == null)
                //    {
                //        shadingCalculationResultsList[i] = new List<IShadingCalculationResult>();
                //    }

                //    foreach(DateTime dateTime in tuple_DateTime.Item2)
                //    {
                //        shadingCalculationResultsList[i].Add(Create.ShadingCalculationResult(ShadingCalculatorOptions.ShadingCalculationType, dateTime, plane, polygonalFace2Ds));
                //    }
                //}
            }

            for(int i =0; i < count_ShadingElement; i++)
            {
                ShadingModel.Assign(shadingElements[i], shadingCalculationResultsList[i]);
            }

            return true;
        }
    }
}
