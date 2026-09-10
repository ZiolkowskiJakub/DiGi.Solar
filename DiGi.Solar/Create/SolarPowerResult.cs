namespace DiGi.Solar
{
    public static partial class Create
    {
        /// <summary>
        /// Computes the total solar power incident on a partially shaded surface, in W.
        /// <para>The shadow blocks the direct beam component only, so the beam component is applied to the unshaded area while the sky diffuse and ground-reflected components are applied to the whole surface. A fully shaded surface therefore still receives ambient light.</para>
        /// <para>Shading solvers in this workspace report the SHADED area rather than the unshaded one. Use <see cref="SolarPowerResult_ByShadingFactor(Classes.IrradianceResult, double, double)"/> to feed a shading factor straight from <see cref="Classes.ShadingModel.TryGetShadingFactor(Interfaces.IShadingElement, System.DateTime, out double, bool)"/> without inverting it by hand.</para>
        /// </summary>
        /// <param name="irradianceResult">The <see cref="Classes.IrradianceResult"/> for the hour and surface orientation.</param>
        /// <param name="totalArea">The total area of the surface, in m2.</param>
        /// <param name="unshadedArea">The area of the surface in direct sunlight, in m2. Must not exceed <paramref name="totalArea"/>.</param>
        /// <returns>A <see cref="Classes.SolarPowerResult"/>, or <see langword="null"/> if the irradiance result is null, either area is not a number or negative, or the unshaded area exceeds the total area.</returns>
        public static Classes.SolarPowerResult? SolarPowerResult(this Classes.IrradianceResult? irradianceResult, double totalArea, double unshadedArea)
        {
            bool IsUsable(double value) => !double.IsNaN(value) && !double.IsInfinity(value) && value >= 0;

            if (irradianceResult == null || !IsUsable(totalArea) || !IsUsable(unshadedArea))
            {
                return null;
            }

            if (unshadedArea > totalArea)
            {
                return null;
            }

            return new Classes.SolarPowerResult(irradianceResult, totalArea, unshadedArea);
        }
    }
}
