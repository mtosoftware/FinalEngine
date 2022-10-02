// <copyright file="EnemyMovementSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.ECS;
    using FinalEngine.Examples.StarWarriors.Components;
    using FinalEngine.Platform;

    public class EnemyMovementSystem : EntitySystemBase
    {
        private readonly IWindow window;

        public EnemyMovementSystem(IWindow window)
        {
            this.window = window ?? throw new ArgumentNullException(nameof(window));
        }

        public override GameLoopType LoopType { get; } = GameLoopType.Update;

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TagComponent>() &&
                   entity.ContainsComponent<VelocityComponent>() &&
                   entity.ContainsComponent<TransformComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var isEnemy = entity.GetComponent<TagComponent>()?.Tag == "Enemy";

                if (!isEnemy)
                {
                    continue;
                }

                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();

                if (transform != null && (transform.X < 0 || transform.X > this.window.ClientSize.Width))
                {
                    velocity.AddAngle(180);
                }
            }
        }
    }
}