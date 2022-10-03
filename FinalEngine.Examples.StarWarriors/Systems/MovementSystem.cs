// <copyright file="MovementSystem.cs" company="Software Antics">
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
    using FinalEngine.Launching;

    public class MovementSystem : EntitySystemBase
    {
        public override GameLoopType LoopType { get; } = GameLoopType.Update;

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<VelocityComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            foreach (dynamic entity in entities)
            {
                var transform = entity.Transform;
                var velocity = entity.Velocity;

                float ms = GameTime.Delta;

                transform.X += (float)(Math.Cos(velocity.AngleAsRadians) * velocity.Speed * ms);
                transform.Y += (float)(Math.Sin(velocity.AngleAsRadians) * velocity.Speed * ms);
            }
        }
    }
}