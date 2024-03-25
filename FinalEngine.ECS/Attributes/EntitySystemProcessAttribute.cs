// <copyright file="EntitySystemProcessAttribute.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Attributes;

using System;

/// <summary>
/// Provides an attribute used to determine when an <see cref="EntitySystemBase"/> will execute.
/// </summary>
/// <seealso cref="System.Attribute" />
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class EntitySystemProcessAttribute : Attribute
{
    /// <summary>
    /// Gets the type of the execution.
    /// </summary>
    ///
    /// <value>
    /// The type of the execution.
    /// </value>
    ///
    /// <remarks>
    /// The <see cref="ExecutionType"/> determines when the associated system will be executed.
    /// </remarks>
    public GameLoopType ExecutionType { get; init; }
}
