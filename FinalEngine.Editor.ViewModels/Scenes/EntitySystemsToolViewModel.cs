// <copyright file="EntitySystemsToolViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using Microsoft.Extensions.Logging;

public sealed class EntitySystemsToolViewModel : ToolViewModelBase, IEntitySystemsToolViewModel
{
    public EntitySystemsToolViewModel(ILogger<EntitySystemsToolViewModel> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.Title = "Entity Systems";
        this.ContentID = "EntitySystems";

        logger.LogInformation($"Initializing {this.Title}...");
    }
}
