// <copyright file="INewProjectViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Projects;

using System.Windows.Input;

public interface INewProjectViewModel : IViewModel
{
    ICommand BrowseCommand { get; }

    string ProjectLocation { get; set; }

    string ProjectName { get; set; }
}
