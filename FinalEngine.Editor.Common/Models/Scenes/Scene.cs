// <copyright file="Scene.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models.Scenes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FinalEngine.ECS;

/// <summary>
/// Represents a scene that contains a collection of entities and systems.
/// </summary>
public sealed class Scene : IScene
{
    /// <summary>
    /// The entities contained within the scene.
    /// </summary>
    private readonly ObservableCollection<Entity> entities;

    /// <summary>
    /// The underlying entity world that contains all the scenes entities and systems.
    /// </summary>
    private readonly IEntityWorld world;

    /// <summary>
    /// Initializes a new instance of the <see cref="Scene"/> class.
    /// </summary>
    /// <param name="world">
    /// The entity world to be associated with this scene.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="world"/> parameter cannot be null.
    /// </exception>
    public Scene(IEntityWorld world)
    {
        this.world = world ?? throw new ArgumentNullException(nameof(world));
        this.entities = new ObservableCollection<Entity>();
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<Entity> Entities
    {
        get { return this.entities; }
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="entity"/> parameter cannot be null.
    /// </exception>
    public void AddEntity(Entity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        this.world.AddEntity(entity);
        this.entities.Add(entity);
    }

    /// <inheritdoc/>
    public void Render()
    {
        this.world.ProcessAll(GameLoopType.Render);
    }
}
