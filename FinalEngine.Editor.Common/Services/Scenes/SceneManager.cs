// <copyright file="SceneManager.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using FinalEngine.Editor.Common.Models.Scenes;

/// <summary>
/// Provides a standard implementation of an <see cref="ISceneManager"/>.
/// </summary>
/// <seealso cref="ISceneManager" />
public sealed class SceneManager : ISceneManager
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SceneManager"/> class.
    /// </summary>
    /// <param name="scene">
    /// The scene to be managed.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="scene"/> parameter cannot be null.
    /// </exception>
    public SceneManager(IScene scene)
    {
        this.ActiveScene = scene ?? throw new ArgumentNullException(nameof(scene));
    }

    /// <inheritdoc/>
    public IScene ActiveScene { get; }
}
