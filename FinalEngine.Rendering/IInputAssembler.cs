namespace FinalEngine.Rendering
{
    using FinalEngine.Rendering.Buffers;

    public interface IInputAssembler
    {
        void DrawIndices(int first, int count);

        void SetBuffer(IBuffer buffer);

        void SetPrimitiveTopology(PrimitiveTopology topology);
    }
}