using DiGi.Core.Classes;
using DiGi.Solar.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    public abstract class ShadingCalculationResult : GuidResult<IShadingElement>, IShadingCalculationResult
    {
        [JsonInclude, JsonPropertyName("DateTime")]
        private System.DateTime dateTime;

        public ShadingCalculationResult(System.DateTime dateTime)
            : base()
        {
            this.dateTime = dateTime;
        }

        public ShadingCalculationResult(ShadingCalculationResult shadingCalculationResult)
            : base(shadingCalculationResult)
        {
            if(shadingCalculationResult != null)
            {
                dateTime = shadingCalculationResult.dateTime;
            }
        }

        public ShadingCalculationResult(JsonObject jsonObject)
            : base(jsonObject)
        {

        }

        [JsonIgnore]
        public System.DateTime DateTime
        {
            get 
            { 
                return dateTime; 
            }
        }

        [JsonIgnore]
        public abstract double Area { get; }
    }
}
