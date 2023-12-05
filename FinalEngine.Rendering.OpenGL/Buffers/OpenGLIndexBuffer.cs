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

public class OpenGLIndexBuffer<T> : IOpenGLIndexBuffer
    where T : struct
{
    private readonly IOpenGLInvoker invoker;

    private int rendererID;

    public OpenGLIndexBuffer(IOpenGLInvoker invoker, IEnumMapper mapper, BufferUsageHint usage, IReadOnlyCollection<T> data, int sizeInBytes)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        this.Type = mapper.Reverse<BufferUsageType>(usage);
        this.Length = data.Count;

        this.rendererID = invoker.CreateBuffer();
        invoker.NamedBufferData(this.rendererID, sizeInBytes, data.ToArray(), usage);
    }

    ~OpenGLIndexBuffer()
    {
        this.Dispose(false);
    }

    public int Length { get; private set; }

    public BufferUsageType Type { get; }

    protected bool IsDisposed { get; private set; }

    public void Bind()
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
        this.invoker.BindBuffer(BufferTarget.ElementArrayBuffer, this.rendererID);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Update<TData>(IReadOnlyCollection<TData> data)
        where TData : struct
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        this.Length = data.Count;
        this.invoker.NamedBufferSubData(this.rendererID, IntPtr.Zero, this.Length * Marshal.SizeOf<TData>(), data.ToArray());
    }

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
