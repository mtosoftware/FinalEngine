﻿// <copyright file="MissileEntityTemplate.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Templates
{
    using FinalEngine.ECS;
    using FinalEngine.Examples.StarWarriors.Components;
    using FinalEngine.Rendering.Textures;

    public class MissileEntityTemplate : IEntityTemplate
    {
        private readonly ITexture2D missileTexture;

        public MissileEntityTemplate(ITexture2D missileTexture)
        {
            this.missileTexture = missileTexture ?? throw new System.ArgumentNullException(nameof(missileTexture));
        }

        public Entity CreateEntity()
        {
            var entity = new Entity();

            entity.AddComponent<TransformComponent>();
            entity.AddComponent<VelocityComponent>();

            entity.AddComponent(new ExpiresComponent()
            {
                LifeTime = 2000,
            });

            entity.AddComponent(new SpriteComponent()
            {
                Texture = this.missileTexture,
            });

            entity.AddComponent(new TagComponent()
            {
                Tag = "Bullet",
            });

            return entity;
        }
    }
}