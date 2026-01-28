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

        public ShadingRelationCluster(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        public ShadingRelationCluster(ShadingRelationCluster? shadingRelationCluster)
            : base(shadingRelationCluster)
        {
        }

        public ShadingSolverResultRelation? AddRelation(IShadingElement? shadingElement, IEnumerable<IShadingSolverResult>? shadingSolverResults)
        {
            if (shadingElement == null || shadingSolverResults == null)
            {
                return null;
            }

            ShadingSolverResultRelation? shadingSolverResultRelation = GetRelation<ShadingSolverResultRelation>(Core.Create.UniqueReference(shadingElement));
            if (shadingSolverResultRelation != null)
            {
                Remove(shadingSolverResultRelation);
            }

            return AddRelation(new ShadingSolverResultRelation(shadingElement, shadingSolverResults));
        }

        public List<TShadingSolverResult>? GetShadingSolverResults<TShadingSolverResult>(ShadingSolverResultRelation? shadingSolverResultRelation) where TShadingSolverResult : IShadingSolverResult
        {
            List<IUniqueReference>? uniqueReferences = shadingSolverResultRelation?.UniqueReferences_To;
            if (uniqueReferences == null)
            {
                return null;
            }

            List<TShadingSolverResult> result = [];
            foreach (IUniqueReference uniqueReference in uniqueReferences)
            {
                if (uniqueReference == null)
                {
                    continue;
                }

                TShadingSolverResult? shadingSolverResult = GetValue<TShadingSolverResult>(uniqueReference as GuidReference);
                if (shadingSolverResult == null)
                {
                    continue;
                }

                result.Add(shadingSolverResult);
            }

            return result;
        }
    }
}