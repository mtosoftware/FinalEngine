// <copyright file="IImageInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Loaders.Invocation;

using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

internal interface IImageInvoker
{
    Image<TPixel> Load<TPixel>(Stream stream)
        where TPixel : unmanaged, IPixel<TPixel>;
}
