// <copyright file="IOpenGLShader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Pipeline
{
    using System;
    using FinalEngine.Rendering.Pipeline;

    /// <summary>
    ///   Defines an interface that represents an OpenGL shader.
    /// </summary>
    /// <seealso cref="IShader"/>
    public interface IOpenGLShader : IShader
    {
        /// <summary>
        ///   Attaches this <see cref="IOpenGLShader"/> to the specified <paramref name="program"/>.
        /// </summary>
        /// <param name="program">
        ///   Specifies a <see cref="int"/> that represents the shader program OpenGL renderer identifier to attach the <see cref="IShader"/> too.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        ///   The <see cref="IOpenGLShader"/> has been disposed.
        /// </exception>
        void Attach(int program);
    }
}
