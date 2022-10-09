// <copyright file="OpenGLTexture2D.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Textures
{
    using System;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.Textures;
    using FinalEngine.Utilities;
    using OpenTK.Graphics.OpenGL4;
    using PixelFormat = FinalEngine.Rendering.Textures.PixelFormat;
    using TKPixelForamt = OpenTK.Graphics.OpenGL4.PixelFormat;
    using TKPixelType = OpenTK.Graphics.OpenGL4.PixelType;
    using TKTextureWrapMode = OpenTK.Graphics.OpenGL4.TextureWrapMode;

    /// <summary>
    ///   Provides an OpenGL implementation of an <see cref="ITexture2D"/> and <see cref="IOpenGLTexture"/>.
    /// </summary>
    /// <seealso cref="ITexture2D"/>
    /// <seealso cref="IOpenGLTexture"/>
    public class OpenGLTexture2D : ITexture2D, IOpenGLTexture
    {
        /// <summary>
        ///   The OpenGL invoker.
        /// </summary>
        private readonly IOpenGLInvoker invoker;

        /// <summary>
        ///   The OpenGL renderer identifier.
        /// </summary>
        private int rendererID;

        /// <summary>
        ///   Initializes a new instance of the <see cref="OpenGLTexture2D"/> class.
        /// </summary>
        /// <param name="invoker">
        ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
        /// </param>
        /// <param name="mapper">
        ///   Specifies an <see cref="IEnumMapper"/> that represents the enumeration mapper used to map OpenGL enumerations to the rendering APIs equivalent.
        /// </param>
        /// <param name="description">
        ///   Specifies a <see cref="Texture2DDescription"/> that represents the properties used when creating this <see cref="OpenGLTexture2D"/>.
        /// </param>
        /// <param name="format">
        ///   Specifies a <see cref="PixelFormat"/> that represents the format of this <see cref="OpenGLTexture2D"/>.
        /// </param>
        /// <param name="internalFormat">
        ///   Specifies a <see cref="SizedFormat"/> that represents the internal format of this <see cref="OpenGLTexture2D"/>.
        /// </param>
        /// <param name="data">
        ///   Specifies a <see cref="IntPtr"/> that represents the data this <see cref="OpenGLTexture2D"/> will contain.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="invoker"/> or <paramref name="mapper"/> parameter is null.
        /// </exception>
        public OpenGLTexture2D(
            IOpenGLInvoker invoker,
            IEnumMapper mapper,
            Texture2DDescription description,
            PixelFormat format,
            SizedFormat internalFormat,
            IntPtr data)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker), $"The specified {nameof(invoker)} parameter cannot be null.");

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper), $"The specified {nameof(mapper)} parameter cannot be null.");
            }

            this.rendererID = invoker.CreateTexture(TextureTarget.Texture2D);

            this.Format = format;
            this.InternalFormat = internalFormat;

            this.Description = description;

            invoker.TextureStorage2D(this.rendererID, 1, mapper.Forward<SizedInternalFormat>(this.InternalFormat), description.Width, description.Height);

            invoker.TextureParameter(this.rendererID, TextureParameterName.TextureMinFilter, (int)mapper.Forward<TextureMinFilter>(description.MinFilter));
            invoker.TextureParameter(this.rendererID, TextureParameterName.TextureMagFilter, (int)mapper.Forward<TextureMagFilter>(description.MagFilter));
            invoker.TextureParameter(this.rendererID, TextureParameterName.TextureWrapS, (int)mapper.Forward<TKTextureWrapMode>(description.WrapS));
            invoker.TextureParameter(this.rendererID, TextureParameterName.TextureWrapT, (int)mapper.Forward<TKTextureWrapMode>(description.WrapT));

            invoker.TextureSubImage2D(
                texture: this.rendererID,
                level: 0,
                xoffset: 0,
                yoffset: 0,
                width: description.Width,
                height: description.Height,
                format: mapper.Forward<TKPixelForamt>(this.Format),
                type: mapper.Forward<TKPixelType>(description.PixelType),
                pixels: data);
        }

        /// <summary>
        ///   Finalizes an instance of the <see cref="OpenGLTexture2D"/> class.
        /// </summary>
        ~OpenGLTexture2D()
        {
            this.Dispose(false);
        }

        /// <summary>
        ///   Gets the description that describes the properties of this <see cref="OpenGLTexture2D"/>.
        /// </summary>
        /// <value>
        ///   The description that describes the properties of this <see cref="OpenGLTexture2D"/>.
        /// </value>
        public Texture2DDescription Description { get; }

        /// <summary>
        ///   Gets the format of this <see cref="OpenGLTexture2D"/>.
        /// </summary>
        /// <value>
        ///   The format of this <see cref="OpenGLTexture2D"/>.
        /// </value>
        public PixelFormat Format { get; }

        /// <summary>
        ///   Gets the internal format of this <see cref="OpenGLTexture2D"/>.
        /// </summary>
        /// <value>
        ///   The internal format of this <see cref="OpenGLTexture2D"/>.
        /// </value>
        public SizedFormat InternalFormat { get; }

        /// <summary>
        ///   Gets a value indicating whether this <see cref="OpenGLTexture2D"/> is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="OpenGLTexture2D"/> is disposed; otherwise, <c>false</c>.
        /// </value>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        ///   Binds this <see cref="IOpenGLTexture"/> to the graphics processing unit.
        /// </summary>
        /// <param name="unit">
        ///   Specifies an <see cref="int"/> that represents which texture slot to activate.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        ///   The <see cref="OpenGLTexture2D"/> has been disposed.
        /// </exception>
        public void Bind(int unit)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenGLTexture2D));
            }

            this.invoker.BindTextureUnit(unit, this.rendererID);
        }

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing && this.rendererID != -1)
            {
                this.invoker.DeleteTexture(this.rendererID);
                this.rendererID = -1;
            }

            this.IsDisposed = true;
        }
    }
}
