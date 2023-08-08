// <copyright file="SoundResourceLoader.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions.Resources.Loaders.Audio;

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using FinalEngine.Audio;
using FinalEngine.Audio.OpenAL;
using FinalEngine.Extensions.Resources.Factories;
using FinalEngine.Resources;

/// <summary>
/// Provides an implementation of a <see cref="ResourceLoaderBase{TResource}"/> that can load <see cref="ISound"/> resources.
/// </summary>
public class SoundResourceLoader : ResourceLoaderBase<ISound>
{
    /// <summary>
    /// The CASL sound factory, used to load a CASL sound resource.
    /// </summary>
    private readonly ICASLSoundFactory factory;

    /// <summary>
    /// The file system, used to load sound resources.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundResourceLoader"/> class.
    /// </summary>
    /// <param name="fileSystem">The file system.</param>
    [ExcludeFromCodeCoverage]
    public SoundResourceLoader(IFileSystem fileSystem)
        : this(fileSystem, new CASLSoundFactory())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundResourceLoader"/> class.
    /// </summary>
    /// <param name="fileSystem">
    /// The file system, used to load sound resources.
    /// </param>
    /// <param name="factory">
    /// The factory used to create an OpenAL sound.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="fileSystem"/> parameter cannot be null.
    /// </exception>
    public SoundResourceLoader(IFileSystem fileSystem, ICASLSoundFactory factory)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
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

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
        }

        return new OpenALSound(this.factory.CreateSound(filePath));
    }
}
