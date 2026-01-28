using DiGi.Geometry.Planar.Interfaces;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Solar.Classes;
using DiGi.Solar.Enums;
using DiGi.Solar.Interfaces;

namespace DiGi.Solar.ComputeSharp
{
    public static partial class Create
    {
        public static IShadingSolverResult? ShadingSolverResult(this ShadingSolverType shadingSolverType, DateTime dateTime, Plane? plane, IEnumerable<IPolygonalFace2D>? polygonalFace2Ds)
        {
            if (shadingSolverType == ShadingSolverType.Undefined)
            {
                return null;
            }

            switch (shadingSolverType)
            {
                case ShadingSolverType.Geometrical:
                    if (plane == null)
                    {
                        return null;
                    }

                    return new GeometricalShadingSolverResult(dateTime, plane, polygonalFace2Ds);

                case ShadingSolverType.Numerical:
                    double area = 0;
                    if (polygonalFace2Ds != null)
                    {
                        foreach (IPolygonalFace2D polygonalFace2D in polygonalFace2Ds)
                        {
                            if (polygonalFace2D == null)
                            {
                                continue;
                            }

                            double area_Temp = polygonalFace2D.GetArea();
                            if (double.IsNaN(area_Temp))
                            {
                                continue;
                            }

                            area += area_Temp;
                        }
                    }

                    return new NumericalShadingSolverResult(dateTime, area);
            }

            return null;
        }
    }
}