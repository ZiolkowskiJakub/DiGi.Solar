using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Core.Relation.Classes;
using DiGi.Solar.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace DiGi.Solar.Classes
{
    public class ShadingRelationCluster : UniqueObjectRelationCluster<IShadingUniqueObject, IShadingRelation>, ISolarObject
    {
        public ShadingRelationCluster()
            : base()
        {

        }

        public ShadingRelationCluster(JsonObject jsonObject)
            : base(jsonObject)
        {

        }

        public ShadingRelationCluster(ShadingRelationCluster shadingRelationCluster)
            : base(shadingRelationCluster)
        {

        }

        public ShadingCalculationResultRelation AddRelation(IShadingElement shadingElement, IEnumerable<IShadingCalculationResult> shadingCalculationResults)
        {
            if (shadingElement == null || shadingCalculationResults == null)
            {
                return null;
            }

            ShadingCalculationResultRelation shadingCalculationResultRelation = GetRelation<ShadingCalculationResultRelation>(Core.Create.UniqueReference(shadingElement));
            if (shadingCalculationResultRelation != null)
            {
                Remove(shadingCalculationResultRelation);
            }

            return AddRelation(new ShadingCalculationResultRelation(shadingElement, shadingCalculationResults));
        }

        public List<TShadingCalculationResult> GetShadingCalculationResults<TShadingCalculationResult>(ShadingCalculationResultRelation shadingCalculationResultRelation) where TShadingCalculationResult : IShadingCalculationResult
        {
            List<IUniqueReference> uniqueReferences = shadingCalculationResultRelation?.UniqueReferences_To;
            if (uniqueReferences == null)
            {
                return null;
            }

            List<TShadingCalculationResult> result = new List<TShadingCalculationResult>();
            foreach (IUniqueReference uniqueReference in uniqueReferences)
            {
                if (uniqueReference == null)
                {
                    continue;
                }

                TShadingCalculationResult shadingCalculationResult = GetValue<TShadingCalculationResult>(uniqueReference as GuidReference);
                if (shadingCalculationResult == null)
                {
                    continue;
                }

                result.Add(shadingCalculationResult);
            }

            return result;
        }
    }
}
