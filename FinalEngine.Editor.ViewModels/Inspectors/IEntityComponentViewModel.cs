// <copyright file="IEntityComponentViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.ECS;

/// <summary>
/// Defines an interface that represents a model of an entity component view.
/// </summary>
public interface IEntityComponentViewModel
{
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
}
