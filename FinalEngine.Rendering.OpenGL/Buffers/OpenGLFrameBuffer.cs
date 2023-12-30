namespace FinalEngine.Rendering.OpenGL.Buffers;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.OpenGL.Textures;
using FinalEngine.Rendering.Textures;
using FinalEngine.Utilities;


public class OpenGLFrameBuffer : IOpenGLFrameBuffer
{
    private int rendererID;
    private readonly IOpenGLInvoker invoker;
    public ITexture2D? DepthTarget { get; }
    public IReadOnlyList<ITexture2D> ColorTargets { get; }
    public int Width { get; }
    public int Height { get; }


    protected bool IsDisposed { get; private set; }

    public OpenGLFrameBuffer(IOpenGLInvoker invoker, IEnumMapper mapper, IReadOnlyList<ITexture2D> colorTargets, ITexture2D? depthTarget)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(colorTargets, nameof(colorTargets));
        
        if (colorTargets is not IReadOnlyList<IOpenGLTexture>)
        {
            throw new ArgumentException(
                $"The specified {nameof(colorTargets)} parameter is not of type {nameof(IReadOnlyList<IOpenGLTexture>)}.",
                nameof(colorTargets));
        }

        if (depthTarget is not null && depthTarget is not IOpenGLTexture)
        {
            throw new ArgumentException(
                $"The specified {nameof(depthTarget)} parameter is not of type {nameof(IOpenGLTexture)}.",
                nameof(depthTarget));
        }

        this.DepthTarget = depthTarget;
        this.ColorTargets = colorTargets;
        this.rendererID = invoker.CreateFramebuffer();

        // Color 
        int attachmentCount = colorTargets.Count;
        for (int i = 0; i < attachmentCount; i++)
        {
            ((IOpenGLTexture)colorTargets[i]).Attach(FramebufferAttachment.ColorAttachment0 + i, this.rendererID);
        }
        Span<DrawBuffersEnum> bufs = stackalloc DrawBuffersEnum[attachmentCount];
        for (int i = 0; i < attachmentCount; i++)
        {
            bufs[i] = DrawBuffersEnum.ColorAttachment0 + i;
        }
        this.invoker.NamedFramebufferDrawBuffers(this.rendererID, attachmentCount, ref bufs[0]);

        // depth
        if (depthTarget != null)
        {
            ((IOpenGLTexture)depthTarget).Attach(FramebufferAttachment.DepthStencilAttachment, this.rendererID);
        }


        var status = invoker.CheckNamedFramebufferStatus(this.rendererID, FramebufferTarget.Framebuffer);
        if (status != FramebufferStatus.FramebufferComplete)
        {
            throw new FrameBufferNotCompleteException($"The {nameof(OpenGLFrameBuffer)} is not complete: {status}.");
        }
    }

    public void Bind()
    {
        this.invoker.Bindframebuffer(FramebufferTarget.Framebuffer, this.rendererID);
    }

    public void UnBind()
    {
        this.invoker.Bindframebuffer(FramebufferTarget.Framebuffer, 0);
    }

    ~OpenGLFrameBuffer()
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
            this.invoker.DeleteFramebuffer(this.rendererID);
            this.rendererID = -1;
        }

        this.IsDisposed = true;
    }
}

