namespace FinalEngine.Rendering.Direct3D11.Invokers
{
    using Vortice.DXGI;

    public interface IDXGISwapChainInvoker
    {
        void Present(int syncInterval, PresentFlags flags);
    }
}