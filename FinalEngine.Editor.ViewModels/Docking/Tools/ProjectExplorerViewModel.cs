// <copyright file="ProjectExplorerViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using FinalEngine.Editor.Common.Events;
    using FinalEngine.Editor.Common.Services;
    using FinalEngine.Editor.ViewModels.Extensions;
    using Microsoft.Toolkit.Mvvm.Input;

    //// TODO: Documentation
    //// TODO: From there, document again and I think this feature is done.

    public class ProjectExplorerViewModel : ToolViewModelBase, IProjectExplorerViewModel
    {
        private bool canViewToolBar;

        private IRelayCommand? collapseAllCommand;

        private IRelayCommand? expandAllCommand;

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

        public bool CanViewToolBar
        {
            get { return this.canViewToolBar; }
            set { this.SetProperty(ref this.canViewToolBar, value); }
        }

        public ICommand CollapseAllCommand
        {
            get { return this.collapseAllCommand ??= new RelayCommand(this.CollapseAll); }
        }

        public ICommand ExpandAllCommand
        {
            get { return this.expandAllCommand ??= new RelayCommand(this.ExpandAll); }
        }

        public ObservableCollection<FileItemViewModel> FileNodes { get; }

        private void CollapseAll()
        {
            foreach (FileItemViewModel? node in this.FileNodes)
            {
                if (node == null)
                {
                    continue;
                }

                node.CollapseAll();
            }
        }

        private void ExpandAll()
        {
            foreach (FileItemViewModel? node in this.FileNodes)
            {
                if (node == null)
                {
                    continue;
                }

                node.ExpandAll();
            }
        }

        private void ProjectFileHandler_ProjectChanged(object? sender, ProjectChangedEventArgs e)
        {
            this.FileNodes.ConstructHierarchy(e.Location);
            this.CanViewToolBar = true;
        }
    }
}