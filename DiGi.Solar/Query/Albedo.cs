namespace DiGi.Solar
{
    public static partial class Query
    {
        /// <summary>
        /// Resolves the ground reflectance (albedo) to be used for the ground-reflected irradiance component.
        /// <para>When the ground is snow covered, the supplied albedo is ignored and the result is <see cref="Constants.Albedo.Snow"/>.</para>
        /// <para>An albedo that is null, not a number, zero or negative, or greater than one resolves to <see cref="Constants.Albedo.Default"/>. The upper bound subsumes the weather-file missing marker 999. Zero is treated as missing because weather records that carry no albedo column report it as zero rather than as the missing marker, and taking that at face value would silently remove the whole ground-reflected component.</para>
        /// <para>An EPW snow depth is not a reliable indicator of lying snow: POL_Warsaw.123750_IWEC.epw reports a constant 3.0 cm for every hour from April to November, a filler value that resolves to the snow reflectance for most of the year if read at face value (ZiolkowskiJakub/DiGi.Solar#2). The caller must decide <paramref name="snowCovered"/> from a source it trusts rather than pass a raw snow depth.</para>
        /// </summary>
        /// <param name="albedo">The ground reflectance as supplied by the weather record, as a decimal fraction. May be null, or a missing marker.</param>
        /// <param name="snowCovered">Whether snow is lying on the ground, decided by the caller. See the summary for why the library does not accept a snow depth.</param>
        /// <returns>The ground reflectance to use, as a decimal fraction between 0 and 1.</returns>
        public static double Albedo(double? albedo, bool snowCovered)
        {
            if (snowCovered)
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
