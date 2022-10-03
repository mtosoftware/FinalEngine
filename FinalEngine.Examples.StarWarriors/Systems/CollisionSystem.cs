﻿// <copyright file="CollisionSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Systems
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.ECS.Components;
    using FinalEngine.Examples.StarWarriors.Components;
    using FinalEngine.Rendering.Components;

    public class CollisionSystem : EntitySystemBase
    {
        private readonly IEntityWorld world;

        public CollisionSystem(IEntityWorld world)
        {
            this.world = world ?? throw new System.ArgumentNullException(nameof(world));
        }

        public override GameLoopType LoopType { get; } = GameLoopType.Update;

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<SpriteComponent>() &&
                   (entity.Tag == "Bullet" || entity.Tag == "Player" || entity.Tag == "Enemy");
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            var bullets = entities.Where(x => x.Tag == "Bullet").ToList();
            var ships = entities.Where(x => x.Tag is "Player" or "Enemy").ToList();

            if (bullets == null || ships == null)
            {
                return;
            }

            for (int shipIndex = 0; ships.Count > shipIndex; ++shipIndex)
            {
                Entity ship = ships[shipIndex];
                for (int bulletIndex = 0; bullets.Count > bulletIndex; ++bulletIndex)
                {
                    Entity bullet = bullets[bulletIndex];

                    if (this.CollisionExists(bullet, ship))
                    {
                        TransformComponent bulletTransform = bullet.GetComponent<TransformComponent>();

                        this.world.RemoveEntity(bullet);

                        HealthComponent healthComponent = ship.GetComponent<HealthComponent>();
                        healthComponent.AddDamage(4);

                        if (!healthComponent.IsAlive)
                        {
                            TransformComponent shipTransform = ship.GetComponent<TransformComponent>();
                            this.world.RemoveEntity(ship);
                            break;
                        }
                    }
                }
            }
        }

        private bool CollisionExists(Entity entity1, Entity entity2)
        {
            return Vector2.Distance(entity1.GetComponent<TransformComponent>().Position, entity2.GetComponent<TransformComponent>().Position) < 20;
        }
    }
}