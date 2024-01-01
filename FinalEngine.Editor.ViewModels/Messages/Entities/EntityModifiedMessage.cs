// <copyright file="EntityModifiedMessage.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Messages.Entities;

using System;
using FinalEngine.ECS;

public sealed class EntityModifiedMessage
{
    public EntityModifiedMessage(Entity entity)
    {
        this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public Entity Entity { get; }
}
