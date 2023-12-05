// <copyright file="CASLSoundFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL.Factories;

using System;
using System.Diagnostics.CodeAnalysis;
using CASLSound = CASL.Sound;
using ICASLSound = CASL.ISound;

[ExcludeFromCodeCoverage]
internal sealed class CASLSoundFactory : ICASLSoundFactory
{
    public ICASLSound CreateSound(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
        }

        return new CASLSound(filePath);
    }
}
