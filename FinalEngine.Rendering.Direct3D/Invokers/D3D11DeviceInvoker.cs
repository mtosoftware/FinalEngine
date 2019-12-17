namespace FinalEngine.Rendering.Direct3D.Invokers
{
    using System;
    using SharpGen.Runtime;
    using Vortice.Direct3D11;

    public sealed class D3D11DeviceInvoker : ID3D11DeviceInvoker
    {
        private readonly ID3D11Device device;

        public D3D11DeviceInvoker(ID3D11Device device)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device), $"The specified { nameof(device) } parameter is null.");
        }

        public ID3D11PixelShader CreatePixelShader(IntPtr shaderBytecodeRef, PointerSize bytecodeLength)
        {
            return device.CreatePixelShader(shaderBytecodeRef, bytecodeLength);
        }

        public ID3D11RasterizerState CreateRasterizerState(RasterizerDescription description)
        {
            return device.CreateRasterizerState(description);
        }

        public ID3D11VertexShader CreateVertexShader(IntPtr shaderBytecodeRef, PointerSize bytecodeLength)
        {
            return device.CreateVertexShader(shaderBytecodeRef, bytecodeLength);
        }
    }
}