using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Represents the result of a numerical shading solver calculation, providing details such as the calculated area.
    /// </summary>
    public class NumericalShadingSolverResult : ShadingSolverResult
    {
        [JsonInclude, JsonPropertyName("Area")]
        private readonly double area;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericalShadingSolverResult"/> class with a specified date time and area.
        /// </summary>
        /// <param name="dateTime">The date and time associated with the shading calculation result.</param>
        /// <param name="area">The numerical value of the calculated shading area.</param>
        public NumericalShadingSolverResult(System.DateTime dateTime, double area)
            : base(dateTime)
        {
            this.area = area;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericalShadingSolverResult"/> class by copying an existing result instance.
        /// </summary>
        /// <param name="numericalShadingSolverResult">The source <see cref="NumericalShadingSolverResult"/> instance to copy from.</param>
        public NumericalShadingSolverResult(NumericalShadingSolverResult numericalShadingSolverResult)
            : base(numericalShadingSolverResult)
        {
            if (numericalShadingSolverResult != null)
            {
                area = numericalShadingSolverResult.area;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericalShadingSolverResult"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the result data.</param>
        public NumericalShadingSolverResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the numerical shading area.
        /// </summary>
        [JsonIgnore]
        public override double Area
        {
            get
            {
                return area;
            }
        }
    }
}