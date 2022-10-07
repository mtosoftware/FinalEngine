// <copyright file="IEntityFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    /// <summary>
    ///   Defines an interface that provides a factory method for creating entities.
    /// </summary>
    public interface IEntityFactory
    {
        /// <summary>
        ///   Creates an <see cref="Entity"/>.
        /// </summary>
        /// <returns>
        ///   The newly created <see cref="Entity"/>.
        /// </returns>
        Entity CreateEntity();
    }
}
