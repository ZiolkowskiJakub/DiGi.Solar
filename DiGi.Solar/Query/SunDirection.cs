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
        public static Vector3D SunDirection(this SolarTimes solarTimes)
        {
            Angle angle_SolarElevation = solarTimes.SolarElevation;
            if (angle_SolarElevation == null)
            {
                return null;
            }

            Angle angle_SolarAzimuth = solarTimes.SolarAzimuth;
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

        public static Vector3D SunDirection(this Coordinates coordinates, UTC uTC, DateTime dateTime, bool includeNight = false)
        {
            if (coordinates == null || dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                return null;
            }

            Angle angle_Latitude = new Angle(coordinates.Latitude);
            Angle angle_Longitude = new Angle(coordinates.Longitude);

            int timeOffset = Convert.ToInt32(Core.Query.TimeOffset(uTC));

            SolarTimes solarTimes = new SolarTimes(dateTime, timeOffset, angle_Latitude, angle_Longitude);
            if (!includeNight && (dateTime < solarTimes.Sunrise || dateTime > solarTimes.Sunset))
            {
                return null;
            }

            return SunDirection(solarTimes);
        }

        public static Vector3D SunDirection(this ShadingModel shadingModel, DateTime dateTime, bool includeNight = false)
        {
            if(shadingModel == null)
            {
                return null;
            }

            return SunDirection(shadingModel.Coordinates, shadingModel.UTC, dateTime, includeNight);
        }
    }
}
