// <copyright file="IOpenGLInputLayout.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers;

using FinalEngine.Rendering.Buffers;

/// <summary>
///   Defines an interface that represents an OpenGL input layout that describes the formating of vertex buffer data.
/// </summary>
/// <seealso cref="IInputLayout"/>
public interface IOpenGLInputLayout : IInputLayout
{
    /// <summary>
    ///   Binds this <see cref="IOpenGLInputLayout"/> to the graphics processing unit.
    /// </summary>
    void Bind();

    /// <summary>
    ///   Unbinds this <see cref="IOpenGLInputLayout"/> from the graphics processing unit.
    /// </summary>
    void Unbind();
}
