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
    /// Creates an <see cref="ICASLSound" /> by loading it from the specified <paramref name="filePath" />.
    /// </summary>
    ///
    /// <param name="filePath">
    /// Specifies a <see cref="string"/> that represents the file path of the sound to load.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="filePath"/> parameter cannot be null or whitespace.
    /// </exception>
    ///
    /// <returns>
    /// An <see cref="ICASLSound"/> that represents the CASL sound implementation.
    /// </returns>
    public ICASLSound CreateSound(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
        }

        return new CASLSound(filePath);
    }
}
