namespace FinalEngine.Rendering
{
    using FinalEngine.Rendering.Buffers;

    public interface IInputAssembler
    {
        void SetBuffer(IBuffer buffer);

        void SetPrimitiveTopology(PrimitiveTopology topology);
    }
}