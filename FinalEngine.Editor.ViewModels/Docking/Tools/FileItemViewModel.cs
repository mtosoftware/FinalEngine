// <copyright file="FileItemViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System.Collections.ObjectModel;
    using FinalEngine.Editor.ViewModels.Extensions;
    using Microsoft.Toolkit.Mvvm.ComponentModel;

    public enum FileItemType
    {
        Directory,

        File
    }

    public class FileItemViewModel : ObservableObject
    {
        private bool isExpanded;

        private bool isSelected;

        private string? name;

        private string? path;

        private FileItemType type;

        public FileItemViewModel(FileItemType type)
        {
            this.Type = type;
            this.Children = new ObservableCollection<FileItemViewModel>();
        }

        public ObservableCollection<FileItemViewModel> Children { get; }

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

        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.SetProperty(ref this.isSelected, value); }
        }

        public string Name
        {
            get { return this.name ?? string.Empty; }
            set { this.SetProperty(ref this.name, value); }
        }

        public string Path
        {
            get { return this.path ?? string.Empty; }
            set { this.SetProperty(ref this.path, value); }
        }

        public FileItemType Type
        {
            get { return this.type; }
            set { this.SetProperty(ref this.type, value); }
        }

        private bool CanExpand
        {
            get { return this.Children.Count > 0; }
        }

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