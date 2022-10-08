// <copyright file="TagComponent.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Core
{
    public class TagComponent : IComponent
    {
        public TagComponent(string tag)
        {
            this.Name = tag;
        }

        public string? Name { get; set; }
    }
}
