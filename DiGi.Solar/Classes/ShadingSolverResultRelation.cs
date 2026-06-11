using DiGi.Core.Relation.Classes;
using DiGi.Solar.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Represents a one-to-many directional relationship between a shading element and its corresponding shading solver results.
    /// </summary>
    public class ShadingSolverResultRelation : OneToManyDirectionalRelation<IShadingElement, IShadingSolverResult>, IShadingRelation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolverResultRelation"/> class with a specific shading element and a single solver result.
        /// </summary>
        /// <param name="shadingElement">The parent shading element.</param>
        /// <param name="shadingSolverResult">The related shading solver result.</param>
        public ShadingSolverResultRelation(IShadingElement shadingElement, IShadingSolverResult shadingSolverResult)
            : base(shadingElement, [shadingSolverResult])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolverResultRelation"/> class with a specific shading element and a collection of solver results.
        /// </summary>
        /// <param name="shadingElement">The parent shading element.</param>
        /// <param name="shadingSolverResults">The collection of related shading solver results.</param>
        public ShadingSolverResultRelation(IShadingElement shadingElement, IEnumerable<IShadingSolverResult> shadingSolverResults)
            : base(shadingElement, shadingSolverResults)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolverResultRelation"/> class by copying an existing relation.
        /// </summary>
        /// <param name="shadingSolverResultRelation">The source relation to copy.</param>
        public ShadingSolverResultRelation(ShadingSolverResultRelation shadingSolverResultRelation)
            : base(shadingSolverResultRelation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolverResultRelation"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing relation data, or null.</param>
        public ShadingSolverResultRelation(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }
    }
}