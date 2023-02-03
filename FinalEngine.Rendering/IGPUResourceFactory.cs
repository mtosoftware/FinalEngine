// <copyright file="IGPUResourceFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;

/// <summary>
///   Defines an interface that provides methods for creating GPU resources.
/// </summary>
public interface IGPUResourceFactory
{
    /// <summary>
    ///   Creates an <see cref="IIndexBuffer"/> that contains the specified <paramref name="data"/>.
    /// </summary>
    /// <typeparam name="T">
    ///   Specifies the type of data the <see cref="IIndexBuffer"/> will contain.
    /// </typeparam>
    /// <param name="type">
    ///   Specifies a <see cref="BufferUsageType"/> that represents the buffer usage type.
    /// </param>
    /// <param name="data">
    ///   Specifies an <see cref="IReadOnlyCollection{T}"/> that represents the data the buffer will contain.
    /// </param>
    /// <param name="sizeInBytes">
    ///   Specifies an <see cref="int"/> that represents the size in bytes of the <paramref name="data"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="data"/> parameter is null.
    /// </exception>
    /// <returns>
    ///   The newly created <see cref="IIndexBuffer"/>.
    /// </returns>
    IIndexBuffer CreateIndexBuffer<T>(BufferUsageType type, IReadOnlyCollection<T> data, int sizeInBytes)
        where T : struct;

    /// <summary>
    ///   Creates an <see cref="IInputLayout"/> that formats vertex buffers to the specified <paramref name="elements"/>.
    /// </summary>
    /// <param name="elements">
    ///   Specifies an <see cref="IReadOnlyCollection{InputElement}"/> that represents the elements the input layout will format to when bound.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="elements"/> parameter is null.
    /// </exception>
    /// <returns>
    ///   The newly created <see cref="IInputLayout"/>.
    /// </returns>
    IInputLayout CreateInputLayout(IReadOnlyCollection<InputElement> elements);

    /// <summary>
    ///   Creates an <see cref="IShader"/> targeting the specified <paramref name="target"/>, containing the specified <paramref name="sourceCode"/>.
    /// </summary>
    /// <param name="target">
    ///   Specifies a <see cref="PipelineTarget"/> that represents the entry point of the shader.
    /// </param>
    /// <param name="sourceCode">
    ///   Specifies a <see cref="string"/> that represents the source code of shader.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="sourceCode"/> parameter is null, empty or consists of only white-space.
    /// </exception>
    /// <returns>
    ///   The newly created <see cref="IShader"/>.
    /// </returns>
    IShader CreateShader(PipelineTarget target, string sourceCode);

    /// <summary>
    ///   Creates an <see cref="IShaderProgram"/> from the specified <paramref name="shaders"/>.
    /// </summary>
    /// <param name="shaders">
    ///   Specifies an <see cref="IReadOnlyCollection{IShader}"/> that represents the collection of shaders used to create the program with.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="shaders"/> parameter is null.
    /// </exception>
    /// <returns>
    ///   The newly created <see cref="IShaderProgram"/>.
    /// </returns>
    IShaderProgram CreateShaderProgram(IReadOnlyCollection<IShader> shaders);

    /// <summary>
    ///   Creates an <see cref="ITexture2D"/> from the specified <paramref name="description"/>, containing the specified <paramref name="data"/> and of the specified <paramref name="format"/> and <paramref name="internalFormat"/>.
    /// </summary>
    /// <typeparam name="T">
    ///   Specifies the type of data the texture will contain.
    /// </typeparam>
    /// <param name="description">
    ///   Specifies a <see cref="Texture2DDescription"/> that represents the description of the texture.
    /// </param>
    /// <param name="data">
    ///   Specifies a <see cref="IReadOnlyCollection{T}"/> that represents the data the texture will contain.
    /// </param>
    /// <param name="format">
    ///   Specifies a <see cref="PixelFormat"/> that represents the format of the pixel data.
    /// </param>
    /// <param name="internalFormat">
    ///   Specifies a <see cref="SizedFormat"/> that represents the sized internal format to be used to store the texture image data.
    /// </param>
    /// <returns>
    ///   The newly created <see cref="ITexture2D"/>.
    /// </returns>
    ITexture2D CreateTexture2D<T>(Texture2DDescription description, IReadOnlyCollection<T>? data, PixelFormat format = PixelFormat.Rgba, SizedFormat internalFormat = SizedFormat.Rgba8);

    /// <summary>
    ///   Creates an <see cref="IVertexBuffer"/> that contains the specified <paramref name="data"/>.
    /// </summary>
    /// <typeparam name="T">
    ///   Specifies the type of data the buffer will contain.
    /// </typeparam>
    /// <param name="type">
    ///   Specifies a <see cref="BufferUsageType"/> that represents the buffer usage type.
    /// </param>
    /// <param name="data">
    ///   Specifies an <see cref="IReadOnlyCollection{T}"/> that represents the data the buffer will contain.
    /// </param>
    /// <param name="sizeInBytes">
    ///   Specifies an <see cref="int"/> that represents the size in bytes of the data.
    /// </param>
    /// <param name="stride">
    ///   Specifies an <see cref="int"/> that represents the total stride of one vertex within the buffer.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="data"/> parameter is null.
    /// </exception>
    /// <returns>
    ///   The newly created <see cref="IVertexBuffer"/>.
    /// </returns>
    IVertexBuffer CreateVertexBuffer<T>(BufferUsageType type, IReadOnlyCollection<T> data, int sizeInBytes, int stride)
        where T : struct;
}
