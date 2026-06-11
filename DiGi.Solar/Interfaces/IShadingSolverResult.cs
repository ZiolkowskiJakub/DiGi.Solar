using DiGi.Core.Interfaces;

namespace DiGi.Solar.Interfaces
{
    /// <summary>
    /// Defines a contract for an object that represents the result of a shading solver operation.
    /// </summary>
    public interface IShadingSolverResult : IShadingUniqueObject, IResult<IShadingElement>
    {
        /// <summary>
        /// Gets the date and time associated with the shading calculation.
        /// </summary>
        System.DateTime DateTime { get; }

        /// <summary>
        /// Gets the calculated area of the shading result.
        /// </summary>
        double Area { get; }
    }
}