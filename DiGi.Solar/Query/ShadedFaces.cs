using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.Solar
{
    public static partial class Query
    {
        /// <summary>
        /// Builds the shaded part of a receiver from its shadow faces: the shadows are merged into one hole-preserving union and each union face is clipped to the receiver.
        /// <para>This is the post-processing both shading solvers share, so the CPU <see cref="Classes.ShadingSolver"/> and the ComputeSharp solver cannot drift apart after the shadows are computed.</para>
        /// <para>If the merge fails (<c>DiGi.Geometry.Planar.Query.Union</c> returns <c>null</c> even after its snap-rounding retry), the unmerged shadows are clipped to the receiver and capped at its area instead (<see cref="ShadowFaces(PolygonalFace2D?, IEnumerable{PolygonalFace2D})"/>): a failed merge reads as shade, overstated at worst, and never as full sun.
        /// The same fallback applies when clipping a merged face to the receiver fails (<c>DiGi.Geometry.Planar.Query.Intersection</c> returns <c>null</c>), which a union face with zero-area sliver holes has caused (ZiolkowskiJakub/DiGi.Solar#16).</para>
        /// </summary>
        /// <param name="polygonalFace2D_Receiver">The receiver face, in its own plane coordinates, or null when the receiver has no face to clip against.</param>
        /// <param name="polygonalFace2Ds_Shadow">The shadow faces, in the receiver's plane coordinates, as the solver produced them before merging; null or empty for a fully sunlit receiver.</param>
        /// <returns>The shaded faces of the receiver, empty when no shadow reaches it; or null when <paramref name="polygonalFace2D_Receiver"/> is null.</returns>
        public static List<PolygonalFace2D>? ShadedFaces(this PolygonalFace2D? polygonalFace2D_Receiver, IEnumerable<PolygonalFace2D>? polygonalFace2Ds_Shadow)
        {
            if (polygonalFace2D_Receiver == null)
            {
                return null;
            }

            List<PolygonalFace2D> polygonalFace2Ds_Result = [];

            List<PolygonalFace2D>? polygonalFace2Ds_Shadow_List = polygonalFace2Ds_Shadow?.ToList();
            if (polygonalFace2Ds_Shadow_List == null || polygonalFace2Ds_Shadow_List.Count == 0)
            {
                return polygonalFace2Ds_Result;
            }

            List<PolygonalFace2D>? polygonalFace2Ds_Union = polygonalFace2Ds_Shadow_List.Union();
            if (polygonalFace2Ds_Union == null)
            {
                return ShadowFaces(polygonalFace2D_Receiver, polygonalFace2Ds_Shadow_List) ?? polygonalFace2Ds_Result;
            }

            foreach (PolygonalFace2D polygonalFace2D_Union in polygonalFace2Ds_Union)
            {
                // A null clip means the overlay failed, not that the shadow misses the receiver: adding
                // nothing would read as full sun, so the unmerged fallback is used instead.
                List<PolygonalFace2D>? polygonalFace2Ds_Intersection = polygonalFace2D_Union.Intersection(polygonalFace2D_Receiver);
                if (polygonalFace2Ds_Intersection == null)
                {
                    return ShadowFaces(polygonalFace2D_Receiver, polygonalFace2Ds_Shadow_List) ?? [];
                }

                polygonalFace2Ds_Result.AddRange(polygonalFace2Ds_Intersection);
            }

            return polygonalFace2Ds_Result;
        }
    }
}
