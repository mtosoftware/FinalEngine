// <copyright file="FreeMovementEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Systems.Input
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.ECS.Components.Core;
    using FinalEngine.Input;

    public class FreeMovementEntitySystem : EntitySystemBase
    {
        private readonly IKeyboard keyboard;

        public FreeMovementEntitySystem(IKeyboard keyboard)
        {
            this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
            this.LoopType = GameLoopType.Update;
        }

        public override GameLoopType LoopType { get; }

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<VelocityComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            foreach (dynamic entity in entities)
            {
                TransformComponent transform = entity.Transform;
                VelocityComponent velocity = entity.Velocity;

                float moveAmount = velocity.Speed;

                if (this.keyboard.IsKeyDown(Key.W))
                {
                    transform.Translate(transform.Forward, moveAmount);
                }

                if (this.keyboard.IsKeyDown(Key.S))
                {
                    transform.Translate(transform.Forward, -moveAmount);
                }

                if (this.keyboard.IsKeyDown(Key.A))
                {
                    transform.Translate(transform.Left, -moveAmount);
                }

                if (this.keyboard.IsKeyDown(Key.D))
                {
                    transform.Translate(transform.Left, moveAmount);
                }
            }
        }
    }
}