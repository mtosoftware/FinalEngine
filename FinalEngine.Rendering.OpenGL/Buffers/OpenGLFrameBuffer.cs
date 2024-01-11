// <copyright file="OpenGLFrameBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.OpenGL.Textures;
using OpenTK.Graphics.OpenGL4;

public class OpenGLFrameBuffer : IFrameBuffer, IOpenGLFrameBuffer
{
    private readonly IOpenGLInvoker invoker;

    private int rendererID;

    public OpenGLFrameBuffer(IOpenGLInvoker invoker, IReadOnlyList<IOpenGLTexture>? colorTargets, IOpenGLTexture? depthTarget)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        int maximumColorAttachments = this.invoker.GetInteger(GetPName.MaxColorAttachments);
        int colorAttachmentCount = colorTargets?.Count ?? 0;

        if (colorAttachmentCount > maximumColorAttachments)
        {
            throw new FrameBufferTargetException($"The number of {nameof(colorTargets)} should not exceed the maximum number of color attachmemts: '{maximumColorAttachments}'.");
        }

        this.rendererID = invoker.CreateFramebuffer();

        for (int i = 0; i < colorAttachmentCount; i++)
        {
            colorTargets![i].Attach(FramebufferAttachment.ColorAttachment0 + i, this.rendererID);
        }

        if (colorAttachmentCount > 0)
        {
            Span<DrawBuffersEnum> bufs = stackalloc DrawBuffersEnum[colorAttachmentCount];

            for (int i = 0; i < colorAttachmentCount; i++)
            {
                bufs[i] = DrawBuffersEnum.ColorAttachment0 + i;
            }

            this.invoker.NamedFramebufferDrawBuffers(this.rendererID, colorAttachmentCount, ref bufs[0]);
        }

        depthTarget?.Attach(FramebufferAttachment.DepthAttachment, this.rendererID);

        if (colorAttachmentCount <= 0)
        {
            this.invoker.NamedFramebufferDrawBuffer(this.rendererID, DrawBufferMode.None);
            this.invoker.NamedFramebufferReadBuffer(this.rendererID, ReadBufferMode.None);
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
