// <copyright file="ProjectExplorerViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using FinalEngine.Editor.Common.Events;
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

            IEnumerable<string> directories = Directory.EnumerateDirectories(location, string.Empty, SearchOption.TopDirectoryOnly);

            foreach (string directory in directories)
            {
                var node = new DirectoryNode()
                {
                    Name = Path.GetFileName(directory),
                    DirectoryPath = directory,
                };

                this.FileNodes.Add(node);
            }

            IEnumerable<string> files = Directory.EnumerateFiles(location, string.Empty, SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                var node = new FileNode()
                {
                    Name = Path.GetFileName(file),
                    DirectoryPath = file,
                };

                this.FileNodes.Add(node);
            }
        }

        private void ProjectFileHandler_ProjectChanged(object? sender, ProjectChangedEventArgs e)
        {
            this.FileNodes.Clear();
            this.ConstructFileNodes(e.Location);
        }
    }
}