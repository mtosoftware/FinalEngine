// <copyright file="IScene.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models.Scenes;

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
    /// Adds the specified <paramref name="entity"/> to the scene.
    /// </summary>
    /// <param name="entity">
    /// The entity to be added.
    /// </param>
    void AddEntity(Entity entity);

    /// <summary>
    /// Renders the scene, processing all rendering systems.
    /// </summary>
    void Render();
}
