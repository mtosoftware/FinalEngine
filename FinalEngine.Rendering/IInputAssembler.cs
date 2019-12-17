namespace FinalEngine.Rendering
{
    using FinalEngine.Rendering.Buffers;

    /// <summary>
    ///   Defines an interface that represents the input assembly stage of a rendering pipeline.
    /// </summary>
    public interface IInputAssembler
    {
        /// <summary>
        ///   Binds the specified <paramref name="buffer"/> to the rendering pipeline, preparing it for use with subsequent draw commands.
        /// </summary>
        /// <param name="buffer">
        ///   Specifies a <see cref="IBuffer"/> that represents the buffer to bind to the GPU.
        /// </param>
        void SetBuffer(IBuffer buffer);

        /// <summary>
        ///   Sets the primitive type and data order that describes the input data to assembled.
        /// </summary>
        /// <param name="topology">
        ///   Specifies a <see cref="PrimitiveTopology"/> that represents the topology.
        /// </param>
        void SetPrimitiveTopology(PrimitiveTopology topology);
    }
}