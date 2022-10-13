// <copyright file="IRenderTarget.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers
{
    using System;
    using FinalEngine.Rendering.Textures;

    /// <summary>
    ///   Defines an interface that represents a render target (frame buffer).
    /// </summary>
    public interface IRenderTarget : IDisposable
    {
        void AttachTexture(RenderTargetAttachmentType type, ITexture texture);

        bool IsComplete(RenderTargetBindType type);
    }
}
