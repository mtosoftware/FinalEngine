// <copyright file="EntitySystemProcessAttribute.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

using System;

/// <summary>
///   Indicates the execution type of an entity system.
/// </summary>
/// <seealso cref="Attribute"/>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class EntitySystemProcessAttribute : Attribute
{
    /// <summary>
    ///   Gets the execution type.
    /// </summary>
    /// <value>
    ///   The execution type.
    /// </value>
    public GameLoopType ExecutionType { get; init; }
}
