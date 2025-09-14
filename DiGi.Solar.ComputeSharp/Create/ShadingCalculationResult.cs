using DiGi.Geometry.Planar.Interfaces;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Solar.Classes;
using DiGi.Solar.Enums;
using DiGi.Solar.Interfaces;

namespace DiGi.Solar.ComputeSharp
{
    public static partial class Create
    {
        public static IShadingCalculationResult? ShadingCalculationResult(this ShadingCalculationType shadingCalculationType, DateTime dateTime, Plane? plane, IEnumerable<IPolygonalFace2D>? polygonalFace2Ds)
        {
            if(shadingCalculationType == ShadingCalculationType.Undefined)
            {
                return null;
            }

            switch(shadingCalculationType)
            {
                case ShadingCalculationType.Geometrical:
                    if(plane == null)
                    {
                        return null;
                    }

                    return new GeometricalShadingCalculationResult(dateTime, plane, polygonalFace2Ds);


                case ShadingCalculationType.Numerical:
                    double area = 0;
                    if(polygonalFace2Ds != null)
                    {
                        foreach(IPolygonalFace2D polygonalFace2D in polygonalFace2Ds)
                        {
                            if (polygonalFace2D == null)
                            {
                                continue;
                            }

                            double area_Temp = polygonalFace2D.GetArea();
                            if(double.IsNaN(area_Temp))
                            {
                                continue;
                            }

                            area += area_Temp;
                        }
                    }

                    return new NumericalShadingCalculationResult(dateTime, area);
            }

            return null;
        }
    }
}
