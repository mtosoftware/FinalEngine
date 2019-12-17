namespace FinalEngine.Rendering.Direct3D.Invokers
{
    using Vortice.Direct3D;

    public interface ID3D11CompilerInvoker
    {
        bool Compile(string shaderSource, string entryPoint, string sourceName, string profile, out Blob blob, out Blob errorBlob);
    }
}