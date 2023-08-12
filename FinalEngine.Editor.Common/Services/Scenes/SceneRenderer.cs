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
    /// Initializes a new instance of the <see cref="SceneRenderer"/> class.
    /// </summary>
    /// <param name="renderDevice">
    /// The render device.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="renderDevice"/> parameter cannot be null.
    /// </exception>
    public SceneRenderer(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    /// <inheritdoc/>
    public void Render()
    {
        this.renderDevice.Clear(Color.FromArgb(255, 30, 30, 30));
    }
}
