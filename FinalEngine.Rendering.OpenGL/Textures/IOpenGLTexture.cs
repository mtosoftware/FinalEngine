// <copyright file="IOpenGLTexture.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Textures;

using FinalEngine.Rendering.Textures;
using OpenTK.Graphics.OpenGL4;

public interface IOpenGLTexture : ITexture
{
    void Attach(FramebufferAttachment type, int framebuffer);

    void Bind(int unit);

    void CopyImageSubData(
        int srcLevel,
        int srcX,
        int srcY,
        int srcZ,
        int dstName,
        ImageTarget dstTarget,
        int dstLevel,
        int dstX,
        int dstY,
        int dstZ);
}
