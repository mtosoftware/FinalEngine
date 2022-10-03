// <copyright file="PlayerControllerSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.ECS;
    using FinalEngine.ECS.Components;
    using FinalEngine.Examples.StarWarriors.Components;
    using FinalEngine.Input;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;

    public sealed class PlayerControllerSystem : EntitySystemBase
    {
        private readonly IKeyboard keyboard;

        private readonly IEntityFactory missileTemplate;

        private readonly IRasterizer rasterizer;

        private readonly IEntityWorld world;

        public PlayerControllerSystem(IKeyboard keyboard, IRasterizer rasterizer, IEntityFactory missileTemplate, IEntityWorld world)
        {
            this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
            this.rasterizer = rasterizer ?? throw new ArgumentNullException(nameof(rasterizer));
            this.missileTemplate = missileTemplate ?? throw new ArgumentNullException(nameof(missileTemplate));
            this.world = world ?? throw new ArgumentNullException(nameof(world));
        }

        public override GameLoopType LoopType { get; } = GameLoopType.Update;

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.Tag == "Player";
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            var viewport = this.rasterizer.GetViewport();

            foreach (var entity in entities)
            {
                var transform = entity.GetComponent<TransformComponent>();
                float moveSpeed = 0.3f * GameTime.Delta;

                if (this.keyboard.IsKeyDown(Key.A))
                {
                    transform.X -= moveSpeed;

                    if (transform.X < 32)
                    {
                        transform.X = 32;
                    }
                }
                else if (this.keyboard.IsKeyDown(Key.D))
                {
                    transform.X += moveSpeed;

                    if (transform.X > viewport.Width - 32)
                    {
                        transform.X = viewport.Width - 32;
                    }
                }

                if (this.keyboard.IsKeyReleased(Key.Space) || this.keyboard.IsKeyReleased(Key.Enter))
                {
                    this.AddMissile(transform);
                    this.AddMissile(transform, 89, -9);
                    this.AddMissile(transform, 91, +9);
                }
            }
        }

        private void AddMissile(TransformComponent transform, float angle = 90.0f, float offsetX = 0.0f)
        {
            Entity missile = this.missileTemplate.CreateEntity();

            missile.GetComponent<TransformComponent>().X = transform.X + 1 + offsetX + 32;
            missile.GetComponent<TransformComponent>().Y = transform.Y - 20;

            missile.GetComponent<VelocityComponent>().Speed = -0.5f;
            missile.GetComponent<VelocityComponent>().Angle = angle;

            world.AddEntity(missile);
        }
    }
}