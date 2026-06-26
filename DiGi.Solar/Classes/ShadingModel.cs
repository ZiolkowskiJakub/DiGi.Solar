using DiGi.Core;
using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.Solar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Represents a shading model that manages the relationship between shading elements and their corresponding solver results.
    /// </summary>
    public class ShadingModel : GuidObject, ISolarObject, IGuidModel
    {
        [JsonInclude, JsonPropertyName("Coordinates")]
        private readonly Coordinates? coordinates;

        [JsonInclude, JsonPropertyName("ShadingRelationCluster")]
        private readonly ShadingRelationCluster shadingRelationCluster = [];

        [JsonInclude, JsonPropertyName("UTC")]
        private readonly Core.Enums.UTC uTC;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingModel"/> class with the specified UTC offset and coordinates.
        /// </summary>
        /// <param name="uTC">The UTC time zone offset.</param>
        /// <param name="coordinates">The geographic coordinates associated with the model.</param>
        public ShadingModel(Core.Enums.UTC uTC, Coordinates? coordinates)
            : base()
        {
            this.uTC = uTC;
            this.coordinates = Core.Query.Clone(coordinates);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingModel"/> class by cloning an existing shading model.
        /// </summary>
        /// <param name="shadingModel">The source shading model to clone.</param>
        public ShadingModel(ShadingModel shadingModel)
            : base(shadingModel)
        {
            if (shadingModel != null)
            {
                uTC = shadingModel.uTC;
                coordinates = Core.Query.Clone(shadingModel.coordinates);
                shadingRelationCluster = Core.Query.Clone(shadingModel.shadingRelationCluster) ?? [];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingModel"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the model data.</param>
        public ShadingModel(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the geographic coordinates associated with the shading model.
        /// </summary>
        [JsonIgnore]
        public Coordinates? Coordinates
        {
            get
            {
                return Core.Query.Clone(coordinates);
            }
        }

        /// <summary>
        /// Gets the UTC time zone offset for the shading model.
        /// </summary>
        [JsonIgnore]
        public Core.Enums.UTC UTC
        {
            get
            {
                return uTC;
            }
        }

        /// <summary>
        /// Assigns a shading element and its corresponding solver results to the model's relation cluster.
        /// </summary>
        /// <param name="shadingElement">The shading element to assign.</param>
        /// <param name="shadingSolverResults">A collection of solver results associated with the element.</param>
        /// <returns>True if the assignment was successful; otherwise, false.</returns>
        public bool Assign(IShadingElement? shadingElement, IEnumerable<IShadingSolverResult>? shadingSolverResults)
        {
            if (shadingElement == null || shadingSolverResults == null)
            {
                return false;
            }

            if (!Update(shadingElement))
            {
                return false;
            }

            List<IShadingSolverResult> shadingSolverResults_Temp = [];
            foreach (IShadingSolverResult shadingSolverResult in shadingSolverResults)
            {
                if (Update(shadingSolverResult))
                {
                    shadingSolverResults_Temp.Add(shadingSolverResult);
                }
            }

            return shadingRelationCluster.AddRelation(shadingElement, shadingSolverResults_Temp) != null;
        }

        /// <summary>
        /// Retrieves a list of shading elements of the specified type, optionally filtered by their shading-only status.
        /// </summary>
        /// <typeparam name="TShadingElement">The type of shading element to retrieve.</typeparam>
        /// <param name="shadingOnly">Optional filter to include only elements that are marked as shading only.</param>
        /// <returns>A list of matching shading elements, or null if none were found.</returns>
        public List<TShadingElement>? GetShadingElements<TShadingElement>(bool? shadingOnly = null) where TShadingElement : IShadingElement
        {
            return shadingRelationCluster.GetValues<TShadingElement>(x => shadingOnly is null || x?.ShadingOnly == shadingOnly.Value)?.CloneAndFilterNulls();
        }

        /// <summary>
        /// Retrieves all shading solver results of the specified type.
        /// </summary>
        /// <typeparam name="TShadingSolverResult">The type of solver result to retrieve.</typeparam>
        /// <returns>A list of matching solver results, or null if none were found.</returns>
        public List<TShadingSolverResult>? GetShadingSolverResults<TShadingSolverResult>() where TShadingSolverResult : IShadingSolverResult
        {
            return shadingRelationCluster.GetValues<TShadingSolverResult>()?.CloneAndFilterNulls();
        }

        /// <summary>
        /// Retrieves shading solver results of the specified type for a specific shading element.
        /// </summary>
        /// <typeparam name="TShadingSolverResult">The type of solver result to retrieve.</typeparam>
        /// <param name="shadingElement">The shading element associated with the results.</param>
        /// <returns>A list of matching solver results, or null if the element is null or no results were found.</returns>
        public List<TShadingSolverResult>? GetShadingSolverResults<TShadingSolverResult>(IShadingElement? shadingElement) where TShadingSolverResult : IShadingSolverResult
        {
            if (shadingElement == null)
            {
                return null;
            }

            ShadingSolverResultRelation? shadingSolverResultRelation = shadingRelationCluster.GetRelation<ShadingSolverResultRelation>(Core.Create.UniqueReference(shadingElement));
            if (shadingSolverResultRelation == null)
            {
                return null;
            }

            return shadingRelationCluster.GetShadingSolverResults<TShadingSolverResult>(shadingSolverResultRelation)?.CloneAndFilterNulls();
        }

        /// <summary>
        /// Attempts to calculate the shading factor for a specific element at a given date and time.
        /// </summary>
        /// <param name="shadingElement">The shading element to evaluate.</param>
        /// <param name="dateTime">The date and time of evaluation.</param>
        /// <param name="factor">When this method returns, contains the calculated shading factor if successful; otherwise, <see cref="System.Double.NaN"/>.</param>
        /// <param name="interpolation">Whether to interpolate between known results if an exact match for the date and time is not found.</param>
        /// <returns>True if a shading factor was successfully determined; otherwise, false.</returns>
        public bool TryGetShadingFactor(IShadingElement shadingElement, DateTime dateTime, out double factor, bool interpolation = true)
        {
            factor = double.NaN;

            if (shadingElement is null || shadingElement.ShadingOnly)
            {
                return false;
            }

            IPolygonalFace3D? polygonalFace3D = shadingElement.PolygonalFace3D;
            if (polygonalFace3D is null)
            {
                return false;
            }

            double area = polygonalFace3D.GetArea();
            if (double.IsNaN(area))
            {
                return false;
            }

            if (area == 0)
            {
                factor = 0;
                return true;
            }

            ShadingSolverResultRelation? shadingSolverResultRelation = shadingRelationCluster.GetRelation<ShadingSolverResultRelation>(Create.UniqueReference(shadingElement));
            if (shadingSolverResultRelation is null)
            {
                return false;
            }

            List<IShadingSolverResult>? shadingSolverResults = shadingRelationCluster.GetShadingSolverResults<IShadingSolverResult>(shadingSolverResultRelation);
            if (shadingSolverResults is null)
            {
                return false;
            }

            List<Tuple<long, IShadingSolverResult>> tuples = [];
            foreach (IShadingSolverResult shadingSolverResult in shadingSolverResults)
            {
                double area_Temp = shadingSolverResult.Area;
                if (double.IsNaN(area_Temp))
                {
                    continue;
                }

                if (shadingSolverResult.DateTime.Equals(dateTime))
                {
                    factor = area_Temp / area;
                    return true;
                }

                tuples.Add(new Tuple<long, IShadingSolverResult>(shadingSolverResult.DateTime.Ticks, shadingSolverResult));
            }

            if (!interpolation)
            {
                return false;
            }

            if (tuples.Count == 0)
            {
                return false;
            }

            tuples.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            long ticks = dateTime.Ticks;

            if (ticks < tuples.First().Item1)
            {
                return false;
            }

            if (ticks > tuples.Last().Item1)
            {
                return false;
            }

            for (int i = 0; i < tuples.Count - 1; i++)
            {
                if (ticks >= tuples[i].Item1 && ticks <= tuples[i + 1].Item1)
                {
                    double ticks_Start = System.Convert.ToDouble(tuples[i].Item1);
                    double area_Start = tuples[i].Item2.Area;
                    double ticks_End = System.Convert.ToDouble(tuples[i + 1].Item1);
                    double area_End = tuples[i + 1].Item2.Area;

                    if (ticks_Start.Equals(ticks_End))
                    {
                        factor = area_Start / area;
                        return true;
                    }

                    double area_Interpolated = area_Start + (area_End - area_Start) / (ticks_End - ticks_Start) * (System.Convert.ToDouble(ticks) - ticks_Start);
                    factor = area_Interpolated / area;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Updates or adds a shading element to the model's relation cluster.
        /// </summary>
        /// <param name="shadingElement">The shading element to update.</param>
        /// <returns>True if the element was successfully added or updated; otherwise, false.</returns>
        public bool Update(IShadingElement shadingElement)
        {
            if (shadingElement == null)
            {
                return false;
            }

            return shadingRelationCluster.Add(Core.Query.Clone(shadingElement));
        }

        /// <summary>
        /// Updates or adds a shading solver result to the model's relation cluster.
        /// </summary>
        /// <param name="shadingSolverResult">The shading solver result to update.</param>
        /// <returns>True if the solver result was successfully added or updated; otherwise, false.</returns>
        public bool Update(IShadingSolverResult shadingSolverResult)
        {
            if (shadingSolverResult == null)
            {
                return false;
            }

            return shadingRelationCluster.Add(Core.Query.Clone(shadingSolverResult));
        }
    }
}