namespace FinalEngine.Rendering.Direct3D.Invokers
{
    using Vortice.Direct3D11;

    public interface ID3D11DeviceContextInvoker
    {
        void RSSetScissor(int x, int y, int width, int height);

        void RSSetState(ID3D11RasterizerState state);

        void RSSetViewport(int x, int y, int width, int height);
    }
}