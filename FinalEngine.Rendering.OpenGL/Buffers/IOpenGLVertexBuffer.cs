// <copyright file="IOpenGLVertexBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers
{
    using System;
    using System.Collections.Generic;
    using FinalEngine.Rendering.Buffers;

    /// <summary>
    ///   Defines an interface that represents an OpenGL vertex buffer.
    /// </summary>
    /// <seealso cref="IVertexBuffer"/>
    public interface IOpenGLVertexBuffer : IVertexBuffer
    {
        /// <summary>
        ///   Binds this <see cref="IOpenGLVertexBuffer"/> to the graphics processing unit.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///   The <see cref="IOpenGLVertexBuffer"/> has been disposed.
        /// </exception>
        void Bind();

        /// <summary>
        ///   Updates the vertex buffer by filling it with the specified <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="TData">
        ///   The type of data to fill the buffer with.
        /// </typeparam>
        /// <param name="data">
        ///   The data to fill with the buffer with.
        /// </param>
        /// <param name="stride">
        ///   The total number of bytes a single vertex contains.
        /// </param>
        void Update<TData>(IReadOnlyCollection<TData> data, int stride)
            where TData : struct;
    }
}
