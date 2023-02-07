// <copyright file="OpenALSound.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL;

using System;
using CASLSound = CASL.Sound;

public class OpenALSound : ISound
{
    private CASLSound? sound;

    public OpenALSound(string filePath)
    {
        this.sound = new CASLSound(filePath);
    }

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

    protected bool IsDisposed { get; private set; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Pause()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenALSound));
        }

        this.sound!.Pause();
    }

    public void Start()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenALSound));
        }

        this.sound!.Play();
    }

    public void Stop()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenALSound));
        }

        this.sound!.Stop();
    }

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
