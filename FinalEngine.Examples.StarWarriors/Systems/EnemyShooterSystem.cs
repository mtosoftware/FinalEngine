// <copyright file="EnemyShooterSystem.cs" company="Software Antics">
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

    public class EnemyShooterSystem : EntitySystemBase
    {
        private static readonly long TwoSecondsTicks = TimeSpan.FromSeconds(2).Ticks;

        private readonly IEntityFactory missileTemplate;

        private readonly IEntityWorld world;

        public EnemyShooterSystem(IEntityFactory missileTemplate, IEntityWorld world)
        {
            this.missileTemplate = missileTemplate ?? throw new ArgumentNullException(nameof(missileTemplate));
            this.world = world ?? throw new ArgumentNullException(nameof(world));
        }

        public override GameLoopType LoopType { get; } = GameLoopType.Update;

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<WeaponComponent>() &&
                   entity.Tag == "Enemy";
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var transform = entity.GetComponent<TransformComponent>();
                var weapon = entity.GetComponent<WeaponComponent>();

                if ((weapon.ShotAt + TwoSecondsTicks) < DateTime.Now.Ticks)
                {
                    Entity missile = this.missileTemplate.CreateEntity();

                    missile.GetComponent<TransformComponent>().X = transform.X;
                    missile.GetComponent<TransformComponent>().Y = transform.Y + 20;

                    missile.GetComponent<VelocityComponent>().Speed = -0.5f;
                    missile.GetComponent<VelocityComponent>().Angle = 270;
                    weapon.ShotAt = DateTime.Now.Ticks;

                    this.world.AddEntity(missile);
                }
            }
        }
    }
}