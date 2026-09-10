namespace DiGi.Solar
{
    public static partial class Create
    {
        /// <summary>
        /// Computes the total solar power incident on a partially shaded surface, in W, from the SHADED fraction of that surface.
        /// <para>This is the entry point for callers holding a shading solver result. <see cref="Classes.ShadingModel.TryGetShadingFactor(Interfaces.IShadingElement, System.DateTime, out double, bool)"/> and the <see cref="Interfaces.IShadingSolverResult.Area"/> of a solver result both describe the area in SHADOW, so passing either into the unshaded-area overload directly would invert the answer.</para>
        /// </summary>
        /// <param name="irradianceResult">The <see cref="Classes.IrradianceResult"/> for the hour and surface orientation.</param>
        /// <param name="totalArea">The total area of the surface, in m2.</param>
        /// <param name="shadingFactor">The shaded fraction of the surface, between 0 for fully lit and 1 for fully shaded.</param>
        /// <returns>A <see cref="Classes.SolarPowerResult"/>, or <see langword="null"/> if the irradiance result is null, the total area is not a number or negative, or the shading factor is not a number or lies outside the range 0 to 1.</returns>
        public static Classes.SolarPowerResult? SolarPowerResult_ByShadingFactor(this Classes.IrradianceResult? irradianceResult, double totalArea, double shadingFactor)
        {
            if (double.IsNaN(shadingFactor) || shadingFactor < 0 || shadingFactor > 1)
            {
                return null;
            }

            return SolarPowerResult(irradianceResult, totalArea, totalArea * (1 - shadingFactor));
        }
    }
}
