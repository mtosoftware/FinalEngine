// <copyright file="FileItemViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using Microsoft.Toolkit.Mvvm.ComponentModel;

    public class FileItemViewModel : ObservableObject
    {
        private string? name;

        private string? path;

        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        public string Path
        {
            get { return this.path; }
            set { this.SetProperty(ref this.path, value); }
        }
    }
}