using DiGi.Core.Interfaces;

namespace DiGi.Solar.Interfaces
{
    public interface IShadingSolverResult : IShadingUniqueObject, IResult<IShadingElement>
    {
        System.DateTime DateTime { get; }

        double Area { get; }
    }
}
