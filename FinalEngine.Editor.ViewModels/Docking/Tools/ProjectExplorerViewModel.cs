// <copyright file="ProjectExplorerViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System;
    using System.Collections.ObjectModel;
    using FinalEngine.Editor.Common.Events;
    using FinalEngine.Editor.Common.Services;
    using FinalEngine.Editor.ViewModels.Extensions;

    //// TODO: Documentation
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
            this.FileNodes = new ObservableCollection<FileItemViewModel>();

            projectFileHandler.ProjectChanged += this.ProjectFileHandler_ProjectChanged;
        }

        public ObservableCollection<FileItemViewModel> FileNodes { get; }

        private void ProjectFileHandler_ProjectChanged(object? sender, ProjectChangedEventArgs e)
        {
            this.FileNodes.ConstructHierarchy(e.Location);
        }
    }
}