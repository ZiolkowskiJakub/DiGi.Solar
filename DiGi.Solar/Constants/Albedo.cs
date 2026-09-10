namespace DiGi.Solar.Constants
{
    /// <summary>
    /// Provides ground reflectance (albedo) constants used by the irradiance calculation.
    /// </summary>
    public static class Albedo
    {
        /// <summary>
        /// Default ground reflectance applied when no usable albedo value is available.
        /// </summary>
        public const double Default = 0.2;

        /// <summary>
        /// Ground reflectance applied when snow is lying on the ground.
        /// </summary>
        public const double Snow = 0.7;

        /// <summary>
        /// The marker weather files use for a missing numeric field, which both albedo and snow depth encode as 999.
        /// <para>The albedo branch of <see cref="Query.Albedo(double?, double?)"/> does not read this constant, because rejecting anything greater than 1 already subsumes it. It is the snow depth check that needs it, so that a missing snow depth is not read as 999 cm of lying snow.</para>
        /// </summary>
        public const double Missing = 999;
    }
}
