// <copyright file="FileItemViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System.Collections.ObjectModel;
    using FinalEngine.Editor.ViewModels.Extensions;
    using Microsoft.Toolkit.Mvvm.ComponentModel;

    public class FileItemViewModel : ObservableObject
    {
        private bool isExpanded;

        private string? name;

        private string? path;

        public FileItemViewModel()
        {
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

        private bool CanExpand
        {
            get { return this.Children.Count > 0; }
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