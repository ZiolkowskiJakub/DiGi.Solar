using DiGi.Solar.ComputeSharp.Enums;

namespace DiGi.Solar.ComputeSharp
{
    public static partial class Create
    {
        /// <summary>
        /// Gets the ComputeSharp graphics device matching the specified <see cref="ComputeDeviceType"/>.
        /// <para>Only a device passing <see cref="Query.IsSupported(global::ComputeSharp.GraphicsDevice?)"/> is returned: hardware-accelerated and supporting double precision, which the shading shaders require.
        /// The WARP software device is never returned (ZiolkowskiJakub/DiGi.Solar#10), neither for the obsolete <c>ComputeDeviceType.Software</c> nor as the ComputeSharp default device on a machine without a hardware adapter.</para>
        /// <para>The returned device is shared by ComputeSharp and must not be disposed by the caller.</para>
        /// </summary>
        /// <param name="computeDeviceType">The type of device to get.</param>
        /// <returns>
        /// The ComputeSharp default device (<see cref="ComputeDeviceType.Default"/>) or the first supported hardware-accelerated device (<see cref="ComputeDeviceType.Hardware"/>);
        /// <see langword="null"/> when no such device can be created or it is not supported, and always for <c>ComputeDeviceType.Software</c>.
        /// </returns>
        public static global::ComputeSharp.GraphicsDevice? GraphicsDevice(this ComputeDeviceType computeDeviceType)
        {
            global::ComputeSharp.GraphicsDevice? result;
            try
            {
                switch (computeDeviceType)
                {
                    case ComputeDeviceType.Default:
                        result = global::ComputeSharp.GraphicsDevice.GetDefault();
                        break;

                    case ComputeDeviceType.Hardware:
                        result = global::ComputeSharp.GraphicsDevice.QueryDevices(x => x.IsHardwareAccelerated).FirstOrDefault(x => x.IsSupported());
                        break;

                    default:
                        return null;
                }
            }
            catch (NotSupportedException)
            {
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }

            if (!result.IsSupported())
            {
                return null;
            }

            return result;
        }
    }
}
