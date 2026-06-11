using DiGi.Geometry.Spatial.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.Solar
{
    public static partial class Query
    {
        /// <summary>
        /// Groups directions from a dictionary of dates and vectors based on a specified angle tolerance.
        /// </summary>
        /// <param name="dictionary">The dictionary containing date-time keys and their corresponding 3D direction vectors.</param>
        /// <param name="angleTolerance">The maximum angle difference allowed to group two directions together.</param>
        /// <returns>A list of tuples, where each tuple contains a representative <see cref="T:DiGi.Geometry.Spatial.Classes.Vector3D" /> and a list of <see cref="T:System.DateTime" /> values associated with that direction; returns <see langword="null" /> if the input dictionary is null.</returns>
        public static List<Tuple<Vector3D, List<DateTime>>>? GroupDirections(this Dictionary<DateTime, Vector3D>? dictionary, double angleTolerance)
        {
            if (dictionary == null)
            {
                return null;
            }

            List<Tuple<Vector3D, List<DateTime>>> result = [];
            foreach (KeyValuePair<DateTime, Vector3D> keyValuePair in dictionary)
            {
                int index = result.FindIndex(x => x.Item1.Angle(keyValuePair.Value) < angleTolerance);
                if (index == -1)
                {
                    index = result.Count;
                    result.Add(new Tuple<Vector3D, List<DateTime>>(keyValuePair.Value, []));
                }
                else
                {
                    Vector3D? vector3D = (result[index].Item1 + keyValuePair.Value) / 2;
                    if (vector3D is not null)
                    {
                        result[index] = new Tuple<Vector3D, List<DateTime>>(vector3D, result[index].Item2);
                    }
                }

                result[index].Item2.Add(keyValuePair.Key);
            }

            return result;
        }
    }
}