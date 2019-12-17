namespace FinalEngine.Rendering.Direct3D11.Invokers
{
    using System;
    using Vortice.DXGI;

    public sealed class D3D11SwapChainInvoker : IDXGISwapChainInvoker
    {
        private readonly IDXGISwapChain swapChain;

        public D3D11SwapChainInvoker(IDXGISwapChain swapChain)
        {
            this.swapChain = swapChain ?? throw new ArgumentNullException(nameof(swapChain), $"The specified { nameof(swapChain) } parameter is null.");
        }

        public void Present(int syncInterval, PresentFlags flags)
        {
            swapChain.Present(syncInterval, flags);
        }
    }
}