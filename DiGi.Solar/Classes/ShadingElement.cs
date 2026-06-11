using DiGi.Core.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.Solar.Interfaces;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Represents an element that provides shading in a solar analysis context.
    /// </summary>
    public class ShadingElement : GuidObject, IShadingElement
    {
        [JsonInclude, JsonPropertyName("PolygonalFace3D")]
        private readonly IPolygonalFace3D? polygonalFace3D;

        [JsonInclude, JsonPropertyName("Reference")]
        private readonly string? reference;

        [JsonInclude, JsonPropertyName("ShadingOnly")]
        private readonly bool shadingOnly = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingElement"/> class with a specified unique identifier and properties.
        /// </summary>
        /// <param name="guid">The unique identifier for the object.</param>
        /// <param name="reference">The reference string identifying the element.</param>
        /// <param name="polygonalFace3D">The 3D polygonal face representing the geometry of the shading element.</param>
        /// <param name="shadingOnly">A flag indicating if the element is used exclusively for shading calculations.</param>
        public ShadingElement(Guid guid, string? reference, IPolygonalFace3D? polygonalFace3D, bool shadingOnly)
            : base(guid)
        {
            this.reference = reference;
            this.polygonalFace3D = Core.Query.Clone(polygonalFace3D);
            this.shadingOnly = shadingOnly;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingElement"/> class with specified properties and a generated unique identifier.
        /// </summary>
        /// <param name="reference">The reference string identifying the element.</param>
        /// <param name="polygonalFace3D">The 3D polygonal face representing the geometry of the shading element.</param>
        /// <param name="shadingOnly">A flag indicating if the element is used exclusively for shading calculations.</param>
        public ShadingElement(string? reference, IPolygonalFace3D? polygonalFace3D, bool shadingOnly)
            : base()
        {
            this.reference = reference;
            this.polygonalFace3D = Core.Query.Clone(polygonalFace3D);
            this.shadingOnly = shadingOnly;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingElement"/> class with specified geometry and properties, and a generated unique identifier.
        /// </summary>
        /// <param name="polygonalFace3D">The 3D polygonal face representing the geometry of the shading element.</param>
        /// <param name="shadingOnly">A flag indicating if the element is used exclusively for shading calculations.</param>
        public ShadingElement(IPolygonalFace3D? polygonalFace3D, bool shadingOnly)
            : base()
        {
            this.polygonalFace3D = Core.Query.Clone(polygonalFace3D);
            this.shadingOnly = shadingOnly;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingElement"/> class by cloning an existing shading element.
        /// </summary>
        /// <param name="shadingElement">The source <see cref="ShadingElement"/> to clone.</param>
        public ShadingElement(ShadingElement? shadingElement)
            : base(shadingElement)
        {
            if (shadingElement != null)
            {
                reference = shadingElement.reference;
                polygonalFace3D = Core.Query.Clone(shadingElement.polygonalFace3D);
                shadingOnly = shadingElement.shadingOnly;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingElement"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the element data.</param>
        public ShadingElement(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the 3D polygonal face associated with this shading element.
        /// </summary>
        [JsonIgnore]
        public IPolygonalFace3D? PolygonalFace3D
        {
            get
            {
                return Core.Query.Clone(polygonalFace3D);
            }
        }

        /// <summary>
        /// Gets the reference identifier of the shading element.
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
        /// Gets a value indicating whether the element is used exclusively for shading calculations.
        /// </summary>
        [JsonIgnore]
        public bool ShadingOnly
        {
            get
            {
                return shadingOnly;
            }
        }
    }
}