namespace FinalEngine.Rendering.Direct3D11
{
    using System;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.Direct3D11.Buffers;
    using Vortice.Direct3D11;
    using Vortice.DXGI;

    public sealed class Direct3D11InputAssembler : IInputAssembler
    {
        private readonly ID3D11DeviceContext deviceContext;

        public Direct3D11InputAssembler(ID3D11DeviceContext deviceContext)
        {
            this.deviceContext = deviceContext ?? throw new ArgumentNullException(nameof(deviceContext), $"The specified { nameof(deviceContext) } parameter is null.");
        }

        public void SetBuffer(IBuffer buffer)
        {
            Direct3D11Buffer dxBuffer = buffer.Cast<Direct3D11Buffer>();
            ID3D11Buffer resource = dxBuffer.Resource;
            int stride = dxBuffer.StrideReference;

            switch (buffer.Type)
            {
                case BufferType.VertexBuffer:
                    deviceContext.IASetVertexBuffers(0, 1, new ID3D11Buffer[] { resource }, new int[] { stride }, new int[] { 0 });
                    break;

                case BufferType.IndexBuffer:
                    deviceContext.IASetIndexBuffer(resource, Format.R32_UInt, 0);
                    break;
            }
        }

        public void SetPrimitiveTopology(PrimitiveTopology topology)
        {
            deviceContext.IASetPrimitiveTopology(topology.ToDirect3D());
        }
    }
}