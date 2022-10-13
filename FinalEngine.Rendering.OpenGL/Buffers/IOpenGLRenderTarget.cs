// <copyright file="IOpenGLRenderTarget.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers
{
    using FinalEngine.Rendering.Buffers;
    using OpenTK.Graphics.OpenGL4;

    public interface IOpenGLRenderTarget : IRenderTarget
    {
        void Bind(FramebufferTarget target);
    }
}
