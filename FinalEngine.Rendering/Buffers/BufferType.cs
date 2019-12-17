namespace FinalEngine.Rendering.Buffers
{
    /// <summary>
    ///   Enumerates the available buffer types that determines how an <see cref="IBuffer"/> will be bound a rendering pipeline.
    /// </summary>
    public enum BufferType
    {
        /// <summary>
        ///   Specifies the buffer will be bound as a vertex buffer to the input assembler stage.
        /// </summary>
        VertexBuffer,

        /// <summary>
        ///   Specifies the buffer will be bound as an index buffer to the input assembler stage.
        /// </summary>
        IndexBuffer
    }
}