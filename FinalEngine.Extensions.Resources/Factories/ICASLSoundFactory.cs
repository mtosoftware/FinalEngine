// <copyright file="ICASLSoundFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions.Resources.Factories;

using ICASLSound = CASL.ISound;

/// <summary>
/// Defines an interface that provides a method to load an <see cref="ICASLSound"/>.
/// </summary>
public interface ICASLSoundFactory
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
    ICASLSound CreateSound(string filePath);
}
