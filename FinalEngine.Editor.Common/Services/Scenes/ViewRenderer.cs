// <copyright file="SceneRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Drawing;
using FinalEngine.Rendering;

public sealed class ViewRenderer : IViewRenderer
{
    private readonly IRenderDevice renderDevice;

    private readonly IRenderPipeline renderPipeline;

    private readonly ISceneManager sceneManager;

    private bool isInitialized;

    public ViewRenderer(IRenderPipeline renderPipeline, IRenderDevice renderDevice, ISceneManager sceneManager)
    {
        this.renderPipeline = renderPipeline ?? throw new ArgumentNullException(nameof(renderPipeline));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
    }

    public void Render()
    {
        this.Initialize();

        this.renderDevice.Clear(Color.FromArgb(255, 30, 30, 30));
        this.sceneManager.ActiveScene.Render();
    }

    private void Initialize()
    {
        if (this.isInitialized)
        {
            return;
        }

        this.renderPipeline.Initialize();

        this.isInitialized = true;
    }
}
