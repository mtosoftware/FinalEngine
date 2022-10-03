// <copyright file="EntitySystemBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    ///   Provides a base implementation of an entity system that can process entities based on the aspect of the system.
    /// </summary>
    public abstract class EntitySystemBase
    {
        /// <summary>
        ///   The entities that are contained within this system.
        /// </summary>
        /// <remarks>
        ///   The entities contained within this system should, at all times return <c>true</c> when passed through <see cref="IsMatch(IReadOnlyEntity)"/>.
        /// </remarks>
        private readonly IList<Entity> entities;

        /// <summary>
        ///   Initializes a new instance of the <see cref="EntitySystemBase"/> class.
        /// </summary>
        protected EntitySystemBase()
        {
            this.entities = new List<Entity>();
        }

        /// <summary>
        ///   Gets the game loop type for this <see cref="EntitySystemBase"/>.
        /// </summary>
        /// <value>
        ///   The game loop type for this <see cref="EntitySystemBase"/>.
        /// </value>
        /// <remarks>
        ///   A game loop type determines when the <see cref="Process"/> function should be called.
        /// </remarks>
        public abstract GameLoopType LoopType { get; }

        /// <summary>
        ///   Calls the <see cref="Process(IEnumerable{Entity})"/> function, the frequency depends on the <see cref="LoopType"/>.
        /// </summary>
        public void Process()
        {
            // Copy the list so that the collection can be modified in a foreach loop.
            this.Process(this.entities.ToList());
        }

        /// <summary>
        ///   Adds or removes the specified <paramref name="entity"/> from this <see cref="EntitySystemBase"/>.
        /// </summary>
        /// <param name="entity">
        ///   The entity to add or remove from this <see cref="EntitySystemBase"/>.
        /// </param>
        /// <param name="forceRemove">
        ///   if set to <c>true</c> the specified <paramref name="entity"/>, if contained in the system, will be forcefully removed.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="entity"/> parameter cannot be null.
        /// </exception>
        /// <remarks>
        ///   The specified <paramref name="entity"/> will be added to the system when <see cref="IsMatch(IReadOnlyEntity)"/> returns true on the <paramref name="entity"/><b>and</b> the entity is not already contained within the system. An entity will be removed from the system when <see cref="IsMatch(IReadOnlyEntity)"/> returns false on the <paramref name="entity"/><b>and</b> the entity is contained within the system. When <paramref name="forceRemove"/> is <c>true</c>, the <paramref name="entity"/> will be forcefully removed from the system, regardless of whether the <paramref name="entity"/> matches the aspect of the system.
        /// </remarks>
        internal void AddOrRemoveByAspect(Entity entity, bool forceRemove = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"The specified {nameof(entity)} parameter cannot be null.");
            }

            bool isMatch = this.IsMatch(entity);
            bool isAdded = this.entities.Contains(entity);

            if (forceRemove && isAdded)
            {
                this.entities.Remove(entity);
            }
            else if (isMatch && !isAdded)
            {
                this.entities.Add(entity);
            }
            else if (!isMatch && isAdded)
            {
                this.entities.Remove(entity);
            }
        }

        /// <summary>
        ///   Determines whether the specified <paramref name="entity"/> matches the aspect of this <see cref="EntitySystemBase"/>.
        /// </summary>
        /// <param name="entity">
        ///   The entity to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="entity"/> matches the aspect of the system; otherwise, <c>false</c>.
        /// </returns>
        protected abstract bool IsMatch([NotNull] IReadOnlyEntity entity);

        /// <summary>
        ///   Processes the specified <paramref name="entities"/>, frequency is dependent on <see cref="LoopType"/>.
        /// </summary>
        /// <param name="entities">
        ///   The entities to process.
        /// </param>
        protected abstract void Process([NotNull] IEnumerable<Entity> entities);
    }
}