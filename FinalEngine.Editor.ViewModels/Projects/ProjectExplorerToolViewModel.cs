// <copyright file="ProjectExplorerToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Projects;

using System;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="IProjectExplorerToolViewModel"/>.
/// </summary>
/// <seealso cref="ToolViewModelBase" />
/// <seealso cref="IProjectExplorerToolViewModel" />
public sealed class ProjectExplorerToolViewModel : ToolViewModelBase, IProjectExplorerToolViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectExplorerToolViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    public ProjectExplorerToolViewModel(ILogger<ProjectExplorerToolViewModel> logger)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        this.Title = "Project Explorer";
        this.ContentID = "ProjectExplorer";

        logger.LogInformation($"Initializing {this.Title}...");
    }
}
