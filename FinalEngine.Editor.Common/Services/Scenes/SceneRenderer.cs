// <copyright file="SceneRenderer.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Drawing;
using FinalEngine.Rendering;

/// <summary>
/// Provides a standard implementation of an <see cref="ISceneRenderer"/>.
/// </summary>
/// <seealso cref="ISceneRenderer" />
public sealed class SceneRenderer : ISceneRenderer
{
    /// <summary>
    /// The render device.
    /// </summary>
    private readonly IRenderDevice renderDevice;

    /// <summary>
    /// The scene manager, used to retrieve and render the currently active scene.
    /// </summary>
    private readonly ISceneManager sceneManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneRenderer"/> class.
    /// </summary>
    /// <param name="renderDevice">
    /// The render device.
    /// </param>
    /// <param name="sceneManager">
    /// The scene manager, used to retrieve and render the currently active scene.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="renderDevice"/> parameter cannot be null.
    /// </exception>
    public SceneRenderer(IRenderDevice renderDevice, ISceneManager sceneManager)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
    }

    /// <inheritdoc/>
    public void Render()
    {
        this.renderDevice.Clear(Color.FromArgb(255, 30, 30, 30));
        this.sceneManager.ActiveScene.Render();
    }
}
