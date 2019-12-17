namespace FinalEngine.Rendering.Direct3D.Invokers
{
    using System;
    using SharpGen.Runtime;
    using Vortice.Direct3D11;

    public interface ID3D11DeviceInvoker
    {
        ID3D11PixelShader CreatePixelShader(IntPtr shaderBytecodeRef, PointerSize bytecodeLength);

        ID3D11RasterizerState CreateRasterizerState(RasterizerDescription description);

        ID3D11VertexShader CreateVertexShader(IntPtr shaderBytecodeRef, PointerSize bytecodeLength);
    }
}