using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Spatial.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.Solar
{
    public static partial class Query
    {
        /// <summary>
        /// Computes the unmerged shadows that caster triangles throw onto a receiver plane along one propagation direction.
        /// <para>Each triangle is clipped to the part lying upstream of the receiver plane (between the light source and the plane), projected onto the plane along <paramref name="direction"/> and expressed in the plane's coordinates. Triangles whose projection misses the receiver's bounding box, and slivers with no area, are dropped.</para>
        /// <para>This is the per-direction step of <see cref="Classes.ShadingSolver.Solve"/>; merge and clip the result with <see cref="ShadedFaces(PolygonalFace2D?, IEnumerable{PolygonalFace2D}?)"/>. A direction grazing the plane (its dot product with the plane normal within <paramref name="tolerance"/>) casts no shadow.</para>
        /// </summary>
        /// <param name="plane">The receiver plane. This value can be null.</param>
        /// <param name="boundingBox2D">The bounding box of the receiver face in the plane's coordinates. This value can be null.</param>
        /// <param name="coordinates">The caster triangles, 9 coordinates (3 points x, y, z) per triangle.</param>
        /// <param name="indexes">The index of the element each triangle belongs to, one per triangle.</param>
        /// <param name="index">The index of the receiver; its own triangles (equal index) are skipped, so a receiver never shades itself. Use -1 to skip none.</param>
        /// <param name="direction">The propagation direction of the light, pointing away from its source. Need not be unit length.</param>
        /// <param name="tolerance">The distance tolerance.</param>
        /// <returns>The shadow faces in the receiver plane's coordinates, empty when no shadow reaches the receiver or an input is null.</returns>
        public static List<PolygonalFace2D> ProjectedShadowFaces(this Plane? plane, BoundingBox2D? boundingBox2D, double[]? coordinates, int[]? indexes, int index, Vector3D? direction, double tolerance)
        {
            List<PolygonalFace2D> result = [];

            if (plane is null || boundingBox2D is null || coordinates is null || indexes is null || direction is null)
            {
                return result;
            }

            Vector3D? normal = plane.Normal;
            Vector3D? axisX = plane.AxisX;
            Vector3D? axisY = plane.AxisY;
            Point3D? origin = plane.Origin;
            if (normal is null || axisX is null || axisY is null || origin is null)
            {
                return result;
            }

            double normalX = normal.X, normalY = normal.Y, normalZ = normal.Z;
            double axisXX = axisX.X, axisXY = axisX.Y, axisXZ = axisX.Z;
            double axisYX = axisY.X, axisYY = axisY.Y, axisYZ = axisY.Z;
            double originX = origin.X, originY = origin.Y, originZ = origin.Z;
            double minX = boundingBox2D.Min.X - tolerance, minY = boundingBox2D.Min.Y - tolerance;
            double maxX = boundingBox2D.Max.X + tolerance, maxY = boundingBox2D.Max.Y + tolerance;

            double vectorX = direction.X, vectorY = direction.Y, vectorZ = direction.Z;

            // Change of the signed plane distance per unit travelled along the direction; near zero the light grazes the plane and casts no shadow on it.
            double dotProduct = (normalX * vectorX) + (normalY * vectorY) + (normalZ * vectorZ);
            if (Math.Abs(dotProduct) <= tolerance)
            {
                return result;
            }

            // Sign that turns a signed plane distance into "distance on the source side" (positive = upstream).
            double sign = dotProduct > 0 ? 1 : -1;

            // Clipped polygon of one caster triangle: at most 4 points (a triangle cut by one plane).
            double[] xs = new double[4], ys = new double[4], zs = new double[4], ss = new double[4];

            int count_Triangle = Math.Min(indexes.Length, coordinates.Length / 9);
            for (int j = 0; j < count_Triangle; j++)
            {
                if (indexes[j] == index && index != -1)
                {
                    continue;
                }

                int offset = j * 9;

                double upstream_Max = double.MinValue;
                for (int k = 0; k < 3; k++)
                {
                    double x = coordinates[offset + (k * 3)] - originX;
                    double y = coordinates[offset + (k * 3) + 1] - originY;
                    double z = coordinates[offset + (k * 3) + 2] - originZ;

                    double upstream = -sign * ((normalX * x) + (normalY * y) + (normalZ * z));
                    if (upstream > upstream_Max)
                    {
                        upstream_Max = upstream;
                    }
                }

                if (upstream_Max <= tolerance)
                {
                    continue;
                }

                // Sutherland-Hodgman clip of the triangle against the upstream half-space (upstream >= 0).
                int count = 0;
                for (int k = 0; k < 3; k++)
                {
                    int offset_1 = offset + (k * 3);
                    int offset_2 = offset + (((k + 1) % 3) * 3);

                    double x_1 = coordinates[offset_1] - originX, y_1 = coordinates[offset_1 + 1] - originY, z_1 = coordinates[offset_1 + 2] - originZ;
                    double x_2 = coordinates[offset_2] - originX, y_2 = coordinates[offset_2 + 1] - originY, z_2 = coordinates[offset_2 + 2] - originZ;

                    double s_1 = (normalX * x_1) + (normalY * y_1) + (normalZ * z_1);
                    double s_2 = (normalX * x_2) + (normalY * y_2) + (normalZ * z_2);

                    double upstream_1 = -sign * s_1;
                    double upstream_2 = -sign * s_2;

                    bool inside_1 = upstream_1 >= -tolerance;
                    bool inside_2 = upstream_2 >= -tolerance;

                    if (inside_1)
                    {
                        xs[count] = x_1;
                        ys[count] = y_1;
                        zs[count] = z_1;
                        ss[count] = s_1;
                        count++;
                    }

                    if (inside_1 != inside_2 && Math.Abs(upstream_1 - upstream_2) > tolerance)
                    {
                        double factor = upstream_1 / (upstream_1 - upstream_2);
                        if (factor > 0 && factor < 1)
                        {
                            xs[count] = x_1 + (factor * (x_2 - x_1));
                            ys[count] = y_1 + (factor * (y_2 - y_1));
                            zs[count] = z_1 + (factor * (z_2 - z_1));
                            ss[count] = 0;
                            count++;
                        }
                    }
                }

                if (count < 3)
                {
                    continue;
                }

                // Project along the direction onto the plane and express in plane coordinates.
                Point2D[] point2Ds = new Point2D[count];
                double area = 0;
                bool inRange_X_Min = false, inRange_X_Max = false, inRange_Y_Min = false, inRange_Y_Max = false;
                for (int k = 0; k < count; k++)
                {
                    double factor = -ss[k] / dotProduct;

                    double x = xs[k] + (factor * vectorX);
                    double y = ys[k] + (factor * vectorY);
                    double z = zs[k] + (factor * vectorZ);

                    double u = (axisXX * x) + (axisXY * y) + (axisXZ * z);
                    double v = (axisYX * x) + (axisYY * y) + (axisYZ * z);

                    point2Ds[k] = new Point2D(u, v);

                    inRange_X_Min |= u >= minX;
                    inRange_X_Max |= u <= maxX;
                    inRange_Y_Min |= v >= minY;
                    inRange_Y_Max |= v <= maxY;
                }

                if (!inRange_X_Min || !inRange_X_Max || !inRange_Y_Min || !inRange_Y_Max)
                {
                    continue;
                }

                for (int k = 0; k < count; k++)
                {
                    Point2D point2D_1 = point2Ds[k];
                    Point2D point2D_2 = point2Ds[(k + 1) % count];
                    area += (point2D_1.X * point2D_2.Y) - (point2D_2.X * point2D_1.Y);
                }

                if (Math.Abs(area / 2) <= tolerance * tolerance)
                {
                    continue;
                }

                if (Geometry.Planar.Create.PolygonalFace2D(point2Ds) is PolygonalFace2D polygonalFace2D_Shadow)
                {
                    result.Add(polygonalFace2D_Shadow);
                }
            }

            return result;
        }
    }
}
