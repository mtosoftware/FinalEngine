// <copyright file="OpenALSound.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL;

using System;
using System.Diagnostics.CodeAnalysis;
using CASLSound = CASL.Sound;
using ICASLSound = CASL.ISound;

internal sealed class OpenALSound : ISound, IDisposable
{
    private bool isDisposed;

    private ICASLSound? sound;

    [ExcludeFromCodeCoverage]
    internal OpenALSound(string filePath)
        : this(new CASLSound(filePath))
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));
    }

    internal OpenALSound(ICASLSound sound)
    {
        this.sound = sound ?? throw new ArgumentNullException(nameof(sound));
    }

    public bool IsLooping
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            return this.sound!.IsLooping;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            this.sound!.IsLooping = value;
        }
    }

    public float Volume
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            return this.sound!.Volume;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            this.sound!.Volume = value;
        }
    }

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

    public void Pause()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        this.sound!.Pause();
    }

    public void Play()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        this.sound!.Play();
    }

    public void Stop()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        this.sound!.Stop();
    }
}
