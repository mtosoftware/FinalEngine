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

/// <summary>
/// Provides an entity system that draws sprites to the world.
/// </summary>
/// <seealso cref="EntitySystemBase" />
[EntitySystemProcess(ExecutionType = GameLoopType.Render)]
public sealed class SpriteRenderEntitySystem : EntitySystemBase
{
    /// <summary>
    /// The sprite drawer, used to draw entities with sprite components to the scene.
    /// </summary>
    private readonly ISpriteDrawer drawer;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpriteRenderEntitySystem"/> class.
    /// </summary>
    /// <param name="drawer">
    /// The sprite drawer, used to draw entities with sprite components to the scene.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="drawer"/> parameter cannot be null.
    /// </exception>
    public SpriteRenderEntitySystem(ISpriteDrawer drawer)
    {
        this.drawer = drawer ?? throw new ArgumentNullException(nameof(drawer));
    }

    /// <summary>
    /// Determines whether the specified <paramref name="entity" /> matches the aspect of this <see cref="EntitySystemBase" />.
    /// </summary>
    /// <param name="entity">
    /// The entity to check.
    /// </param>
    /// <returns>
    /// <c>true</c> if the specified <paramref name="entity" /> matches the aspect of the system; otherwise, <c>false</c>.
    /// </returns>
    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() &&
               entity.ContainsComponent<SpriteComponent>();
    }

    /// <summary>
    /// Processes the specified <paramref name="entities" />.
    /// </summary>
    /// <param name="entities">
    /// The entities to process.
    /// </param>
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
