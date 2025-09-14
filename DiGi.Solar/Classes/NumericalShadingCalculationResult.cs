using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    public class NumericalShadingCalculationResult : ShadingCalculationResult
    {
        [JsonInclude, JsonPropertyName("Area")]
        private readonly double area;

        public NumericalShadingCalculationResult(System.DateTime dateTime, double area)
            : base(dateTime)
        {
            this.area = area;
        }

        public NumericalShadingCalculationResult(NumericalShadingCalculationResult numericalShadingCalculationResult)
            : base(numericalShadingCalculationResult)
        {
            if(numericalShadingCalculationResult != null)
            {
                area = numericalShadingCalculationResult.area;
            }
        }

        public NumericalShadingCalculationResult(JsonObject jsonObject)
            : base(jsonObject)
        {

        }

        [JsonIgnore]
        public override double Area
        {
            get 
            { 
                return area; 
            }
        }

    }
}
