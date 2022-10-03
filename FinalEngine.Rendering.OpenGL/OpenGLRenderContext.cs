// <copyright file="OpenGLRenderContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL
{
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
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker), $"The specified {nameof(invoker)} parameter cannot be null.");

            if (bindings == null)
            {
                throw new ArgumentNullException(nameof(bindings), $"The specified {nameof(bindings)} parameter cannot be null.");
            }

            this.context = context ?? throw new ArgumentNullException(nameof(context), $"The specified {nameof(context)} parameter cannot be null.");

            context.MakeCurrent();
            invoker.LoadBindings(bindings);
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
    }
}