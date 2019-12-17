namespace FinalEngine.Rendering
{
    public interface IRenderDevice : IRenderContext
    {
        IGPUResourceFactory Factory { get; }

        IInputAssembler InputAssembler { get; }

        ISwapChain SwapChain { get; }

        void Clear(float r, float g, float b, float a);

        void DrawIndices(int first, int count);
    }
}