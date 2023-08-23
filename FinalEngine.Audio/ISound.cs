// <copyright file="ISound.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio;

using FinalEngine.Resources;

/// <summary>
/// Represents an interface that defines a sound or audio source.
/// </summary>
///
/// <remarks>
/// The <see cref="ISound" /> interface expands upon the capabilities of <see cref="IResource" />, empowering developers with enhanced control over sound instantiation through <see cref="IResourceManager" /> instances.
/// </remarks>
///
/// <example>
/// Below you'll find an example showing how to typically instantiate an instance of <see cref="ISound" />. This example assumes that the following criteria has been met:
///
/// <list type="bullet">
///     <item>
///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
///     </item>
///     <item>
///         A <see cref="ResourceLoaderBase{TResource}" /> has been implemented for the <see cref="ISound" /> resource type and registered to the <see cref="ResourceManager.Instance" />.
///     </item>
/// </list>
///
/// <code>
/// // Load the resource via the resource manager.
/// // This is how you should typically instantiate implementations that implement IResource.
/// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
///
/// // Now, we can adjust the volume (range is between 0-100).
/// sound.Volume = 80.0f;
///
/// // We can also set whether the audio should loop (false, by default).
/// sound.IsLooping = true;
///
/// // Finally, let's play the sound.
/// sound.Play();
/// </code>
/// </example>
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
    ///
    /// <example>
    /// Below you'll find an example showing how to instantiate and loop an <see cref="ISound"/> instance. This example assumes that the following criteria has been met:
    ///
    /// <list type="bullet">
    ///     <item>
    ///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
    ///     </item>
    ///     <item>
    ///         A <see cref="ResourceLoaderBase{TResource}" /> has been implemented for the <see cref="ISound" /> resource type and registered to the <see cref="ResourceManager.Instance" />.
    ///     </item>
    /// </list>
    ///
    /// <code>
    /// // Load the resource via the resource manager.
    /// // This is how you should typically instantiate implementations that implement IResource.
    /// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Modify the property to ensure that the sound will loop.
    /// sound.IsLooping = true;
    ///
    /// // Play the sound
    /// sound.Play();
    /// </code>
    /// </example>
    bool IsLooping { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="float"/> value representing the volume of this <see cref="ISound"/>.
    /// </summary>
    ///
    /// <value>
    /// A <see cref="float"/> value representing the volume of this <see cref="ISound"/>.
    /// </value>
    ///
    /// <remarks>
    /// The <see cref="Volume"/> property's implementation should handle values within the range of 0 to 100. Values outside this range should be adjusted to fit within it.
    /// </remarks>
    ///
    /// <example>
    /// Below you'll find an example showing how to adjust the <see cref="Volume"/> of an <see cref="ISound"/> instance. This example assumes that the following criteria has been met:
    ///
    /// <list type="bullet">
    ///     <item>
    ///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
    ///     </item>
    ///     <item>
    ///         A <see cref="ResourceLoaderBase{TResource}" /> has been implemented for the <see cref="ISound" /> resource type and registered to the <see cref="ResourceManager.Instance" />.
    ///     </item>
    /// </list>
    ///
    /// <code>
    /// // Load the resource via the resource manager.
    /// // This is how you should typically instantiate implementations that implement IResource.
    /// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Adjust the volume to be 50%.
    /// sound.Volume = 50.0f;
    ///
    /// // Play the sound.
    /// sound.Play();
    /// </code>
    /// </example>
    float Volume { get; set; }

    /// <summary>
    /// Pauses playback of this <see cref="ISound"/>.
    /// </summary>
    ///
    /// <remarks>
    /// When the <see cref="Pause"/> method is implemented, it should halt audio playback while retaining the current position. Resuming playback through <see cref="Play"/> should continue from where the sound was paused.
    /// </remarks>
    ///
    /// <example>
    /// Below you'll find an example showing how to <see cref="Pause"/> an <see cref="ISound"/> instance. This example assumes that the following criteria has been met:
    ///
    /// <list type="bullet">
    ///     <item>
    ///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
    ///     </item>
    ///     <item>
    ///         A <see cref="ResourceLoaderBase{TResource}" /> has been implemented for the <see cref="ISound" /> resource type and registered to the <see cref="ResourceManager.Instance" />.
    ///     </item>
    /// </list>
    ///
    /// <code>
    /// // Load the resource via the resource manager.
    /// // This is how you should typically instantiate implementations that implement IResource.
    /// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Play the sound.
    /// sound.Play();
    ///
    /// var watch = new Stopwatch();
    /// watch.Start();
    ///
    /// // Wait 10 seconds, then pause the sound.
    /// while (watch.ElapsedMilliseconds &lt; 10000)
    /// {
    ///     continue;
    /// }
    ///
    /// sound.Pause();
    ///
    /// // Wait another 5 seconds, then resume playback.
    /// while (watch.ElapsedMilliseconds &lt; 15000)
    /// {
    ///     continue;
    /// }
    ///
    /// sound.Play();
    /// </code>
    /// </example>
    void Pause();

    /// <summary>
    /// Starts or resumes playback of this <see cref="ISound"/>.
    /// </summary>
    ///
    /// <remarks>
    /// The <see cref="Play"/> method's implementation should initiate or resume audio playback from its current position. If the sound was previously paused using the <see cref="Pause"/> method, invoking <see cref="Play"/> should continue playback from where it was paused.
    /// </remarks>
    ///
    /// <example>
    /// Below you'll find an example showing how to <see cref="Play"/> an <see cref="ISound"/> instance. This example assumes that the following criteria has been met:
    ///
    /// <list type="bullet">
    ///     <item>
    ///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
    ///     </item>
    ///     <item>
    ///         A <see cref="ResourceLoaderBase{TResource}" /> has been implemented for the <see cref="ISound" /> resource type and registered to the <see cref="ResourceManager.Instance" />.
    ///     </item>
    /// </list>
    ///
    /// <code>
    /// // Load the resource via the resource manager.
    /// // This is how you should typically instantiate implementations that implement IResource.
    /// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Play the sound.
    /// sound.Play();
    /// </code>
    /// </example>
    void Play();

    /// <summary>
    /// Stops playback of this <see cref="ISound"/> and resets its position to the beginning.
    /// </summary>
    ///
    /// <remarks>
    /// The <see cref="Stop"/> method's implementation should halt audio playback and reset its position to the beginning. Subsequent calls to the <see cref="Play"/> method should cause the sound to begin playing from the start.
    /// </remarks>
    ///
    /// <example>
    /// Below you'll find an example showing how to <see cref="Stop"/> an <see cref="ISound"/> instance. This example assumes that the following criteria has been met:
    ///
    /// <list type="bullet">
    ///     <item>
    ///         The user intends to use the singleton implementation of <see cref="IResourceManager" /> (see <see cref="ResourceManager.Instance" />).
    ///     </item>
    ///     <item>
    ///         A <see cref="ResourceLoaderBase{TResource}" /> has been implemented for the <see cref="ISound" /> resource type and registered to the <see cref="ResourceManager.Instance" />.
    ///     </item>
    /// </list>
    ///
    /// <code>
    /// // Load the resource via the resource manager.
    /// // This is how you should typically instantiate implementations that implement IResource.
    /// ISound sound = ResourceManager.Instance.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    /// // Play the sound.
    /// sound.Play();
    ///
    /// var watch = new Stopwatch();
    /// watch.Start();
    ///
    /// // Wait 10 seconds, then stop the sound.
    /// while (watch.ElapsedMilliseconds &lt; 10000)
    /// {
    ///     continue;
    /// }
    ///
    /// sound.Stop();
    /// </code>
    /// </example>
    void Stop();
}
