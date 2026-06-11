using DiGi.Core.Interfaces;

namespace DiGi.Solar.Interfaces
{
    /// <summary>
    /// Defines a contract for shading objects that can be serialized to and from JSON.
    /// </summary>
    public interface IShadingSerializableObject : IShadingObject, ISerializableObject
    {
    }
}