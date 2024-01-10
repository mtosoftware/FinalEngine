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

public class OpenglCubeTexture : ICubeTexture, IOpenGLTexture, IDisposable
{
    private readonly IOpenGLInvoker invoker;

    private int rendererID;
    public PixelFormat Format { get; }
    public SizedFormat InternalFormat { get; }
    public CubeTextureDescription Description { get; }
    protected bool IsDisposed { get; private set; }

    public int RenderId
    {
        get { return this.rendererID; }
    }
    public OpenglCubeTexture(
        IOpenGLInvoker invoker,
        IEnumMapper mapper,
        CubeTextureDescription description,
        SizedFormat internalFormat,
        IOpenGLTexture[] data)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.rendererID = invoker.CreateTexture(TextureTarget.TextureCubeMap);
        this.InternalFormat = internalFormat;
        this.Description = description;

        int mipmap = (int)Math.Ceiling(Math.Max(Math.Log2(description.Width + 1), Math.Log2(description.Height + 1)));
        invoker.TextureStorage2D(this.rendererID, mipmap, mapper.Forward<SizedInternalFormat>(this.InternalFormat),description.Width, description.Height);
       
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureMinFilter,
            (int)mapper.Forward<TextureMinFilter>(description.MinFilter));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureMagFilter,
            (int)mapper.Forward<TextureMagFilter>(description.MagFilter));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureWrapS,
            (int)mapper.Forward<TKTextureWrapMode>(description.WrapS));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureWrapT,
            (int)mapper.Forward<TKTextureWrapMode>(description.WrapT));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureWrapR,
            (int)mapper.Forward<TKTextureWrapMode>(description.WrapR));

        for (int i = 0; i < data.Length; i++)
        {
            invoker.CopyImageSubData(data[i].RenderId,
                ImageTarget.Texture2D,0,0,0,0,this.rendererID,
                ImageTarget.TextureCubeMap,0,0,0,i,description.Width,description.Height,1);

            if (description.GenerateMipmaps)
            {
                invoker.GenerateTextureMipmap(this.rendererID);
            }
        }
    }

    public void Attach(FramebufferAttachment type, int framebuffer)
    {
        // todo
        throw new NotImplementedException();
    }

    public void Bind(int unit)
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
        this.invoker.BindTextureUnit(unit, this.rendererID);
    }

    ~OpenglCubeTexture()
    {
        this.Dispose(false);
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
