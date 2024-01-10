// <copyright file="ITexture.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using System;
using FinalEngine.Resources;

public interface ITexture : IResource, IDisposable
{
    PixelFormat Format { get; }

    SizedFormat InternalFormat { get; }
}
