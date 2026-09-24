using System.ComponentModel;

namespace DiGi.Solar.ComputeSharp.Enums
{
    /// <summary>
    /// Specifies the compute device the ComputeSharp shading solver runs on.
    /// </summary>
    public enum ComputeDeviceType
    {
        /// <summary>
        /// The default ComputeSharp device (the first adapter in performance order) when it is hardware-accelerated and supports double precision.
        /// <para>Never the WARP software device: on a machine without a hardware adapter no device is selected and the solver falls back to the CPU <see cref="Solar.Classes.ShadingSolver"/>.</para>
        /// </summary>
        [Description("Default")] Default,

        /// <summary>
        /// The first hardware-accelerated device (GPU) that supports double precision.
        /// </summary>
        [Description("Hardware")] Hardware,

        /// <summary>
        /// The WARP software device. Withdrawn: no device is selected for it, so a solve requesting it returns false.
        /// <para>WARP loses shadows cast between buildings (13 to 28 % of the sun-facing samples of the 20 to 720 surface benchmark grids) and needs about 16 minutes to create the shading pipeline
        /// (ZiolkowskiJakub/DiGi.Solar#10). The CPU <see cref="Solar.Classes.ShadingSolver"/> is the software path.</para>
        /// </summary>
        [Obsolete("WARP loses shadows cast between buildings and needs about 16 minutes to create its pipeline, so no device is selected for it; use the CPU DiGi.Solar.Classes.ShadingSolver instead. See ZiolkowskiJakub/DiGi.Solar#10.")]
        [Description("Software")] Software,
    }
}
