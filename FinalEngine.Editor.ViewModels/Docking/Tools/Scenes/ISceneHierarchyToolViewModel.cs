// <copyright file="ISceneHierarchyToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

using System.Collections.Generic;
using FinalEngine.ECS;

/// <summary>
/// Defines an interface that represents a model of a scene hierarchy tool view.
/// </summary>
/// <seealso cref="FinalEngine.Editor.ViewModels.Docking.Tools.IToolViewModel" />
public interface ISceneHierarchyToolViewModel : IToolViewModel
{
    /// <summary>
    /// Gets the entities within the active scene.
    /// </summary>
    /// <value>
    /// The entities within the active scene.
    /// </value>
    IReadOnlyCollection<Entity> Entities { get; }
}
