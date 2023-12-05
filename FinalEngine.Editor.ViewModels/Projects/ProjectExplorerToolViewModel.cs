// <copyright file="ProjectExplorerToolViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Projects;

using System;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using Microsoft.Extensions.Logging;

public sealed class ProjectExplorerToolViewModel : ToolViewModelBase, IProjectExplorerToolViewModel
{
    public ProjectExplorerToolViewModel(ILogger<ProjectExplorerToolViewModel> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.Title = "Project Explorer";
        this.ContentID = "ProjectExplorer";

        logger.LogInformation($"Initializing {this.Title}...");
    }
}
