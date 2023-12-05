// <copyright file="ITexture.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using FinalEngine.Resources;

public interface ITexture : IResource
{
    PixelFormat Format { get; }

    SizedFormat InternalFormat { get; }
}
