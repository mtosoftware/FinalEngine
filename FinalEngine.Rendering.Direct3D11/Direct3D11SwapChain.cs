namespace FinalEngine.Rendering.Direct3D11
{
    using System;
    using FinalEngine.Rendering.Direct3D11.Invokers;
    using Vortice.DXGI;

    public sealed class Direct3D11SwapChain : ISwapChain
    {
        private readonly IDXGISwapChainInvoker swapChain;

        public Direct3D11SwapChain(IDXGISwapChainInvoker swapChain)
        {
            this.swapChain = swapChain ?? throw new ArgumentNullException(nameof(swapChain), $"The specified { nameof(swapChain) } parameter is null.");
        }

        public int SyncInterval { get; set; }

        public void Present()
        {
            swapChain.Present(SyncInterval, PresentFlags.None);
        }
    }
}