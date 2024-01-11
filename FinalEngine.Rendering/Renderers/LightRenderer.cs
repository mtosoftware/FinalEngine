// <copyright file="LightRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

using System;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;

public sealed class LightRenderer : ILightRenderer
{
    private readonly IRenderDevice renderDevice;

    private IShaderProgram? ambientProgram;

    private IShaderProgram? directionalProgram;

    private IShaderProgram? pointProgram;

    private IShaderProgram? spotProgram;

    public LightRenderer(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    private IShaderProgram AmbientProgram
    {
        get { return this.ambientProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Lighting\\lighting-ambient.fesp"); }
    }

    private IShaderProgram DirectionalProgram
    {
        get { return this.directionalProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Lighting\\lighting-directional.fesp"); }
    }

    private IShaderProgram PointProgram
    {
        get { return this.pointProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Lighting\\lighting-point.fesp"); }
    }

    private IShaderProgram SpotProgram
    {
        get { return this.spotProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Lighting\\lighting-spot.fesp"); }
    }

    public void Conclude()
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

    public void Prepare()
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

    public void Render(Light light)
    {
        ArgumentNullException.ThrowIfNull(light, nameof(light));

        switch (light.Type)
        {
            case LightType.Directional:
                this.RenderDirectionalLight(light);
                break;

            case LightType.Point:
                this.RenderPointLight(light);
                break;

            case LightType.Spot:
                this.RenderSpotLight(light);
                break;

            case LightType.Ambient:
                this.RenderAmbientLight();
                break;

            default:
                throw new NotSupportedException($"The specified {nameof(light)} is not supported by the {nameof(LightRenderer)}.");
        }

        this.renderDevice.Pipeline.SetUniform("u_light.base.color", light.Color);
        this.renderDevice.Pipeline.SetUniform("u_light.base.intensity", light.Intensity);
    }

    private void RenderAmbientLight()
    {
        this.renderDevice.Pipeline.SetShaderProgram(this.AmbientProgram);
    }

    private void RenderAttenuation(Light light)
    {
        this.renderDevice.Pipeline.SetUniform("u_light.attenuation.constant", light.Attenuation.Constant);
        this.renderDevice.Pipeline.SetUniform("u_light.attenuation.linear", light.Attenuation.Linear);
        this.renderDevice.Pipeline.SetUniform("u_light.attenuation.quadratic", light.Attenuation.Quadratic);
    }

    private void RenderDirectionalLight(Light light)
    {
        this.renderDevice.Pipeline.SetShaderProgram(this.DirectionalProgram);
        this.renderDevice.Pipeline.SetUniform("u_light.direction", light.Transform.Forward);
    }

    private void RenderPointLight(Light light)
    {
        this.renderDevice.Pipeline.SetShaderProgram(this.PointProgram);

        this.RenderAttenuation(light);
        this.renderDevice.Pipeline.SetUniform("u_light.position", light.Transform.Position);
    }

    private void RenderSpotLight(Light light)
    {
        this.renderDevice.Pipeline.SetShaderProgram(this.SpotProgram);

        this.RenderAttenuation(light);

        this.renderDevice.Pipeline.SetUniform("u_light.position", light.Transform.Position);
        this.renderDevice.Pipeline.SetUniform("u_light.direction", light.Transform.Forward);
        this.renderDevice.Pipeline.SetUniform("u_light.radius", light.Radius);
        this.renderDevice.Pipeline.SetUniform("u_light.outerRadius", light.OuterRadius);
    }
}
