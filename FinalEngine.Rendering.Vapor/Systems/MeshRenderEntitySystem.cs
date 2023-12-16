// <copyright file="MeshRenderEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Systems;

using System;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.ECS;
using FinalEngine.Rendering.Vapor.Components;
using FinalEngine.Rendering.Vapor.Data;

public sealed class MeshRenderEntitySystem : EntitySystemBase
{
    private readonly IRenderingEngine renderingEngine;

    public MeshRenderEntitySystem(IRenderingEngine renderingEngine)
    {
        this.renderingEngine = renderingEngine ?? throw new ArgumentNullException(nameof(renderingEngine));
    }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() &&
               entity.ContainsComponent<ModelComponent>();
    }

    protected override void OnEntityAdded(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        var transform = entity.GetComponent<TransformComponent>();
        var model = entity.GetComponent<ModelComponent>();

        this.renderingEngine.AddModel(new Model()
        {
            EntityId = entity.UniqueIdentifier,
            Mesh = model.Mesh,
            Material = model.Material,
            Transform = transform,
        });

        base.OnEntityAdded(entity);
    }

    protected override void OnEntityRemoved(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        this.renderingEngine.RemoveModel(entity.UniqueIdentifier);

        base.OnEntityRemoved(entity);
    }
}
