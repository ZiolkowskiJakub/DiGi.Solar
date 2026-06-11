using DiGi.Core.Relation.Interfaces;

namespace DiGi.Solar.Interfaces
{
    /// <summary>
    /// Defines the contract for a shading relation, combining the characteristics of a shading object and a relationship between unique objects.
    /// </summary>
    public interface IShadingRelation : IShadingObject, IRelation
    {
    }
}