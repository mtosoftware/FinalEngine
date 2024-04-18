// <copyright file="Texture2DResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Loaders.Textures;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Loaders.Invocation;
using FinalEngine.Rendering.Textures;
using FinalEngine.Rendering.Vapor.Settings;
using FinalEngine.Resources;
using SixLabors.ImageSharp.PixelFormats;

internal sealed class Texture2DResourceLoader : ResourceLoaderBase<ITexture2D>
{
    private readonly IFileSystem fileSystem;

    private readonly IImageInvoker invoker;

    private readonly IRenderDevice renderDevice;

    public Texture2DResourceLoader(IFileSystem fileSystem, IRenderDevice renderDevice)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.invoker = new ImageInvoker();
    }

    internal TextureQualitySettings TextureQualitySettings { get; set; }

    public override ITexture2D LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
        }

        using (var stream = this.fileSystem.File.OpenRead(filePath))
        {
            using (var image = this.invoker.Load<Rgba32>(stream))
            {
                int width = image.Width;
                int height = image.Height;

                var pixels = new List<byte>(4 * image.Width * image.Height);

                for (int y = 0; y < image.Height; y++)
                {
                    image.ProcessPixelRows(processor =>
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            var pixel = processor.GetRowSpan(y);

                            pixels.Add(pixel[x].R);
                            pixels.Add(pixel[x].G);
                            pixels.Add(pixel[x].B);
                            pixels.Add(pixel[x].A);
                        }
                    });
                }

                var rasterState = this.renderDevice.Rasterizer.GetRasterState();

                return this.renderDevice.Factory.CreateTexture2D(
                    new Texture2DDescription()
                    {
                        Width = width,
                        Height = height,

                        MinFilter = this.TextureQualitySettings.MinFilter,
                        MagFilter = this.TextureQualitySettings.MagFilter,

                        WrapS = TextureWrapMode.Repeat,
                        WrapT = TextureWrapMode.Repeat,

                        PixelType = PixelType.Byte,

                        GenerateMipmaps = true,
                    },
                    pixels.ToArray(),
                    PixelFormat.Rgba,
                    SizedFormat.Rgba8);
            }
        }
    }
}
