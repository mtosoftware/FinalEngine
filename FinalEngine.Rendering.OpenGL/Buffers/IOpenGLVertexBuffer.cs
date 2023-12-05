// <copyright file="IOpenGLVertexBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers;

using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;

public interface IOpenGLVertexBuffer : IVertexBuffer
{
    void Bind();

    void Update<TData>(IReadOnlyCollection<TData> data, int stride)
        where TData : struct;
}
