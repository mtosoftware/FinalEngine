// <copyright file="SpriteRenderEntitySystem.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Systems.Rendering;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using FinalEngine.ECS.Components.Core;
using FinalEngine.ECS.Components.Rendering;
using FinalEngine.Rendering;

[EntitySystemProcess(ExecutionType = GameLoopType.Render)]
public sealed class SpriteRenderEntitySystem : EntitySystemBase
{
    private readonly ISpriteDrawer drawer;

    public SpriteRenderEntitySystem(ISpriteDrawer drawer)
    {
        this.drawer = drawer ?? throw new ArgumentNullException(nameof(drawer));
    }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() &&
               entity.ContainsComponent<SpriteComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        this.drawer.Begin();

        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var sprite = entity.GetComponent<SpriteComponent>();

            if (sprite.Texture == null)
            {
                continue;
            }

            var position = new Vector2(
                transform.Position.X,
                transform.Position.Y);

            float rotation = transform.Rotation.X;

            var scale = new Vector2(
                transform.Scale.X,
                transform.Scale.Y);

            this.drawer.Draw(
                sprite.Texture,
                sprite.Color,
                sprite.Origin,
                position,
                rotation,
                scale);
        }

        this.drawer.End();
    }
}
