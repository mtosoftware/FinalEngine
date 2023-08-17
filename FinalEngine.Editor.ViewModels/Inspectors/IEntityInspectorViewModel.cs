// <copyright file="IEntityInspectorViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System.Collections.Generic;

/// <summary>
/// Defines an interface that represents a model of the entity inspector view.
/// </summary>
public interface IEntityInspectorViewModel
{
    /// <summary>
    /// Gets the component view models.
    /// </summary>
    /// <value>
    /// The component view models.
    /// </value>
    ICollection<IEntityComponentViewModel> ComponentViewModels { get; }
}
