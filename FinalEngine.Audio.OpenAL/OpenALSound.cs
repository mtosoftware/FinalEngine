// <copyright file="OpenALSound.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL;

using System;
using CASLSound = CASL.Sound;

/// <summary>
/// Provides an OpenAL implementation of an <see cref="ISound"/>.
/// </summary>
/// <seealso cref="ISound" />
public class OpenALSound : ISound
{
    /// <summary>
    /// The sound instance for this <see cref="OpenALSound"/>.
    /// </summary>
    private CASLSound? sound;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenALSound"/> class.
    /// </summary>
    /// <param name="filePath">
    /// The file path of the audio source.
    /// </param>
    public OpenALSound(string filePath)
    {
        this.sound = new CASLSound(filePath);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ISound" /> is looping.
    /// </summary>
    /// <value>
    /// <c>true</c> if this <see cref="ISound" /> is looping; otherwise, <c>false</c>.
    /// </value>
    /// <exception cref="ObjectDisposedException">
    /// The underlying native sound is disposed.
    /// </exception>
    public bool IsLooping
    {
        get
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenALSound));
            }

            return this.sound!.IsLooping;
        }

        set
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenALSound));
            }

            this.sound!.IsLooping = value;
        }
    }

    /// <summary>
    /// Gets or sets the volume of this <see cref="ISound" />.
    /// </summary>
    /// <value>
    /// The volume of this <see cref="ISound" />.
    /// </value>
    /// <exception cref="ObjectDisposedException">
    /// The underlying native sound is disposed.
    /// </exception>
    /// <remarks>
    /// The <see cref="Volume" /> property should be set within a range of 0-1.
    /// </remarks>
    public float Volume
    {
        get
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenALSound));
            }

            return this.sound!.Volume;
        }

        set
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenALSound));
            }

            this.sound!.Volume = value;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDisposed { get; private set; }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Pauses this <see cref="ISound" />.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// The underlying native sound is disposed.
    /// </exception>
    public void Pause()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenALSound));
        }

        this.sound!.Pause();
    }

    /// <summary>
    /// Starts (plays) this <see cref="ISound" />.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// The underlying native sound is disposed.
    /// </exception>
    public void Start()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenALSound));
        }

        this.sound!.Play();
    }

    /// <summary>
    /// Stops this <see cref="ISound" /> and resets the start position.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// The underlying native sound is disposed.
    /// </exception>
    /// <remarks>
    /// If you wish to pause a sound and later play it from it's current position use <see cref="Pause" />.
    /// </remarks>
    public void Stop()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenALSound));
        }

        this.sound!.Stop();
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.sound != null)
            {
                this.sound.Dispose();
                this.sound = null;
            }
        }

        this.IsDisposed = true;
    }
}
