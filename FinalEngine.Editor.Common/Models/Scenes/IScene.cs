// <copyright file="IScene.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models.Scenes;

using System;
using System.Collections.Generic;
using FinalEngine.ECS;

/// <summary>
/// Defines an interface that represents a scene.
/// </summary>
public interface IScene
{
    /// <summary>
    /// Gets the entities contained within this scene.
    /// </summary>
    /// <value>
    /// The entities contained within this scene.
    /// </value>
    IReadOnlyCollection<Entity> Entities { get; }

    /// <summary>
    /// Adds an entity with the specified <paramref name="tag"/> and <paramref name="uniqueID"/> to this <see cref="IScene"/>.
    /// </summary>
    /// <param name="tag">
    /// The name/tag of the entity to create and add to this <see cref="IScene"/>.
    /// </param>
    /// <param name="uniqueID">
    /// The unique identifier of the entity to create and add to this <see cref="IScene"/>.
    /// </param>
    void AddEntity(string tag, Guid uniqueID);

    /// <summary>
    /// Renders the scene, processing all rendering systems.
    /// </summary>
    void Render();
}
