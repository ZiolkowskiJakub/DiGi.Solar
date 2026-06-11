using DiGi.Core.Interfaces;

namespace DiGi.Solar.Interfaces
{
    /// <summary>
    /// Defines a contract for a shading object that is both serializable and possesses a unique identifier.
    /// </summary>
    public interface IShadingUniqueObject : IShadingSerializableObject, IUniqueObject
    {
    }
}