// <copyright file="ProjectExplorerViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using FinalEngine.Editor.Common.Models;
    using FinalEngine.Editor.ViewModels.Extensions;
    using FinalEngine.Editor.ViewModels.Messages;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IProjectExplorerViewModel"/>.
    /// </summary>
    /// <seealso cref="ToolViewModelBase"/>
    /// <seealso cref="IProjectExplorerViewModel"/>
    public class ProjectExplorerViewModel : ToolViewModelBase, IProjectExplorerViewModel
    {
        /// <summary>
        ///   Indicates whether the tool bar can be shown.
        /// </summary>
        private bool canShowToolBar;

        /// <summary>
        ///   The collapse all command.
        /// </summary>
        private ICommand? collapseAllCommand;

        /// <summary>
        ///   The expand all command.
        /// </summary>
        private ICommand? expandAllCommand;

        /// <summary>
        ///   The refresh command.
        /// </summary>
        private ICommand? refreshCommand;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectExplorerViewModel"/> class.
        /// </summary>
        /// <param name="projectFileHandler">
        ///   The project file handler.
        /// </param>
        /// <param name="messenger">
        ///   The messanger.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="projectFileHandler"/> parameter cannot be null.
        /// </exception>
        public ProjectExplorerViewModel(IMessenger messenger)
        {
            if (messenger == null)
            {
                throw new ArgumentNullException(nameof(messenger));
            }

            this.Title = "Project Explorer";
            this.ContentID = "ProjectExplorerTool";
            this.FileNodes = new ObservableCollection<FileItemViewModel>();

            messenger.Register<ProjectExplorerViewModel, ProjectChangedMessage>(this, (r, m) => r.HandleProjectChanged(m));
        }

        /// <summary>
        ///   Gets or sets a value indicating whether this instance can show the tool bar.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can show the tool bar; otherwise, <c>false</c>.
        /// </value>
        public bool CanShowToolBar
        {
            get { return this.canShowToolBar; }
            set { this.SetProperty(ref this.canShowToolBar, value); }
        }

        /// <summary>
        ///   Gets the collapse all command.
        /// </summary>
        /// <value>
        ///   The collapse all command.
        /// </value>
        public ICommand CollapseAllCommand
        {
            get { return this.collapseAllCommand ??= new RelayCommand(this.CollapseAll); }
        }

        /// <summary>
        ///   Gets the expand all command.
        /// </summary>
        /// <value>
        ///   The expand all command.
        /// </value>
        public ICommand ExpandAllCommand
        {
            get { return this.expandAllCommand ??= new RelayCommand(this.ExpandAll); }
        }

        /// <summary>
        ///   Gets the file nodes within the directory of the current project.
        /// </summary>
        /// <value>
        ///   The file nodes within the directory of the current project.
        /// </value>
        public ObservableCollection<FileItemViewModel> FileNodes { get; }

        /// <summary>
        ///   Gets the refresh command.
        /// </summary>
        /// <value>
        ///   The refresh command.
        /// </value>
        public ICommand RefreshCommand
        {
            get { return this.refreshCommand ??= new RelayCommand(this.Refresh); }
        }

        /// <summary>
        ///   Recursively collapses all file nodes.
        /// </summary>
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

        /// <summary>
        ///   Recursively expansd all file nodes.
        /// </summary>
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

        /// <summary>
        ///   Handles when the new project has opened.
        /// </summary>
        /// <param name="message">
        ///   The message.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="message"/> parameter cannot be null.
        /// </exception>
        private void HandleProjectChanged(ProjectChangedMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Project project = message.Project;

            this.FileNodes.ConstructHierarchy(project.Location);
            this.CanShowToolBar = true;
        }

        /// <summary>
        ///   Refreshes the explorer.
        /// </summary>
        /// <remarks>
        ///   This function currently just recursively collapses all file nodes.
        /// </remarks>
        private void Refresh()
        {
            this.CollapseAll();
        }
    }
}