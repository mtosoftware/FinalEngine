// <copyright file="EntityModifiedMessage.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Messages.Entities;

using System;
using FinalEngine.ECS;

/// <summary>
///   Provides a message that should be published when an <see cref="FinalEngine.ECS.Entity"/> has been modified.
/// </summary>
public sealed class EntityModifiedMessage
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="EntityModifiedMessage"/> class.
    /// </summary>
    /// <param name="entity">
    ///   The entity that has been modified.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="entity"/> parameter cannot be null.
    /// </exception>
    public EntityModifiedMessage(Entity entity)
    {
        this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    /// <summary>
    ///   Gets the entity that has been modified.
    /// </summary>
    /// <value>
    ///   The entity that has been modified.
    /// </value>
    public Entity Entity { get; }
}
