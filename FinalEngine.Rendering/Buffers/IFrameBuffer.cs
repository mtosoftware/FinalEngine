namespace FinalEngine.Rendering.Buffers;
using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Textures;

public interface IFrameBuffer : IDisposable
{
    ITexture2D? DepthTarget { get; }

    IReadOnlyList<ITexture2D> ColorTargets { get; }

    int Width { get; }
    int Height { get; }

    int ActualUsefulAttachmentCount { get; }
}
