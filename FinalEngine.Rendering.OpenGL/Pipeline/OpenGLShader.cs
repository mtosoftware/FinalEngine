// <copyright file="OpenGLShader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Pipeline
{
    using System;
    using FinalEngine.Rendering.Exceptions;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Utilities;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    ///   Provides an OpenGL implementation of an <see cref="IOpenGLShader"/>.
    /// </summary>
    /// <seealso cref="IOpenGLShader"/>
    public class OpenGLShader : IOpenGLShader
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
        ///   Initializes a new instance of the <see cref="OpenGLShader"/> class.
        /// </summary>
        /// <param name="invoker">
        ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
        /// </param>
        /// <param name="mapper">
        ///   Specifies an <see cref="IEnumMapper"/> that represents the enumeration mapper used to map OpenGL enumerations to the rendering APIs equivalent.
        /// </param>
        /// <param name="type">
        ///   Specifies a <see cref="ShaderType"/> that represents the entry point of this <see cref="OpenGLShader"/>.
        /// </param>
        /// <param name="sourceCode">
        ///   Specifies a <see cref="string"/> that represents the source code this <see cref="OpenGLShader"/> will compile.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="invoker"/> or <paramref name="mapper"/> parameter is null - or - the specified <paramref name="sourceCode"/> parameter is null, empty or consists of only whitespace.
        /// </exception>
        /// <exception cref="ShaderCompilationErrorException">
        ///   The <see cref="OpenGLShader"/> failed to compile - see <see cref="ShaderCompilationErrorException.ErrorLog"/> for more details.
        /// </exception>
        public OpenGLShader(IOpenGLInvoker invoker, IEnumMapper mapper, ShaderType type, string sourceCode)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker), $"The specified {nameof(invoker)} parameter cannot be null.");

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper), $"The specified {nameof(mapper)} parameter cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(sourceCode))
            {
                throw new ArgumentNullException(nameof(sourceCode), $"The specified {nameof(sourceCode)} parameter cannot be null, empty or contain only whitespace.");
            }

            this.EntryPoint = mapper.Reverse<PipelineTarget>(type);

            this.rendererID = invoker.CreateShader(type);
            invoker.ShaderSource(this.rendererID, sourceCode);
            invoker.CompileShader(this.rendererID);

            string? log = invoker.GetShaderInfoLog(this.rendererID);

            if (!string.IsNullOrWhiteSpace(log))
            {
                throw new ShaderCompilationErrorException($"The {nameof(OpenGLShader)} failed to compile.", log);
            }
        }

        /// <summary>
        ///   Finalizes an instance of the <see cref="OpenGLShader"/> class.
        /// </summary>
        ~OpenGLShader()
        {
            this.Dispose(false);
        }

        /// <summary>
        ///   Gets the entry point of this <see cref="OpenGLShader"/>.
        /// </summary>
        /// <value>
        ///   The entry point of this <see cref="OpenGLShader"/>.
        /// </value>
        public PipelineTarget EntryPoint { get; }

        /// <summary>
        ///   Gets a value indicating whether this <see cref="OpenGLShader"/> is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="OpenGLShader"/> is disposed; otherwise, <c>false</c>.
        /// </value>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        ///   Attaches this <see cref="OpenGLShader"/> to the specified <paramref name="program"/>.
        /// </summary>
        /// <param name="program">
        ///   Specifies a <see cref="int"/> that represents the shader program OpenGL renderer identifier to attach the <see cref="OpenGLShader"/> too.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        ///   The <see cref="OpenGLShader"/> has been disposed.
        /// </exception>
        public void Attach(int program)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenGLShader));
            }

            this.invoker.AttachShader(program, this.rendererID);
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
                this.invoker.DeleteShader(this.rendererID);
                this.rendererID = -1;
            }

            this.IsDisposed = true;
        }
    }
}