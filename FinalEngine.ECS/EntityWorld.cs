// <copyright file="EntityWorld.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IEntityWorld"/> and <see cref="IEntitySystemsProcessor"/>.
    /// </summary>
    /// <seealso cref="IEntityWorld"/>
    /// <seealso cref="IEntitySystemsProcessor"/>
    public class EntityWorld : IEntityWorld
    {
        /// <summary>
        ///   The entities contained within the world.
        /// </summary>
        private readonly IList<Entity> entities;

        /// <summary>
        ///   The systems contained within the world.
        /// </summary>
        private readonly IList<EntitySystemBase> systems;

        /// <summary>
        ///   Initializes a new instance of the <see cref="EntityWorld"/> class.
        /// </summary>
        public EntityWorld()
        {
            this.entities = new List<Entity>();
            this.systems = new List<EntitySystemBase>();
        }

        /// <summary>
        ///   Adds the specified <paramref name="entity"/> to this <see cref="IEntityWorld"/>.
        /// </summary>
        /// <param name="entity">
        ///   The entity to add to this <see cref="EntityWorld"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="entity"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="entity"/> parameter has already been added to this entity world.
        /// </exception>
        public void AddEntity(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

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

        /// <summary>
        ///   Adds the specified <paramref name="system"/> to this <see cref="IEntityWorld"/>.
        /// </summary>
        /// <param name="system">
        ///   The system to add to this <see cref="IEntityWorld"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="system"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="system"/> is a type that has already been added to this entity world.
        /// </exception>
        public void AddSystem(EntitySystemBase system)
        {
            if (system == null)
            {
                throw new ArgumentNullException(nameof(system));
            }

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

        /// <summary>
        ///   Processes all systems that match the specified loop <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        ///   The loop type of systems to process.
        /// </param>
        public void ProcessAll(GameLoopType type)
        {
            foreach (var system in this.systems)
            {
                if (system.LoopType != type)
                {
                    continue;
                }

                system.Process();
            }
        }

        /// <summary>
        ///   Removes the specified <paramref name="entity"/> from this <see cref="IEntityWorld"/>.
        /// </summary>
        /// <param name="entity">
        ///   The entity to remove from this <see cref="IEntityWorld"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="entity"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="entity"/> parameter has not been added to this entity world.
        /// </exception>
        public void RemoveEntity(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (!this.entities.Contains(entity))
            {
                throw new ArgumentException($"The specified {nameof(entity)} parameter has not been added to this entity world.", nameof(entity));
            }

            entity.OnComponentsChanged -= this.Entity_OnComponentsChanged;

            foreach (var system in this.systems)
            {
                system.AddOrRemoveByAspect(entity, true);
            }

            this.entities.Remove(entity);
        }

        /// <summary>
        ///   Removes a system of the specified <paramref name="type"/> from this <see cref="IEntityWorld"/>.
        /// </summary>
        /// <param name="type">
        ///   The type of system to remove from this <see cref="IEntityWorld"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   type - The specified <paramref name="type"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="type"/> parameter does not inherit from <see cref="EntitySystemBase"/> or the specified <paramref name="type"/> parameter is not an entity system type that has been added to this entity world.
        /// </exception>
        /// <remarks>
        ///   The specified <paramref name="type"/> must inherit from <see cref="EntitySystemBase"/>.
        /// </remarks>
        public void RemoveSystem(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!typeof(EntitySystemBase).IsAssignableFrom(type))
            {
                throw new ArgumentException($"The specified {nameof(type)} parameter does not inherit from {nameof(EntitySystemBase)}.", nameof(type));
            }

            for (int i = this.systems.Count - 1; i >= 0; i--)
            {
                if (this.systems[i].GetType() == type)
                {
                    this.systems.RemoveAt(i);

                    return;
                }
            }

            throw new ArgumentException($"The specified {nameof(type)} parameter is not an entity system type that has been added to this entity world.", nameof(type));
        }

        /// <summary>
        ///   Handles the <see cref="Entity.OnComponentsChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///   The sender or entity.
        /// </param>
        /// <param name="e">
        ///   The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="sender"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="sender"/> parameter is not of type <see cref="Entity"/>.
        /// </exception>
        private void Entity_OnComponentsChanged(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

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
}
