// <copyright file="ViewRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Drawing;
using FinalEngine.Editor.Common.Services.Scenes.Cameras;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Renderers;

public sealed class ViewRenderer : IViewRenderer
{
    private readonly EditorCamera camera;

    private readonly IRenderDevice renderDevice;

    private readonly IRenderingEngine renderingEngine;

    private readonly IRenderPipeline renderPipeline;

    private readonly ISceneManager sceneManager;

    private bool isInitialized;

    public ViewRenderer(IRenderPipeline renderPipeline, IRenderDevice renderDevice, ISceneManager sceneManager, IRenderingEngine renderingEngine, IRenderQueue<Model> renderQueue)
    {
        this.renderPipeline = renderPipeline ?? throw new ArgumentNullException(nameof(renderPipeline));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
        this.renderingEngine = renderingEngine ?? throw new ArgumentNullException(nameof(renderingEngine));

        this.camera = new EditorCamera();
    }

    public void AdjustView(int width, int height)
    {
        this.camera.ViewportWidth = width;
        this.camera.ViewportHeight = height;

        this.renderDevice.Rasterizer.SetViewport(new Rectangle(0, 0, width, height));
    }

    public void Render()
    {
        this.Initialize();

        this.sceneManager.ActiveScene.Render();
        this.renderingEngine.Render(this.camera);
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
