// <copyright file="SceneRenderer.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Rendering;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering;
using Microsoft.Extensions.Logging;

public sealed class SceneRenderer : ISceneRenderer
{
    private readonly ILogger<SceneRenderer> logger;

    private readonly IRenderDevice renderDevice;

    public SceneRenderer(ILogger<SceneRenderer> logger, IRenderDevice renderDevice)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    public void ChangeProjection(int projectionWidth, int projectionHeight)
    {
        this.logger.LogDebug($"Changing projection to: ({projectionWidth}, {projectionHeight}).");

        this.renderDevice.Pipeline.SetUniform("u_projection", Matrix4x4.CreateOrthographicOffCenter(0, projectionWidth, 0, projectionHeight, -1, 1));
        this.renderDevice.Rasterizer.SetViewport(new Rectangle(0, 0, projectionWidth, projectionHeight));
    }

    public void Render()
    {
        this.renderDevice.Clear(Color.FromArgb(255, 30, 30, 30));
    }
}
