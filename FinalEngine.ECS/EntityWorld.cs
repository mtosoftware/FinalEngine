// <copyright file="EntityWorld.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

using System;
using System.Collections.Generic;
using FinalEngine.ECS.Exceptions;

public class EntityWorld : IEntityWorld
{
    private readonly List<Entity> entities;

    private readonly List<EntitySystemBase> systems;

    public EntityWorld()
    {
        this.entities = [];
        this.systems = [];
    }

    public void AddEntity(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        if (this.entities.Contains(entity))
        {
            throw new ArgumentException($"The specified {nameof(entity)} parameter has already been added to this entity world.", nameof(entity));
        }

        entity.OnComponentsChanged += this.Entity_OnComponentsChanged;

        foreach (var system in this.systems)
        {
            system.AddOrRemoveByAspect(entity);
        }

        this.entities.Add(entity);
    }

    public void AddSystem(EntitySystemBase system)
    {
        ArgumentNullException.ThrowIfNull(system, nameof(system));

        foreach (var other in this.systems)
        {
            if (other.GetType() == system.GetType())
            {
                throw new ArgumentException($"The specified {nameof(system)} is a type that has already been added to this entity world.", nameof(system));
            }
        }

        foreach (var entity in this.entities)
        {
            system.AddOrRemoveByAspect(entity);
        }

        this.systems.Add(system);
    }

    public void RemoveEntity(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        if (!this.entities.Contains(entity))
        {
            throw new EntityNotFoundException(entity.UniqueIdentifier);
        }

        entity.OnComponentsChanged -= this.Entity_OnComponentsChanged;

        foreach (var system in this.systems)
        {
            system.AddOrRemoveByAspect(entity, true);
        }

        this.entities.Remove(entity);
    }

    public void RemoveSystem(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));

        if (!typeof(EntitySystemBase).IsAssignableFrom(type))
        {
            throw new ArgumentException($"The specified {nameof(type)} parameter does not inherit from {nameof(EntitySystemBase)}.", nameof(type));
        }

        for (int i = this.systems.Count - 1; i >= 0; i--)
        {
            if (this.systems[i].GetType() == type)
            {
                var system = this.systems[i];

                system.RemoveAllEntities();
                this.systems.RemoveAt(i);

                return;
            }
        }

        throw new ArgumentException($"The specified {nameof(type)} parameter is not an entity system type that has been added to this entity world.", nameof(type));
    }

    private void Entity_OnComponentsChanged(object? sender, EventArgs e)
    {
        if (sender is not Entity entity)
        {
            throw new ArgumentException($"The specified {nameof(sender)} parameter is not of type {nameof(Entity)}.", nameof(sender));
        }

        foreach (var system in this.systems)
        {
            system.AddOrRemoveByAspect(entity);
        }
    }
}
