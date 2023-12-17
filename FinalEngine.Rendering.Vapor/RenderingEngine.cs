// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Geometry;
using FinalEngine.Rendering.Vapor.Lighting;
using FinalEngine.Rendering.Vapor.Renderers;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly IGeometryRenderer geometryRenderer;

    private readonly ILightRenderer lightRenderer;

    private readonly List<Light> lights;

    private readonly Dictionary<Model, IEnumerable<Transform>> modelToTransformationMap;

    private readonly IRenderDevice renderDevice;

    public RenderingEngine(IRenderDevice renderDevice, IGeometryRenderer geometryRenderer, ILightRenderer lightRenderer)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.geometryRenderer = geometryRenderer ?? throw new ArgumentNullException(nameof(geometryRenderer));
        this.lightRenderer = lightRenderer ?? throw new ArgumentNullException(nameof(lightRenderer));

        this.modelToTransformationMap = [];
        this.lights = [];
    }

    public void Enqueue(Model model, Transform transform)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));
        ArgumentNullException.ThrowIfNull(transform, nameof(transform));

        if (!this.modelToTransformationMap.TryGetValue(model, out var batch))
        {
            batch = new List<Transform>();
            this.modelToTransformationMap.Add(model, batch);
        }

        ((IList<Transform>)batch).Add(transform);
    }

    public void Enqueue(Light light)
    {
        ArgumentNullException.ThrowIfNull(light, nameof(light));

        if (this.lights.Contains(light))
        {
            throw new ArgumentException($"The specified {nameof(light)} parameter has already been enqueued with this {nameof(RenderingEngine)}.", nameof(light));
        }

        this.lights.Add(light);
    }

    public void Render(ICamera camera)
    {
        ArgumentNullException.ThrowIfNull(camera, nameof(camera));

        //this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription()
        //{
        //    Enabled = true,
        //    SourceMode = BlendMode.One,
        //    DestinationMode = BlendMode.One,
        //});

        //this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        //{
        //    WriteEnabled = false,
        //    ComparisonMode = ComparisonMode.Equal,
        //});

        //GL.Enable(EnableCap.Blend);
        //GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
        //GL.DepthMask(false);
        //GL.DepthFunc(DepthFunction.Equal);

        foreach (var light in this.lights)
        {
            this.lightRenderer.Render(light);

            this.renderDevice.Pipeline.SetUniform("u_projection", camera.Projection);
            this.renderDevice.Pipeline.SetUniform("u_view", camera.View);
            this.renderDevice.Pipeline.SetUniform("u_viewPosition", camera.Transform.Position);

            this.geometryRenderer.Render(this.modelToTransformationMap);
        }

        //GL.DepthFunc(DepthFunction.Less);
        //GL.DepthMask(true);
        //GL.Disable(EnableCap.Blend);

        //this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription()
        //{
        //    Enabled = false,
        //});

        //this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        //{
        //    ReadEnabled = true,
        //    ComparisonMode = ComparisonMode.Less,
        //    WriteEnabled = true,
        //});

        this.lights.Clear();
        this.modelToTransformationMap.Clear();
    }
}
