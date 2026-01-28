using DiGi.Core.Classes;
using DiGi.Solar.Interfaces;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Solar.Classes
{
    public abstract class ShadingSolverResult : GuidResult<IShadingElement>, IShadingSolverResult
    {
        [JsonInclude, JsonPropertyName("DateTime")]
        private readonly DateTime dateTime = DateTime.MinValue;

        public ShadingSolverResult(DateTime dateTime)
            : base()
        {
            this.dateTime = dateTime;
        }

        public ShadingSolverResult(ShadingSolverResult? shadingSolverResult)
            : base(shadingSolverResult)
        {
            if (shadingSolverResult != null)
            {
                dateTime = shadingSolverResult.dateTime;
            }
        }

        public ShadingSolverResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        [JsonIgnore]
        public abstract double Area { get; }

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