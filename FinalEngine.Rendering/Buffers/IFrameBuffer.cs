// <copyright file="IFrameBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers;

using System.Collections.Generic;
using FinalEngine.Rendering.Textures;

public interface IFrameBuffer
{
    IEnumerable<ITexture2D> ColorTargets { get; }

    ITexture2D? DepthTarget { get; }
}
