namespace FinalEngine.Rendering.OpenGL.Buffers;
using System;
using System.Collections.Generic;
using Exceptions;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.OpenGL.Textures;
using FinalEngine.Rendering.Textures;
using FinalEngine.Utilities;
using OpenTK.Graphics.OpenGL4;

public class OpenGLFrameBuffer : IOpenGLFrameBuffer
{
    private int rendererID;
    private readonly IOpenGLInvoker invoker;
    public ITexture2D? DepthTarget { get; }
    public IReadOnlyCollection<ITexture2D> ColorTargets { get; }
    public int Width { get; }
    public int Height { get; }

    protected bool IsDisposed { get; private set; }

    public OpenGLFrameBuffer(IOpenGLInvoker invoker, IEnumMapper mapper, IReadOnlyCollection<ITexture2D> colorTargets, ITexture2D? depthTarget)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(colorTargets, nameof(colorTargets));

        if (colorTargets is not IReadOnlyCollection<IOpenGLTexture>)
        {
            throw new ArgumentException(
                $"The specified {nameof(colorTargets)} parameter is not of type {nameof(IReadOnlyCollection<IOpenGLTexture>)}.",
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

        int i = 0;
        foreach (var colorTarget in colorTargets)
        {
            ((IOpenGLTexture)colorTarget).Attach(FramebufferAttachment.ColorAttachment0 + i++, this.rendererID);
        }

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
        Span<DrawBuffersEnum> bufs = stackalloc DrawBuffersEnum[this.ColorTargets.Count];
        for (int i = 0; i < this.ColorTargets.Count; i++)
        {
            bufs[i] = DrawBuffersEnum.ColorAttachment0 + i;
        }

        this.invoker.DrawBuffers(this.ColorTargets.Count, ref bufs[0]);
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

