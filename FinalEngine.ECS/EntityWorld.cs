// <copyright file="EntityWorld.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    //// TODO: Write a unit testable version of this class.

    [ExcludeFromCodeCoverage]
    public class EntityWorld : IEntityWorld, IEntitySystemsProcessor
    {
        private readonly IList<Entity> entities;

        private readonly IList<EntitySystemBase> systems;

        public EntityWorld()
        {
            this.entities = new List<Entity>();
            this.systems = new List<EntitySystemBase>();
        }

        public void AddEntity(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"The specified {nameof(entity)} parameter cannot be null.");
            }

            if (this.entities.Contains(entity))
            {
                throw new ArgumentException($"The specified {nameof(entity)} parameter has already been added to this entity world.", nameof(entity));
            }

            entity.OnComponentsChanged += this.Entity_OnComponentsChanged;

            foreach (EntitySystemBase system in this.systems)
            {
                system.AddOrRemoveByAspect(entity);
            }

            this.entities.Add(entity);
        }

        public void AddSystem(EntitySystemBase system)
        {
            if (system == null)
            {
                throw new ArgumentNullException(nameof(system), $"The specified {nameof(system)} parameter cannot be null.");
            }

            foreach (EntitySystemBase other in this.systems)
            {
                if (other.GetType() == system.GetType())
                {
                    throw new ArgumentException($"The specified {nameof(system)} is a type that has already been added to this entity world.", nameof(system));
                }
            }

            foreach (Entity entity in this.entities)
            {
                system.AddOrRemoveByAspect(entity);
            }

            this.systems.Add(system);
        }

        public void ClearEntities()
        {
            while (this.entities.Count > 0)
            {
                this.RemoveEntity(this.entities[0]);
            }
        }

        public void ClearSystems()
        {
            while (this.systems.Count > 0)
            {
                this.RemoveSystem(this.systems[0].GetType());
            }
        }

        public void ProcessAll(GameLoopType type)
        {
            foreach (EntitySystemBase system in this.systems)
            {
                if (system.LoopType != type)
                {
                    continue;
                }

                system.Process();
            }
        }

        public void RemoveEntity(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"The specified {nameof(entity)} parameter cannot be null.");
            }

            if (!this.entities.Contains(entity))
            {
                throw new ArgumentException($"The specified {nameof(entity)} parameter has not been added to this entity world.", nameof(entity));
            }

            entity.OnComponentsChanged -= this.Entity_OnComponentsChanged;

            foreach (EntitySystemBase system in this.systems)
            {
                system.AddOrRemoveByAspect(entity, true);
            }

            this.entities.Remove(entity);
        }

        public void RemoveSystem(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type), $"The specified {nameof(type)} parameter cannot be null.");
            }

            if (!typeof(EntitySystemBase).IsAssignableFrom(type))
            {
                throw new ArgumentException($"The specified {nameof(type)} parameter does not inherit from {nameof(EntitySystemBase)}.", nameof(type));
            }

            foreach (EntitySystemBase system in this.systems)
            {
                if (system.GetType() == type)
                {
                    this.systems.Remove(system);
                }
            }

            throw new ArgumentException($"The specified {nameof(type)} parameter is not an entity system type that has been added to this entity world.", nameof(type));
        }

        public void RemoveSystem<TSystem>()
            where TSystem : EntitySystemBase
        {
            this.RemoveSystem(typeof(TSystem));
        }

        private void Entity_OnComponentsChanged(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender), $"The specified {nameof(sender)} parameter cannot be null.");
            }

            if (sender is not Entity entity)
            {
                throw new ArgumentException($"The specified {nameof(sender)} parameter is not of type {nameof(Entity)}.", nameof(sender));
            }

            foreach (EntitySystemBase system in this.systems)
            {
                system.AddOrRemoveByAspect(entity);
            }
        }
    }
}