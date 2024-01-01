// <copyright file="OpenGLFrameBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers;

using System;
using System.Collections.Generic;
using System.Linq;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.OpenGL.Textures;
using FinalEngine.Rendering.Textures;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = Rendering.Textures.PixelFormat;

public class OpenGLFrameBuffer : IFrameBuffer, IOpenGLFrameBuffer, IDisposable
{
    private readonly IOpenGLInvoker invoker;

    private int rendererID;

    public OpenGLFrameBuffer(IOpenGLInvoker invoker, IReadOnlyList<IOpenGLTexture> colorTargets, IOpenGLTexture? depthTarget)
    {
        ArgumentNullException.ThrowIfNull(colorTargets, nameof(colorTargets));
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        int maximumColorAttachments = this.invoker.GetInteger(GetPName.MaxColorAttachments);

        if (colorTargets.Count > maximumColorAttachments)
        {
            throw new FrameBufferTargetException($"The number of {nameof(colorTargets)} should not exceed the maximum number of color attachmemts: '{maximumColorAttachments}'.");
        }

        this.DepthTarget = (ITexture2D?)depthTarget;
        this.ColorTargets = colorTargets.Cast<ITexture2D>();
        this.rendererID = invoker.CreateFramebuffer();

        int attachmentCount = colorTargets.Count;

        for (int i = 0; i < attachmentCount; i++)
        {
            colorTargets[i].Attach(FramebufferAttachment.ColorAttachment0 + i, this.rendererID);
        }

        Span<DrawBuffersEnum> bufs = stackalloc DrawBuffersEnum[attachmentCount];

        for (int i = 0; i < attachmentCount; i++)
        {
            bufs[i] = DrawBuffersEnum.ColorAttachment0 + i;
        }

        this.invoker.NamedFramebufferDrawBuffers(this.rendererID, attachmentCount, ref bufs[0]);
        if (depthTarget != null)
        {
            var framebufferAttachment = FramebufferAttachment.DepthAttachment;
            if (depthTarget is { Format: PixelFormat.DepthAndStencil })
            {
                framebufferAttachment = FramebufferAttachment.DepthStencilAttachment;
            }
            depthTarget.Attach(framebufferAttachment, this.rendererID);
        }
        

        var status = invoker.CheckNamedFramebufferStatus(this.rendererID, FramebufferTarget.Framebuffer);

        if (status != FramebufferStatus.FramebufferComplete)
        {
            throw new FrameBufferNotCompleteException($"The {nameof(OpenGLFrameBuffer)} is not complete: {status}.");
        }
    }

    ~OpenGLFrameBuffer()
    {
        this.Dispose(false);
    }

    public IEnumerable<ITexture2D> ColorTargets { get; }

    public ITexture2D? DepthTarget { get; }

    protected bool IsDisposed { get; private set; }

    public void Bind()
    {
        this.invoker.BindFramebuffer(FramebufferTarget.Framebuffer, this.rendererID);
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
