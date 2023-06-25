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
using FinalEngine.Rendering.Nodes;

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
               entity.ContainsComponent<MeshComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var model = entity.GetComponent<MeshComponent>();

            this.renderingEngine.EnqueueNode(new MeshNode()
            {
                Mesh = model.Mesh,
                Material = model.Material,
                Transformation = transform.CreateTransformationMatrix(),
            });
        }
    }
}
