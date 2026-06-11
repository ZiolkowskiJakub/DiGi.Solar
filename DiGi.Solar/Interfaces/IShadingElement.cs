using DiGi.Geometry.Spatial.Interfaces;

namespace DiGi.Solar.Interfaces
{
    /// <summary>
    /// Defines the properties of an element that provides shading.
    /// </summary>
    public interface IShadingElement : IShadingUniqueObject
    {
        /// <summary>
        /// Gets the reference identifier of the shading element.
        /// </summary>
        string? Reference { get; }

        /// <summary>
        /// Gets the 3D polygonal face associated with the shading element.
        /// </summary>
        IPolygonalFace3D? PolygonalFace3D { get; }

        /// <summary>
        /// Gets a value indicating whether the element is used exclusively for shading purposes.
        /// </summary>
        bool ShadingOnly { get; }
    }
}