// <copyright file="IOpenGLInputLayout.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers;

using FinalEngine.Rendering.Buffers;

internal interface IOpenGLInputLayout : IInputLayout
{
    void Bind();

    void Unbind();
}
