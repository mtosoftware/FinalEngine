// <copyright file="OpenGLInputAssembler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.OpenGL.Buffers;

public class OpenGLInputAssembler : IInputAssembler
{
    private IOpenGLInputLayout? boundLayout;

    public void SetIndexBuffer(IIndexBuffer buffer)
    {
        ArgumentNullException.ThrowIfNull(buffer, nameof(buffer));

        if (buffer is not IOpenGLIndexBuffer glIndexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLIndexBuffer)}.", nameof(buffer));
        }

        glIndexBuffer.Bind();
    }

    public void SetInputLayout(IInputLayout? layout)
    {
        if (layout == null)
        {
            this.boundLayout?.Unbind();
            return;
        }

        if (layout is not IOpenGLInputLayout glInputLayout)
        {
            throw new ArgumentException($"The specified {nameof(layout)} parameter is not of type {nameof(IOpenGLInputLayout)}.", nameof(layout));
        }

        this.boundLayout?.Unbind();
        this.boundLayout = glInputLayout;

        this.boundLayout.Bind();
    }

    public void SetVertexBuffer(IVertexBuffer buffer)
    {
        ArgumentNullException.ThrowIfNull(buffer, nameof(buffer));

        if (buffer is not IOpenGLVertexBuffer glVertexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLVertexBuffer)}.", nameof(buffer));
        }

        glVertexBuffer.Bind();
    }

    public void UpdateIndexBuffer<T>(IIndexBuffer buffer, IReadOnlyCollection<T> data)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(buffer, nameof(buffer));
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        if (buffer is not IOpenGLIndexBuffer glIndexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLVertexBuffer)}.", nameof(buffer));
        }

        glIndexBuffer.Update(data);
    }

    public void UpdateVertexBuffer<T>(IVertexBuffer buffer, IReadOnlyCollection<T> data, int stride)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(buffer, nameof(buffer));
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        if (buffer is not IOpenGLVertexBuffer glVertexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLVertexBuffer)}.", nameof(buffer));
        }

        glVertexBuffer.Update(data, stride);
    }
}
