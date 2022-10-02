// <copyright file="PlayerShipEntityTemplate.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Templates
{
    using FinalEngine.ECS;

    public class PlayerShipEntityTemplate : IEntityTemplate
    {
        public Entity CreateEntity()
        {
            var entity = new Entity();

            return entity;
        }
    }
}