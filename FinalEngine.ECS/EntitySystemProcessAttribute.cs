// <copyright file="EntitySystemProcessAttribute.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class EntitySystemProcessAttribute : Attribute
{
    public GameLoopType ExecutionType { get; init; }
}
