// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor;

using System;
using System.Collections.Generic;
using System.Drawing;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Geometry;
using FinalEngine.Rendering.Vapor.Lighting;
using FinalEngine.Rendering.Vapor.Renderers;
using FinalEngine.Resources;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly IGeometryRenderer geometryRenderer;

    private readonly ILightRenderer lightRenderer;

    private readonly Dictionary<LightType, IEnumerable<Light>> lightTypeToLightMap;

    private readonly Dictionary<Model, IEnumerable<Transform>> modelToTransformationMap;

    private readonly IRenderDevice renderDevice;

    private IShaderProgram? geometryProgram;

    public RenderingEngine(IRenderDevice renderDevice, IGeometryRenderer geometryRenderer, ILightRenderer lightRenderer)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.geometryRenderer = geometryRenderer ?? throw new ArgumentNullException(nameof(geometryRenderer));
        this.lightRenderer = lightRenderer ?? throw new ArgumentNullException(nameof(lightRenderer));

        this.modelToTransformationMap = [];
        this.lightTypeToLightMap = [];
    }

    public IShaderProgram GeometryProgram
    {
        get { return this.geometryProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\standard-geometry.fesp"); }
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

        if (!this.lightTypeToLightMap.TryGetValue(light.Type, out var batch))
        {
            batch = new List<Light>();
            this.lightTypeToLightMap.Add(light.Type, batch);
        }

        ((IList<Light>)batch).Add(light);
    }

    public void Render(ICamera camera)
    {
        ArgumentNullException.ThrowIfNull(camera, nameof(camera));

        this.renderDevice.Clear(Color.Black);

        this.renderDevice.Pipeline.SetShaderProgram(this.GeometryProgram);
        this.geometryRenderer.Render(this.modelToTransformationMap);

        this.renderDevice.Rasterizer.SetRasterState(new RasterStateDescription()
        {
            CullEnabled = true,
            CullMode = FaceCullMode.Back,
            WindingDirection = WindingDirection.CounterClockwise,
            MultiSamplingEnabled = true,
        });

        this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription()
        {
            Enabled = true,
            SourceMode = BlendMode.One,
            DestinationMode = BlendMode.One,
        });

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            WriteEnabled = false,
            ComparisonMode = ComparisonMode.Equal,
        });

        foreach (var lightType in this.lightTypeToLightMap.Keys)
        {
            if (!this.lightTypeToLightMap.TryGetValue(lightType, out var batch))
            {
                continue;
            }

            foreach (var light in batch)
            {
                this.lightRenderer.Render(light);

                this.renderDevice.Pipeline.SetUniform("u_projection", camera.Projection);
                this.renderDevice.Pipeline.SetUniform("u_view", camera.View);
                this.renderDevice.Pipeline.SetUniform("u_viewPosition", camera.Transform.Position);

                this.geometryRenderer.Render(this.modelToTransformationMap);
            }
        }

        this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription()
        {
            Enabled = false,
        });

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            ReadEnabled = true,
            ComparisonMode = ComparisonMode.Less,
            WriteEnabled = true,
        });

        this.lightTypeToLightMap.Clear();
        this.modelToTransformationMap.Clear();
    }
}
