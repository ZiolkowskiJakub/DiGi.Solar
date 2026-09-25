using DiGi.Core.Classes;
using DiGi.Solar.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Represents how much of the sky and of the ground a receiver of a shading model can see past the geometry around it (its own building and the shading-only neighbours).
    /// <para>Both values are the unblocked share of the isotropic view factor: 1 is an open view (the view factors <c>(1 + cos tilt) / 2</c> and <c>(1 - cos tilt) / 2</c> of <see cref="Create.IrradianceResult(Geometry.Spatial.Classes.Vector3D?, Geometry.Spatial.Classes.Vector3D?, double, double, double, double)"/> apply in full), 0 is a fully blocked view. Scale the sky diffuse component by <see cref="SkyVisibility"/> and the ground-reflected component by <see cref="GroundVisibility"/>.</para>
    /// <para>Instances are plain carriers of already-computed values. Use <see cref="Create.ViewFactorResults(ShadingModel?, System.Collections.Generic.IDictionary{string, Geometry.Spatial.Classes.Vector3D}?, double, int, int)"/> to calculate them.</para>
    /// </summary>
    public class ViewFactorResult : SerializableResult, ISolarSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(GroundVisibility))]
        private readonly double groundVisibility = 1;

        [JsonInclude, JsonPropertyName(nameof(Reference))]
        private readonly string? reference = null;

        [JsonInclude, JsonPropertyName(nameof(SkyVisibility))]
        private readonly double skyVisibility = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewFactorResult"/> class from already-computed visibilities.
        /// </summary>
        /// <param name="reference">The reference of the receiving shading element.</param>
        /// <param name="skyVisibility">The unblocked share of the receiver's isotropic sky view factor, from 0 (blocked) to 1 (open).</param>
        /// <param name="groundVisibility">The unblocked share of the receiver's isotropic ground view factor, from 0 (blocked) to 1 (open).</param>
        public ViewFactorResult(string? reference, double skyVisibility, double groundVisibility)
            : base()
        {
            this.reference = reference;
            this.skyVisibility = skyVisibility;
            this.groundVisibility = groundVisibility;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewFactorResult"/> class by copying an existing instance.
        /// </summary>
        /// <param name="viewFactorResult">The source <see cref="ViewFactorResult"/> to copy from.</param>
        public ViewFactorResult(ViewFactorResult? viewFactorResult)
            : base(viewFactorResult)
        {
            if (viewFactorResult != null)
            {
                reference = viewFactorResult.reference;
                skyVisibility = viewFactorResult.skyVisibility;
                groundVisibility = viewFactorResult.groundVisibility;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewFactorResult"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the view factor data.</param>
        public ViewFactorResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the unblocked share of the receiver's isotropic ground view factor, from 0 (blocked) to 1 (open).
        /// </summary>
        [JsonIgnore]
        public double GroundVisibility
        {
            get
            {
                return groundVisibility;
            }
        }

        /// <summary>
        /// Gets the reference of the receiving shading element (<see cref="Interfaces.IShadingElement.Reference"/>).
        /// </summary>
        [JsonIgnore]
        public string? Reference
        {
            get
            {
                return reference;
            }
        }

        /// <summary>
        /// Gets the unblocked share of the receiver's isotropic sky view factor, from 0 (blocked) to 1 (open).
        /// </summary>
        [JsonIgnore]
        public double SkyVisibility
        {
            get
            {
                return skyVisibility;
            }
        }
    }
}
