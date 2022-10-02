// <copyright file="TagComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Components
{
    using FinalEngine.ECS;

    public class TagComponent : IComponent
    {
        public string? Tag { get; set; }
    }
}