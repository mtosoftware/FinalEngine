namespace FinalEngine.Rendering.Direct3D.Pipeline
{
    using System;
    using FinalEngine.Logging;
    using FinalEngine.Rendering.Direct3D.Invokers;
    using FinalEngine.Rendering.Pipeline;
    using Vortice.Direct3D;

    public sealed class Direct3DShaderCompiler : IShaderCompiler
    {
        private const string ShaderProfileVersion = "_5_0";

        private readonly ID3D11CompilerInvoker compiler;

        private readonly ID3D11DeviceInvoker device;

        public Direct3DShaderCompiler(ID3D11DeviceInvoker device, ID3D11CompilerInvoker compiler)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device), $"The specified { nameof(device) } parameter is null.");
            this.compiler = compiler ?? throw new ArgumentNullException(nameof(compiler), $"The specified { nameof(compiler) } parameter is null.");
        }

        public IShader CompileShaderFromSource(PipelineTarget target, string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
            {
                throw new ArgumentNullException(nameof(sourceCode), $"The specified { nameof(sourceCode) } parameter is null.");
            }

            switch (target)
            {
                case PipelineTarget.Vertex:
                    return new Direct3DVertexShader(device, CreateShaderBlob(sourceCode, "VShader", "vs"));

                case PipelineTarget.Fragment:
                    return new Direct3DFragmentShader(device, CreateShaderBlob(sourceCode, "PShader", "ps"));

                default:
                    throw new NotSupportedException($"The specified { nameof(target) } is not supported.");
            }
        }

        private Blob CreateShaderBlob(string sourceCode, string entryPoint, string profileSuffix)
        {
            if (!compiler.Compile(sourceCode, entryPoint, null, profileSuffix + ShaderProfileVersion, out Blob blob, out Blob error))
            {
                Logger.Instance.Log(LogType.Error, $"[D3D11 SHADER] { error.ConvertToString() }");
            }

            return blob;
        }
    }
}