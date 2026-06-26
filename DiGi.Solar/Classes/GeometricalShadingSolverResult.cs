using DiGi.Core;
using DiGi.Geometry.Planar.Interfaces;
using DiGi.Geometry.Spatial;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Represents the result of a geometrical shading solver, containing spatial data such as the plane and polygonal faces used for shading calculations.
    /// </summary>
    public class GeometricalShadingSolverResult : ShadingSolverResult
    {
        [JsonInclude, JsonPropertyName("Plane")]
        private readonly Plane? plane;

        [JsonInclude, JsonPropertyName("PolygonalFace2Ds")]
        private readonly List<IPolygonalFace2D>? polygonalFace2Ds;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DiGi.Solar.Classes.GeometricalShadingSolverResult"/> class.
        /// </summary>
        /// <param name="dateTime">The date and time associated with the shading result.</param>
        /// <param name="plane">The plane on which the geometrical calculations are performed.</param>
        /// <param name="polygonalFace2Ds">The collection of 2D polygonal faces involved in the shading.</param>
        public GeometricalShadingSolverResult(System.DateTime dateTime, Plane? plane, IEnumerable<IPolygonalFace2D>? polygonalFace2Ds)
            : base(dateTime)
        {
            this.plane = Core.Query.Clone(plane);
            this.polygonalFace2Ds = Core.Query.CloneAndFilterNulls(polygonalFace2Ds);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DiGi.Solar.Classes.GeometricalShadingSolverResult"/> class by cloning an existing result.
        /// </summary>
        /// <param name="geometricalShadingSolverResult">The source geometrical shading solver result to clone.</param>
        public GeometricalShadingSolverResult(GeometricalShadingSolverResult? geometricalShadingSolverResult)
            : base(geometricalShadingSolverResult)
        {
            if (geometricalShadingSolverResult != null)
            {
                plane = Core.Query.Clone(geometricalShadingSolverResult.plane);
                polygonalFace2Ds = Core.Query.CloneAndFilterNulls(geometricalShadingSolverResult.polygonalFace2Ds);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DiGi.Solar.Classes.GeometricalShadingSolverResult"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the result data.</param>
        public GeometricalShadingSolverResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the total area of all associated 2D polygonal faces.
        /// </summary>
        [JsonIgnore]
        public override double Area
        {
            get
            {
                if (polygonalFace2Ds == null || polygonalFace2Ds.Count == 0)
                {
                    return 0;
                }

                double result = 0;
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

                    result += area_Temp;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the plane associated with this shading solver result.
        /// </summary>
        [JsonIgnore]
        public Plane? Plane
        {
            get
            {
                return Core.Query.Clone(plane);
            }
        }

        /// <summary>
        /// Gets the list of 2D polygonal faces associated with this shading solver result.
        /// </summary>
        [JsonIgnore]
        public List<IPolygonalFace2D>? PolygonalFace2Ds
        {
            get
            {
                return Core.Query.CloneAndFilterNulls(polygonalFace2Ds);
            }
        }

        /// <summary>
        /// Converts the 2D polygonal faces to their 3D representations based on the associated plane.
        /// </summary>
        /// <returns>A list of 3D polygonal faces, or <see langword="null"/> if the plane or 2D faces are not defined.</returns>
        public List<IPolygonalFace3D>? GetPolygonalFace3Ds()
        {
            if (plane == null || polygonalFace2Ds == null)
            {
                return null;
            }

            return polygonalFace2Ds.FilterNulls()?.ConvertAll(plane.Convert)?.FilterNulls();
        }
    }
}