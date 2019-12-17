namespace FinalEngine.Rendering.Direct3D11.Invokers
{
    using Vortice.Direct3D;
    using Vortice.Direct3D11;
    using Vortice.DXGI;

    public interface ID3D11DeviceContextInvoker
    {
        void IASetIndexBuffer(ID3D11Buffer indexBuffer, Format format, int offset);

        void IASetPrimitiveTopology(PrimitiveTopology topology);

        void IASetVertexBuffer(ID3D11Buffer vertexBuffer, int stride, int offset);
    }
}