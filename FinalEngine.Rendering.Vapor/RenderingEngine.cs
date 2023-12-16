// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Data;
using FinalEngine.Rendering.Vapor.Geometry;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly List<Model> models;

    private readonly IRenderDevice renderDevice;

    public RenderingEngine(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

        this.models = [];

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            ReadEnabled = true,
        });

        this.renderDevice.Rasterizer.SetRasterState(new RasterStateDescription()
        {
            CullEnabled = true,
            CullMode = FaceCullMode.Back,
            WindingDirection = WindingDirection.CounterClockwise,
            FillMode = RasterMode.Solid,
            MultiSamplingEnabled = true,
            ScissorEnabled = false,
        });
    }

    public ICamera? Camera { get; set; }

    public void AddModel(Model model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));
        this.models.Add(model);

        Console.WriteLine(this.models.Count.ToString());
    }

    public void RemoveModel(Guid entityId)
    {
        var model = this.models.FirstOrDefault(x =>
        {
            return x.EntityId == entityId;
        }) ?? throw new ArgumentException($"Failed to locate model from entity with ID: '{entityId}'.", nameof(entityId));

        this.models.Remove(model);
    }

    public void Render()
    {
        this.renderDevice.Clear(Color.Black);

        this.RenderScene();
    }

    private void RenderScene()
    {
        if (this.Camera == null)
        {
            return;
        }

        foreach (var model in this.models)
        {
            this.UpdateUniforms(model.Transform, model.Material);

            var mesh = model.Mesh;
            var material = model.Material;

            if (mesh == null)
            {
                continue;
            }

            material.Bind(this.renderDevice.Pipeline);
            mesh.Bind(this.renderDevice.InputAssembler);
            mesh.Draw(this.renderDevice);
        }
    }

    private void UpdateUniforms(Transform transform, IMaterial material)
    {
        var pipeline = this.renderDevice.Pipeline;

        if (this.Camera == null)
        {
            return;
        }

        pipeline.SetUniform("u_projection", this.Camera.Projection);
        pipeline.SetUniform("u_view", this.Camera.View);
        pipeline.SetUniform("u_transform", transform.CreateTransformationMatrix());
        pipeline.SetUniform("u_viewPosition", this.Camera.Transform.Position);

        pipeline.SetUniform("u_material.diffuseTexture", 0);
        pipeline.SetUniform("u_material.specularTexture", 1);
        pipeline.SetUniform("u_material.normalTexture", 2);
        pipeline.SetUniform("u_material.shininess", material.Shininess);
    }
}
