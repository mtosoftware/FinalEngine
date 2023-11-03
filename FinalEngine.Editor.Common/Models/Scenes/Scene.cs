// <copyright file="Scene.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models.Scenes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Exceptions.Entities;
using Microsoft.Extensions.Logging;

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
    /// The logger.
    /// </summary>
    private readonly ILogger<Scene> logger;

    /// <summary>
    /// The underlying entity world that contains all the scenes entities and systems.
    /// </summary>
    private readonly IEntityWorld world;

    /// <summary>
    /// Initializes a new instance of the <see cref="Scene"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="world">
    /// The entity world to be associated with this scene.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/> or <paramref name="world"/> parameter cannot be null.
    /// </exception>
    public Scene(ILogger<Scene> logger, IEntityWorld world)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

        var uniqueID = entity.UniqueIdentifier;

        this.logger.LogInformation($"Adding new {nameof(Entity)} to {nameof(Scene)} with ID: '{uniqueID}'.");

        this.world.AddEntity(entity);
        this.entities.Add(entity);

        this.logger.LogInformation($"Added {nameof(Entity)} to {nameof(Scene)} with ID: '{uniqueID}'.");
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// Failed to locate an <see cref="Entity"/> that matches the specified <paramref name="uniqueIdentifier"/>.
    /// </exception>
    public void RemoveEntity(Guid uniqueIdentifier)
    {
        this.logger.LogInformation($"Removing {nameof(Entity)} from {nameof(Scene)} with ID: '{uniqueIdentifier}'.");

        var entity = this.entities.FirstOrDefault(x =>
        {
            return x.UniqueIdentifier == uniqueIdentifier;
        });

        if (entity == null)
        {
            this.logger.LogError($"Failed to locate entity with ID: '{uniqueIdentifier}'.");
            throw new EntityNotFoundException(uniqueIdentifier);
        }

        this.world.RemoveEntity(entity);
        this.entities.Remove(entity);

        this.logger.LogInformation($"Removed {nameof(Entity)} from {nameof(Scene)} with ID: '{uniqueIdentifier}'.");
    }

    /// <inheritdoc/>
    public void Render()
    {
        this.world.ProcessAll(GameLoopType.Render);
    }
}
