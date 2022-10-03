// <copyright file="SpriteRenderSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.ECS.Components;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Components;

    public class SpriteRenderSystem : EntitySystemBase
    {
        private readonly ISpriteDrawer spriteDrawer;

        public SpriteRenderSystem(ISpriteDrawer spriteDrawer)
        {
            this.spriteDrawer = spriteDrawer ?? throw new ArgumentNullException(nameof(spriteDrawer));
            this.LoopType = GameLoopType.Render;
        }

        public override GameLoopType LoopType { get; }

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<SpriteComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            this.spriteDrawer.Begin();

            foreach (var entity in entities)
            {
                var transform = entity.GetComponent<TransformComponent>();
                var sprite = entity.GetComponent<SpriteComponent>();

                if (sprite.Texture == null)
                {
                    return;
                }

                var sizeX = sprite.SpriteWidth * transform.Scale.X;
                var sizeY = sprite.SpriteHeight * transform.Scale.Y;

                this.spriteDrawer.Draw(
                    sprite.Texture,
                    sprite.Color,
                    sprite.Origin,
                    transform.Position,
                    transform.Rotation,
                    new Vector2(sizeX, sizeY));
            }

            this.spriteDrawer.End();
        }
    }
}