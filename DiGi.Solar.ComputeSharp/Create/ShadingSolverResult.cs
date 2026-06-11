using DiGi.Geometry.Planar.Interfaces;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Solar.Classes;
using DiGi.Solar.Enums;
using DiGi.Solar.Interfaces;

namespace DiGi.Solar.ComputeSharp
{
    public static partial class Create
    {
        /// <summary>
        /// Creates an <see cref="IShadingSolverResult" /> based on the specified <see cref="ShadingSolverType" />, date and time, plane, and polygonal faces.
        /// </summary>
        /// <param name="shadingSolverType">The <see cref="ShadingSolverType" /> that determines the type of shading solver result to be created.</param>
        /// <param name="dateTime">The <see cref="System.DateTime" /> representing the time for which the shading is solved.</param>
        /// <param name="plane">The <see cref="T:DiGi.Geometry.Spatial.Classes.Plane" /> used for geometrical shading calculations. This can be null if a numerical solver is used.</param>
        /// <param name="polygonalFace2Ds">An <see cref="System.Collections.Generic.IEnumerable{IPolygonalFace2D}" /> representing the faces involved in shading calculations.</param>
        /// <returns>An implementation of <see cref="IShadingSolverResult" /> if a valid solver type is provided and necessary parameters are present; otherwise, null.</returns>
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