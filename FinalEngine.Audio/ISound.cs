// <copyright file="ISound.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio;

using System;
using FinalEngine.Resources;

public interface ISound : IResource, IDisposable
{
    bool IsLooping { get; set; }

    float Volume { get; set; }

    void Pause();

    void Play();

    void Stop();
}
