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

/// <summary>
/// Provides an <see cref="ISound"/> resource loader.
/// </summary>
///
/// <seealso cref="ResourceLoaderBase{TResource}"/>
internal sealed class SoundResourceLoader : ResourceLoaderBase<ISound>
{
    private readonly ICASLSoundFactory factory;

    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundResourceLoader"/> class.
    /// </summary>
    ///
    /// <param name="fileSystem">
    /// The file system used to load <see cref="ISound"/> resources.
    /// </param>
    [ExcludeFromCodeCoverage(Justification = "Cannot unit test")]
    public SoundResourceLoader(IFileSystem fileSystem)
        : this(fileSystem, new CASLSoundFactory())
    {
    }

    internal SoundResourceLoader(IFileSystem fileSystem, ICASLSoundFactory factory)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    /// <summary>
    /// Loads a <see cref="ISound"/> resource from the specified <paramref name="filePath" />.
    /// </summary>
    ///
    /// <param name="filePath">
    /// The file path of the resource to load.
    /// </param>
    ///
    /// <returns>
    /// The loaded resource, of type <see cref="ISound"/>.
    /// </returns>
    ///
    /// <exception cref="System.IO.FileNotFoundException">
    /// The specified <paramref name="filePath"/> parameter cannot be located on the current file system.
    /// </exception>
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
