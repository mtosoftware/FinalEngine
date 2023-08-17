// <copyright file="EntitySystemsToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="IEntitySystemsToolViewModel"/>.
/// </summary>
/// <seealso cref="ToolViewModelBase" />
/// <seealso cref="IEntitySystemsToolViewModel" />
public sealed class EntitySystemsToolViewModel : ToolViewModelBase, IEntitySystemsToolViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntitySystemsToolViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    public EntitySystemsToolViewModel(ILogger<EntitySystemsToolViewModel> logger)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        this.Title = "Entity Systems";
        this.ContentID = "EntitySystems";

        logger.LogInformation($"Initializing {this.Title}...");
    }
}
