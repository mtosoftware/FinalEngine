// <copyright file="ICASLSoundFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL.Factories;

using ICASLSound = CASL.ISound;

/// <summary>
/// Represents an interface that defines a method to load an <see cref="ICASLSound"/>.
/// </summary>
internal interface ICASLSoundFactory
{
    /// <summary>
    /// Creates an <see cref="ICASLSound"/> by loading it from the specified <paramref name="filePath"/>.
    /// </summary>
    ///
    /// <param name="filePath">
    /// The file path of the sound to load.
    /// </param>
    ///
    /// <returns>
    /// Returns the newly created <see cref="ICASLSound"/>.
    /// </returns>
    ICASLSound CreateSound(string filePath);
}
