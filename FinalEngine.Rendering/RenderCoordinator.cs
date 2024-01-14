// <copyright file="RenderCoordinator.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.Renderers;

public sealed class RenderCoordinator : IRenderCoordinator
{
    private readonly IRenderQueue<Light> lightRenderQueue;

    private readonly IRenderQueue<RenderModel> modelRenderQueue;

    public RenderCoordinator(IRenderQueue<RenderModel> modelRenderQueue, IRenderQueue<Light> lightRenderQueue)
    {
        this.modelRenderQueue = modelRenderQueue ?? throw new ArgumentNullException(nameof(modelRenderQueue));
        this.lightRenderQueue = lightRenderQueue ?? throw new ArgumentNullException(nameof(lightRenderQueue));
    }

    public bool CanRenderGeometry
    {
        get { return this.modelRenderQueue.CanRender; }
    }

    public bool CanRenderLights
    {
        get { return this.lightRenderQueue.CanRender; }
    }

    public void ClearQueues()
    {
        this.modelRenderQueue.Clear();
        this.lightRenderQueue.Clear();
    }
}
