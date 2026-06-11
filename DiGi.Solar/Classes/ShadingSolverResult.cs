using DiGi.Core.Classes;
using DiGi.Solar.Interfaces;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Represents the result of a shading solver calculation.
    /// </summary>
    public abstract class ShadingSolverResult : GuidResult<IShadingElement>, IShadingSolverResult
    {
        [JsonInclude, JsonPropertyName("DateTime")]
        private readonly DateTime dateTime = DateTime.MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolverResult"/> class with a specified date and time.
        /// </summary>
        /// <param name="dateTime">The date and time associated with the shading solver result.</param>
        public ShadingSolverResult(DateTime dateTime)
            : base()
        {
            this.dateTime = dateTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolverResult"/> class by copying an existing shading solver result.
        /// </summary>
        /// <param name="shadingSolverResult">The source shading solver result to copy from.</param>
        public ShadingSolverResult(ShadingSolverResult? shadingSolverResult)
            : base(shadingSolverResult)
        {
            if (shadingSolverResult != null)
            {
                dateTime = shadingSolverResult.dateTime;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolverResult"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the result data.</param>
        public ShadingSolverResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the area associated with the shading solver result.
        /// </summary>
        [JsonIgnore]
        public abstract double Area { get; }

        /// <summary>
        /// Gets the date and time associated with the shading solver result.
        /// </summary>
        [JsonIgnore]
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
        }
    }
}