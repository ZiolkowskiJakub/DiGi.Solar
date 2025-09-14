using DiGi.Core.Relation.Classes;
using DiGi.Solar.Interfaces;
using System.Collections.Generic;

namespace DiGi.Solar.Classes
{
    public class ShadingCalculationResultRelation : OneToManyDirectionalRelation<IShadingElement, IShadingCalculationResult>, IShadingRelation
    {
        public ShadingCalculationResultRelation(IShadingElement shadingElement, IShadingCalculationResult shadingCalculationResult)
            : base(shadingElement, [shadingCalculationResult])
        {

        }

        public ShadingCalculationResultRelation(IShadingElement shadingElement, IEnumerable<IShadingCalculationResult> shadingCalculationResults)
            : base(shadingElement, shadingCalculationResults)
        {

        }
    }
}
