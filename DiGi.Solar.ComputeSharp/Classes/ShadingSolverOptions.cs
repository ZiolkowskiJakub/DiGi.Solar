using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Solar.Enums;
using DiGi.Solar.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.ComputeSharp.Classes
{
    /// <summary>
    /// Provides configuration options for the shading solver, including tolerances and time series settings.
    /// </summary>
    public class ShadingSolverOptions : SerializableObject, IShadingSerializableObject, IOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolverOptions"/> class by copying values from an existing options instance.
        /// </summary>
        /// <param name="shadingSolverOptions">The source shading solver options to copy from.</param>
        public ShadingSolverOptions(ShadingSolverOptions? shadingSolverOptions)
            : base(shadingSolverOptions)
        {
            if (shadingSolverOptions != null)
            {
                AngleTolerance = shadingSolverOptions.AngleTolerance;
                ShadingSolverType = shadingSolverOptions.ShadingSolverType;
                TimeSeries = Core.Query.Clone(shadingSolverOptions.TimeSeries) ?? new DateTimeSeries(DateTime.Now);
                Tolerance = shadingSolverOptions.Tolerance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolverOptions"/> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the options data.</param>
        public ShadingSolverOptions(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolverOptions"/> class with default values.
        /// </summary>
        public ShadingSolverOptions()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the angle tolerance used by the shading solver.
        /// </summary>
        [JsonInclude, JsonPropertyName("AngleTolerance")]
        public double AngleTolerance { get; set; } = Core.Constants.Tolerance.Angle / 2;

        /// <summary>
        /// Gets or sets the type of shading solver to be employed.
        /// </summary>
        [JsonInclude, JsonPropertyName("ShadingSolverType")]
        public ShadingSolverType ShadingSolverType { get; set; } = ShadingSolverType.Numerical;

        /// <summary>
        /// Gets or sets the time series used for shading calculations.
        /// </summary>
        [JsonInclude, JsonPropertyName("TimeSeries")]
        public ITimeSeries TimeSeries { get; set; } = new DateTimeSeries(DateTime.Now);

        /// <summary>
        /// Gets or sets the distance tolerance used by the shading solver.
        /// </summary>
        [JsonInclude, JsonPropertyName("Tolerance")]
        public double Tolerance { get; set; } = Core.Constants.Tolerance.Distance;
    }
}