// <copyright file="ISceneHierarchyToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.ECS;

/// <summary>
/// Defines an interface that represents a model of a scene hierarchy tool view.
/// </summary>
/// <seealso cref="IToolViewModel" />
public interface ISceneHierarchyToolViewModel : IToolViewModel
{
    /// <summary>
    /// Gets the delete entity command.
    /// </summary>
    /// <value>
    /// The delete entity command.
    /// </value>
    /// <remarks>
    /// Deletes the <see cref="SelectedEntity"/> from the hierarchy.
    /// </remarks>
    IRelayCommand DeleteEntityCommand { get; }

    /// <summary>
    /// Gets the entities within the active scene.
    /// </summary>
    /// <value>
    /// The entities within the active scene.
    /// </value>
    IReadOnlyCollection<Entity> Entities { get; }

    /// <summary>
    /// Gets or sets the selected entity.
    /// </summary>
    /// <value>
    /// The selected entity.
    /// </value>
    Entity? SelectedEntity { get; set; }
}
