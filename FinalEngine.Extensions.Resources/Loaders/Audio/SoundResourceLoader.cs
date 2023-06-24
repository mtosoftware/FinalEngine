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

/// <summary>
/// Provides an implementation of a <see cref="ResourceLoaderBase{TResource}"/> that can load <see cref="ISound"/> resources.
/// </summary>
public class SoundResourceLoader : ResourceLoaderBase<ISound>
{
    /// <summary>
    /// The file system, used to load sound resources.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundResourceLoader"/> class.
    /// </summary>
    /// <param name="fileSystem">
    /// The file system, used to load sound resources.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="fileSystem"/> parameter cannot be null.
    /// </exception>
    public SoundResourceLoader(IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    /// <summary>
    /// Loads a resource of the specified <see cref="ISound"/> from the specified <paramref name="filePath" />.
    /// </summary>
    /// <param name="filePath">The file path to load the resource.</param>
    /// <returns>
    /// The newly loaded resource.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="filePath"/> parameter cannot be null, empty or consist of only whitespace characters.
    /// </exception>
    /// <exception cref="FileNotFoundException">
    /// The specified <paramref name="filePath"/> parameter cannot be located.
    /// </exception>
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
