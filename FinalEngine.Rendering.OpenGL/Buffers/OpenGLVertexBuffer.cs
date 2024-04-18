// <copyright file="OpenGLVertexBuffer.cs" company="Software Antics">
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

internal sealed class OpenGLVertexBuffer<T> : IOpenGLVertexBuffer
    where T : struct
{
    private readonly IOpenGLInvoker invoker;

    private bool isDisposed;

    private int rendererID;

    public OpenGLVertexBuffer(IOpenGLInvoker invoker, IEnumMapper mapper, BufferUsageHint usage, IReadOnlyCollection<T> data, int sizeInBytes, int stride)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        this.Type = mapper.Reverse<BufferUsageType>(usage);
        this.Stride = stride;

        this.rendererID = invoker.CreateBuffer();
        invoker.NamedBufferData(this.rendererID, sizeInBytes, data.ToArray(), usage);
    }

    ~OpenGLVertexBuffer()
    {
        this.Dispose(false);
    }

    public int Stride { get; private set; }

    public BufferUsageType Type { get; }

    public void Bind()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        this.invoker.BindVertexBuffer(0, this.rendererID, IntPtr.Zero, this.Stride);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Update<TData>(IReadOnlyCollection<TData> data, int stride)
        where TData : struct
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        this.Stride = stride;

        this.invoker.NamedBufferSubData(this.rendererID, IntPtr.Zero, data.Count * Marshal.SizeOf<TData>(), data.ToArray());
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.rendererID != -1)
        {
            this.invoker.DeleteBuffer(this.rendererID);
            this.rendererID = -1;
        }

        this.isDisposed = true;
    }
}
