namespace FinalEngine.Rendering.Buffers
{
    using System;

    /// <summary>
    ///   Defines an interface that represents a primitive buffer resource.
    /// </summary>
    /// <seealso cref="System.IDisposable"/>
    public interface IBuffer : IDisposable
    {
        /// <summary>
        ///   Gets a <see cref="int"/> that represents the size in bytes of this <see cref="IBuffer"/>.
        /// </summary>
        /// <value>
        ///   The size in bytes of this <see cref="IBuffer"/>.
        /// </value>
        int SizeInBytes { get; }

        /// <summary>
        ///   Gets a <see cref="BufferType"/> that represents the type of this <see cref="IBuffer"/>.
        /// </summary>
        /// <value>
        ///   The type of this <see cref="IBuffer"/>.
        /// </value>
        BufferType Type { get; }
    }
}