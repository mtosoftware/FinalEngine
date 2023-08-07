// <copyright file="ProjectExplorerToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Projects;

using CommunityToolkit.Mvvm.Messaging;

public sealed class ProjectExplorerToolViewModel : ToolViewModelBase, IProjectExplorerToolViewModel
{
    public ProjectExplorerToolViewModel(IMessenger messenger)
        : base(messenger)
    {
        this.Title = "Project Explorer";
        this.ContentID = "ProjectExplorer";
        this.PaneLocation = PaneLocation.Bottom;
    }
}
