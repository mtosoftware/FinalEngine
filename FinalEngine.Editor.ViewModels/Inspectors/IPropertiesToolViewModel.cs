// <copyright file="IPropertiesToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.Editor.ViewModels.Docking.Tools;

/// <summary>
/// Defines an interface that represents a model of a properties tool view.
/// </summary>
/// <seealso cref="IToolViewModel" />
public interface IPropertiesToolViewModel : IToolViewModel
{
    /// <summary>
    /// Gets the current view model.
    /// </summary>
    /// <value>
    /// The current view model.
    /// </value>
    ObservableObject? CurrentViewModel { get; }
}
