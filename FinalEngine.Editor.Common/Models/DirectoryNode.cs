// <copyright file="DirectoryNode.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;

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

        // TODO: Turn this into an extension method since it's literally the exact same thing.
        private void Expand()
        {
            if (this.Children.Count > 1)
            {
                return;
            }

            // TODO: Handle access denied issue, something to do with hidden and system files, etc.
            IEnumerable<string> directories = Directory.EnumerateDirectories(this.DirectoryPath, string.Empty, SearchOption.TopDirectoryOnly);

            foreach (string directory in directories)
            {
                var node = new DirectoryNode()
                {
                    Name = Path.GetFileName(directory),
                    DirectoryPath = directory,
                };

                this.Children.Add(node);
            }

            // TODO: Handle access denied issue, something to do with hidden and system files, etc.
            IEnumerable<string> files = Directory.EnumerateFiles(this.DirectoryPath, string.Empty, SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                var node = new FileNode()
                {
                    Name = Path.GetFileName(file),
                    DirectoryPath = file,
                };

                this.Children.Add(node);
            }
        }
    }
}