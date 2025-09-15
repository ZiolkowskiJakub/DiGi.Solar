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
    public class GeometricalShadingSolverResult : ShadingSolverResult
    {
        [JsonInclude, JsonPropertyName("Plane")]
        private readonly Plane? plane;

        [JsonInclude, JsonPropertyName("PolygonalFace2Ds")]
        private readonly List<IPolygonalFace2D>? polygonalFace2Ds;

        public GeometricalShadingSolverResult(System.DateTime dateTime, Plane? plane, IEnumerable<IPolygonalFace2D>? polygonalFace2Ds)
            : base(dateTime)
        {
            this.plane = Core.Query.Clone(plane);
            this.polygonalFace2Ds = Core.Query.CloneAndFilterNulls(polygonalFace2Ds);
        }

        public GeometricalShadingSolverResult(GeometricalShadingSolverResult? geometricalShadingSolverResult)
            : base(geometricalShadingSolverResult)
        {
            if(geometricalShadingSolverResult != null)
            {
                plane = Core.Query.Clone(geometricalShadingSolverResult.plane);
                polygonalFace2Ds = Core.Query.CloneAndFilterNulls(geometricalShadingSolverResult.polygonalFace2Ds);
            }
        }

        public GeometricalShadingSolverResult(JsonObject? jsonObject)
            : base(jsonObject)
        {

        }

        [JsonIgnore]
        public override double Area
        {
            get 
            { 
                if(polygonalFace2Ds == null || polygonalFace2Ds.Count == 0)
                {
                    return 0;
                }

                double result = 0;
                foreach(IPolygonalFace2D polygonalFace2D in polygonalFace2Ds)
                {
                    if(polygonalFace2D == null)
                    {
                        continue;
                    }

                    double area_Temp = polygonalFace2D.GetArea();
                    if(double.IsNaN(area_Temp))
                    {
                        continue;
                    }

                    result += area_Temp;

                }

                return result; 
            }
        }

        [JsonIgnore]
        public Plane? Plane
        {
            get
            {
                return Core.Query.Clone(plane);
            }
        }

        [JsonIgnore]
        public List<IPolygonalFace2D>? PolygonalFace2Ds
        {
            get
            {
                return Core.Query.CloneAndFilterNulls(polygonalFace2Ds);
            }
        }

        public List<IPolygonalFace3D>? GetPolygonalFace3Ds()
        {
            if(plane == null || polygonalFace2Ds == null)
            {
                return null;
            }

            return polygonalFace2Ds.ConvertAll(plane.Convert)?.FilterNulls();
        }

    }
}
