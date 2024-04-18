// <copyright file="ImageInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Loaders.Invocation;

using System.Diagnostics.CodeAnalysis;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

[ExcludeFromCodeCoverage(Justification = "Invocation")]
internal sealed class ImageInvoker : IImageInvoker
{
    public Image<TPixel> Load<TPixel>(Stream stream)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        return Image.Load<TPixel>(stream);
    }
}
