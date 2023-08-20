// <copyright file="IEntityComponent.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

using System.Diagnostics.CodeAnalysis;

/// <summary>
///   Defines an interface that represents a component, which is pure data.
/// </summary>
[SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Components are raw data that should be implemented by the user.")]
public interface IEntityComponent
{
}
