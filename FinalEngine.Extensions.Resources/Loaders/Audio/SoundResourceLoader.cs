// <copyright file="SoundResourceLoader.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions.Resources.Loaders.Audio;

using System;
using System.IO;
using FinalEngine.Audio;
using FinalEngine.Audio.OpenAL;
using FinalEngine.IO;
using FinalEngine.Resources;

public class SoundResourceLoader : ResourceLoaderBase<ISound>
{
    private readonly IFileSystem fileSystem;

    public SoundResourceLoader(IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public override ISound LoadResource(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(filePath));
        }

        if (!this.fileSystem.FileExists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
        }

        return new OpenALSound(filePath);
    }
}
