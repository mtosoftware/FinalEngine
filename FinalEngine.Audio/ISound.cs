// <copyright file="ISound.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio;

using System.Diagnostics.CodeAnalysis;
using FinalEngine.Resources;

/// <summary>
/// Defines an interface that represents a sound.
/// </summary>
public interface ISound : IResource
{
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ISound"/> is looping.
    /// </summary>
    /// <value>
    /// <c>true</c> if this <see cref="ISound"/> is looping; otherwise, <c>false</c>.
    /// </value>
    bool IsLooping { get; set; }

    /// <summary>
    /// Gets or sets the volume of this <see cref="ISound"/>.
    /// </summary>
    /// <value>
    /// The volume of this <see cref="ISound"/>.
    /// </value>
    /// <remarks>
    /// The <see cref="Volume"/> property should be set within a range of 0-1.
    /// </remarks>
    float Volume { get; set; }

    /// <summary>
    /// Pauses this <see cref="ISound"/>.
    /// </summary>
    void Pause();

    /// <summary>
    /// Starts (plays) this <see cref="ISound"/>.
    /// </summary>
    void Start();

    /// <summary>
    /// Stops this <see cref="ISound"/> and resets the start position.
    /// </summary>
    /// <remarks>
    /// If you wish to pause a sound and later play it from it's current position use <see cref="Pause"/>.
    /// </remarks>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Required by API")]
    void Stop();
}
