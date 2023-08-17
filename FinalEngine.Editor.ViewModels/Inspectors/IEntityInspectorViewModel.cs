// <copyright file="IEntityInspectorViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System.Collections.ObjectModel;
using FinalEngine.Editor.ViewModels.Components;

/// <summary>
/// Defines an interface that represents a model of the entity inspector view.
/// </summary>
public interface IEntityInspectorViewModel
{
    /// <summary>
    /// Gets the component view models for the entity.
    /// </summary>
    /// <value>
    /// The component view models for the entity.
    /// </value>
    ObservableCollection<IEntityComponentViewModel> ComponentViewModels { get; }
}
