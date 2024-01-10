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
using FinalEngine.Resources;
using FinalEngine.Rendering.Textures;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly Light ambientLight;

    private readonly IGeometryRenderer geometryRenderer;

    private readonly ILightRenderer lightRenderer;
    private readonly ISkyboxRenderer skyboxRenderer;

    private readonly Dictionary<LightType, IEnumerable<Light>> lightTypeToLightMap;

    private readonly Dictionary<Model, IEnumerable<Transform>> modelToTransformationMap;

    private readonly IRenderDevice renderDevice;

    private IShaderProgram? geometryProgram;

    private ICubeTexture cubeTexture;

    public RenderingEngine(IRenderDevice renderDevice, IGeometryRenderer geometryRenderer, ILightRenderer lightRenderer,ISkyboxRenderer skyboxRenderer)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.geometryRenderer = geometryRenderer ?? throw new ArgumentNullException(nameof(geometryRenderer));
        this.lightRenderer = lightRenderer ?? throw new ArgumentNullException(nameof(lightRenderer));
        this.skyboxRenderer = skyboxRenderer;

        this.modelToTransformationMap = [];
        this.lightTypeToLightMap = [];

        this.ambientLight = new Light()
        {
            Type = LightType.Ambient,
            Intensity = 0.1f,
        };

        // Todo input from other project
        var right = ResourceManager.Instance.LoadResource<ITexture2D>("Resources/Textures/skybox/right.png");
        var left = ResourceManager.Instance.LoadResource<ITexture2D>("Resources/Textures/skybox/left.png");
        var top = ResourceManager.Instance.LoadResource<ITexture2D>("Resources/Textures/skybox/top.png");
        var bottom = ResourceManager.Instance.LoadResource<ITexture2D>("Resources/Textures/skybox/bottom.png");
        var front = ResourceManager.Instance.LoadResource<ITexture2D>("Resources/Textures/skybox/front.png");
        var back = ResourceManager.Instance.LoadResource<ITexture2D>("Resources/Textures/skybox/back.png");
        this.cubeTexture =
            renderDevice.Factory.CreateCubeTexture(new CubeTextureDescription(){Width = right.Description.Width, Height = right.Description.Height, WrapR = TextureWrapMode.Clamp,WrapS = TextureWrapMode.Clamp, WrapT = TextureWrapMode.Clamp },
                right, left, top, bottom, back, front);

        this.renderDevice.Initialize();
    }

    private IShaderProgram GeometryProgram
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

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            ReadEnabled = true,
            WriteEnabled = true,
            ComparisonMode = ComparisonMode.Less
        });

        this.renderDevice.Rasterizer.SetRasterState(new RasterStateDescription()
        {
            CullEnabled = true,
            CullMode = FaceCullMode.Back,
            WindingDirection = WindingDirection.CounterClockwise,
            MultiSamplingEnabled = true,
        });

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

                foreach (var light in batch)
                {
                    if (light.Type == LightType.Ambient)
                    {
                        continue;
                    }

                    this.PrepareLightingPass();
                    this.lightRenderer.Render(light);
                    this.RenderScene(camera);
                }
            }

            this.FinalizeLightingPass();
        }

        this.skyboxRenderer.Render(this.cubeTexture, camera);
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
    }

    private void PrepareLightingPass()
    {
        this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription()
        {
            Enabled = true,
            SourceMode = BlendMode.One,
            DestinationMode = BlendMode.One,
        });

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            ReadEnabled = true,
            WriteEnabled = false,
            ComparisonMode = ComparisonMode.Equal,
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
}
