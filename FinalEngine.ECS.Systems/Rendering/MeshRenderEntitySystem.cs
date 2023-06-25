// <copyright file="MeshRenderEntitySystem.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Systems.Rendering;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.ECS.Components.Core;
using FinalEngine.ECS.Components.Rendering;
using FinalEngine.Rendering;

[EntitySystemProcess(ExecutionType = GameLoopType.Render)]
public sealed class MeshRenderEntitySystem : EntitySystemBase, ISceneRenderer
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
               entity.ContainsComponent<MeshComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var model = entity.GetComponent<MeshComponent>();

            var mesh = model.Mesh;
            var material = model.Material;

            if (mesh == null || material == null)
            {
                continue;
            }

            //// TODO: Move this into the appropriae light rendering system.
            this.renderDevice.Pipeline.SetUniform("u_material.diffuseTexture", 0);

            this.renderDevice.Pipeline.SetTexture(material.DiffuseTexture, 0);

            this.renderDevice.Pipeline.SetUniform("u_transform", transform.CreateTransformationMatrix());

            mesh.Bind(this.renderDevice.InputAssembler);
            mesh.Render(this.renderDevice);
        }
    }
}
