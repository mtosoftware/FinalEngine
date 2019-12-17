namespace FinalEngine.Rendering.Direct3D.Invokers
{
    using System;
    using SharpGen.Runtime;
    using Vortice.Direct3D;

    public delegate Result CompileFunc(string shaderSource, string entryPoint, string sourceName, string profile, out Blob blob, out Blob errorBlob);

    public sealed class D3D11CompilerInvoker : ID3D11CompilerInvoker
    {
        private readonly CompileFunc compile;

        public D3D11CompilerInvoker(CompileFunc compile)
        {
            this.compile = compile ?? throw new ArgumentNullException(nameof(compile), $"The specified { nameof(compile) } parameter is null.");
        }

        public bool Compile(string shaderSource, string entryPoint, string sourceName, string profile, out Blob blob, out Blob errorBlob)
        {
            return compile(shaderSource, entryPoint, sourceName, profile, out blob, out errorBlob).Success;
        }
    }
}