using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Solar.Enums;
using DiGi.Solar.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.ComputeSharp.Classes
{
    public class ShadingSolverOptions : SerializableObject, IShadingSerializableObject, IOptions
    {
        public ShadingSolverOptions(ShadingSolverOptions? shadingSolverOptions)
            : base(shadingSolverOptions)
        {
            if (shadingSolverOptions != null)
            {
                AngleTolerance = shadingSolverOptions.AngleTolerance;
                ShadingSolverType = shadingSolverOptions.ShadingSolverType;
                TimeSeries = Core.Query.Clone(shadingSolverOptions.TimeSeries) ?? new DateTimeSeries(DateTime.Now);
                Tolerance = shadingSolverOptions.Tolerance;
            }
        }

        public ShadingSolverOptions(JsonObject? jsonObject)
            : base(jsonObject)
        {

        }

        public ShadingSolverOptions()
            : base()
        {

        }

        [JsonInclude, JsonPropertyName("AngleTolerance")]
        public double AngleTolerance { get; set; } = Core.Constans.Tolerance.Angle / 2;

        [JsonInclude, JsonPropertyName("ShadingSolverType")]
        public ShadingSolverType ShadingSolverType { get; set; } = ShadingSolverType.Numerical;

        [JsonInclude, JsonPropertyName("TimeSeries")]
        public ITimeSeries TimeSeries { get; set; } = new DateTimeSeries(DateTime.Now);

        [JsonInclude, JsonPropertyName("Tolerance")]
        public double Tolerance { get; set; } = Core.Constans.Tolerance.Distance;
    }
}
