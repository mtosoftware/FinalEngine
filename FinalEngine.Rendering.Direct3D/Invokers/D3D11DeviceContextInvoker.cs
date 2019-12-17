namespace FinalEngine.Rendering.Direct3D.Invokers
{
    using System;
    using Vortice.Direct3D11;
    using Vortice.Mathematics;

    public sealed class D3D11DeviceContextInvoker : ID3D11DeviceContextInvoker
    {
        private readonly ID3D11DeviceContext deviceContext;

        public D3D11DeviceContextInvoker(ID3D11DeviceContext deviceContext)
        {
            this.deviceContext = deviceContext ?? throw new ArgumentNullException(nameof(deviceContext), $"The specified { nameof(deviceContext) } parameter is null.");
        }

        public void RSSetScissor(int x, int y, int width, int height)
        {
            deviceContext.RSSetScissorRect(new Rect(x, y, width, height));
        }

        public void RSSetState(ID3D11RasterizerState state)
        {
            deviceContext.RSSetState(state);
        }

        public void RSSetViewport(int x, int y, int width, int height)
        {
            deviceContext.RSSetViewport(x, y, width, height);
        }
    }
}