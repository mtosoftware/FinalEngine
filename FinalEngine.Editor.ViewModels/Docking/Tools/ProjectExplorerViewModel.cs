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

        private void ConstructFileNodes(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            this.FileNodes.ConstructHierarchy(location);
        }

        private void ProjectFileHandler_ProjectChanged(object? sender, ProjectChangedEventArgs e)
        {
            this.ConstructFileNodes(e.Location);
        }
    }
}