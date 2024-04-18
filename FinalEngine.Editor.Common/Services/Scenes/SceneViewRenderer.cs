// <copyright file="SceneViewRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Drawing;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Renderers;
using FinalEngine.Resources;

public sealed class SceneViewRenderer : ISceneViewRenderer
{
    private readonly ICamera camera;

    private readonly IRenderingEngine renderingEngine;

    private readonly IRenderPipeline renderPipeline;

    private readonly IRenderQueue<RenderModel> renderQueue;

    private readonly ISceneManager sceneManager;

    private bool isInitialized;

    private Model model;

    public SceneViewRenderer(
        IRenderPipeline renderPipeline,
        ICamera camera,
        ISceneManager sceneManager,
        IRenderingEngine renderingEngine,
        IRenderQueue<RenderModel> renderQueue)
    {
        this.renderPipeline = renderPipeline ?? throw new ArgumentNullException(nameof(renderPipeline));
        this.camera = camera ?? throw new ArgumentNullException(nameof(camera));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
        this.renderingEngine = renderingEngine ?? throw new ArgumentNullException(nameof(renderingEngine));
        this.renderQueue = renderQueue ?? throw new ArgumentNullException(nameof(renderQueue));
    }

    public void AdjustView(int width, int height)
    {
        this.camera.Bounds = new Rectangle(0, 0, width, height);
    }

    public void Render()
    {
        this.Initialize();

        if (this.model.RenderModel != null)
        {
            this.renderQueue.Enqueue(this.model.RenderModel);
        }

        if (this.model.Children != null)
        {
            foreach (var child in this.model.Children)
            {
                if (child.RenderModel == null)
                {
                    continue;
                }

                this.renderQueue.Enqueue(child.RenderModel);
            }
        }

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

        this.model = ResourceManager.Instance.LoadResource<Model>("Resources\\Models\\Sponza\\Sponza.obj");

        this.isInitialized = true;
    }
}
