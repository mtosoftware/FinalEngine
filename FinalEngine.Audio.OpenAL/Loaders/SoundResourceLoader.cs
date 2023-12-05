// <copyright file="SoundResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL.Loaders;

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using FinalEngine.Audio.OpenAL.Factories;
using FinalEngine.Resources;

public class SoundResourceLoader : ResourceLoaderBase<ISound>
{
    private readonly ICASLSoundFactory factory;

    private readonly IFileSystem fileSystem;

    [ExcludeFromCodeCoverage]
    public SoundResourceLoader(IFileSystem fileSystem)
        : this(fileSystem, new CASLSoundFactory())
    {
    }

    internal SoundResourceLoader(IFileSystem fileSystem, ICASLSoundFactory factory)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public override ISound LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
        }

        return new OpenALSound(this.factory.CreateSound(filePath));
    }
}
