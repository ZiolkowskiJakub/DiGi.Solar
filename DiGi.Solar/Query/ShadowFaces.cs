using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using System.Collections.Generic;

namespace DiGi.Solar
{
    public static partial class Query
    {
        /// <summary>
        /// Builds the shadow of a receiver from unmerged shadow faces: each face is clipped to the receiver, and faces are kept in input order while their total area stays within the receiver face area.
        /// <para>Use when the merged union of the shadows is unavailable (<c>DiGi.Geometry.Planar.Query.Union</c> returned <c>null</c> even after its snap-rounding retry). Overlapping shadows are then counted twice, up to the cap, which overstates the shaded area but never above the receiver: a failed merge reads as shade rather than as full sun.</para>
        /// <para>For shadows that do not overlap each other the result is exactly the union, so the fallback is exact rather than merely bounded in the common case. The summed area stays within the receiver area unless the first clipped face is larger than the receiver on its own, which the clip itself cannot produce (it is inside the receiver) other than by numerical overshoot.</para>
        /// </summary>
        /// <param name="polygonalFace2D_Receiver">The receiver face, in its own plane coordinates, or null when the receiver has no face to clip and cap against.</param>
        /// <param name="polygonalFace2Ds_Shadow">The shadow faces, in the receiver's plane coordinates, as the solver produced them before merging.</param>
        /// <returns>A list of shadow faces clipped to the receiver, whose summed area never exceeds the receiver face area; or null when <paramref name="polygonalFace2D_Receiver"/> is null.</returns>
        public static List<PolygonalFace2D>? ShadowFaces(this PolygonalFace2D? polygonalFace2D_Receiver, IEnumerable<PolygonalFace2D> polygonalFace2Ds_Shadow)
        {
            List<PolygonalFace2D> polygonalFace2Ds_Result = [];

            if (polygonalFace2D_Receiver == null)
            {
                return null;
            }

            double area_Cap = polygonalFace2D_Receiver.GetArea();
            if (double.IsNaN(area_Cap) || area_Cap <= 0)
            {
                return polygonalFace2Ds_Result;
            }

            double area_Accumulated = 0;
            foreach (PolygonalFace2D? polygonalFace2D_Shadow in polygonalFace2Ds_Shadow)
            {
                if (polygonalFace2D_Shadow == null)
                {
                    continue;
                }

                // A null intersection means the overlay itself failed, so this face says nothing: it is skipped, unlike the empty list, which means it misses the receiver.
                List<PolygonalFace2D>? polygonalFace2Ds_Clip = polygonalFace2D_Shadow.Intersection(polygonalFace2D_Receiver);
                if (polygonalFace2Ds_Clip == null)
                {
                    continue;
                }

                foreach (PolygonalFace2D polygonalFace2D_Clip in polygonalFace2Ds_Clip)
                {
                    double area_Clip = polygonalFace2D_Clip.GetArea();
                    if (double.IsNaN(area_Clip) || area_Clip <= 0)
                    {
                        continue;
                    }

                    // The union is gone, so the receiver area is the only bound left: stop before it is broken, but always report the first face, however large, so a shaded receiver is never read as sunlit.
                    if (area_Accumulated > 0 && area_Accumulated + area_Clip > area_Cap)
                    {
                        return polygonalFace2Ds_Result;
                    }

                    area_Accumulated += area_Clip;
                    polygonalFace2Ds_Result.Add(polygonalFace2D_Clip);
                }
            }

            return polygonalFace2Ds_Result;
        }
    }
}
