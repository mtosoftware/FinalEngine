// <copyright file="TagComponent.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Core;

/// <summary>
/// Provides a component that represents a name or tag for an <see cref="Entity"/>.
/// </summary>
/// <seealso cref="IEntityComponent" />
public sealed class TagComponent : IEntityComponent
{
    /// <summary>
    /// Gets or sets the tag.
    /// </summary>
    /// <value>
    /// The tag.
    /// </value>
    public string? Tag { get; set; }
}
