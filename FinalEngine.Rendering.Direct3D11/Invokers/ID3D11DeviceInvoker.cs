using System;
using Vortice.Direct3D11;

namespace FinalEngine.Rendering.Direct3D11.Invokers
{
    public interface ID3D11DeviceInvoker
    {
        ID3D11Buffer CreateBuffer(BufferDescription description, IntPtr data);
    }
}