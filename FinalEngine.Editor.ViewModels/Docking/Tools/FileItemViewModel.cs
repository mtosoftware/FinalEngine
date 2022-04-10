// <copyright file="FileItemViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System.Collections.ObjectModel;
    using FinalEngine.Editor.ViewModels.Extensions;
    using Microsoft.Toolkit.Mvvm.ComponentModel;

    /// <summary>
    ///   Enumerates the available file item types for an <see cref="IFileItemViewModel"/>.
    /// </summary>
    public enum FileItemType
    {
        /// <summary>
        ///   The file item is a folder.
        /// </summary>
        Directory,

        /// <summary>
        ///   The file item is a file.
        /// </summary>
        File,
    }

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IFileItemViewModel"/>.
    /// </summary>
    /// <seealso cref="ObservableObject"/>
    /// <seealso cref="IFileItemViewModel"/>
    public class FileItemViewModel : ObservableObject, IFileItemViewModel
    {
        /// <summary>
        ///   Indicates whether this instance is expanded.
        /// </summary>
        private bool isExpanded;

        /// <summary>
        ///   Indicates whether this instance is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        ///   The name of the file item.
        /// </summary>
        private string? name;

        /// <summary>
        ///   The path of the file item on the file system.
        /// </summary>
        private string? path;

        /// <summary>
        ///   The file item type.
        /// </summary>
        private FileItemType type;

        /// <summary>
        ///   Initializes a new instance of the <see cref="FileItemViewModel"/> class.
        /// </summary>
        /// <param name="type">
        ///   The file item type.
        /// </param>
        public FileItemViewModel(FileItemType type)
        {
            this.Type = type;
            this.Children = new ObservableCollection<FileItemViewModel>();
        }

        /// <summary>
        ///   Gets the children.
        /// </summary>
        /// <value>
        ///   The children.
        /// </value>
        public ObservableCollection<FileItemViewModel> Children { get; }

        /// <summary>
        ///   Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }

            set
            {
                if (!this.CanExpand)
                {
                    return;
                }

                this.SetProperty(ref this.isExpanded, value);
                this.Expand();
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.SetProperty(ref this.isSelected, value); }
        }

        /// <summary>
        ///   Gets or sets the name.
        /// </summary>
        /// <value>
        ///   The name.
        /// </value>
        public string Name
        {
            get { return this.name ?? string.Empty; }
            set { this.SetProperty(ref this.name, value); }
        }

        /// <summary>
        ///   Gets or sets the path.
        /// </summary>
        /// <value>
        ///   The path.
        /// </value>
        public string Path
        {
            get { return this.path ?? string.Empty; }
            set { this.SetProperty(ref this.path, value); }
        }

        /// <summary>
        ///   Gets or sets the item type.
        /// </summary>
        /// <value>
        ///   The item type.
        /// </value>
        public FileItemType Type
        {
            get { return this.type; }
            set { this.SetProperty(ref this.type, value); }
        }

        /// <summary>
        ///   Gets a value indicating whether this instance can expand.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can expand; otherwise, <c>false</c>.
        /// </value>
        private bool CanExpand
        {
            get { return this.Children.Count > 0; }
        }

        /// <summary>
        ///   Collapses all children within this file item.
        /// </summary>
        public void CollapseAll()
        {
            this.IsExpanded = false;

            foreach (FileItemViewModel? child in this.Children)
            {
                if (child == null)
                {
                    continue;
                }

                child.CollapseAll();
            }
        }

        /// <summary>
        ///   Expands all children within this file item.
        /// </summary>
        public void ExpandAll()
        {
            this.IsExpanded = true;

            foreach (FileItemViewModel? child in this.Children)
            {
                if (child == null)
                {
                    continue;
                }

                child.ExpandAll();
            }
        }

        /// <summary>
        ///   Expands the file item, construct the inner hierarchy of it's potential children.
        /// </summary>
        private void Expand()
        {
            if (string.IsNullOrWhiteSpace(this.Path))
            {
                return;
            }

            this.Children.ConstructHierarchy(this.Path);
        }
    }
}