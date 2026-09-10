using DiGi.Core.Interfaces;

namespace DiGi.Solar.Interfaces
{
    /// <summary>
    /// Defines a contract for solar objects that can be serialized to and from JSON.
    /// </summary>
    public interface ISolarSerializableObject : ISolarObject, ISerializableObject
    {
    }
}
