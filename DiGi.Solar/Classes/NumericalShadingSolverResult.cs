using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    public class NumericalShadingSolverResult : ShadingSolverResult
    {
        [JsonInclude, JsonPropertyName("Area")]
        private readonly double area;

        public NumericalShadingSolverResult(System.DateTime dateTime, double area)
            : base(dateTime)
        {
            this.area = area;
        }

        public NumericalShadingSolverResult(NumericalShadingSolverResult numericalShadingSolverResult)
            : base(numericalShadingSolverResult)
        {
            if (numericalShadingSolverResult != null)
            {
                area = numericalShadingSolverResult.area;
            }
        }

        public NumericalShadingSolverResult(JsonObject? jsonObject)
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
