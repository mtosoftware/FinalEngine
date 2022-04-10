// <copyright file="IFileItemViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System.Collections.ObjectModel;

    /// <summary>
    ///   Defines an interface that represents an individual file item within an <see cref="IProjectExplorerViewModel"/>.
    /// </summary>
    public interface IFileItemViewModel
    {
        /// <summary>
        ///   Gets the children.
        /// </summary>
        /// <value>
        ///   The children.
        /// </value>
        ObservableCollection<FileItemViewModel> Children { get; }

        /// <summary>
        ///   Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        bool IsExpanded { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        bool IsSelected { get; set; }

        /// <summary>
        ///   Gets or sets the name.
        /// </summary>
        /// <value>
        ///   The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        ///   Gets or sets the path.
        /// </summary>
        /// <value>
        ///   The path.
        /// </value>
        string Path { get; set; }

        /// <summary>
        ///   Gets or sets the item type.
        /// </summary>
        /// <value>
        ///   The item type.
        /// </value>
        FileItemType Type { get; set; }

        /// <summary>
        ///   Collapses all children within this file item.
        /// </summary>
        void CollapseAll();

        /// <summary>
        ///   Expands all children within this file item.
        /// </summary>
        void ExpandAll();
    }
}