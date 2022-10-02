// <copyright file="EnemyShipEntityTemplate.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Templates
{
    using System.Drawing;
    using FinalEngine.ECS;
    using FinalEngine.Examples.StarWarriors.Components;
    using FinalEngine.Rendering.Textures;

    public class EnemyShipEntityTemplate : IEntityTemplate
    {
        private readonly ITexture2D enemyTexture;

        public EnemyShipEntityTemplate(ITexture2D enemyTexture)
        {
            this.enemyTexture = enemyTexture ?? throw new System.ArgumentNullException(nameof(enemyTexture));
        }

        public Entity CreateEntity()
        {
            var entity = new Entity();

            entity.AddComponent<TransformComponent>();

            entity.AddComponent(new HealthComponent()
            {
                Points = 10,
            });

            entity.AddComponent(new TagComponent()
            {
                Tag = "Enemy",
            });

            entity.AddComponent(new VelocityComponent());
            entity.AddComponent(new SpriteComponent()
            {
                Color = Color.Red,
                Texture = this.enemyTexture,
            });

            entity.AddComponent<WeaponComponent>();

            return entity;
        }
    }
}