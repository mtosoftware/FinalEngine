namespace FinalEngine.Rendering.Direct3D11.Invokers
{
    using System;
    using Vortice.Direct3D;
    using Vortice.Direct3D11;
    using Vortice.DXGI;

    public sealed class D3D11DeviceContextInvoker : ID3D11DeviceContextInvoker
    {
        private readonly ID3D11DeviceContext deviceContext;

        public D3D11DeviceContextInvoker(ID3D11DeviceContext deviceContext)
        {
            this.deviceContext = deviceContext ?? throw new ArgumentNullException(nameof(deviceContext), $"The specified { nameof(deviceContext) } parameter is null.");
        }

        public void IASetIndexBuffer(ID3D11Buffer indexBuffer, Format format, int offset)
        {
            deviceContext.IASetIndexBuffer(indexBuffer, format, offset);
        }

        public void IASetPrimitiveTopology(PrimitiveTopology topology)
        {
            deviceContext.IASetPrimitiveTopology(topology);
        }

        public void IASetVertexBuffer(ID3D11Buffer vertexBuffer, int stride, int offset)
        {
            deviceContext.IASetVertexBuffers(0, 1, new ID3D11Buffer[] { vertexBuffer }, new int[] { stride }, new int[] { offset });
        }
    }
}