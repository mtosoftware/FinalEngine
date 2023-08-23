// <copyright file="CASLSoundFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL.Factories;

using System;
using System.Diagnostics.CodeAnalysis;
using CASLSound = CASL.Sound;
using ICASLSound = CASL.ISound;

/// <summary>
/// Provides a standard implementation of an <see cref="ICASLSoundFactory"/>.
/// </summary>
/// <seealso cref="ICASLSoundFactory" />
[ExcludeFromCodeCoverage]
internal sealed class CASLSoundFactory : ICASLSoundFactory
{
    /// <summary>
    /// Creates an <see cref="ICASLSound"/> by loading it from the specified <paramref name="filePath"/>.
    /// </summary>
    /// <param name="filePath">
    /// The file path of the sound to load.
    /// </param>
    /// <returns>
    /// The newly created <see cref="ICASLSound"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="filePath"/> parameter cannot be null, empty or consist of only whitespace characters.
    /// </exception>
    public ICASLSound CreateSound(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        return new CASLSound(filePath);
    }
}
