// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Core;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Renderers;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly Light ambientLight;


    private readonly IGeometryRenderer geometryRenderer;

    private readonly ILightRenderer lightRenderer;

    private readonly Dictionary<LightType, IEnumerable<Light>> lightTypeToLightMap;

    private readonly Dictionary<Model, IEnumerable<Transform>> modelToTransformationMap;

    private readonly IRenderDevice renderDevice;

    private IShaderProgram? geometryProgram;

    private readonly Light sunLight;

    private IShaderProgram? shadowMapProgram;
    private IFrameBuffer depthFrameBuffer;

    public RenderingEngine(IRenderDevice renderDevice, IGeometryRenderer geometryRenderer, ILightRenderer lightRenderer)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.geometryRenderer = geometryRenderer ?? throw new ArgumentNullException(nameof(geometryRenderer));
        this.lightRenderer = lightRenderer ?? throw new ArgumentNullException(nameof(lightRenderer));

        this.modelToTransformationMap = [];
        this.lightTypeToLightMap = [];

        this.ambientLight = new Light() { Type = LightType.Ambient, Intensity = 0.1f, };

        this.sunLight = new Light()
        {
            Intensity = 3f,
            Type = LightType.Directional, Color = new Vector3(0.9f,0.9f,0.9f), Direction = new Vector3(0, -1f, 0.3f),
            Position = new Vector3(0,1000,0)
        };
        var colorTarget =
            renderDevice.Factory.CreateTexture2D<byte>(new Texture2DDescription() { Width = 1024, Height = 1024 },
                null);
        var depthTarget = renderDevice.Factory.CreateTexture2D<float>(
            new Texture2DDescription() { Width = 1024, Height = 1024, PixelType = PixelType.Float }, null,
            PixelFormat.Depth, SizedFormat.Depth);
        this.depthFrameBuffer = renderDevice.Factory.CreateFrameBuffer(new[] { colorTarget }, depthTarget);
    }

    private IShaderProgram GeometryProgram
    {
        get
        {
            return this.geometryProgram ??=
                ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\standard-geometry.fesp");
        }
    }

    private IShaderProgram ShadowMapProgram
    {
        get
        {
            return this.shadowMapProgram ??=
                ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\shadowMap.fesp");
        }
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
        this.Enqueue(this.sunLight);
        ArgumentNullException.ThrowIfNull(camera, nameof(camera));

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription() { ReadEnabled = true, });

        this.renderDevice.Rasterizer.SetRasterState(new RasterStateDescription()
        {
            CullEnabled = true,
            CullMode = FaceCullMode.Back,
            WindingDirection = WindingDirection.CounterClockwise,
            MultiSamplingEnabled = true,
        });

        var oldViewport = this.renderDevice.Rasterizer.GetViewport();
        this.renderDevice.Pipeline.SetFrameBuffer(this.depthFrameBuffer);
        this.renderDevice.Clear(Color.Black);
        this.renderDevice.Pipeline.SetShaderProgram(this.ShadowMapProgram);
        this.renderDevice.Rasterizer.SetViewport(new Rectangle(0,0,1024,1024));
        this.UpdateLight(this.sunLight);
        this.geometryRenderer.Render(this.modelToTransformationMap);



        this.renderDevice.Pipeline.SetFrameBuffer(null);
        this.renderDevice.Rasterizer.SetViewport(oldViewport);
        this.renderDevice.Clear(Color.Black);

        if (this.lightTypeToLightMap.Count <= 0)
        {
            this.renderDevice.Pipeline.SetShaderProgram(this.GeometryProgram);
            this.RenderScene(camera);
        }
        else
        {
            this.lightRenderer.Render(this.ambientLight);
            this.RenderScene(camera);

            foreach (var kvp in this.lightTypeToLightMap)
            {
                var type = kvp.Key;
                var batch = kvp.Value;

                this.PrepareLightingPass();

                foreach (var light in batch)
                {
                    if (light.Type == LightType.Ambient)
                    {
                        continue;
                    }

                    if (light.Type == LightType.Directional)
                    {
                        this.UpdateLight(this.sunLight);
                        this.renderDevice.Pipeline.SetTexture(this.depthFrameBuffer.DepthTarget!,3);
                    }
                    this.lightRenderer.Render(light);
                    this.RenderScene(camera);
                }
            }

            this.FinalizeLightingPass();
        }

        this.lightTypeToLightMap.Clear();
        this.modelToTransformationMap.Clear();
    }

    public void SetAmbientLight(Vector3 color, float intensity)
    {
        this.ambientLight.Color = color;
        this.ambientLight.Intensity = intensity;
    }

    private void FinalizeLightingPass()
    {
        this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription() { Enabled = false, });

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            ReadEnabled = true, ComparisonMode = ComparisonMode.Less, WriteEnabled = true,
        });
    }

    private void PrepareLightingPass()
    {
        this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription()
        {
            Enabled = true, SourceMode = BlendMode.One, DestinationMode = BlendMode.One,
        });

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            ReadEnabled = true, WriteEnabled = false, ComparisonMode = ComparisonMode.Equal,
        });
    }

    private void RenderScene(ICamera camera)
    {
        this.UpdateCamera(camera);
        this.geometryRenderer.Render(this.modelToTransformationMap);
    }

    private void UpdateCamera(ICamera camera)
    {
        this.renderDevice.Pipeline.SetUniform("u_projection", camera.Projection);
        this.renderDevice.Pipeline.SetUniform("u_view", camera.View);
        this.renderDevice.Pipeline.SetUniform("u_viewPosition", camera.Transform.Position);
    }

    private void UpdateLight(Light light)
    {
        var lightView = Matrix4x4.CreateLookTo(light.Position, light.Direction, Vector3.UnitY);
        var lightProjection = Matrix4x4.CreateOrthographicOffCenter(-1000, 1000, -1000, 1000, 1, 75000f);
        this.renderDevice.Pipeline.SetUniform("u_lightProjection", lightProjection);
        this.renderDevice.Pipeline.SetUniform("u_lightView", lightView);
    }
}
