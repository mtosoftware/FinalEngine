// <copyright file="DirectoryItemViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System.Collections.ObjectModel;
    using FinalEngine.Editor.ViewModels.Extensions;

    public class DirectoryItemViewModel : FileItemViewModel
    {
        private bool isExpanded;

        public DirectoryItemViewModel()
        {
            this.Children = new ObservableCollection<FileItemViewModel>()
            {
                null!,
            };
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
                this.SetProperty(ref this.isExpanded, value);
                this.Expand();
            }
        }

        private void Expand()
        {
            this.Children.ConstructHierarchy(this.Path);
        }
    }
}