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
    }
}
