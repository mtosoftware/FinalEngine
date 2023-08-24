// <copyright file="OpenALSound.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL;

using System;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Audio.OpenAL.Loaders;
using FinalEngine.Resources;
using CASLSound = CASL.Sound;
using ICASLSound = CASL.ISound;

/// <summary>
/// Provides a standard implementation of an <see cref="ISound"/> using the OpenAL framework.
/// </summary>
///
/// <remarks>
/// The <see cref="OpenALSound" /> implementation expands upon the capabilities of <see cref="IResource" />, empowering developers with enhanced control over sound instantiation through <see cref="IResourceManager" /> instances. Furthermore, the following coding formats are supported by the <see cref="OpenALSound"/> implementation:
///
/// <list type="bullet">
///     <item>
///         .MP3 (MPEG Audio Layer III)
///     </item>
///     <item>
///         .OGG (Ogg Vorbis Compressed Audio File)
///     </item>
/// </list>
///
/// If you require support for other formats we recommend either rolling your own <see cref="ISound"/> implementation and associated <see cref="ResourceLoaderBase{TResource}"/> - or - converting the audio file(s) you have to one of the supported coding formats.
/// </remarks>
///
/// <example>
/// Below you'll find an example showing how to typically instantiate an instance of <see cref="OpenALSound" />. This example assumes that the following criteria has been met:
///
/// <list type="bullet">
///     <item>
///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
///     </item>
///     <item>
///         The <see cref="SoundResourceLoader"/> has been registered to the <see cref="ResourceManager.Instance"/>.
///     </item>
/// </list>
///
/// <code>
/// // Load the resource via the resource manager.
/// // This is how you should typically instantiate implementations that implement IResource.
/// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
///
/// // Now, we can adjust the volume (range is between 0-100).
/// sound.Volume = 80.0f;
///
/// // We can also set whether the audio should loop (false, by default).
/// sound.IsLooping = true;
///
/// // Finally, let's play the sound.
/// sound.Play();
/// </code>
/// </example>
/// <seealso cref="ISound" />
/// <seealso cref="IDisposable" />
public sealed class OpenALSound : ISound, IDisposable
{
    /// <summary>
    /// Indicates whether this instance is disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// The underlying CASL sound instance.
    /// </summary>
    private ICASLSound? sound;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenALSound"/> class.
    /// </summary>
    ///
    /// <param name="filePath">
    /// Specifies a <see cref="string"/> that represents the file path of the sound to load.
    /// </param>
    ///
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="filePath"/> parameter cannot be null or whitespace.
    /// </exception>
    [ExcludeFromCodeCoverage]
    public OpenALSound(string filePath)
        : this(new CASLSound(filePath))
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenALSound"/> class.
    /// </summary>
    ///
    /// <param name="sound">
    /// Specifies an <see cref="ICASLSound"/> that represents the CASL sound instance to use.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="sound"/> parameter cannot be null.
    /// </exception>
    internal OpenALSound(ICASLSound sound)
    {
        this.sound = sound ?? throw new ArgumentNullException(nameof(sound));
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="OpenALSound" /> is set to loop.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if this <see cref="OpenALSound" /> is set to loop; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <remarks>
    /// The <see cref="IsLooping" /> property determines whether the <see cref="OpenALSound"/> should restart playback from the beginning once it reaches the end.
    /// </remarks>
    ///
    /// <example>
    /// Below you'll find an example showing how to instantiate and loop an <see cref="OpenALSound" /> instance. This example assumes that the following criteria has been met:
    ///
    /// <list type="bullet">
    ///     <item>
    ///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
    ///     </item>
    ///     <item>
    ///         The <see cref="SoundResourceLoader"/> has been registered to the <see cref="ResourceManager.Instance"/>.
    ///     </item>
    /// </list>
    ///
    /// <code>
    /// // Load the resource via the resource manager.
    /// // This is how you should typically instantiate implementations that implement IResource.
    /// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Modify the property to ensure that the sound will loop.
    /// sound.IsLooping = true;
    ///
    /// // Play the sound
    /// sound.Play();
    /// </code>
    /// </example>
    ///
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="OpenALSound"/> instance has been disposed.
    /// </exception>
    public bool IsLooping
    {
        get
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenALSound));
            }

            return this.sound!.IsLooping;
        }

        set
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenALSound));
            }

            this.sound!.IsLooping = value;
        }
    }

    /// <summary>
    /// Gets or sets a <see cref="float"/> that represents the volume of this <see cref="OpenALSound"/>.
    /// </summary>
    ///
    /// <value>
    /// A <see cref="float"/> that represents the volume of this <see cref="OpenALSound"/>.
    /// </value>
    ///
    /// <remarks>
    /// The <see cref="Volume"/> property handles values within the range of 0 to 100. Values outside this range should are adjusted to fit within it.
    /// </remarks>
    ///
    /// <example>
    /// Below you'll find an example showing how to adjust the <see cref="Volume"/> of an <see cref="OpenALSound"/> instance. This example assumes that the following criteria has been met:
    ///
    /// <list type="bullet">
    ///     <item>
    ///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
    ///     </item>
    ///     <item>
    ///         The <see cref="SoundResourceLoader"/> has been registered to the <see cref="ResourceManager.Instance"/>.
    ///     </item>
    /// </list>
    ///
    /// <code>
    /// // Load the resource via the resource manager.
    /// // This is how you should typically instantiate implementations that implement IResource.
    /// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Adjust the volume to be 50%.
    /// sound.Volume = 50.0f;
    ///
    /// // Play the sound.
    /// sound.Play();
    /// </code>
    /// </example>
    ///
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="OpenALSound"/> instance has been disposed.
    /// </exception>
    public float Volume
    {
        get
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenALSound));
            }

            return this.sound!.Volume;
        }

        set
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenALSound));
            }

            this.sound!.Volume = value;
        }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        if (this.isDisposed)
        {
            return;
        }

        if (this.sound != null)
        {
            this.sound.Dispose();
            this.sound = null;
        }

        this.isDisposed = true;
    }

    /// <summary>
    /// Pauses playback of this <see cref="OpenALSound"/>.
    /// </summary>
    ///
    /// <remarks>
    /// The <see cref="Pause"/> method halts audio playback while retaining the current position. Resuming playback through <see cref="Play"/> will continue from where the sound was paused.
    /// </remarks>
    ///
    /// <example>
    /// Below you'll find an example showing how to <see cref="Pause"/> an <see cref="OpenALSound"/> instance. This example assumes that the following criteria has been met:
    ///
    /// <list type="bullet">
    ///     <item>
    ///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
    ///     </item>
    ///     <item>
    ///         The <see cref="SoundResourceLoader"/> has been registered to the <see cref="ResourceManager.Instance"/>.
    ///     </item>
    /// </list>
    ///
    /// <code>
    /// // Load the resource via the resource manager.
    /// // This is how you should typically instantiate implementations that implement IResource.
    /// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Play the sound.
    /// sound.Play();
    ///
    /// var watch = new Stopwatch();
    /// watch.Start();
    ///
    /// // Wait 10 seconds, then pause the sound.
    /// while (watch.ElapsedMilliseconds &lt; 10000)
    /// {
    ///     continue;
    /// }
    ///
    /// sound.Pause();
    ///
    /// // Wait another 5 seconds, then resume playback.
    /// while (watch.ElapsedMilliseconds &lt; 15000)
    /// {
    ///     continue;
    /// }
    ///
    /// sound.Play();
    /// </code>
    /// </example>
    ///
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="OpenALSound"/> instance has been disposed.
    /// </exception>
    public void Pause()
    {
        if (this.isDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenALSound));
        }

        this.sound!.Pause();
    }

    /// <summary>
    /// Starts or resumes playback of this <see cref="OpenALSound"/>.
    /// </summary>
    ///
    /// <remarks>
    /// The <see cref="Play"/> initiates or resumes audio playback from its current position. If the sound was previously paused using the <see cref="Pause"/> method, invoking <see cref="Play"/> will continue playback from where it was paused.
    /// </remarks>
    ///
    /// <example>
    /// Below you'll find an example showing how to <see cref="Play"/> an <see cref="OpenALSound"/> instance. This example assumes that the following criteria has been met:
    ///
    /// <list type="bullet">
    ///     <item>
    ///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
    ///     </item>
    ///     <item>
    ///         The <see cref="SoundResourceLoader"/> has been registered to the <see cref="ResourceManager.Instance"/>.
    ///     </item>
    /// </list>
    ///
    /// <code>
    /// // Load the resource via the resource manager.
    /// // This is how you should typically instantiate implementations that implement IResource.
    /// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Play the sound.
    /// sound.Play();
    /// </code>
    /// </example>
    ///
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="OpenALSound"/> instance has been disposed.
    /// </exception>
    public void Play()
    {
        if (this.isDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenALSound));
        }

        this.sound!.Play();
    }

    /// <summary>
    /// Stops playback of this <see cref="OpenALSound"/> and resets its position to the beginning.
    /// </summary>
    ///
    /// <remarks>
    /// The <see cref="Stop"/> will halt audio playback and reset its position to the beginning. Subsequent calls to the <see cref="Play"/> method will cause the sound to begin playing from the start.
    /// </remarks>
    ///
    /// <example>
    /// Below you'll find an example showing how to <see cref="Stop"/> an <see cref="OpenALSound"/> instance. This example assumes that the following criteria has been met:
    ///
    /// <list type="bullet">
    ///     <item>
    ///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
    ///     </item>
    ///     <item>
    ///         The <see cref="SoundResourceLoader"/> has been registered to the <see cref="ResourceManager.Instance"/>.
    ///     </item>
    /// </list>
    ///
    /// <code>
    /// // Load the resource via the resource manager.
    /// // This is how you should typically instantiate implementations that implement IResource.
    /// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Play the sound.
    /// sound.Play();
    ///
    /// var watch = new Stopwatch();
    /// watch.Start();
    ///
    /// // Wait 10 seconds, then stop the sound.
    /// while (watch.ElapsedMilliseconds &lt; 10000)
    /// {
    ///     continue;
    /// }
    ///
    /// sound.Stop();
    /// </code>
    /// </example>
    ///
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="OpenALSound"/> instance has been disposed.
    /// </exception>
    public void Stop()
    {
        if (this.isDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenALSound));
        }

        this.sound!.Stop();
    }
}
