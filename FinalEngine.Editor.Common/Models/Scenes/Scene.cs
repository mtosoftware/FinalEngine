// <copyright file="Scene.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models.Scenes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
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
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="tag"/> parameter cannot be null or whitespace.
    /// </exception>
    public void AddEntity(string tag, Guid uniqueID)
    {
        this.logger.LogInformation($"Adding new {nameof(Entity)} to {nameof(Scene)} with ID: '{uniqueID}'.");

        if (string.IsNullOrWhiteSpace(tag))
        {
            throw new ArgumentException($"'{nameof(tag)}' cannot be null or whitespace.", nameof(tag));
        }

        var entity = new Entity(uniqueID);

        entity.AddComponent(new TagComponent()
        {
            Tag = tag,
        });

        this.world.AddEntity(entity);
        this.entities.Add(entity);

        this.logger.LogInformation($"Added {nameof(Entity)} to {nameof(Scene)} with ID: '{uniqueID}'.");
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// Failed to locate an <see cref="Entity"/> that matches the specified <paramref name="uniqueID"/>.
    /// </exception>
    public void RemoveEntity(Guid uniqueID)
    {
        this.logger.LogInformation($"Removing {nameof(Entity)} from {nameof(Scene)} with ID: '{uniqueID}'.");

        var entity = this.entities.FirstOrDefault(x =>
        {
            return x.UniqueID == uniqueID;
        }) ?? throw new ArgumentException($"Failed to locate an {nameof(Entity)} that matches the specified {nameof(uniqueID)}: '{uniqueID}'.");

        this.world.RemoveEntity(entity);
        this.entities.Remove(entity);

        this.logger.LogInformation($"Removed {nameof(Entity)} from {nameof(Scene)} with ID: '{uniqueID}'.");
    }

    /// <inheritdoc/>
    public void Render()
    {
        this.world.ProcessAll(GameLoopType.Render);
    }
}
