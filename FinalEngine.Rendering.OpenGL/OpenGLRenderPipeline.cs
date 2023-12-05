// <copyright file="OpenGLRenderPipeline.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using FinalEngine.Rendering.OpenGL.Invocation;

public sealed class OpenGLRenderPipeline : IRenderPipeline
{
    private readonly IOpenGLInvoker invoker;

    private bool isDisposed;

    private int vao;

    public OpenGLRenderPipeline()
        : this(new OpenGLInvoker())
    {
    }

    public OpenGLRenderPipeline(IOpenGLInvoker invoker)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
    }

    ~OpenGLRenderPipeline()
    {
        this.Dispose(false);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Initialize()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);

        this.vao = this.invoker.GenVertexArray();
        this.invoker.BindVertexArray(this.vao);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.vao != -1)
            {
                this.invoker.DeleteVertexArray(this.vao);
                this.vao = -1;
            }
        }

        this.isDisposed = true;
    }
}
