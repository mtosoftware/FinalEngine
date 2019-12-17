namespace FinalEngine.Rendering
{
    using FinalEngine.Rendering.Buffers;

    public interface IGPUResourceFactory
    {
        IBuffer CreateBuffer<T>(BufferType type, T[] data, int sizeInBytes, int structStride) where T : struct;
    }
}