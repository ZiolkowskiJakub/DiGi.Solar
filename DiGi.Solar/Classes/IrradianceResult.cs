using DiGi.Core.Classes;
using DiGi.Solar.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Represents the irradiance incident on a single surface orientation for a single hour, split into its direct beam, sky diffuse and ground-reflected components.
    /// <para>Instances are plain carriers of already-computed values. Use <see cref="Create.IrradianceResult(Geometry.Spatial.Classes.Vector3D, Geometry.Spatial.Classes.Vector3D, double, double, double, double)"/> to compute the components from radiation values and a surface orientation.</para>
    /// </summary>
    public class IrradianceResult : SerializableResult, ISolarSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Beam))]
        private readonly double beam = 0;

        [JsonInclude, JsonPropertyName(nameof(Diffuse))]
        private readonly double diffuse = 0;

        [JsonInclude, JsonPropertyName(nameof(Ground))]
        private readonly double ground = 0;

        [JsonInclude, JsonPropertyName(nameof(Albedo))]
        private readonly double albedo = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="IrradianceResult"/> class from already-computed irradiance components.
        /// </summary>
        /// <param name="beam">The direct beam irradiance on the surface, in W/m2.</param>
        /// <param name="diffuse">The sky diffuse irradiance on the surface, in W/m2.</param>
        /// <param name="ground">The ground-reflected irradiance on the surface, in W/m2.</param>
        /// <param name="albedo">The ground reflectance used to compute the ground-reflected component, as a decimal fraction.</param>
        public IrradianceResult(double beam, double diffuse, double ground, double albedo)
            : base()
        {
            this.beam = beam;
            this.diffuse = diffuse;
            this.ground = ground;
            this.albedo = albedo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IrradianceResult"/> class by copying an existing instance.
        /// </summary>
        /// <param name="irradianceResult">The source <see cref="IrradianceResult"/> to copy from.</param>
        public IrradianceResult(IrradianceResult? irradianceResult)
            : base(irradianceResult)
        {
            if (irradianceResult != null)
            {
                beam = irradianceResult.beam;
                diffuse = irradianceResult.diffuse;
                ground = irradianceResult.ground;
                albedo = irradianceResult.albedo;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IrradianceResult"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the irradiance data.</param>
        public IrradianceResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the direct beam irradiance on the surface, in W/m2. Zero when the sun is behind the surface.
        /// </summary>
        [JsonIgnore]
        public double Beam
        {
            get
            {
                return beam;
            }
        }

        /// <summary>
        /// Gets the sky diffuse irradiance on the surface, in W/m2.
        /// </summary>
        [JsonIgnore]
        public double Diffuse
        {
            get
            {
                return diffuse;
            }
        }

        /// <summary>
        /// Gets the ground-reflected irradiance on the surface, in W/m2.
        /// </summary>
        [JsonIgnore]
        public double Ground
        {
            get
            {
                return ground;
            }
        }

        /// <summary>
        /// Gets the ground reflectance used to compute <see cref="Ground"/>, as a decimal fraction.
        /// </summary>
        [JsonIgnore]
        public double Albedo
        {
            get
            {
                return albedo;
            }
        }

        /// <summary>
        /// Gets the total irradiance incident on an unshaded surface, in W/m2, being the sum of <see cref="Beam"/>, <see cref="Diffuse"/> and <see cref="Ground"/>.
        /// </summary>
        [JsonIgnore]
        public double Total
        {
            get
            {
                return beam + diffuse + ground;
            }
        }
    }
}
