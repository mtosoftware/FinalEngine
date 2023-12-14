// <copyright file="VelocityComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Core;

using System.ComponentModel;

[Category("Core")]
public sealed class VelocityComponent : IEntityComponent
{
    public VelocityComponent()
    {
        this.Speed = 1.0f;
    }

    public float Speed { get; set; }
}
