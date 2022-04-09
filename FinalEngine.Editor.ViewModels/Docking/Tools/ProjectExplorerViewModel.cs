// <copyright file="ProjectExplorerViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using FinalEngine.Editor.Common.Events;
    using FinalEngine.Editor.Common.Services;
    using FinalEngine.Editor.ViewModels.Extensions;
    using Microsoft.Toolkit.Mvvm.Input;

    //// TODO: Documentation
    //// TODO: From there, document again and I think this feature is done.

    public class ProjectExplorerViewModel : ToolViewModelBase, IProjectExplorerViewModel
    {
        private readonly ICommand? newFolderCommand;

        private bool canViewToolBar;

        private ICommand? collapseAllCommand;

        private ICommand? expandAllCommand;

        private ICommand? refreshCommand;

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

        public ICommand RefreshCommand
        {
            get { return this.refreshCommand ??= new RelayCommand(this.Refresh); }
        }

        private FileItemViewModel? SelectedItem
        {
            get { return this.FileNodes.FirstOrDefault(x => x.IsSelected); }
        }

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

        private void Refresh()
        {
            this.CollapseAll();
        }
    }
}