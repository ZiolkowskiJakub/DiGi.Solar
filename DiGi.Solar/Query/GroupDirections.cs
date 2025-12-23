using DiGi.Geometry.Spatial.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.Solar
{
    public static partial class Query
    {
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
