// <copyright file="EnemyMovementSystem.cs" company="Software Antics">
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
    using FinalEngine.Rendering;

    public class EnemyMovementSystem : EntitySystemBase
    {
        private readonly IRasterizer rasterizer;

        public EnemyMovementSystem(IRasterizer rasterizer)
        {
            this.rasterizer = rasterizer ?? throw new ArgumentNullException(nameof(rasterizer));
        }

        public override GameLoopType LoopType { get; } = GameLoopType.Update;

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.Tag == "Enemy" &&
                   entity.ContainsComponent<VelocityComponent>() &&
                   entity.ContainsComponent<TransformComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();
                var viewport = this.rasterizer.GetViewport();

                if (transform != null && (transform.X < 0 || transform.X > viewport.Width))
                {
                    velocity.AddAngle(180);
                }
            }
        }
    }
}