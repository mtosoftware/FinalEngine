// <copyright file="OpenGLTexture2D.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Textures;

using System;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Textures;
using FinalEngine.Utilities;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = FinalEngine.Rendering.Textures.PixelFormat;
using TKPixelForamt = OpenTK.Graphics.OpenGL4.PixelFormat;
using TKPixelType = OpenTK.Graphics.OpenGL4.PixelType;
using TKTextureWrapMode = OpenTK.Graphics.OpenGL4.TextureWrapMode;

public class OpenGLTexture2D : ITexture2D, IOpenGLTexture, IDisposable
{
    private readonly IOpenGLInvoker invoker;

    private int rendererID;

    public OpenGLTexture2D(
        IOpenGLInvoker invoker,
        IEnumMapper mapper,
        Texture2DDescription description,
        PixelFormat format,
        SizedFormat internalFormat,
        IntPtr data)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        this.rendererID = invoker.CreateTexture(TextureTarget.Texture2D);

        this.Format = format;
        this.InternalFormat = internalFormat;

        this.Description = description;

        int mipmap = (int)Math.Ceiling(Math.Max(Math.Log2(description.Width + 1), Math.Log2(description.Height + 1)));

        invoker.TextureStorage2D(this.rendererID, mipmap, mapper.Forward<SizedInternalFormat>(this.InternalFormat), description.Width, description.Height);

        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureMinFilter, (int)mapper.Forward<TextureMinFilter>(description.MinFilter));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureMagFilter, (int)mapper.Forward<TextureMagFilter>(description.MagFilter));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureWrapS, (int)mapper.Forward<TKTextureWrapMode>(description.WrapS));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureWrapT, (int)mapper.Forward<TKTextureWrapMode>(description.WrapT));

        if (data != IntPtr.Zero)
        {
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

            if (description.GenerateMipmaps)
            {
                invoker.GenerateTextureMipmap(this.rendererID);
            }
        }
    }

    ~OpenGLTexture2D()
    {
        this.Dispose(false);
    }

    public Texture2DDescription Description { get; }

    public PixelFormat Format { get; }

    public SizedFormat InternalFormat { get; }

    protected bool IsDisposed { get; private set; }

    public void Attach(FramebufferAttachment type, int framebuffer)
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
        this.invoker.NamedFramebufferTexture(framebuffer, type, this.rendererID, 0);
    }

    public void Bind(int unit)
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
        this.invoker.BindTextureUnit(unit, this.rendererID);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

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
