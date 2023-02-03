// <copyright file="OpenGLInputAssembler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.OpenGL.Buffers;
using FinalEngine.Rendering.OpenGL.Invocation;

/// <summary>
///   Provides an OpenGL implementation of an <see cref="IInputAssembler"/>.
/// </summary>
/// <seealso cref="IInputAssembler"/>
public class OpenGLInputAssembler : IInputAssembler
{
    /// <summary>
    ///   The OpenGL invoker.
    /// </summary>
    private readonly IOpenGLInvoker invoker;

    /// <summary>
    ///   The currently bound input layout.
    /// </summary>
    /// <remarks>
    ///   This is used to call <see cref="IOpenGLInputLayout.Unbind"/> when the user calls <see cref="SetInputLayout(IInputLayout?)"/> and passes <c>null</c> as the parameter.
    /// </remarks>
    private IOpenGLInputLayout? boundLayout;

    /// <summary>
    ///   Initializes a new instance of the <see cref="OpenGLInputAssembler"/> class.
    /// </summary>
    /// <param name="invoker">
    ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="invoker"/> parameter is null.
    /// </exception>
    public OpenGLInputAssembler(IOpenGLInvoker invoker)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
    }

    /// <summary>
    ///   Sets the specified <paramref name="buffer"/>, binding it to the GPU.
    /// </summary>
    /// <param name="buffer">
    ///   Specifies a <see cref="IIndexBuffer"/> that represents the index buffer to bind.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="buffer"/> parameter cannot be null.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///   The specified <paramref name="buffer"/> is not the correct implementation. If this exception occurs, you're attempting to bind an index buffer that does not implement <see cref="IOpenGLIndexBuffer"/>.
    /// </exception>
    public void SetIndexBuffer(IIndexBuffer buffer)
    {
        if (buffer == null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        if (buffer is not IOpenGLIndexBuffer glIndexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLIndexBuffer)}.", nameof(buffer));
        }

        glIndexBuffer.Bind();
    }

    /// <summary>
    ///   Sets the specified <paramref name="layout"/>, binding it to the GPU.
    /// </summary>
    /// <param name="layout">
    ///   Specifies an <see cref="IInputLayout"/> that represents the layout to bind.
    /// </param>
    /// <remarks>
    ///   Passing <c>null</c> to the <paramref name="layout"/> parameter will unbind the previously bound input layout.
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///   The specified <paramref name="layout"/> is not the correct implementation. If this exception occurs, you're attempting to bind an input layout that does not implement <see cref="IOpenGLInputLayout"/>.
    /// </exception>
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

    /// <summary>
    ///   Sets the specified <paramref name="buffer"/>, binding it to the GPU.
    /// </summary>
    /// <param name="buffer">
    ///   Specifies a <see cref="IVertexBuffer"/> that represents the vertex buffer to bind.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="buffer"/> parameter cannot be null.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///   The specified <paramref name="buffer"/> is not the correct implementation. If this exception occurs, you're attempting to bind an vertex buffer that does not implement <see cref="IOpenGLVertexBuffer"/>.
    /// </exception>
    public void SetVertexBuffer(IVertexBuffer buffer)
    {
        if (buffer == null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        if (buffer is not IOpenGLVertexBuffer glVertexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLVertexBuffer)}.", nameof(buffer));
        }

        glVertexBuffer.Bind();
    }

    /// <summary>
    ///   Updates the specified index <paramref name="buffer"/> and fills it with the specified <paramref name="data"/>.
    /// </summary>
    /// <typeparam name="T">
    ///   The type of data to fill with the buffer with.
    /// </typeparam>
    /// <param name="buffer">
    ///   The buffer to fill.
    /// </param>
    /// <param name="data">
    ///   The data to fill the buffer with.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="buffer"/> or <paramref name="data"/> parameter is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///   The specified <paramref name="buffer"/> is not the correct implementation. If this exception occurs, you're attempting to update an index buffer that does not implement <see cref="IOpenGLIndexBuffer"/>.
    /// </exception>
    public void UpdateIndexBuffer<T>(IIndexBuffer buffer, IReadOnlyCollection<T> data)
        where T : struct
    {
        if (buffer == null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        if (buffer is not IOpenGLIndexBuffer glIndexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLVertexBuffer)}.", nameof(buffer));
        }

        glIndexBuffer.Update(data);
    }

    /// <summary>
    ///   Updates the specified vertex <paramref name="buffer"/> and fills it with the specified <paramref name="data"/>.
    /// </summary>
    /// <typeparam name="T">
    ///   The type of data to fill with the buffer with.
    /// </typeparam>
    /// <param name="buffer">
    ///   The buffer to fill.
    /// </param>
    /// <param name="data">
    ///   The data to fill the buffer with.
    /// </param>
    /// <param name="stride">
    ///   The total number of bytes for a single vertex.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="buffer"/> or <paramref name="data"/> parameter is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///   The specified <paramref name="buffer"/> is not the correct implementation. If this exception occurs, you're attempting to update a vertex buffer that does not implement <see cref="IOpenGLVertexBuffer"/>.
    /// </exception>
    public void UpdateVertexBuffer<T>(IVertexBuffer buffer, IReadOnlyCollection<T> data, int stride)
        where T : struct
    {
        if (buffer == null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        if (buffer is not IOpenGLVertexBuffer glVertexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLVertexBuffer)}.", nameof(buffer));
        }

        glVertexBuffer.Update(data, stride);
    }
}
