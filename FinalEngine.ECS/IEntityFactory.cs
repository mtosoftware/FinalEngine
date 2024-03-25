// <copyright file="IEntityFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

/// <summary>
/// Defines an interface that provides a method to create an <see cref="Entity"/>.
/// </summary>
///
/// <remarks>
/// You should implement this interface in a scenario where an <see cref="Entity"/> must be created and assigned a default collection of components and can be reused. It simply provides a means to simplify the relationship between entities and components.
/// </remarks>
public interface IEntityFactory
{
    /// <summary>
    /// Creates the entity.
    /// </summary>
    ///
    /// <returns>
    /// The newly created <see cref="Entity"/>.
    /// </returns>
    Entity CreateEntity();
}
