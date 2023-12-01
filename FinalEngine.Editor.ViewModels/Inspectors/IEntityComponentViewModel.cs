// <copyright file="IEntityComponentViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.ECS;

/// <summary>
/// Defines an interface that represents a model of an entity component view.
/// </summary>
public interface IEntityComponentViewModel
{
    /// <summary>
    /// Gets a value indicating whether the components properties are visible.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the components properties are visible; otherwise, <c>false</c>.
    /// </value>
    bool IsVisible { get; }

    /// <summary>
    /// Gets the name of the <see cref="IEntityComponent"/>.
    /// </summary>
    /// <value>
    /// The name of the <see cref="IEntityComponent"/>.
    /// </value>
    string Name { get; }

    /// <summary>
    /// Gets the property view models.
    /// </summary>
    /// <value>
    /// The property view models.
    /// </value>
    ICollection<ObservableObject> PropertyViewModels { get; }

    /// <summary>
    /// Gets the toggle command.
    /// </summary>
    /// <value>
    /// The toggle command.
    /// </value>
    /// <remarks>
    /// The <see cref="ToggleCommand"/> is used to toggle the visibility of the components properties.
    /// </remarks>
    ICommand ToggleCommand { get; }

    ICommand RemoveCommand { get; }
}
