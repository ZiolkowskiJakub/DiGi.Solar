using DiGi.Core.Classes;
using DiGi.Solar.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Represents the total solar power incident on a partially shaded surface for a single hour.
    /// <para>The shadow blocks the direct beam component only. Sky diffuse and ground-reflected radiation still reach the shaded part of the surface, so the components are applied to different areas.</para>
    /// <para>Instances are plain carriers of already-computed values. Use <see cref="Create.SolarPowerResult(IrradianceResult, double, double)"/> or <see cref="Create.SolarPowerResult_ByShadingFactor(IrradianceResult, double, double)"/> to build one.</para>
    /// </summary>
    public class SolarPowerResult : SerializableResult, ISolarSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(TotalArea))]
        private readonly double totalArea = 0;

        [JsonInclude, JsonPropertyName(nameof(UnshadedArea))]
        private readonly double unshadedArea = 0;

        [JsonInclude, JsonPropertyName(nameof(IrradianceResult))]
        private readonly IrradianceResult? irradianceResult = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolarPowerResult"/> class.
        /// </summary>
        /// <param name="irradianceResult">The <see cref="Classes.IrradianceResult"/> whose components are applied to the areas below.</param>
        /// <param name="totalArea">The total area of the surface, in m2.</param>
        /// <param name="unshadedArea">The area of the surface in direct sunlight, in m2.</param>
        public SolarPowerResult(IrradianceResult? irradianceResult, double totalArea, double unshadedArea)
            : base()
        {
            this.irradianceResult = irradianceResult;
            this.totalArea = totalArea;
            this.unshadedArea = unshadedArea;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolarPowerResult"/> class by copying an existing instance.
        /// </summary>
        /// <param name="solarPowerResult">The source <see cref="SolarPowerResult"/> to copy from.</param>
        public SolarPowerResult(SolarPowerResult? solarPowerResult)
            : base(solarPowerResult)
        {
            if (solarPowerResult != null)
            {
                totalArea = solarPowerResult.totalArea;
                unshadedArea = solarPowerResult.unshadedArea;
                irradianceResult = Core.Query.Clone(solarPowerResult.irradianceResult);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolarPowerResult"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the solar power data.</param>
        public SolarPowerResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the total area of the surface, in m2.
        /// </summary>
        [JsonIgnore]
        public double TotalArea
        {
            get
            {
                return totalArea;
            }
        }

        /// <summary>
        /// Gets the area of the surface in direct sunlight, in m2. Never greater than <see cref="TotalArea"/>.
        /// </summary>
        [JsonIgnore]
        public double UnshadedArea
        {
            get
            {
                return unshadedArea;
            }
        }

        /// <summary>
        /// Gets the area of the surface in shadow, in m2, being <see cref="TotalArea"/> less <see cref="UnshadedArea"/>.
        /// </summary>
        [JsonIgnore]
        public double ShadedArea
        {
            get
            {
                return totalArea - unshadedArea;
            }
        }

        /// <summary>
        /// Gets the <see cref="Classes.IrradianceResult"/> the power is derived from.
        /// </summary>
        [JsonIgnore]
        public IrradianceResult? IrradianceResult
        {
            get
            {
                return Core.Query.Clone(irradianceResult);
            }
        }

        /// <summary>
        /// Gets the total solar power incident on the surface, in W.
        /// <para>Computed as the unshaded area times the beam component, plus the total area times the sum of the sky diffuse and ground-reflected components.</para>
        /// <para>Returns <see cref="double.NaN"/> when <see cref="IrradianceResult"/> is null, which the factories never produce but deserialization of an incomplete document can.</para>
        /// </summary>
        [JsonIgnore]
        public double Power
        {
            get
            {
                if (irradianceResult == null)
                {
                    return double.NaN;
                }

                return (unshadedArea * irradianceResult.Beam) + (totalArea * (irradianceResult.Diffuse + irradianceResult.Ground));
            }
        }
    }
}
