// <copyright file="ICreateEntityViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Entities;

using System;
using CommunityToolkit.Mvvm.Input;

/// <summary>
/// Defines an interface that represents a model of the create entity view.
/// </summary>
public interface ICreateEntityViewModel
{
    /// <summary>
    /// Gets the create command.
    /// </summary>
    /// <value>
    /// The create command.
    /// </value>
    /// <remarks>
    /// The <see cref="CreateCommand"/> creates a new entity and adds it to the current scene.
    /// </remarks>
    IRelayCommand CreateCommand { get; }

    /// <summary>
    /// Gets the entity unique identifier.
    /// </summary>
    /// <value>
    /// The entity unique identifier.
    /// </value>
    Guid EntityGuid { get; }

    /// <summary>
    /// Gets or sets the name of the entity.
    /// </summary>
    /// <value>
    /// The name of the entity.
    /// </value>
    string EntityName { get; set; }

    /// <summary>
    /// Gets the title of the view.
    /// </summary>
    /// <value>
    /// The title of the view.
    /// </value>
    string Title { get; }
}
