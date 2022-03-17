// <copyright file="ProjectExplorerViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System;
    using System.Collections.ObjectModel;
    using FinalEngine.Editor.Common.Events;
    using FinalEngine.Editor.Common.Extensions;
    using FinalEngine.Editor.Common.Models;
    using FinalEngine.Editor.Common.Services;

    //// TODO: Beautiful icons - Any icons that are not supported should just be a blank file icon (easier and better than interoping) - folders should have open and close icons.
    //// TODO: Documentation
    //// TODO: Add a toolbar to the top of the view for expanding/collapsing all nodes - also add support for creating new folders (files will come later) - folders created via right click context AND toolbar.
    //// TODO: Think about the whole assets/resource directory thing... Maybe the Project model should have a static property AssetsDirectoryName = "Assets"? Or should it not be static? who knows.
    //// TODO: Fix that stupid binding issue with file nodes.
    //// TODO: From there, document again and I think this feature is done.

    public class ProjectExplorerViewModel : ToolViewModelBase, IProjectExplorerViewModel
    {
        public ProjectExplorerViewModel(IProjectFileHandler projectFileHandler)
        {
            if (projectFileHandler == null)
            {
                throw new ArgumentNullException(nameof(projectFileHandler));
            }

            this.Title = "Project Explorer";
            this.ContentID = "ProjectExplorerTool";
            this.FileNodes = new ObservableCollection<FileNode>();

            projectFileHandler.ProjectChanged += this.ProjectFileHandler_ProjectChanged;
        }

        public ObservableCollection<FileNode> FileNodes { get; }

        private void ProjectFileHandler_ProjectChanged(object? sender, ProjectChangedEventArgs e)
        {
            this.FileNodes.ConstructHierarchy(e.Location);
        }
    }
}