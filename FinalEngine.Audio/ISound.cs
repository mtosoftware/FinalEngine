// <copyright file="ISound.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio;

using FinalEngine.Resources;

/// <summary>
/// Defines an interface that defines a sound or audio source.
/// </summary>
///
/// <remarks>
/// The <see cref="ISound" /> interface expands upon the capabilities of <see cref="IResource" />, empowering developers with enhanced control over sound instantiation through <see cref="IResourceManager" /> instances.
/// </remarks>
/// <seealso cref="IResource" />
public interface ISound : IResource
{
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ISound"/> is set to loop.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if this <see cref="ISound"/> is set to loop; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <remarks>
    /// The <see cref="IsLooping"/> property determines whether the implementation should restart playback from the beginning once it reaches the end.
    /// </remarks>
    bool IsLooping { get; set; }

    /// <summary>
    /// Gets or sets the volume of this <see cref="ISound"/>.
    /// </summary>
    ///
    /// <value>
    /// A <see cref="float"/> value that represents the volume of this <see cref="ISound"/>.
    /// </value>
    ///
    /// <remarks>
    /// The <see cref="Volume"/> property's implementation should handle values within the range of 0 to 100. Values outside this range should be adjusted to fit within it.
    /// </remarks>
    float Volume { get; set; }

    /// <summary>
    /// Pauses playback of this <see cref="ISound"/>.
    /// </summary>
    ///
    /// <remarks>
    /// When the <see cref="Pause"/> method is implemented, it should halt audio playback while retaining the current position. Resuming playback through <see cref="Play"/> should continue from where the sound was paused.
    /// </remarks>
    void Pause();

    /// <summary>
    /// Starts or resumes playback of this <see cref="ISound"/>.
    /// </summary>
    ///
    /// <remarks>
    /// The <see cref="Play"/> method's implementation should initiate or resume audio playback from its current position. If the sound was previously paused using the <see cref="Pause"/> method, invoking <see cref="Play"/> should continue playback from where it was paused.
    /// </remarks>
    void Play();

    /// <summary>
    /// Stops playback of this <see cref="ISound"/> and resets its position to the beginning.
    /// </summary>
    ///
    /// <remarks>
    /// The <see cref="Stop"/> method's implementation should halt audio playback and reset its position to the beginning. Subsequent calls to the <see cref="Play"/> method should cause the sound to begin playing from the start.
    /// </remarks>
    void Stop();
}
