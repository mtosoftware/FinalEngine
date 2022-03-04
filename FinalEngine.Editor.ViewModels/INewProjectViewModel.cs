// <copyright file="INewProjectViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.Windows.Input;
    using FinalEngine.Editor.ViewModels.Events;

    public interface INewProjectViewModel
    {
        event EventHandler<NewProjectEventArgs> ProjectCreated;

        ICommand BrowseCommand { get; }

        ICommand CreateCommand { get; }

        string ProjectLocation { get; set; }

        string ProjectName { get; set; }
    }
}