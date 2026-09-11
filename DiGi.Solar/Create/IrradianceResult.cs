using DiGi.Geometry.Spatial.Classes;

namespace DiGi.Solar
{
    public static partial class Create
    {
        /// <summary>
        /// Computes the three irradiance components incident on a surface for a single hour, using the Liu and Jordan isotropic sky model.
        /// <para>The direct beam component is the direct normal radiation projected onto the surface normal, and is zero when the sun is behind the surface. The sky diffuse component uses the isotropic sky view factor, and the ground-reflected component uses the complementary ground view factor.</para>
        /// <para>The tilt of the surface is taken from <paramref name="surfaceNormal"/> and is not supplied separately, so the two can never disagree. Tilts beyond 90 degrees are valid and describe a downward-facing surface, which sees more ground than sky.</para>
        /// <para><paramref name="sunDirection"/> is the sun ray propagation direction as returned by <see cref="Query.SunDirection(Innovative.SolarCalculator.SolarTimes)"/>, which points away from the sun and therefore downwards while the sun is above the horizon.</para>
        /// </summary>
        /// <param name="surfaceNormal">The outward normal of the surface. Need not be unit length.</param>
        /// <param name="sunDirection">The sun ray propagation direction, pointing away from the sun. Need not be unit length.</param>
        /// <param name="globalHorizontalRadiation">The global horizontal radiation, in W/m2.</param>
        /// <param name="directNormalRadiation">The direct normal radiation, in W/m2.</param>
        /// <param name="diffuseHorizontalRadiation">The diffuse horizontal radiation, in W/m2.</param>
        /// <param name="albedo">The ground reflectance as a decimal fraction, as resolved by <see cref="Query.Albedo(double?, bool)"/>.</param>
        /// <returns>A <see cref="Classes.IrradianceResult"/> carrying the three components and the ground reflectance used, or <see langword="null"/> if either vector is null or has no length, or if any radiation value is not a number or is negative, or if the ground reflectance is not a number or lies outside the range 0 to 1.</returns>
        public static Classes.IrradianceResult? IrradianceResult(this Vector3D? surfaceNormal, Vector3D? sunDirection, double globalHorizontalRadiation, double directNormalRadiation, double diffuseHorizontalRadiation, double albedo)
        {
            bool IsUsable(double value) => !double.IsNaN(value) && !double.IsInfinity(value) && value >= 0;

            // A ground reflectance is a fraction. Anything above 1 is a missing marker or a unit
            // mix-up rather than a physical value, and would silently inflate the ground component.
            if (!IsUsable(globalHorizontalRadiation) || !IsUsable(directNormalRadiation) || !IsUsable(diffuseHorizontalRadiation) || !IsUsable(albedo) || albedo > 1)
            {
                return null;
            }

            // Vector3D.Unit normalizes a copy in place and never returns null, so a zero-length
            // input has to be rejected here rather than by a null check on the result.
            if (surfaceNormal == null || sunDirection == null || !IsUsable(surfaceNormal.Length) || !IsUsable(sunDirection.Length) || surfaceNormal.Length <= 0 || sunDirection.Length <= 0)
            {
                return null;
            }

            Vector3D? vector3D_Normal = surfaceNormal.Unit;
            Vector3D? vector3D_Sun = sunDirection.Unit;
            if (vector3D_Normal == null || vector3D_Sun == null)
            {
                return null;
            }

            // The cosine of the tilt from horizontal is the dot product of the unit normal with the
            // world Z axis, which is its Z ordinate. Taking it directly rather than as
            // Math.Cos(normal.Angle(WorldZ)) avoids an acos/cos round trip that is ill-conditioned
            // near the poles of the range and that leaves a vertical surface at 6.1E-17 instead of 0.
            double cosineTilt = vector3D_Normal.Z;
            if (double.IsNaN(cosineTilt))
            {
                return null;
            }

            // The sun direction points away from the sun, so the cosine of the incidence angle is
            // the negated dot product rather than the dot product itself.
            double cosineIncidence = -vector3D_Sun.DotProduct(vector3D_Normal);
            if (double.IsNaN(cosineIncidence))
            {
                return null;
            }

            double beam = cosineIncidence > 0 ? directNormalRadiation * cosineIncidence : 0;
            double diffuse = diffuseHorizontalRadiation * (1 + cosineTilt) / 2;
            double ground = globalHorizontalRadiation * albedo * (1 - cosineTilt) / 2;

            return new Classes.IrradianceResult(beam, diffuse, ground, albedo);
        }
    }
}
