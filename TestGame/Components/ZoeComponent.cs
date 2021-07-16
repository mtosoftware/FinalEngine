// <copyright file="ZoeComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame.Components
{
    using FinalEngine.ECS;

    public class ZoeComponent : IComponent
    {
        public string IsLovable { get; } = "Yes, she is";
    }
}