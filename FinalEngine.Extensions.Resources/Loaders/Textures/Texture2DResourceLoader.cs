// <copyright file="Texture2DResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions.Resources.Loaders.Textures;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using FinalEngine.Extensions.Resources.Invocation;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Settings;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

/// <summary>
///   Provides a resource loader for loading textures from a file system.
/// </summary>
public class Texture2DResourceLoader : ResourceLoaderBase<ITexture2D>
{
    /// <summary>
    ///   The GPU resource factory.
    /// </summary>
    private readonly IGPUResourceFactory factory;

    /// <summary>
    ///   The file system.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    ///   The image invoker.
    /// </summary>
    private readonly IImageInvoker invoker;

    /// <summary>
    /// Initializes a new instance of the <see cref="Texture2DResourceLoader"/> class.
    /// </summary>
    /// <param name="fileSystem">
    ///   The file system used to open textures to load.
    /// </param>
    /// <param name="factory">
    ///   The GPU resource factory used to create a texture once it's been loaded.
    /// </param>
    [ExcludeFromCodeCoverage]
    public Texture2DResourceLoader(IFileSystem fileSystem, IGPUResourceFactory factory)
        : this(fileSystem, factory, new ImageInvoker())
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Texture2DResourceLoader"/> class.
    /// </summary>
    /// <param name="fileSystem">
    ///   The file system used to open textures to load.
    /// </param>
    /// <param name="factory">
    ///   The GPU resource factory used to create a texture once it's been loaded.
    /// </param>
    /// <param name="invoker">
    ///   The image invoker used to handle <see cref="Image"/> manipulation.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="fileSystem"/>, <paramref name="factory"/> or <paramref name="invoker"/> parameter is null.
    /// </exception>
    public Texture2DResourceLoader(
        IFileSystem fileSystem,
        IGPUResourceFactory factory,
        IImageInvoker invoker)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
    }

    /// <summary>
    /// Gets or sets the texture quality settings.
    /// </summary>
    /// <value>
    /// The texture quality settings.
    /// </value>
    public TextureQualitySettings TextureQualitySettings { get; set; }

    /// <summary>
    ///   Loads the texture from the specified <paramref name="filePath"/>.
    /// </summary>
    /// <param name="filePath">
    ///   The file path of the texture to load.
    /// </param>
    /// <returns>
    ///   The newly loaded texture resource.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="filePath"/> parameter is null.
    /// </exception>
    /// <exception cref="FileNotFoundException">
    ///   The specified <paramref name="filePath"/> parameter cannot be located by the file system.
    /// </exception>
    public override ITexture2D LoadResource(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(filePath));
        }

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
                    var row = image.GetPixelRowSpan(y);

                    for (int x = 0; x < image.Width; x++)
                    {
                        pixels.Add(row[x].R);
                        pixels.Add(row[x].G);
                        pixels.Add(row[x].B);
                        pixels.Add(row[x].A);
                    }
                }

                return this.factory.CreateTexture2D(
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
                    pixels.ToArray());
            }
        }
    }
}
