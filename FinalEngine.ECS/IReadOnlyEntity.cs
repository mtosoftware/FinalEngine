// <copyright file="IReadOnlyEntity.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    using System;

    /// <summary>
    /// Defines an interface that provides methods for determining which components (and types of) are contained within an entity.
    /// </summary>
    public interface IReadOnlyEntity
    {
        /// <summary>
        ///   Gets the tag for this <see cref="IReadOnlyEntity"/>.
        /// </summary>
        /// <value>
        ///   The tag for this <see cref="IReadOnlyEntity"/>.
        /// </value>
        string? Tag { get; }

        /// <summary>
        ///   Determines whether the specified <paramref name="component"/> is contained within this <see cref="IReadOnlyEntity"/>.
        /// </summary>
        /// <param name="component">
        ///   The component to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="component"/> is contained within this <see cref="IReadOnlyEntity"/>; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsComponent(IComponent component);

        /// <summary>
        ///   Determines whether a component of the specified <paramref name="type"/> is contained within this <see cref="IReadOnlyEntity"/>.
        /// </summary>
        /// <param name="type">
        ///   The type of component to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the a component of the specified <paramref name="type"/> is contained within this <see cref="IReadOnlyEntity"/>; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsComponent(Type type);

        /// <summary>
        ///   Determines whether a component of the specified <typeparamref name="TComponent"/> is contained within this <see cref="IReadOnlyEntity"/>.
        /// </summary>
        /// <typeparam name="TComponent">
        ///   The type of component to check.
        /// </typeparam>
        /// <returns>
        ///   <c>true</c> if a component of the specified <typeparamref name="TComponent"/> is contained within this <see cref="IReadOnlyEntity"/>; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsComponent<TComponent>()
            where TComponent : IComponent;
    }
}