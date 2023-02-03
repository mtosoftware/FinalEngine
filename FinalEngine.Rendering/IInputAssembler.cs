// <copyright file="IInputAssembler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;

/// <summary>
///   Defines an interface that represents the input-assembly stage of a graphics pipeline.
/// </summary>
public interface IInputAssembler
{
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
    ///   The specified <paramref name="buffer"/> is not the correct implementation. If this exception occurs, you're attempting to bind an index buffer that was created with a different rendering API than the one that's currently in use.
    /// </exception>
    void SetIndexBuffer(IIndexBuffer buffer);

    /// <summary>
    ///   Sets the specified <paramref name="layout"/>, binding it to the GPU.
    /// </summary>
    /// <param name="layout">
    ///   Specifies an <see cref="Nullable{IInputLayout}"/> that represents the layout to bind.
    /// </param>
    /// <remarks>
    ///   Passing <c>null</c> to the <paramref name="layout"/> parameter will unbind the currently bound input layout.
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///   The specified <paramref name="layout"/> is not the correct implementation. If this exception occurs, you're attempting to bind an input layout that was created with a different rendering API than the one that's currently in use.
    /// </exception>
    void SetInputLayout(IInputLayout? layout);

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
    ///   The specified <paramref name="buffer"/> is not the correct implementation. If this exception occurs, you're attempting to bind an vertex buffer that was created with a different rendering API than the one that's currently in use.
    /// </exception>
    void SetVertexBuffer(IVertexBuffer buffer);

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
    void UpdateIndexBuffer<T>(IIndexBuffer buffer, IReadOnlyCollection<T> data)
        where T : struct;

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
    void UpdateVertexBuffer<T>(IVertexBuffer buffer, IReadOnlyCollection<T> data, int stride)
        where T : struct;
}
