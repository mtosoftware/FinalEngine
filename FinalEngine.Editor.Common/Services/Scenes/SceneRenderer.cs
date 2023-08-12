// <copyright file="SceneRenderer.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Drawing;
using FinalEngine.Rendering;

public sealed class SceneRenderer : ISceneRenderer
{
    private readonly IRenderDevice renderDevice;

    public SceneRenderer(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    public void Render()
    {
        this.renderDevice.Clear(Color.FromArgb(255, 30, 30, 30));
    }
}
