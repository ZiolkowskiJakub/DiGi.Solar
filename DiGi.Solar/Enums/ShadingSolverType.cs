using System.ComponentModel;

namespace DiGi.Solar.Enums
{
    /// <summary>
    /// Specifies the type of solver used for calculating shading.
    /// </summary>
    public enum ShadingSolverType
    {
        /// <summary>
        /// The shading solver type is undefined.
        /// </summary>
        [Description("Undefined")] Undefined,

        /// <summary>
        /// A numerical method for calculating shading.
        /// </summary>
        [Description("Numerical")] Numerical,

        /// <summary>
        /// A geometrical method for calculating shading.
        /// </summary>
        [Description("Geometrical")] Geometrical,
    }
}