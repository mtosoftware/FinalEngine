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

    /// <summary>
    ///   Provides a system that can render sprites using an <see cref="ISpriteDrawer"/>.
    /// </summary>
    /// <seealso cref="EntitySystemBase"/>
    public class SpriteRenderSystem : EntitySystemBase
    {
        /// <summary>
        ///   The sprite drawer.
        /// </summary>
        private readonly ISpriteDrawer spriteDrawer;

        /// <summary>
        ///   Initializes a new instance of the <see cref="SpriteRenderSystem"/> class.
        /// </summary>
        /// <param name="spriteDrawer">
        ///   The sprite drawer.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="spriteDrawer"/> parameter cannot be null.
        /// </exception>
        public SpriteRenderSystem(ISpriteDrawer spriteDrawer)
        {
            this.spriteDrawer = spriteDrawer ?? throw new ArgumentNullException(nameof(spriteDrawer));
            this.LoopType = GameLoopType.Render;
        }

        /// <summary>
        ///   Gets the game loop type for this <see cref="EntitySystemBase"/>.
        /// </summary>
        /// <value>
        ///   The game loop type for this <see cref="EntitySystemBase"/>.
        /// </value>
        /// <remarks>
        ///   A game loop type determines when the <see cref="EntitySystemBase.Process"/> function should be called.
        /// </remarks>
        public override GameLoopType LoopType { get; }

        /// <summary>
        ///   Determines whether the specified <paramref name="entity"/> matches the aspect of this <see cref="EntitySystemBase"/>.
        /// </summary>
        /// <param name="entity">
        ///   The entity to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="entity"/> matches the aspect of the system; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<SpriteComponent>();
        }

        /// <summary>
        ///   Processes the specified <paramref name="entities"/>, rendering them with an <see cref="ISpriteDrawer"/>.
        /// </summary>
        /// <param name="entities">
        ///   The entities to process and render.
        /// </param>
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