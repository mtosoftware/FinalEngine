﻿// <copyright file="ExpirationSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.ECS;
    using FinalEngine.Examples.StarWarriors.Components;
    using FinalEngine.Launching;

    public class ExpirationSystem : EntitySystemBase
    {
        private readonly IEntityWorld world;

        public ExpirationSystem(IEntityWorld world)
        {
            this.world = world ?? throw new ArgumentNullException(nameof(world));
        }

        public override GameLoopType LoopType { get; } = GameLoopType.Update;

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<ExpiresComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var expiresComponent = entity.GetComponent<ExpiresComponent>();

                expiresComponent.ReduceLifeTime(GameTime.Delta);

                if (expiresComponent.IsExpired)
                {
                    this.world.RemoveEntity(entity);
                }
            }
        }
    }
}