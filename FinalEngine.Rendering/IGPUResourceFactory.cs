namespace FinalEngine.Rendering
{
    using FinalEngine.Rendering.Buffers;

    /// <summary>
    ///   Defines an interface that provides methods for creating GPU resources.
    /// </summary>
    public interface IGPUResourceFactory
    {
        /// <summary>
        ///   Creates an <see cref="IBuffer"/> that contains the specified <paramref name="data"/> of the specified type, <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        ///   Specifies the type of data to store in the <see cref="IBuffer"/>.
        /// </typeparam>
        /// <param name="type">
        ///   Specifies a <see cref="BufferType"/> that represents the type of data that will be stored in the <see cref="IBuffer"/>.
        /// </param>
        /// <param name="data">
        ///   Specifies a <see cref="T[]"/> that represents the data to store in the <see cref="IBuffer"/>.
        /// </param>
        /// <param name="sizeInBytes">
        ///   Specifies a <see cref="Int32"/> that represents the size (in bytes) of the specified <paramref name="data"/> that will be stored in the <see cref="IBuffer"/>.
        /// </param>
        /// <param name="structStride">
        ///   Specifies a <see cref="Int32"/> that represents the size (in bytes) of the structure of type <typeparamref name="T"/> (if any).
        /// </param>
        /// <returns>
        ///   The newly created <see cref="IBuffer"/>, containg the specified <paramref name="data"/> of type, <typeparamref name="T"/>.
        /// </returns>
        IBuffer CreateBuffer<T>(BufferType type, T[] data, int sizeInBytes, int structStride) where T : struct;
    }
}