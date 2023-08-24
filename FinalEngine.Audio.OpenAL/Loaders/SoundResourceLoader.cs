// <copyright file="SoundResourceLoader.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL.Loaders;

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using FinalEngine.Audio.OpenAL.Factories;
using FinalEngine.Resources;

/// <summary>
/// Provides an implementation of a <see cref="ResourceLoaderBase{TResource}"/> that can load <see cref="ISound"/> resources.
/// </summary>
///
/// <remarks>
/// This implementation loads an <see cref="ISound"/> by creating an <see cref="OpenALSound"/> instance.
/// </remarks>
///
/// <example>
/// Below you'll find an example showing how to register an instance of <see cref="SoundResourceLoader" /> to an <see cref="IResourceManager"/> instance. This example assumes that the following criteria has been met:
///
/// <list type="bullet">
///     <item>
///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
///     </item>
/// </list>
///
/// <code>
/// // First, register the resource loader with the resource manager.
/// // You can choose to load resources directory using the resource loader but then they will not be managed by the manager.
/// ResourceManager.Instance.RegisterLoader(new SoundResourceLoader());
///
/// // Load the resource via the resource manager.
/// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
///
/// // Let's play the sound.
/// sound.Play();
/// </code>
/// </example>
public class SoundResourceLoader : ResourceLoaderBase<ISound>
{
    /// <summary>
    /// The factory used to create the underlying CASL sound instance.
    /// </summary>
    private readonly ICASLSoundFactory factory;

    /// <summary>
    /// The file system used to load sound resources.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundResourceLoader"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public SoundResourceLoader()
        : this(new FileSystem())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundResourceLoader" /> class.
    /// </summary>
    ///
    /// <param name="fileSystem">
    /// Specifies an <see cref="IFileSystem"/> that represents the file system used to load sound resources.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="fileSystem"/> parameter cannot be null.
    /// </exception>
    [ExcludeFromCodeCoverage]
    public SoundResourceLoader(IFileSystem fileSystem)
        : this(fileSystem, new CASLSoundFactory())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundResourceLoader" /> class.
    /// </summary>
    ///
    /// <param name="fileSystem">
    /// Specifies an <see cref="IFileSystem"/> that represents the file system used to load sound resources.
    /// </param>
    ///
    /// <param name="factory">
    /// Specifies an <see cref="ICASLSoundFactory"/> that represents the factory used to create the underlying CASL sound instance.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown when the one of the following parameters are null:
    /// <list type="bullet">
    ///     <item>
    ///         <paramref name="fileSystem"/>
    ///     </item>
    ///     <item>
    ///         <paramref name="factory"/>
    ///     </item>
    /// </list>
    /// </exception>
    internal SoundResourceLoader(IFileSystem fileSystem, ICASLSoundFactory factory)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    /// <summary>
    /// Loads an <see cref="ISound"/> resource from the specified <paramref name="filePath" />.
    /// </summary>
    ///
    /// <param name="filePath">
    /// Specifies a <see cref="string"/> that represents the file path of the sound resource to load.
    /// </param>
    ///
    /// <remarks>
    /// Please note that you should use an instance of an <see cref="IResourceManager"/> (such as <see cref="ResourceManager.Instance"/> and not load resources directly; unless you wish to take control of the life-cycle of the resource.
    /// </remarks>
    ///
    /// <returns>
    /// An <see cref="ISound"/> that represents the newly loaded resource.
    /// </returns>
    ///
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="filePath"/> parameter cannot be null or whitespace.
    /// </exception>
    ///
    /// <exception cref="FileNotFoundException">
    /// The specified <paramref name="filePath"/> parameter cannot be located.
    /// </exception>
    ///
    /// <example>
    /// Below you'll find an example showing how to load a sound resource using the <see cref="SoundResourceLoader"/>.
    ///
    /// <code>
    /// var loader = new SoundResourceLoader();
    ///
    /// // You should typically use an instance of an IResourceManager to load resources
    /// // However, there may be instances where you'd like to take control of the life-cycle of the resource.
    /// ISound sound = loader.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Finally, let's play the sound.
    /// sound.Play();
    /// </code>
    /// </example>
    public override ISound LoadResource(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
        }

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
        }

        return new OpenALSound(this.factory.CreateSound(filePath));
    }
}
