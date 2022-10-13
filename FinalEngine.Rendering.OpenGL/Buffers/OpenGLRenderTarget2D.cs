// <copyright file="OpenGLRenderTarget2D.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers
{
    using System;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.OpenGL.Textures;
    using FinalEngine.Rendering.Textures;
    using FinalEngine.Utilities;
    using OpenTK.Graphics.OpenGL4;

    public class OpenGLRenderTarget2D : IOpenGLRenderTarget, IRenderTarget2D
    {
        private readonly IOpenGLInvoker invoker;

        private readonly IEnumMapper mapper;

        private int rendererID;

        public OpenGLRenderTarget2D(IOpenGLInvoker invoker, IEnumMapper mapper)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.rendererID = invoker.CreateFramebuffer();
        }

        ~OpenGLRenderTarget2D()
        {
            this.Dispose(false);
        }

        protected bool IsDisposed { get; private set; }

        public void AttachTexture(RenderTargetAttachmentType type, ITexture texture)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            if (texture is not IOpenGLTexture glTexture)
            {
                throw new ArgumentException($"The specified {nameof(texture)} parameter is not of type {nameof(IOpenGLTexture)}.", nameof(texture));
            }

            glTexture.Attach(this.mapper.Forward<FramebufferAttachment>(type), this.rendererID);
        }

        public void Bind(FramebufferTarget target)
        {
            this.invoker.Bindframebuffer(target, this.rendererID);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsComplete(RenderTargetBindType type)
        {
            return this.invoker.CheckNamedFramebufferStatus(this.rendererID, this.mapper.Forward<FramebufferTarget>(type)) == FramebufferStatus.FramebufferComplete;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.rendererID != -1)
                {
                    this.invoker.DeleteFramebuffer(this.rendererID);
                    this.rendererID = -1;
                }
            }

            this.IsDisposed = true;
        }
    }
}
