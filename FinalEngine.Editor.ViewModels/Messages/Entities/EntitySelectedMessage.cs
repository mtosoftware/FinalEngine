// <copyright file="EntitySelectedMessage.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Messages.Entities;

using System;
using FinalEngine.ECS;

/// <summary>
/// Provides a message that should be published when an entity has been selected within the scene hierarchy.
/// </summary>
public sealed class EntitySelectedMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntitySelectedMessage"/> class.
    /// </summary>
    /// <param name="entity">
    /// The entity that has been selected.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="entity"/> parameter cannot be null.
    /// </exception>
    public EntitySelectedMessage(Entity entity)
    {
        this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    /// <summary>
    /// Gets the entity that has been selected.
    /// </summary>
    /// <value>
    /// The entity that has been selected.
    /// </value>
    public Entity Entity { get; }
}
