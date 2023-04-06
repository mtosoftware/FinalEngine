// <copyright file="NewProjectViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Projects;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Interaction;
using Microsoft.Extensions.Logging;

//// TODO: Key bindings will work regardless of whether or not a dialog is open.
//// TODO: I think a good way to solve this problem might be through command targets, but ALSO look here: https://stackoverflow.com/questions/23316274/inputbindings-work-only-when-focused
//// TODO: There must be a way on the front-end in XAML to disable use of key bindings when the main window is not focused, surely.
//// TODO: Otherwise, create a way to ensure that the current view is not displayed; GROSS.

public sealed class NewProjectViewModel : ObservableObject, INewProjectViewModel
{
    private readonly ILogger<NewProjectViewModel> logger;

    private readonly IUserActionRequester userActionRequester;

    private ICommand? browseCommand;

    private string? projectLocation;

    private string? projectName;

    public NewProjectViewModel(ILogger<NewProjectViewModel> logger, IUserActionRequester userActionRequester)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));

        //// TODO: Get cool project name from some API, perhaps related to some sort of fantasy naming? Or maybe fruits? I dunno...
        this.ProjectName = "My Project";
        this.ProjectLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    public ICommand BrowseCommand
    {
        get { return this.browseCommand ??= new RelayCommand(this.Browse); }
    }

    public string ProjectLocation
    {
        get { return this.projectLocation ?? string.Empty; }
        set { this.SetProperty(ref this.projectLocation, value); }
    }

    public string ProjectName
    {
        get { return this.projectName ?? string.Empty; }
        set { this.SetProperty(ref this.projectName, value); }
    }

    private void Browse()
    {
        this.logger.LogInformation("Request new project location...");

        string? directory = this.userActionRequester.RequestDirectoryLocation();

        if (directory == null)
        {
            return;
        }

        this.ProjectLocation = directory;
    }
}
