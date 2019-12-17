namespace FinalEngine.Rendering.Direct3D
{
    using System;
    using Vortice.Direct3D11;

    public sealed class Direct3DRasterizer : IRasterizer
    {
        private readonly ID3D11DeviceInvoker device;

        private readonly ID3D11DeviceContextInvoker deviceContext;

        public Direct3DRasterizer(ID3D11DeviceInvoker device, ID3D11DeviceContextInvoker deviceContext)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device), $"The specifeid { nameof(device) } parameter is null.");
            this.deviceContext = deviceContext ?? throw new ArgumentNullException(nameof(deviceContext), $"The specifeid { nameof(deviceContext) } parameter is null.");
        }

        public void SetRasterState(RasterStateDescription description)
        {
            ID3D11RasterizerState state = device.CreateRasterizerState(new RasterizerDescription()
            {
                CullMode = !description.CullEnabled ? CullMode.None : description.CullFaceType == CullFaceType.Front ? CullMode.Front : CullMode.Back,
                FrontCounterClockwise = description.WindingDirection == WindingDirection.CounterClockwise,
                FillMode = description.FillMode == RasterMode.Fill ? FillMode.Solid : FillMode.Wireframe
            });

            deviceContext.RSSetState(state);
            state.Release();
        }

        public void SetScissor(int x, int y, int width, int height)
        {
            deviceContext.RSSetScissor(x, y, width, height);
        }

        public void SetViewport(int x, int y, int width, int height)
        {
            deviceContext.RSSetViewport(x, y, width, height);
        }
    }
}