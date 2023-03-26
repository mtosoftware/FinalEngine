// <copyright file="SceneRenderer.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Rendering;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="ISceneRenderer"/>.
/// </summary>
/// <seealso cref="ISceneRenderer" />
public sealed class SceneRenderer : ISceneRenderer
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<SceneRenderer> logger;

    /// <summary>
    /// The render device.
    /// </summary>
    private readonly IRenderDevice renderDevice;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneRenderer"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="renderDevice">
    /// The render device.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/> or <paramref name="renderDevice"/> parameter cannot be null.
    /// </exception>
    public SceneRenderer(ILogger<SceneRenderer> logger, IRenderDevice renderDevice)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    /// <inheritdoc/>
    public void ChangeProjection(int projectionWidth, int projectionHeight)
    {
        this.logger.LogDebug($"Changing projection to: ({projectionWidth}, {projectionHeight}).");

        this.renderDevice.Pipeline.SetUniform("u_projection", Matrix4x4.CreateOrthographicOffCenter(0, projectionWidth, 0, projectionHeight, -1, 1));
        this.renderDevice.Rasterizer.SetViewport(new Rectangle(0, 0, projectionWidth, projectionHeight));
    }

    /// <inheritdoc/>
    public void Render()
    {
        this.renderDevice.Clear(Color.FromArgb(255, 30, 30, 30));
    }
}
