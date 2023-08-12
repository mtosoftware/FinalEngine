// <copyright file="Scene.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Collections.ObjectModel;
using FinalEngine.ECS;

public sealed class Scene
{
    private readonly ObservableCollection<Entity> entities;

    private readonly IEntityWorld world;

    public Scene()
    {
        this.world = new EntityWorld();

        this.entities = new ObservableCollection<Entity>();
        this.Entities = new ReadOnlyObservableCollection<Entity>(this.entities);
    }

    public ReadOnlyObservableCollection<Entity> Entities { get; }

    public void AddEntity(Entity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        this.entities.Add(entity);
        this.world.AddEntity(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        this.entities.Remove(entity);
        this.world.RemoveEntity(entity);
    }
}
