// <copyright file="IOpenGLIndexBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers;

using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;

internal interface IOpenGLIndexBuffer : IIndexBuffer
{
    void Bind();

    void Update<TData>(IReadOnlyCollection<TData> data)
        where TData : struct;
}
