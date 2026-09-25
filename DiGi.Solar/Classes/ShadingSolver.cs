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
        /// The clip and projection are <see cref="Query.ProjectedShadowFaces(Plane?, BoundingBox2D?, double[], int[], int, Vector3D?, double)"/>, shared with <see cref="Create.ViewFactorResults(ShadingModel?, IDictionary{string, Vector3D}?, double, int, int)"/>;
        /// the merge, clip and fallback are <see cref="Query.ShadedFaces(PolygonalFace2D?, IEnumerable{PolygonalFace2D}?)"/>, shared with the ComputeSharp solver.</para>
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

                BoundingBox2D? boundingBox2D = polygonalFace2D.GetBoundingBox();
                if (boundingBox2D is null)
                {
                    return;
                }

                List<IShadingSolverResult> shadingSolverResults = [];

                foreach (Tuple<Vector3D, List<DateTime>> tuple_DateTime in tuples_DateTime)
                {
                    List<PolygonalFace2D> polygonalFace2Ds_Shadow = Query.ProjectedShadowFaces(plane, boundingBox2D, coordinates_Triangle, indexes_Triangle, i, tuple_DateTime.Item1, tolerance);

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
