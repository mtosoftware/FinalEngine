namespace FinalEngine.Rendering.Direct3D
{
    using System;
    using Vortice.Direct3D11;
    using Vortice.Mathematics;

    public sealed class DX11DeviceInvoker : IDXDeviceInvoker
    {
        private readonly ID3D11Device device;

        private readonly ID3D11DeviceContext deviceContext;

        public DX11DeviceInvoker(ID3D11Device device, ID3D11DeviceContext deviceContext)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device), $"The specified { nameof(device) } parameter is null.");
            this.deviceContext = deviceContext ?? throw new ArgumentNullException(nameof(deviceContext), $"The specified { nameof(deviceContext) } parameter is null.");
        }

        public void SetRasterState(RasterStateDescription description)
        {
            ID3D11RasterizerState state = device.CreateRasterizerState(new RasterizerDescription()
            {
                ScissorEnable = description.ScissorEnabled,
                CullMode = description.CullEnabled ? CullMode.None : description.CullFaceType == CullFaceType.Front ? CullMode.Front : CullMode.Back,
                FrontCounterClockwise = description.WindingDirection == WindingDirection.CounterClockwise,
                FillMode = description.FillMode == RasterMode.Fill ? FillMode.Solid : FillMode.Wireframe
            });

            deviceContext.RSSetState(state);
        }

        public void SetScissor(int x, int y, int width, int height)
        {
            deviceContext.RSSetScissorRect(new Rect(x, y, width, height));
        }

        public void SetViewport(int x, int y, int width, int height)
        {
            deviceContext.RSSetViewport(x, y, width, height);
        }
    }
}