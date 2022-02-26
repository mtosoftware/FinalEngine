// <copyright file="INewProjectViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System.Windows.Input;

    public interface INewProjectViewModel
    {
        ICommand BrowseCommand { get; }

        ICommand CreateCommand { get; }

        string ProjectLocation { get; set; }

        string ProjectName { get; set; }
    }
}