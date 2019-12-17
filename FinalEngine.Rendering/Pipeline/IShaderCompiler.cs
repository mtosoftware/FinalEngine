namespace FinalEngine.Rendering.Pipeline
{
    public interface IShaderCompiler
    {
        IShader CompileShaderFromSource(PipelineTarget target, string sourceCode);
    }
}