// <copyright file="IEntityComponentViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Components;

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// Defines an interface that represents a model of an entity component view.
/// </summary>
public interface IEntityComponentViewModel
{
    /// <summary>
    /// Gets the name of the component.
    /// </summary>
    /// <value>
    /// The name of the component.
    /// </value>
    string Name { get; }

    /// <summary>
    /// Gets the property view models for the component.
    /// </summary>
    /// <value>
    /// The property view models for the component.
    /// </value>
    ObservableCollection<ObservableObject> PropertyViewModels { get; }
}
