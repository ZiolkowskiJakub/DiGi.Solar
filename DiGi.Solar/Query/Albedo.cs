namespace DiGi.Solar
{
    public static partial class Query
    {
        /// <summary>
        /// Resolves the ground reflectance (albedo) to be used for the ground-reflected irradiance component.
        /// <para>Snow lying on the ground takes precedence over any supplied albedo and resolves to <see cref="Constants.Albedo.Snow"/>.</para>
        /// <para>An albedo that is null, not a number, zero or negative, or greater than one resolves to <see cref="Constants.Albedo.Default"/>. The upper bound subsumes the weather-file missing marker <see cref="Constants.Albedo.Missing"/>. Zero is treated as missing because weather records that carry no albedo column report it as zero rather than as the missing marker, and taking that at face value would silently remove the whole ground-reflected component.</para>
        /// </summary>
        /// <param name="albedo">The ground reflectance as supplied by the weather record, as a decimal fraction. May be null, or a missing marker.</param>
        /// <param name="snowDepth">The depth of snow lying on the ground, in centimetres. May be null when unknown, or carry the weather-file missing marker <see cref="Constants.Albedo.Missing"/>, neither of which counts as snow.</param>
        /// <returns>The ground reflectance to use, as a decimal fraction between 0 and 1.</returns>
        public static double Albedo(double? albedo, double? snowDepth)
        {
            if (snowDepth.HasValue && !double.IsNaN(snowDepth.Value) && snowDepth.Value > 0 && snowDepth.Value < Constants.Albedo.Missing)
            {
                return Constants.Albedo.Snow;
            }

            if (!albedo.HasValue)
            {
                return Constants.Albedo.Default;
            }

            double albedo_Temp = albedo.Value;
            if (double.IsNaN(albedo_Temp) || albedo_Temp <= 0 || albedo_Temp > 1)
            {
                return Constants.Albedo.Default;
            }

            return albedo_Temp;
        }
    }
}
