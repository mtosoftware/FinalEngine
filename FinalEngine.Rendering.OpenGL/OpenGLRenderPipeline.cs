// <copyright file="OpenGLRenderPipeline.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using FinalEngine.Rendering.OpenGL.Invocation;

/// <summary>
/// Provides an OpenGL implementation of an <see cref="IRenderPipeline"/>.
/// </summary>
/// <seealso cref="FinalEngine.Rendering.IRenderPipeline" />
public sealed class OpenGLRenderPipeline : IRenderPipeline
{
    /// <summary>
    /// The OpenGL invoker.
    /// </summary>
    private readonly IOpenGLInvoker invoker;

    /// <summary>
    /// Indicates whether this instance is disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// The global vertex array.
    /// </summary>
    private int vao;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGLRenderPipeline"/> class.
    /// </summary>
    public OpenGLRenderPipeline()
        : this(new OpenGLInvoker())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGLRenderPipeline"/> class.
    /// </summary>
    /// <param name="invoker">
    /// The OpenGL invoker.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="invoker"/> parameter cannot be null.
    /// </exception>
    public OpenGLRenderPipeline(IOpenGLInvoker invoker)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="OpenGLRenderPipeline"/> class.
    /// </summary>
    ~OpenGLRenderPipeline()
    {
        this.Dispose(false);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="OpenGLRenderPipeline"/> has been disposed.
    /// </exception>
    public void Initialize()
    {
        if (this.isDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenGLRenderPipeline));
        }

        this.vao = this.invoker.GenVertexArray();
        this.invoker.BindVertexArray(this.vao);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
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
