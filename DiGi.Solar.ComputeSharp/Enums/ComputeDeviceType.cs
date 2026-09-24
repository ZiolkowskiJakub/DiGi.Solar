using System.ComponentModel;

namespace DiGi.Solar.ComputeSharp.Enums
{
    /// <summary>
    /// Specifies the compute device the ComputeSharp shading solver runs on.
    /// </summary>
    public enum ComputeDeviceType
    {
        /// <summary>
        /// The default ComputeSharp device: the first adapter in performance order, with the WARP software device last.
        /// </summary>
        [Description("Default")] Default,

        /// <summary>
        /// The first hardware-accelerated device (GPU) that supports double precision.
        /// </summary>
        [Description("Hardware")] Hardware,

        /// <summary>
        /// The WARP software device, which runs the shaders on the CPU.
        /// </summary>
        [Description("Software")] Software,
    }
}
