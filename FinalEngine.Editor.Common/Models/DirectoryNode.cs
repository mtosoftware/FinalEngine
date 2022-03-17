// <copyright file="DirectoryNode.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using FinalEngine.Editor.Common.Extensions;

    public class DirectoryNode : FileNode, INotifyPropertyChanged
    {
        private bool isExpanded;

        public DirectoryNode()
        {
            this.Children = new ObservableCollection<FileNode>()
            {
                null!,
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<FileNode> Children { get; }

        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }

            set
            {
                if (this.isExpanded == value)
                {
                    return;
                }

                this.isExpanded = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsExpanded)));
                this.Expand();
            }
        }

        private void Expand()
        {
            if (this.Children.Count > 1)
            {
                return;
            }

            this.Children.ConstructHierarchy(this.DirectoryPath);
        }
    }
}