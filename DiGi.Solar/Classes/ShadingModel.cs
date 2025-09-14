using DiGi.Core;
using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.Solar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    public class ShadingModel : GuidObject, ISolarObject, IGuidModel
    {
        [JsonInclude, JsonPropertyName("Coordinates")]
        private readonly Coordinates? coordinates;

        [JsonInclude, JsonPropertyName("ShadingRelationCluster")]
        private readonly ShadingRelationCluster shadingRelationCluster = [];

        [JsonInclude, JsonPropertyName("UTC")]
        private readonly Core.Enums.UTC uTC;

        public ShadingModel(Core.Enums.UTC uTC, Coordinates? coordinates)
            : base()
        {
            this.uTC = uTC;
            this.coordinates = Core.Query.Clone(coordinates);
        }

        public ShadingModel(ShadingModel shadingModel)
            : base(shadingModel)
        {
            if (shadingModel != null)
            {
                uTC = shadingModel.uTC;
                coordinates = Core.Query.Clone(shadingModel.coordinates);
                shadingRelationCluster = Core.Query.Clone(shadingModel.shadingRelationCluster) ?? [];
            }
        }

        public ShadingModel(JsonObject jsonObject)
            : base(jsonObject)
        {

        }

        [JsonIgnore]
        public Coordinates? Coordinates
        {
            get
            {
                return Core.Query.Clone(coordinates);
            }
        }

        [JsonIgnore]
        public Core.Enums.UTC UTC
        {
            get
            {
                return uTC;
            }
        }

        public bool Assign(IShadingElement? shadingElement, IEnumerable<IShadingCalculationResult>? shadingCalculationResults)
        {
            if (shadingElement == null || shadingCalculationResults == null)
            {
                return false;
            }

            if (!Update(shadingElement))
            {
                return false;
            }

            List<IShadingCalculationResult> shadingCalculationResults_Temp = [];
            foreach (IShadingCalculationResult shadingCalculationResult in shadingCalculationResults)
            {
                if (Update(shadingCalculationResult))
                {
                    shadingCalculationResults_Temp.Add(shadingCalculationResult);
                }
            }

            return shadingRelationCluster.AddRelation(shadingElement, shadingCalculationResults_Temp) != null;
        }

        public List<TShadingCalculationResult>? GetShadingCalculationResults<TShadingCalculationResult>() where TShadingCalculationResult : IShadingCalculationResult
        {
            return shadingRelationCluster.GetValues<TShadingCalculationResult>()?.CloneAndFilterNulls();
        }

        public List<TShadingCalculationResult>? GetShadingCalculationResults<TShadingCalculationResult>(IShadingElement? shadingElement) where TShadingCalculationResult : IShadingCalculationResult
        {
            if (shadingElement == null)
            {
                return null;
            }

            ShadingCalculationResultRelation? shadingCalculationResultRelation = shadingRelationCluster.GetRelation<ShadingCalculationResultRelation>(Core.Create.UniqueReference(shadingElement));
            if (shadingCalculationResultRelation == null)
            {
                return null;
            }

            return shadingRelationCluster.GetShadingCalculationResults<TShadingCalculationResult>(shadingCalculationResultRelation)?.CloneAndFilterNulls();
        }

        public List<TShadingElement>? GetShadingElements<TShadingElement>(bool? shadingOnly = null) where TShadingElement : IShadingElement
        {
            return shadingRelationCluster.GetValues<TShadingElement>(x => shadingOnly is null || x!.ShadingOnly == shadingOnly.Value)?.CloneAndFilterNulls();
        }

        public bool TryGetShadingFactor(IShadingElement shadingElement, DateTime dateTime, out double factor, bool interpolation = true)
        {
            factor = double.NaN;

            if (shadingElement is null || shadingElement.ShadingOnly)
            {
                return false;
            }

            IPolygonalFace3D? polygonalFace3D = shadingElement.PolygonalFace3D;
            if (polygonalFace3D is null)
            {
                return false;
            }

            double area = polygonalFace3D.GetArea();
            if (double.IsNaN(area))
            {
                return false;
            }

            if (area == 0)
            {
                factor = 0;
                return true;
            }

            ShadingCalculationResultRelation? shadingCalculationResultRelation = shadingRelationCluster.GetRelation<ShadingCalculationResultRelation>(Create.UniqueReference(shadingElement));
            if (shadingCalculationResultRelation is null)
            {
                return false;
            }

            List<IShadingCalculationResult>? shadingCalculationResults = shadingRelationCluster.GetShadingCalculationResults<IShadingCalculationResult>(shadingCalculationResultRelation);
            if (shadingCalculationResults is null)
            {
                return false;
            }

            List<Tuple<long, IShadingCalculationResult>> tuples = [];
            foreach (IShadingCalculationResult shadingCalculationResult in shadingCalculationResults)
            {
                if (shadingCalculationResult.DateTime.Equals(dateTime))
                {
                    double area_Temp = shadingCalculationResult.Area;
                    if (double.IsNaN(area_Temp))
                    {
                        continue;
                    }

                    factor = area_Temp / area;
                    return true;
                }

                tuples.Add(new Tuple<long, IShadingCalculationResult>(shadingCalculationResult.DateTime.Ticks, shadingCalculationResult));
            }

            if (!interpolation)
            {
                return false;
            }

            if (tuples.Count == 0)
            {
                return false;
            }

            tuples.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            long ticks = dateTime.Ticks;

            if (ticks < tuples.First().Item1)
            {
                return false;
            }

            if (ticks > tuples.Last().Item1)
            {
                return false;
            }

            for (int i = 0; i < tuples.Count - 1; i++)
            {
                if (ticks > tuples[i].Item1)
                {
                    double ticks_Start = System.Convert.ToDouble(tuples[i - 1].Item1);
                    double area_Start = tuples[i - 1].Item2.Area;
                    double ticks_End = System.Convert.ToDouble(tuples[i - 1].Item1);
                    double area_End = tuples[i].Item2.Area;

                    factor = area_Start + (area_Start - area_End) / (ticks_Start - ticks_End) * (System.Convert.ToDouble(ticks) - ticks_Start);
                    return true;
                }
            }

            return false;
        }

        public bool Update(IShadingElement shadingElement)
        {
            if (shadingElement == null)
            {
                return false;
            }

            return shadingRelationCluster.Add(Core.Query.Clone(shadingElement));
        }

        public bool Update(IShadingCalculationResult shadingCalculationResult)
        {
            if (shadingCalculationResult == null)
            {
                return false;
            }

            return shadingRelationCluster.Add(Core.Query.Clone(shadingCalculationResult));
        }
    }
}
