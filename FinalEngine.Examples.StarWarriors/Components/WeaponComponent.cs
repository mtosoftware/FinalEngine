// <copyright file="WeaponComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Components
{
    using FinalEngine.ECS;

    public class WeaponComponent : IComponent
    {
        public long ShotAt { get; set; }
    }
}