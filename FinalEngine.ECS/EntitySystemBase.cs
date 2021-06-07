// <copyright file="EntitySystemBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public abstract class EntitySystemBase
    {
        private readonly IList<Entity> entities;

        protected EntitySystemBase()
        {
            this.entities = new List<Entity>();
        }

        public void Process()
        {
            this.Process(this.entities);
        }

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

        protected abstract bool IsMatch([NotNull] IReadOnlyEntity entity);

        protected abstract void Process([NotNull] IEnumerable<Entity> entities);
    }
}