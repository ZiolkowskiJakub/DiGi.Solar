namespace DiGi.Solar.ComputeSharp
{
    public static partial class Query
    {
        /// <summary>
        /// Determines whether the ComputeSharp shading solver can run on the specified graphics device: the device must be hardware-accelerated and support double precision.
        /// <para>The WARP software device is rejected even though it reports double precision as available: it loses shadows cast between buildings and needs about 16 minutes to create the shading pipeline
        /// (ZiolkowskiJakub/DiGi.Solar#10). The CPU <see cref="Solar.Classes.ShadingSolver"/> is the software path.</para>
        /// </summary>
        /// <param name="graphicsDevice">The graphics device to check.</param>
        /// <returns>True if the device is hardware-accelerated and supports double precision; otherwise, false (including for a null device).</returns>
        public static bool IsSupported(this global::ComputeSharp.GraphicsDevice? graphicsDevice)
        {
            if (graphicsDevice == null)
            {
                return false;
            }

            return graphicsDevice.IsHardwareAccelerated && graphicsDevice.IsDoublePrecisionSupportAvailable();
        }
    }
}
