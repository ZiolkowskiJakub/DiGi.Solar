using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.Solar.Classes;
using DiGi.Solar.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiGi.Solar
{
    public static partial class Create
    {
        /// <summary>
        /// Calculates, for every receiver of a shading model, how much of its sky and of its ground it can see past the other elements (receivers and shading-only casters).
        /// <para>Each hemisphere is split into <paramref name="altitudeCount"/> equal altitude bands times <paramref name="azimuthCount"/> azimuth sectors, and every patch is sampled at its centre direction <c>p</c>. A patch in front of the receiver (<c>cos θ = n · p &gt; 0</c> for the outward normal <c>n</c>) is weighted by <c>cos θ · ΔΩ</c>, the weight an isotropic sky or ground gives it. Its blocked fraction is the shaded share of the receiver lit along <c>-p</c>: <see cref="Query.ProjectedShadowFaces(Plane?, Geometry.Planar.Classes.BoundingBox2D?, double[], int[], int, Vector3D?, double)"/> merged and clipped by <see cref="Query.ShadedFaces(PolygonalFace2D?, IEnumerable{PolygonalFace2D}?)"/>, the same projection the <see cref="ShadingSolver"/> uses for the sun (EnergyPlus computes its isotropic diffuse shading ratio the same way).
        /// The visibility is the weighted unblocked share, <c>Σ w (1 - f) / Σ w</c>, and is exactly 1 when nothing blocks the view, so an open surface keeps the open-sky irradiance bit for bit. A hemisphere with no patch in front of the receiver (the ground of a flat roof) has visibility 1.</para>
        /// <para>Ground patches use every caster, floor slabs included. A ground ray reaches a floor slab of a closed building only by passing its walls or roof, or by starting on its boundary: a wall touching a neighbour sees the neighbour's floor, not the ground. The ground itself is not modelled, so nothing below the lowest caster blocks.</para>
        /// <para>A blocked patch contributes nothing: light reflected by the facades and roofs that block the view is ignored. That is exact for a wall touching a neighbour, but underestimates surfaces in narrow street canyons and courtyards (ZiolkowskiJakub/DiGi.Solar#15).</para>
        /// </summary>
        /// <param name="shadingModel">The shading model. This value can be null.</param>
        /// <param name="normals">The outward unit normal of each receiver, keyed by its reference. A receiver missing from it, or null, uses its plane normal, which is the outward one only if the face is stored that way.</param>
        /// <param name="tolerance">The distance tolerance.</param>
        /// <param name="altitudeCount">The number of altitude bands per hemisphere.</param>
        /// <param name="azimuthCount">The number of azimuth sectors per altitude band.</param>
        /// <returns>One result per receiver that has a plane, a face with area and a triangulation, or <see langword="null"/> when <paramref name="shadingModel"/> is null or a patch count is below 1.</returns>
        public static List<ViewFactorResult>? ViewFactorResults(this ShadingModel? shadingModel, IDictionary<string, Vector3D>? normals, double tolerance, int altitudeCount = 6, int azimuthCount = 24)
        {
            if (shadingModel is null || altitudeCount < 1 || azimuthCount < 1)
            {
                return null;
            }

            List<ViewFactorResult> result = [];

            List<IShadingElement>? shadingElements_All = shadingModel.GetShadingElements<IShadingElement>();
            if (shadingElements_All is null || shadingElements_All.Count == 0)
            {
                return result;
            }

            List<IShadingElement> shadingElements = [];
            List<Plane?> planes = [];
            List<PolygonalFace2D?> polygonalFace2Ds = [];

            // Caster triangles as in ShadingSolver.Solve: 9 coordinates each, with the index of the receiver
            // they belong to (-1 for shading-only), so a receiver never blocks its own view.
            List<double> coordinates = [];
            List<int> indexes_ShadingElement = [];

            foreach (IShadingElement shadingElement in shadingElements_All)
            {
                if (shadingElement is null)
                {
                    continue;
                }

                IPolygonalFace3D? polygonalFace3D = shadingElement.PolygonalFace3D;

                List<Triangle3D>? triangle3Ds = polygonalFace3D?.Triangulate(tolerance);
                if (triangle3Ds is null || triangle3Ds.Count == 0)
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
                    if (point3Ds is null || point3Ds.Count != 3)
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
                return result;
            }

            double[] coordinates_Triangle = [.. coordinates];
            int[] indexes_Triangle = [.. indexes_ShadingElement];

            // Patch centres of the upper hemisphere (x, y, z per patch) and their solid angles; the ground
            // patch is the same patch mirrored below the horizon (z negated).
            int count_Patch = altitudeCount * azimuthCount;
            double[] directions = new double[count_Patch * 3];
            double[] solidAngles = new double[count_Patch];
            double altitudeStep = Math.PI / 2 / altitudeCount;
            double azimuthStep = 2 * Math.PI / azimuthCount;
            for (int i = 0; i < altitudeCount; i++)
            {
                double altitude_1 = i * altitudeStep;
                double altitude_2 = (i + 1) * altitudeStep;
                double altitude = (altitude_1 + altitude_2) / 2;
                double solidAngle = azimuthStep * (Math.Sin(altitude_2) - Math.Sin(altitude_1));

                for (int j = 0; j < azimuthCount; j++)
                {
                    double azimuth = (j + 0.5) * azimuthStep;

                    int index = (i * azimuthCount) + j;
                    directions[index * 3] = Math.Cos(altitude) * Math.Cos(azimuth);
                    directions[(index * 3) + 1] = Math.Cos(altitude) * Math.Sin(azimuth);
                    directions[(index * 3) + 2] = Math.Sin(altitude);
                    solidAngles[index] = solidAngle;
                }
            }

            ViewFactorResult?[] viewFactorResults = new ViewFactorResult?[count_ShadingElement];

            Parallel.For(0, count_ShadingElement, Core.Create.ParallelOptions(), i =>
            {
                IShadingElement shadingElement = shadingElements[i];
                Plane? plane = planes[i];
                PolygonalFace2D? polygonalFace2D = polygonalFace2Ds[i];
                if (plane is null || polygonalFace2D is null)
                {
                    return;
                }

                BoundingBox2D? boundingBox2D = polygonalFace2D.GetBoundingBox();
                double area = polygonalFace2D.GetArea();
                if (boundingBox2D is null || double.IsNaN(area) || double.IsInfinity(area) || area <= 0)
                {
                    return;
                }

                string? reference = shadingElement.Reference;

                Vector3D? normal = null;
                if (reference is not null && normals is not null && normals.TryGetValue(reference, out Vector3D? normal_Outward) && normal_Outward is not null && normal_Outward.Length > 0)
                {
                    normal = normal_Outward.Unit;
                }

                normal ??= plane.Normal;
                if (normal is null)
                {
                    return;
                }

                double normalX = normal.X, normalY = normal.Y, normalZ = normal.Z;

                double Visibility(double sign)
                {
                    double weight_Total = 0;
                    double weight_Open = 0;
                    for (int j = 0; j < count_Patch; j++)
                    {
                        double x = directions[j * 3], y = directions[(j * 3) + 1], z = sign * directions[(j * 3) + 2];

                        double cosine = (normalX * x) + (normalY * y) + (normalZ * z);
                        if (cosine <= 0)
                        {
                            continue;
                        }

                        double weight = cosine * solidAngles[j];

                        // Light from the patch travels towards the receiver, opposite to the patch direction.
                        List<PolygonalFace2D> polygonalFace2Ds_Shadow = Query.ProjectedShadowFaces(plane, boundingBox2D, coordinates_Triangle, indexes_Triangle, i, new Vector3D(-x, -y, -z), tolerance);

                        double fraction = 0;
                        if (polygonalFace2Ds_Shadow.Count != 0)
                        {
                            double area_Shaded = 0;
                            List<PolygonalFace2D>? polygonalFace2Ds_Shaded = Query.ShadedFaces(polygonalFace2D, polygonalFace2Ds_Shadow);
                            if (polygonalFace2Ds_Shaded is not null)
                            {
                                foreach (PolygonalFace2D polygonalFace2D_Shaded in polygonalFace2Ds_Shaded)
                                {
                                    area_Shaded += polygonalFace2D_Shaded.GetArea();
                                }
                            }

                            fraction = double.IsNaN(area_Shaded) || double.IsInfinity(area_Shaded) ? 0 : Math.Max(0, Math.Min(1, area_Shaded / area));
                        }

                        weight_Total += weight;

                        // A blocked patch contributes 0: reflections off the blocking geometry are ignored (DiGi.Solar#15).
                        weight_Open += weight * (1 - fraction);
                    }

                    return weight_Total > 0 ? weight_Open / weight_Total : 1;
                }

                double skyVisibility = Visibility(1);
                double groundVisibility = Visibility(-1);

                viewFactorResults[i] = new ViewFactorResult(reference, skyVisibility, groundVisibility);
            });

            foreach (ViewFactorResult? viewFactorResult in viewFactorResults)
            {
                if (viewFactorResult is not null)
                {
                    result.Add(viewFactorResult);
                }
            }

            return result;
        }
    }
}
