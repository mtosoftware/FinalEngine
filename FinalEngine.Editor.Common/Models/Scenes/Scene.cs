// <copyright file="Scene.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models.Scenes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.ECS.Exceptions;
using FinalEngine.Rendering.Components;
using Microsoft.Extensions.Logging;

public sealed class Scene : IScene
{
    private readonly ObservableCollection<Entity> entities;

    private readonly ILogger<Scene> logger;

    private readonly IEntityWorld world;

    public Scene(ILogger<Scene> logger, IEntityWorld world)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.world = world ?? throw new ArgumentNullException(nameof(world));
        this.entities = [];
    }

    public IReadOnlyCollection<Entity> Entities
    {
        get { return this.entities; }
    }

    public void AddEntity(string tag, Guid uniqueID)
    {
        this.logger.LogInformation($"Adding new {nameof(Entity)} to {nameof(Scene)} with ID: '{uniqueID}'.");

        ArgumentException.ThrowIfNullOrWhiteSpace(tag, nameof(tag));

        var entity = new Entity(uniqueID);

        entity.AddComponent(new TagComponent()
        {
            Name = tag,
        });

        entity.AddComponent(new TransformComponent());

        this.world.AddEntity(entity);
        this.entities.Add(entity);

        this.logger.LogInformation($"Added {nameof(Entity)} to {nameof(Scene)} with ID: '{uniqueID}'.");
    }

    public void AddSystem<TSystem>()
        where TSystem : EntitySystemBase
    {
        this.world.AddSystem<TSystem>();
    }

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

    public void RemoveSystem<TSystem>()
        where TSystem : EntitySystemBase
    {
        this.world.RemoveSystem(typeof(TSystem));
    }

    public void Render()
    {
        this.world.ProcessAll(GameLoopType.Render);
    }
}
