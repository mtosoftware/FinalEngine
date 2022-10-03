// <copyright file="IOpenGLShaderProgram.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Pipeline
{
    using System;
    using FinalEngine.Rendering.Pipeline;

    /// <summary>
    ///   Defines an interface that represents an OpenGL shader program.
    /// </summary>
    /// <seealso cref="IShaderProgram"/>
    public interface IOpenGLShaderProgram : IShaderProgram
    {
        /// <summary>
        ///   Binds this <see cref="IOpenGLShaderProgram"/> to the graphics processing unit.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///   The <see cref="IOpenGLShaderProgram"/> has been disposed.
        /// </exception>
        void Bind();

        /// <summary>
        ///   Gets the uniform location of the specified uniform, <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        ///   Specifies a <see cref="string"/> that represents the name of uniform to look for.
        /// </param>
        /// <returns>
        ///   The location of the uniform, or -1 if the uniform wasn't located.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        ///   The <see cref="IOpenGLShaderProgram"/> has been disposed.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="name"/> parameter is null, empty or consists of only whitespace.
        /// </exception>
        int GetUniformLocation(string name);
    }
}