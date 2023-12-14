// <copyright file="MeshRenderEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Systems.Geometry;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.Rendering.Vapor.Components.Geometry;
using FinalEngine.Rendering.Vapor.Renderers;

public sealed class MeshRenderEntitySystem : EntitySystemBase, IGeometryRenderer
{
    private readonly IRenderDevice renderDevice;

    public MeshRenderEntitySystem(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    public void Render()
    {
        this.Process();
    }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() &&
               entity.ContainsComponent<ModelComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var model = entity.GetComponent<ModelComponent>();

            if (model.Mesh == null)
            {
                continue;
            }

            this.renderDevice.Pipeline.SetUniform("u_transform", transform.CreateTransformationMatrix());

            model.Material.Bind(this.renderDevice.Pipeline);
            model.Mesh.Bind(this.renderDevice.InputAssembler);
            model.Mesh.Draw(this.renderDevice);
        }
    }
}
