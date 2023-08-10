// <copyright file="ProjectExplorerToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Projects;

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
    public ProjectExplorerToolViewModel()
    {
        this.Title = "Project Explorer";
        this.ContentID = "ProjectExplorer";
    }
}
