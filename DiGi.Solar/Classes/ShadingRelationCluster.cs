using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Core.Relation.Classes;
using DiGi.Solar.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Represents a cluster of shading relations, associating shading unique objects with their corresponding shading relations.
    /// </summary>
    public class ShadingRelationCluster : UniqueObjectRelationCluster<IShadingUniqueObject, IShadingRelation>, ISolarObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingRelationCluster"/> class.
        /// </summary>
        public ShadingRelationCluster()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingRelationCluster"/> class from the specified JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the cluster data.</param>
        public ShadingRelationCluster(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingRelationCluster"/> class by copying another shading relation cluster.
        /// </summary>
        /// <param name="shadingRelationCluster">The source shading relation cluster to copy from.</param>
        public ShadingRelationCluster(ShadingRelationCluster? shadingRelationCluster)
            : base(shadingRelationCluster)
        {
        }

        /// <summary>
        /// Adds a shading solver result relation for the specified shading element and results.
        /// </summary>
        /// <param name="shadingElement">The shading element associated with the relation.</param>
        /// <param name="shadingSolverResults">The collection of shading solver results to associate.</param>
        /// <returns>The newly created <see cref="ShadingSolverResultRelation"/>, or null if any input is null.</returns>
        public ShadingSolverResultRelation? AddRelation(IShadingElement? shadingElement, IEnumerable<IShadingSolverResult>? shadingSolverResults)
        {
            if (shadingElement == null || shadingSolverResults == null)
            {
                return null;
            }

            ShadingSolverResultRelation? shadingSolverResultRelation = GetRelation<ShadingSolverResultRelation>(Core.Create.UniqueReference(shadingElement));
            if (shadingSolverResultRelation != null)
            {
                Remove(shadingSolverResultRelation);
            }

            return AddRelation(new ShadingSolverResultRelation(shadingElement, shadingSolverResults));
        }

        /// <summary>
        /// Retrieves a list of shading solver results of the specified type from the given relation.
        /// </summary>
        /// <typeparam name="TShadingSolverResult">The specific type of shading solver result to retrieve.</typeparam>
        /// <param name="shadingSolverResultRelation">The relation containing the references to the solver results.</param>
        /// <returns>A list of <typeparamref name="TShadingSolverResult"/> instances, or null if no results are found.</returns>
        public List<TShadingSolverResult>? GetShadingSolverResults<TShadingSolverResult>(ShadingSolverResultRelation? shadingSolverResultRelation) where TShadingSolverResult : IShadingSolverResult
        {
            List<IUniqueReference>? uniqueReferences = shadingSolverResultRelation?.UniqueReferences_To;
            if (uniqueReferences == null)
            {
                return null;
            }

            List<TShadingSolverResult> result = [];
            foreach (IUniqueReference uniqueReference in uniqueReferences)
            {
                if (uniqueReference == null)
                {
                    continue;
                }

                TShadingSolverResult? shadingSolverResult = GetValue<TShadingSolverResult>(uniqueReference as GuidReference);
                if (shadingSolverResult == null)
                {
                    continue;
                }

                result.Add(shadingSolverResult);
            }

            return result;
        }
    }
}