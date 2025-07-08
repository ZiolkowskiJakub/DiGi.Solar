using DiGi.Core.Classes;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.Solar.Interfaces;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    public class ShadingElement : GuidObject, IShadingElement
    {
        [JsonInclude, JsonPropertyName("PolygonalFace3D")]
        private IPolygonalFace3D polygonalFace3D;

        [JsonInclude, JsonPropertyName("Reference")]
        private string reference;

        [JsonInclude, JsonPropertyName("ShadingOnly")]
        private bool shadingOnly = false;

        public ShadingElement(Guid guid, string reference, IPolygonalFace3D polygonalFace3D, bool shadingOnly)
            : base(guid)
        {
            this.reference = reference;
            this.polygonalFace3D = Core.Query.Clone(polygonalFace3D);
            this.shadingOnly = shadingOnly;
        }

        public ShadingElement(string reference, IPolygonalFace3D polygonalFace3D, bool shadingOnly)
            : base()
        {
            this.reference = reference;
            this.polygonalFace3D = Core.Query.Clone(polygonalFace3D);
            this.shadingOnly = shadingOnly;
        }

        public ShadingElement(IPolygonalFace3D polygonalFace3D, bool shadingOnly)
            : base()
        {
            this.polygonalFace3D = Core.Query.Clone(polygonalFace3D);
            this.shadingOnly = shadingOnly;
        }

        public ShadingElement(ShadingElement shadingElement)
            : base(shadingElement)
        {
            if (shadingElement != null)
            {
                reference = shadingElement.reference;
                polygonalFace3D = Core.Query.Clone(shadingElement.polygonalFace3D);
                shadingOnly = shadingElement.shadingOnly;
            }
        }

        public ShadingElement(JsonObject jsonObject)
            : base(jsonObject)
        {

        }

        [JsonIgnore]
        public IPolygonalFace3D PolygonalFace3D
        {
            get
            {
                return Core.Query.Clone(polygonalFace3D);

            }
        }

        [JsonIgnore]
        public string Reference
        {
            get
            {
                return reference;
            }
        }

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
