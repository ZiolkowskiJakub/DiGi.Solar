using DiGi.Core.Classes;
using DiGi.Core.Enums;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Solar.Classes;
using Innovative.Geometry;
using Innovative.SolarCalculator;
using System;

namespace DiGi.Solar
{
    public static partial class Query
    {
        /// <summary>
        /// Calculates the sun's direction vector based on the provided solar times.
        /// </summary>
        /// <param name="solarTimes">The <see cref="SolarTimes"/> containing the solar elevation and azimuth.</param>
        /// <returns>A <see cref="Vector3D"/> representing the sun's direction, or <see langword="null"/> if the provided <see cref="SolarTimes"/> is null or contains invalid data.</returns>
        public static Vector3D? SunDirection(this SolarTimes? solarTimes)
        {
            Angle? angle_SolarElevation = solarTimes?.SolarElevation;
            if (angle_SolarElevation == null)
            {
                return null;
            }

            Angle angle_SolarAzimuth = solarTimes!.SolarAzimuth;
            if (angle_SolarAzimuth == null)
            {
                return null;
            }

            double solarElevation = Convert.ToDouble(angle_SolarElevation.Radians);
            double solarAzimuth = Convert.ToDouble(angle_SolarAzimuth.Radians) + (Math.PI / 2);

            double x = Math.Cos(solarElevation) * Math.Cos(solarAzimuth);
            double y = Math.Cos(solarElevation + Math.PI) * Math.Sin(solarAzimuth);
            double z = Math.Sin(solarElevation + Math.PI);

            return new Vector3D(x, y, z);
        }

        /// <summary>
        /// Calculates the sun's direction vector for a specific geographic location and date time.
        /// </summary>
        /// <param name="coordinates">The <see cref="Coordinates"/> of the geographic location.</param>
        /// <param name="uTC">The <see cref="UTC"/> timezone offset.</param>
        /// <param name="dateTime">The date and time for which to calculate the sun's position.</param>
        /// <param name="includeNight">If set to <see langword="false"/>, returns <see langword="null"/> if the specified <paramref name="dateTime"/> is before sunrise or after sunset.</param>
        /// <returns>A <see cref="Vector3D"/> representing the sun's direction, or <see langword="null"/> if coordinates are null, date time is invalid, or it is nighttime and <paramref name="includeNight"/> is false.</returns>
        public static Vector3D? SunDirection(this Coordinates? coordinates, UTC uTC, DateTime dateTime, bool includeNight = false)
        {
            if (coordinates == null || dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                return null;
            }

            Angle angle_Latitude = new(coordinates.Latitude);
            Angle angle_Longitude = new(coordinates.Longitude);

            int timeOffset = Convert.ToInt32(Core.Query.TimeOffset(uTC));

            SolarTimes solarTimes = new(dateTime, timeOffset, angle_Latitude, angle_Longitude);
            if (!includeNight && (dateTime < solarTimes.Sunrise || dateTime > solarTimes.Sunset))
            {
                return null;
            }

            return SunDirection(solarTimes);
        }

        /// <summary>
        /// Calculates the sun's direction vector based on a shading model's configuration and a specific date time.
        /// </summary>
        /// <param name="shadingModel">The <see cref="ShadingModel"/> containing location and timezone information.</param>
        /// <param name="dateTime">The date and time for which to calculate the sun's position.</param>
        /// <param name="includeNight">If set to <see langword="false"/>, returns <see langword="null"/> if the specified <paramref name="dateTime"/> is before sunrise or after sunset.</param>
        /// <returns>A <see cref="Vector3D"/> representing the sun's direction, or <see langword="null"/> if the shading model is null or other calculation constraints are not met.</returns>
        public static Vector3D? SunDirection(this ShadingModel? shadingModel, DateTime dateTime, bool includeNight = false)
        {
            if (shadingModel == null)
            {
                return null;
            }

            return SunDirection(shadingModel.Coordinates, shadingModel.UTC, dateTime, includeNight);
        }
    }
}