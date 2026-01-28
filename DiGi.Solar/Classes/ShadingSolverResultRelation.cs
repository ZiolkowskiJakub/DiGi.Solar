using DiGi.Core.Relation.Classes;
using DiGi.Solar.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace DiGi.Solar.Classes
{
    public class ShadingSolverResultRelation : OneToManyDirectionalRelation<IShadingElement, IShadingSolverResult>, IShadingRelation
    {
        public ShadingSolverResultRelation(IShadingElement shadingElement, IShadingSolverResult shadingSolverResult)
            : base(shadingElement, [shadingSolverResult])
        {
        }

        public ShadingSolverResultRelation(IShadingElement shadingElement, IEnumerable<IShadingSolverResult> shadingSolverResults)
            : base(shadingElement, shadingSolverResults)
        {
        }

        public ShadingSolverResultRelation(ShadingSolverResultRelation shadingSolverResultRelation)
            : base(shadingSolverResultRelation)
        {
        }

        public ShadingSolverResultRelation(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }
    }
}