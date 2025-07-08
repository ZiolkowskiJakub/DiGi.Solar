using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Solar.Enums;
using DiGi.Solar.Interfaces;
using System.Text.Json.Serialization;

namespace DiGi.Solar.ComputeSharp.Classes
{
    public class ShadingCalculatorOptions : SerializableObject, IShadingSerializableObject, IOptions
    {
        [JsonInclude, JsonPropertyName("AngleTolerance")]
        public double AngleTolerance { get; set; } = Core.Constans.Tolerance.Angle / 2;

        [JsonInclude, JsonPropertyName("ShadingCalculationType")]
        public ShadingCalculationType ShadingCalculationType { get; set; } = ShadingCalculationType.Numerical;

        [JsonInclude, JsonPropertyName("TimeSeries")]
        public ITimeSeries TimeSeries { get; set; } = new DateTimeSeries(DateTime.Now);

        [JsonInclude, JsonPropertyName("Tolerance")]
        public double Tolerance { get; set; } = Core.Constans.Tolerance.Distance;
    }
}
