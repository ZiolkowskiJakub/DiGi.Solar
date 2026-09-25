using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.Solar.Enums;
using DiGi.Solar.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiGi.Solar.Classes
{
    /// <summary>
    /// Provides a solver that calculates the shading of shading elements on the CPU, with no GPU or ComputeSharp dependency.
    /// <para>Shading-only elements cast shadows on the receivers but are never shaded themselves and receive no results.</para>
    /// </summary>
    public class ShadingSolver : IShadingObject, ISolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolver"/> class with the specified shading model and options.
        /// </summary>
        /// <param name="shadingModel">The shading model to be used for calculations.</param>
        /// <param name="shadingSolverOptions">The options that configure the solver's behavior.</param>
        public ShadingSolver(ShadingModel? shadingModel, ShadingSolverOptions? shadingSolverOptions)
        {
            ShadingModel = shadingModel;
            ShadingSolverOptions = shadingSolverOptions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadingSolver"/> class with the specified shading model and a collection of date-times.
        /// </summary>
        /// <param name="shadingModel">The shading model to be used for calculations.</param>
        /// <param name="dateTimes">An array of date-time values for which shading should be calculated.</param>
        public ShadingSolver(ShadingModel? shadingModel, DateTime[]? dateTimes)
        {
            ShadingModel = shadingModel;
            ShadingSolverOptions = new ShadingSolverOptions();
            if (dateTimes != null)
            {
                ShadingSolverOptions.TimeSeries = new DateTimeCollection(dateTimes);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ShadingModel"/> associated with this solver.
        /// </summary>
        public ShadingModel? ShadingModel { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ShadingSolverOptions"/> that define the parameters for the solving process.
        /// </summary>
        public ShadingSolverOptions? ShadingSolverOptions { get; set; }

        /// <summary>
        /// Executes the shading calculation on the CPU and assigns the results to the receivers of the <see cref="ShadingModel"/>.
        /// <para>For every receiver and sun direction, each triangle of the other elements (receivers and shading-only casters) is clipped to the part lying between the sun and the receiver plane,
        /// projected onto that plane along the sun direction, merged with the other shadows and clipped to the receiver face.</para>
        /// <para>Every receiver receives one result per daytime timestamp, including fully sunlit ones (shaded area 0).</para>
        /// <para>If the merge of one receiver's shadows fails, the unmerged shadows are clipped to the receiver and used instead, capped at its area: that sample's shaded area is then overstated at worst, but it never exceeds the receiver and never reads as full sun.
        /// The merge, clip and fallback are <see cref="Query.ShadedFaces(PolygonalFace2D?, IEnumerable{PolygonalFace2D}?)"/>, shared with the ComputeSharp solver.</para>
        /// <para>Only the part of a caster on the sun side of the receiver plane casts a shadow, so a receiver with the sun behind it is shaded by whatever lies in front of its plane: a wall of a closed building then reads fully shaded.
        /// The ComputeSharp solver computes the same clipped and projected shadows on the GPU, so the two solvers agree up to floating point round-off.</para>
        /// </summary>
        /// <returns>True if the solving operation completed successfully; otherwise, false.</returns>
        public virtual bool Solve()
        {
            if (ShadingSolverOptions == null || ShadingModel == null)
            {
                return false;
            }

            DateTime[]? dateTimes = ShadingSolverOptions.TimeSeries.GetDateTimes();
            if (dateTimes == null || dateTimes.Length == 0)
            {
                return true;
            }

            List<IShadingElement>? shadingElements_All = ShadingModel.GetShadingElements<IShadingElement>();
            if (shadingElements_All == null || shadingElements_All.Count == 0)
            {
                return true;
            }

            double tolerance = ShadingSolverOptions.Tolerance;

            List<IShadingElement> shadingElements = [];
            List<Plane?> planes = [];
            List<PolygonalFace2D?> polygonalFace2Ds = [];

            // Caster triangles of every element (receivers and shading-only), stored as 9 coordinates
            // each, with the index of the receiver they belong to (-1 for shading-only), so a receiver
            // never shades itself.
            List<double> coordinates = [];
            List<int> indexes_ShadingElement = [];

            foreach (IShadingElement shadingElement in shadingElements_All)
            {
                if (shadingElement == null)
                {
                    continue;
                }

                IPolygonalFace3D? polygonalFace3D = shadingElement.PolygonalFace3D;

                List<Triangle3D>? triangle3Ds = polygonalFace3D?.Triangulate(tolerance);
                if (triangle3Ds == null || triangle3Ds.Count == 0)
                {
                    continue;
                }

                int index = -1;
                if (!shadingElement.ShadingOnly)
                {
                    index = shadingElements.Count;
                    shadingElements.Add(shadingElement);
                    planes.Add(polygonalFace3D?.Plane);
                    polygonalFace2Ds.Add(polygonalFace3D?.Geometry2D as PolygonalFace2D);
                }

                foreach (Triangle3D triangle3D in triangle3Ds)
                {
                    List<Point3D>? point3Ds = triangle3D?.GetPoints();
                    if (point3Ds == null || point3Ds.Count != 3)
                    {
                        continue;
                    }

                    foreach (Point3D point3D in point3Ds)
                    {
                        coordinates.Add(point3D.X);
                        coordinates.Add(point3D.Y);
                        coordinates.Add(point3D.Z);
                    }

                    indexes_ShadingElement.Add(index);
                }
            }

            int count_ShadingElement = shadingElements.Count;
            if (count_ShadingElement == 0)
            {
                return true;
            }

            Dictionary<DateTime, Vector3D> dictionary = [];
            foreach (DateTime dateTime in dateTimes)
            {
                Vector3D? sunDirection = Query.SunDirection(ShadingModel, dateTime, false);
                if (sunDirection == null)
                {
                    continue;
                }

                dictionary[dateTime] = sunDirection;
            }

            List<Tuple<Vector3D, List<DateTime>>>? tuples_DateTime = Query.GroupDirections(dictionary, ShadingSolverOptions.AngleTolerance);
            if (tuples_DateTime == null || tuples_DateTime.Count == 0)
            {
                return true;
            }

            ShadingSolverType shadingSolverType = ShadingSolverOptions.ShadingSolverType;
            int count_Triangle = indexes_ShadingElement.Count;
            double[] coordinates_Triangle = [.. coordinates];
            int[] indexes_Triangle = [.. indexes_ShadingElement];

            List<IShadingSolverResult>?[] shadingSolverResultsArray = new List<IShadingSolverResult>?[count_ShadingElement];

            Parallel.For(0, count_ShadingElement, Core.Create.ParallelOptions(), i =>
            {
                Plane? plane = planes[i];
                PolygonalFace2D? polygonalFace2D = polygonalFace2Ds[i];
                if (plane == null || polygonalFace2D == null)
                {
                    return;
                }

                Vector3D? normal = plane.Normal;
                Vector3D? axisX = plane.AxisX;
                Vector3D? axisY = plane.AxisY;
                Point3D? origin = plane.Origin;
                BoundingBox2D? boundingBox2D = polygonalFace2D.GetBoundingBox();
                if (normal is null || axisX is null || axisY is null || origin is null || boundingBox2D is null)
                {
                    return;
                }

                double normalX = normal.X, normalY = normal.Y, normalZ = normal.Z;
                double axisXX = axisX.X, axisXY = axisX.Y, axisXZ = axisX.Z;
                double axisYX = axisY.X, axisYY = axisY.Y, axisYZ = axisY.Z;
                double originX = origin.X, originY = origin.Y, originZ = origin.Z;
                double minX = boundingBox2D.Min.X - tolerance, minY = boundingBox2D.Min.Y - tolerance;
                double maxX = boundingBox2D.Max.X + tolerance, maxY = boundingBox2D.Max.Y + tolerance;

                // Clipped polygon of one caster triangle: at most 4 points (a triangle cut by one plane).
                double[] xs = new double[4], ys = new double[4], zs = new double[4], ss = new double[4];

                List<IShadingSolverResult> shadingSolverResults = [];

                foreach (Tuple<Vector3D, List<DateTime>> tuple_DateTime in tuples_DateTime)
                {
                    Vector3D vector3D = tuple_DateTime.Item1;
                    double vectorX = vector3D.X, vectorY = vector3D.Y, vectorZ = vector3D.Z;

                    // Change of the signed plane distance per unit travelled along the sun direction; near zero the sun grazes the plane and casts no shadow on it.
                    double dotProduct = (normalX * vectorX) + (normalY * vectorY) + (normalZ * vectorZ);

                    List<PolygonalFace2D> polygonalFace2Ds_Shadow = [];
                    if (Math.Abs(dotProduct) > tolerance)
                    {
                        // Sign that turns a signed plane distance into "distance on the sun side" (positive = upstream).
                        double sign = dotProduct > 0 ? 1 : -1;

                        for (int j = 0; j < count_Triangle; j++)
                        {
                            if (indexes_Triangle[j] == i)
                            {
                                continue;
                            }

                            int offset = j * 9;

                            double upstream_Max = double.MinValue;
                            for (int k = 0; k < 3; k++)
                            {
                                double x = coordinates_Triangle[offset + (k * 3)] - originX;
                                double y = coordinates_Triangle[offset + (k * 3) + 1] - originY;
                                double z = coordinates_Triangle[offset + (k * 3) + 2] - originZ;

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

                                double x_1 = coordinates_Triangle[offset_1] - originX, y_1 = coordinates_Triangle[offset_1 + 1] - originY, z_1 = coordinates_Triangle[offset_1 + 2] - originZ;
                                double x_2 = coordinates_Triangle[offset_2] - originX, y_2 = coordinates_Triangle[offset_2 + 1] - originY, z_2 = coordinates_Triangle[offset_2 + 2] - originZ;

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

                            // Project along the sun direction onto the plane and express in plane coordinates.
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
                                polygonalFace2Ds_Shadow.Add(polygonalFace2D_Shadow);
                            }
                        }
                    }

                    // A receiver reached by no shadow is fully sunlit and still gets a result (shaded area 0). The receiver face is never null here.
                    List<PolygonalFace2D> polygonalFace2Ds_Result = Query.ShadedFaces(polygonalFace2D, polygonalFace2Ds_Shadow) ?? [];

                    foreach (DateTime dateTime in tuple_DateTime.Item2)
                    {
                        if (Create.ShadingSolverResult(shadingSolverType, dateTime, plane, polygonalFace2Ds_Result) is IShadingSolverResult shadingSolverResult)
                        {
                            shadingSolverResults.Add(shadingSolverResult);
                        }
                    }
                }

                shadingSolverResultsArray[i] = shadingSolverResults;
            });

            // Receivers without a plane or face keep null, so Assign returns false for them, as in the ComputeSharp solver.
            for (int i = 0; i < count_ShadingElement; i++)
            {
                ShadingModel.Assign(shadingElements[i], shadingSolverResultsArray[i]);
            }

            return true;
        }
    }
}
