// <copyright file="SpriteRenderSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.Examples.StarWarriors.Components;
    using FinalEngine.Rendering;

    public sealed class SpriteRenderSystem : EntitySystemBase
    {
        private readonly ISpriteDrawer spriteDrawer;

        public SpriteRenderSystem(ISpriteDrawer spriteDrawer)
        {
            this.spriteDrawer = spriteDrawer ?? throw new ArgumentNullException(nameof(spriteDrawer));
        }

        public override GameLoopType LoopType { get; } = GameLoopType.Render;

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<SpriteComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            this.spriteDrawer.Begin();

            foreach (dynamic entity in entities)
            {
                var transform = entity.Transform;
                var sprite = entity.Sprite;

                if (sprite.Texture == null)
                {
                    continue;
                }

                this.spriteDrawer.Draw(
                    sprite.Texture,
                    sprite.Color,
                    sprite.Origin,
                    transform.Position,
                    0,
                    new Vector2(sprite.Width * sprite.Scale.X, sprite.Height * sprite.Scale.Y));
            }

            this.spriteDrawer.End();
        }
    }
}