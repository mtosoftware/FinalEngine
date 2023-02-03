// <copyright file="OpenGLIndexBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Utilities;
using OpenTK.Graphics.OpenGL4;

/// <summary>
///   Provides an OpenGL implementation of an <see cref="IOpenGLIndexBuffer"/>.
/// </summary>
/// <typeparam name="T">
///   Specifies the type of data that the <see cref="OpenGLIndexBuffer{T}"/> will contain.
/// </typeparam>
/// <seealso cref="IOpenGLIndexBuffer"/>
public class OpenGLIndexBuffer<T> : IOpenGLIndexBuffer
    where T : struct
{
    /// <summary>
    ///   The OpenGL invoker.
    /// </summary>
    private readonly IOpenGLInvoker invoker;

    /// <summary>
    ///   The OpenGL renderer identifier.
    /// </summary>
    private int rendererID;

    /// <summary>
    ///   Initializes a new instance of the <see cref="OpenGLIndexBuffer{T}"/> class.
    /// </summary>
    /// <param name="invoker">
    ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
    /// </param>
    /// <param name="mapper">
    ///   Specifies an <see cref="IEnumMapper"/> that represents the enumeration mapper used to map OpenGL enumerations to the rendering APIs equivalent.
    /// </param>
    /// <param name="usage">
    ///   Specifies a <see cref="BufferUsageHint"/> that represents how the index buffer will be used.
    /// </param>
    /// <param name="data">
    ///   Specifies an <see cref="IReadOnlyCollection{T}"/> that represents the data this <see cref="OpenGLIndexBuffer{T}"/> will contain.
    /// </param>
    /// <param name="sizeInBytes">
    ///   Specifies an <see cref="int"/> that represents the size in bytes of the specified <paramref name="data"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="invoker"/>, <paramref name="mapper"/> or <paramref name="data"/> parameter is null.
    /// </exception>
    public OpenGLIndexBuffer(IOpenGLInvoker invoker, IEnumMapper mapper, BufferUsageHint usage, IReadOnlyCollection<T> data, int sizeInBytes)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        if (mapper == null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        this.Type = mapper.Reverse<BufferUsageType>(usage);
        this.Length = data.Count;

        this.rendererID = invoker.CreateBuffer();
        invoker.NamedBufferData(this.rendererID, sizeInBytes, data.ToArray(), usage);
    }

    /// <summary>
    ///   Finalizes an instance of the <see cref="OpenGLIndexBuffer{T}"/> class.
    /// </summary>
    ~OpenGLIndexBuffer()
    {
        this.Dispose(false);
    }

    /// <summary>
    ///   Gets an <see cref="int"/> that represents the total amount of indices contained in this <see cref="OpenGLIndexBuffer{T}"/>.
    /// </summary>
    /// <value>
    ///   The total amount of indices contained in this <see cref="OpenGLIndexBuffer{T}"/>.
    /// </value>
    public int Length { get; private set; }

    /// <summary>
    ///   Gets the usage type for this <see cref="OpenGLIndexBuffer{T}"/>.
    /// </summary>
    /// <value>
    ///   The usage type for this <see cref="OpenGLIndexBuffer{T}"/>.
    /// </value>
    public BufferUsageType Type { get; }

    /// <summary>
    ///   Gets a value indicating whether this <see cref="OpenGLIndexBuffer{T}"/> is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="OpenGLIndexBuffer{T}"/> is disposed; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDisposed { get; private set; }

    /// <summary>
    ///   Binds this <see cref="OpenGLIndexBuffer{T}"/> to the graphics processing unit.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    ///   The <see cref="OpenGLIndexBuffer{T}"/> has been disposed.
    /// </exception>
    public void Bind()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenGLIndexBuffer<T>));
        }

        this.invoker.BindBuffer(BufferTarget.ElementArrayBuffer, this.rendererID);
    }

    /// <summary>
    ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Updates the vertex buffer by filling it with the specified <paramref name="data"/>.
    /// </summary>
    /// <typeparam name="TData">
    ///   The type of data to fill the buffer with.
    /// </typeparam>
    /// <param name="data">
    ///   The data to fill with the buffer with.
    /// </param>
    /// <exception cref="ObjectDisposedException">
    ///   The <see cref="OpenGLIndexBuffer{T}"/> has been disposed.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="data"/> parameter is null.
    /// </exception>
    public void Update<TData>(IReadOnlyCollection<TData> data)
        where TData : struct
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenGLIndexBuffer<T>));
        }

        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        this.Length = data.Count;
        this.invoker.NamedBufferSubData(this.rendererID, IntPtr.Zero, this.Length * Marshal.SizeOf<TData>(), data.ToArray());
    }

    /// <summary>
    ///   Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing && this.rendererID != -1)
        {
            this.invoker.DeleteBuffer(this.rendererID);
            this.rendererID = -1;
        }

        this.IsDisposed = true;
    }
}
