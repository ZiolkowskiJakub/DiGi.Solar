using DiGi.Solar.ComputeSharp.Enums;

namespace DiGi.Solar.ComputeSharp
{
    public static partial class Create
    {
        /// <summary>
        /// Gets the ComputeSharp graphics device matching the specified <see cref="ComputeDeviceType"/>.
        /// <para>The returned device is shared by ComputeSharp and must not be disposed by the caller.</para>
        /// </summary>
        /// <param name="computeDeviceType">The type of device to get.</param>
        /// <returns>
        /// The default device (<see cref="ComputeDeviceType.Default"/>), the first hardware-accelerated device (<see cref="ComputeDeviceType.Hardware"/>)
        /// or the WARP software device (<see cref="ComputeDeviceType.Software"/>); <see langword="null"/> when no such device can be created
        /// or it does not support double precision, which the shading shaders require.
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
                        result = global::ComputeSharp.GraphicsDevice.QueryDevices(x => x.IsHardwareAccelerated).FirstOrDefault(x => x.IsDoublePrecisionSupportAvailable());
                        break;

                    case ComputeDeviceType.Software:
                        result = global::ComputeSharp.GraphicsDevice.QueryDevices(x => !x.IsHardwareAccelerated).FirstOrDefault();
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

            if (result == null || !result.IsDoublePrecisionSupportAvailable())
            {
                return null;
            }

            return result;
        }
    }
}
