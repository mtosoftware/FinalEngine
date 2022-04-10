// <copyright file="IProjectExplorerViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    /// <summary>
    ///   Defines an interface that represents a project explorer, used to handle resources within a game project.
    /// </summary>
    /// <seealso cref="FinalEngine.Editor.ViewModels.Docking.IToolViewModel"/>
    public interface IProjectExplorerViewModel : IToolViewModel
    {
        /// <summary>
        ///   Gets or sets a value indicating whether this instance can show the tool bar.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can show the tool bar; otherwise, <c>false</c>.
        /// </value>
        bool CanShowToolBar { get; set; }

        /// <summary>
        ///   Gets the collapse all command.
        /// </summary>
        /// <value>
        ///   The collapse all command.
        /// </value>
        ICommand CollapseAllCommand { get; }

        /// <summary>
        ///   Gets the expand all command.
        /// </summary>
        /// <value>
        ///   The expand all command.
        /// </value>
        ICommand ExpandAllCommand { get; }

        /// <summary>
        ///   Gets the file nodes within the directory of the current project.
        /// </summary>
        /// <value>
        ///   The file nodes within the directory of the current project.
        /// </value>
        ObservableCollection<FileItemViewModel> FileNodes { get; }
    }
}