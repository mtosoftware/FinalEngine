using System;

namespace FinalEngine.Rendering.Direct3D
{
    public sealed class Direct3DRasterizezr : IRasterizer
    {
        private readonly IDXDeviceInvoker device;

        public Direct3DRasterizezr(IDXDeviceInvoker device)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device), $"The specifeid { nameof(device) } parameter is null.");
        }

        public void SetRasterState(RasterStateDescription description)
        {
            device.SetRasterState(description);
        }

        public void SetScissor(int x, int y, int width, int height)
        {
            device.SetScissor(x, y, width, height);
        }

        public void SetViewport(int x, int y, int width, int height)
        {
            device.SetViewport(x, y, width, height);
        }
    }
}