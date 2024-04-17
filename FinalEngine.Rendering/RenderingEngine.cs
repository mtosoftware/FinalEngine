// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Drawing;
using System.IO.Abstractions;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Renderers;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly ILightRenderer lightRenderer;

    private readonly IRenderCoordinator renderCoordinator;

    private readonly IRenderDevice renderDevice;

    private readonly ISceneRenderer sceneRenderer;

    public RenderingEngine(
        IFileSystem fileSystem,
        IRenderDevice renderDevice,
        ILightRenderer lightRenderer,
        ISceneRenderer sceneRenderer,
        IRenderCoordinator renderCoordinator)
    {
        ArgumentNullException.ThrowIfNull(fileSystem, nameof(fileSystem));

        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.lightRenderer = lightRenderer ?? throw new ArgumentNullException(nameof(lightRenderer));
        this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));
        this.renderCoordinator = renderCoordinator ?? throw new ArgumentNullException(nameof(renderCoordinator));

        this.ClearColor = Color.FromArgb(255, 30, 30, 30);
    }

    public Color ClearColor { get; set; }

    public void Render(ICamera camera)
    {
        ArgumentNullException.ThrowIfNull(camera, nameof(camera));

        this.renderDevice.Pipeline.SetFrameBuffer(null);
        this.renderDevice.Rasterizer.SetViewport(camera.Bounds);
        this.renderDevice.Clear(this.ClearColor);

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            ReadEnabled = true,
        });

        this.renderDevice.Rasterizer.SetRasterState(new RasterStateDescription()
        {
            CullEnabled = true,
            CullMode = FaceCullMode.Back,
            WindingDirection = WindingDirection.CounterClockwise,
            MultiSamplingEnabled = true,
        });

        this.RenderScene(camera);

        this.renderCoordinator.ClearQueues();
    }

    private void RenderScene(ICamera camera)
    {
        if (!this.renderCoordinator.CanRenderGeometry)
        {
            return;
        }

        if (!this.renderCoordinator.CanRenderLights)
        {
            this.sceneRenderer.Render(camera, true);
        }
        else
        {
            this.lightRenderer.Render(() =>
            {
                this.sceneRenderer.Render(camera, false);
            });
        }
    }
}
