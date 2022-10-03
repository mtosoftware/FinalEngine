// <copyright file="EnemySpawnSystem.cs" company="Software Antics">
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

    public class EnemySpawnSystem : EntitySystemBase
    {
        private static readonly long ThreeSecondsTicks = TimeSpan.FromSeconds(3).Ticks;

        private readonly IEntityFactory enemyTemplate;

        private readonly IRasterizer rastierzer;

        private readonly IEntityWorld world;

        private Random random;

        private long spawnedTicks;

        public EnemySpawnSystem(IEntityFactory enemyTemplate, IEntityWorld world, IRasterizer rasterizer)
        {
            this.enemyTemplate = enemyTemplate ?? throw new ArgumentNullException(nameof(enemyTemplate));
            this.world = world ?? throw new ArgumentNullException(nameof(world));
            this.rastierzer = rasterizer ?? throw new ArgumentNullException(nameof(rasterizer));
            this.random = new Random();
        }

        public override GameLoopType LoopType { get; } = GameLoopType.Update;

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return false;
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            if ((this.spawnedTicks + ThreeSecondsTicks) < DateTime.Now.Ticks)
            {
                var entity = this.enemyTemplate.CreateEntity();
                var viewport = this.rastierzer.GetViewport();

                entity.GetComponent<TransformComponent>().X = this.random.Next(viewport.Width);
                entity.GetComponent<TransformComponent>().Y = this.random.Next(400) + 50;

                entity.GetComponent<VelocityComponent>().Speed = 0.05f;
                entity.GetComponent<VelocityComponent>().Angle = this.random.Next() % 2 == 0 ? 0 : 180;

                this.spawnedTicks = DateTime.Now.Ticks;

                this.world.AddEntity(entity);
            }
        }
    }
}