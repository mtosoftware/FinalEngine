// <copyright file="OpenGLGPUResourceFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.OpenGL.Buffers;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.OpenGL.Pipeline;
    using FinalEngine.Rendering.OpenGL.Textures;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Rendering.Textures;
    using FinalEngine.Utilities;
    using OpenTK.Graphics.OpenGL4;
    using PixelFormat = FinalEngine.Rendering.Textures.PixelFormat;

    /// <summary>
    ///   Provides an OpenGL implementation of an <see cref="IGPUResourceFactory"/>.
    /// </summary>
    /// <seealso cref="IGPUResourceFactory"/>
    public class OpenGLGPUResourceFactory : IGPUResourceFactory
    {
        /// <summary>
        ///   The OpenGL invoker.
        /// </summary>
        private readonly IOpenGLInvoker invoker;

        /// <summary>
        ///   The OpenGL-to-FinalEngine enumeration mapper.
        /// </summary>
        /// <remarks>
        ///   Used to map OpenGL enumerations to the rendering APIs equivalent.
        /// </remarks>
        private readonly IEnumMapper mapper;

        /// <summary>
        ///   Initializes a new instance of the <see cref="OpenGLGPUResourceFactory"/> class.
        /// </summary>
        /// <param name="invoker">
        ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
        /// </param>
        /// <param name="mapper">
        ///   Specifies an <see cref="IEnumMapper"/> that represents the enumeration mapper used to map OpenGL enumerations to the rendering APIs equivalent.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="invoker"/> or <paramref name="mapper"/> parameter is null.
        /// </exception>
        public OpenGLGPUResourceFactory(IOpenGLInvoker invoker, IEnumMapper mapper)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

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
        /// <returns>
        ///   The newly created <see cref="IIndexBuffer"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="data"/> parameter is null.
        /// </exception>
        public IIndexBuffer CreateIndexBuffer<T>(BufferUsageType type, IReadOnlyCollection<T> data, int sizeInBytes)
            where T : struct
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return new OpenGLIndexBuffer<T>(this.invoker, this.mapper, this.mapper.Forward<BufferUsageHint>(type), data, sizeInBytes);
        }

        /// <summary>
        ///   Creates an <see cref="IInputLayout"/> that formats vertex buffers to the specified <paramref name="elements"/>.
        /// </summary>
        /// <param name="elements">
        ///   Specifies an <see cref="IReadOnlyCollection{InputElement}"/> that represents the elements the input layout will format to when bound.
        /// </param>
        /// <returns>
        ///   The newly created <see cref="IInputLayout"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="elements"/> parameter is null.
        /// </exception>
        public IInputLayout CreateInputLayout(IReadOnlyCollection<InputElement> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            return new OpenGLInputLayout(this.invoker, this.mapper, elements);
        }

        /// <summary>
        ///   Creates an <see cref="IShader"/> targeting the specified <paramref name="target"/>, containing the specified <paramref name="sourceCode"/>.
        /// </summary>
        /// <param name="target">
        ///   Specifies a <see cref="PipelineTarget"/> that represents the entry point of the shader.
        /// </param>
        /// <param name="sourceCode">
        ///   Specifies a <see cref="string"/> that represents the source code of shader.
        /// </param>
        /// <returns>
        ///   The newly created <see cref="IShader"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="sourceCode"/> parameter is null, empty or consists of only whitespace.
        /// </exception>
        public IShader CreateShader(PipelineTarget target, string sourceCode)
        {
            if (string.IsNullOrWhiteSpace(sourceCode))
            {
                throw new ArgumentException($"The specified {nameof(sourceCode)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(sourceCode));
            }

            return new OpenGLShader(this.invoker, this.mapper, this.mapper.Forward<ShaderType>(target), sourceCode);
        }

        /// <summary>
        ///   Creates an <see cref="IShaderProgram"/> from the specified <paramref name="shaders"/>.
        /// </summary>
        /// <param name="shaders">
        ///   Specifies an <see cref="IReadOnlyCollection{IShader}"/> that represents the collection of shaders used to create the program with.
        /// </param>
        /// <returns>
        ///   The newly created <see cref="IShaderProgram"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="shaders"/> parameter is null.
        /// </exception>
        public IShaderProgram CreateShaderProgram(IReadOnlyCollection<IShader> shaders)
        {
            if (shaders == null)
            {
                throw new ArgumentNullException(nameof(shaders));
            }

            return new OpenGLShaderProgram(this.invoker, shaders.Cast<IOpenGLShader>().ToList().AsReadOnly());
        }

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
        public ITexture2D CreateTexture2D<T>(Texture2DDescription description, IReadOnlyCollection<T>? data, PixelFormat format = PixelFormat.Rgba, SizedFormat internalFormat = SizedFormat.Rgba8)
        {
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var ptr = data == null ? IntPtr.Zero : handle.AddrOfPinnedObject();

            var result = new OpenGLTexture2D(this.invoker, this.mapper, description, format, internalFormat, ptr);

            handle.Free();

            return result;
        }

        /// <summary>
        ///   Creates an <see cref="IVertexBuffer"/> that contains the specified <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="T">
        ///   The type of data the buffer will contain.
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
        /// <returns>
        ///   The newly created <see cref="IVertexBuffer"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="data"/> parameter is null.
        /// </exception>
        public IVertexBuffer CreateVertexBuffer<T>(BufferUsageType type, IReadOnlyCollection<T> data, int sizeInBytes, int stride)
            where T : struct
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return new OpenGLVertexBuffer<T>(this.invoker, this.mapper, this.mapper.Forward<BufferUsageHint>(type), data, sizeInBytes, stride);
        }
    }
}
