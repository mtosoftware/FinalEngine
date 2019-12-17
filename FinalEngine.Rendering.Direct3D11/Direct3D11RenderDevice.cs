using System;
using FinalEngine.Rendering.Direct3D11.Invokers;

namespace FinalEngine.Rendering.Direct3D11
{
    public sealed class Direct3D11RenderDevice : IRenderDevice
    {
        private readonly ID3D11DeviceContextInvoker deviceContext;

        public Direct3D11RenderDevice(ID3D11DeviceInvoker device, ID3D11DeviceContextInvoker deviceContext, IDXGISwapChainInvoker swapChain)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), $"The specified { nameof(device) } parameter is null.");
            }

            if (swapChain == null)
            {
                throw new ArgumentNullException(nameof(swapChain), $"The specified { nameof(swapChain) } parameter is null.");
            }

            this.deviceContext = deviceContext ?? throw new ArgumentNullException(nameof(deviceContext), $"The specified { nameof(deviceContext) } parameter is null.");

            InputAssembler = new Direct3D11InputAssembler(deviceContext);
            SwapChain = new Direct3D11SwapChain(swapChain);
            Factory = new Direct3D11GPUResourceFactory(device);
        }

        public IGPUResourceFactory Factory { get; }

        public IInputAssembler InputAssembler { get; }

        public ISwapChain SwapChain { get; }

        public void Clear(float r, float g, float b, float a)
        {
            throw new NotImplementedException();
        }

        public void DrawIndices(int first, int count)
        {
            deviceContext.DrawIndexed(count, first, 0);
        }
    }
}