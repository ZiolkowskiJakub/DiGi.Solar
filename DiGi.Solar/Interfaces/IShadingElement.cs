using DiGi.Geometry.Spatial.Interfaces;

namespace DiGi.Solar.Interfaces
{
    public interface IShadingElement : IShadingUniqueObject
    {
        string? Reference { get; }

        IPolygonalFace3D? PolygonalFace3D { get; }

        bool ShadingOnly { get; }
    }
}
