// <copyright file="IOpenGLShaderProgram.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Pipeline;

using FinalEngine.Rendering.Pipeline;

public interface IOpenGLShaderProgram : IShaderProgram
{
    void Bind();

    bool TryGetUniformLocation(string name, out int location);
}
