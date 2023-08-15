// <copyright file="Scene.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models.Scenes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FinalEngine.ECS;

public sealed class Scene
{
    private readonly ObservableCollection<Entity> entities;

    private readonly IEntityWorld world;

    public Scene(IEntityWorld world)
    {
        this.world = world ?? throw new ArgumentNullException(nameof(world));
    }

    public IReadOnlyCollection<Entity> Entities
    {
        get { return this.entities; }
    }

    public void AddEntity(Entity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        this.world.AddEntity(entity);
        this.entities.Add(entity);
    }
}
