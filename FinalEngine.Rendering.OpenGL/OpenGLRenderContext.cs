// <copyright file="OpenGLRenderContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL.Invocation;
using OpenTK;
using OpenTK.Windowing.Common;

/// <summary>
///   Provides an OpenGL implementation of an <see cref="IRenderContext"/>.
/// </summary>
/// <seealso cref="IRenderContext"/>
public class OpenGLRenderContext : IRenderContext
{
    /// <summary>
    ///   The underlying OpenGL rendering context.
    /// </summary>
    private readonly IGraphicsContext context;

    /// <summary>
    ///   The OpenGL invoker.
    /// </summary>
    private readonly IOpenGLInvoker invoker;

    /// <summary>
    ///   The global vertex array object.
    /// </summary>
    /// <remarks>
    ///   A global vertex array object is required as the rendering API has no concept of VAOs.
    /// </remarks>
    private int vao;

    /// <summary>
    ///   Initializes a new instance of the <see cref="OpenGLRenderContext"/> class.
    /// </summary>
    /// <param name="invoker">
    ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
    /// </param>
    /// <param name="bindings">
    ///   Specifies an <see cref="IBindingsContext"/> that represents the bindings context used to load the OpenGL bindings.
    /// </param>
    /// <param name="context">
    ///   Specifies an <see cref="IGraphicsContext"/> that represents the underlying rendering context.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="invoker"/>, <paramref name="bindings"/> or <paramref name="context"/> parameter is null.
    /// </exception>
    public OpenGLRenderContext(IOpenGLInvoker invoker, IBindingsContext bindings, IGraphicsContext context)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        if (bindings == null)
        {
            throw new ArgumentNullException(nameof(bindings));
        }

        this.context = context ?? throw new ArgumentNullException(nameof(context));

        context.MakeCurrent();
        invoker.LoadBindings(bindings);

        this.vao = this.invoker.GenVertexArray();
        this.invoker.BindVertexArray(this.vao);
    }

    /// <summary>
    ///   Finalizes an instance of the <see cref="OpenGLRenderContext"/> class.
    /// </summary>
    ~OpenGLRenderContext()
    {
        this.Dispose(false);
    }

    /// <summary>
    ///   Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDisposed { get; private set; }

    /// <summary>
    ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Swaps the front and back buffers, displaying the rendered scene to the user.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    ///   The <see cref="IRenderContext"/> has been disposed.
    /// </exception>
    /// <exception cref="RenderContextException">
    ///   The <see cref="IRenderContext"/> is not current on the calling thread.
    /// </exception>
    public void SwapBuffers()
    {
        if (!this.context.IsCurrent)
        {
            throw new RenderContextException($"This {nameof(OpenGLRenderContext)} is not current on the calling thread.");
        }

        this.context.SwapBuffers();
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

        if (disposing)
        {
            if (this.vao != -1)
            {
                this.invoker.DeleteVertexArray(this.vao);
                this.vao = -1;
            }
        }

        this.IsDisposed = true;
    }
}
