// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Drawing;
using System.IO.Abstractions;
using FinalEngine.Rendering.Core;
using FinalEngine.Rendering.Renderers;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly ILightRenderer lightRenderer;

    private readonly IPostRenderer postRenderer;

    private readonly IRenderCoordinator renderCoordinator;

    private readonly IRenderDevice renderDevice;

    private readonly ISceneRenderer sceneRenderer;

    private readonly ISkyboxRenderer skyboxRenderer;

    public RenderingEngine(
        IFileSystem fileSystem,
        IRenderDevice renderDevice,
        ILightRenderer lightRenderer,
        ISkyboxRenderer skyboxRenderer,
        ISceneRenderer sceneRenderer,
        IPostRenderer postRenderer,
        IRenderCoordinator renderCoordinator)
    {
        ArgumentNullException.ThrowIfNull(fileSystem, nameof(fileSystem));

        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.lightRenderer = lightRenderer ?? throw new ArgumentNullException(nameof(lightRenderer));
        this.skyboxRenderer = skyboxRenderer ?? throw new ArgumentNullException(nameof(skyboxRenderer));
        this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));
        this.postRenderer = postRenderer ?? throw new ArgumentNullException(nameof(postRenderer));
        this.renderCoordinator = renderCoordinator ?? throw new ArgumentNullException(nameof(renderCoordinator));

        this.renderDevice.Pipeline.AddShaderHeader("lighting", fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\lighting.glsl"));
        this.renderDevice.Pipeline.AddShaderHeader("material", fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\material.glsl"));
        this.renderDevice.Pipeline.AddShaderHeader("effects", fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\effects.glsl"));

        this.renderDevice.Initialize();
    }

    public void Render(ICamera camera)
    {
        ArgumentNullException.ThrowIfNull(camera, nameof(camera));

        this.renderDevice.Pipeline.SetFrameBuffer(null);
        this.renderDevice.Rasterizer.SetViewport(camera.Bounds);
        this.renderDevice.Clear(Color.Black);

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

        if (!this.renderCoordinator.CanRenderEffects)
        {
            this.RenderScene(camera);
        }
        else
        {
            this.postRenderer.Render(camera, () =>
            {
                this.RenderScene(camera);
            });
        }

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

        this.skyboxRenderer.Render(camera);
    }
}
