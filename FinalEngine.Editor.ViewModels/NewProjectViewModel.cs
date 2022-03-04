// <copyright file="NewProjectViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Windows.Input;
    using FinalEngine.Editor.Common.Exceptions;
    using FinalEngine.Editor.Common.Services;
    using FinalEngine.Editor.ViewModels.Events;
    using FinalEngine.Editor.ViewModels.Interaction;
    using FinalEngine.Editor.ViewModels.Validation;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;

    public class NewProjectViewModel : ObservableValidator, INewProjectViewModel
    {
        private readonly IProjectFileHandler projectFileHandler;

        private readonly IUserActionRequester userActionRequester;

        private ICommand? browseCommand;

        private IRelayCommand? createCommand;

        private string? projectLocation;

        private string? projectName;

        public NewProjectViewModel(IUserActionRequester userActionRequester, IProjectFileHandler projectFileHandler)
        {
            this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));
            this.projectFileHandler = projectFileHandler ?? throw new ArgumentNullException(nameof(projectFileHandler));

            this.ProjectName = "My Project";
            this.ProjectLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public event EventHandler<NewProjectEventArgs> ProjectCreated;

        public ICommand BrowseCommand
        {
            get { return this.browseCommand ??= new RelayCommand(this.Browse); }
        }

        public ICommand CreateCommand
        {
            get { return this.createCommand ??= new RelayCommand<ICloseable>(this.Create, (o) => !this.HasErrors); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must specify a project location.")]
        [Directory]
        public string ProjectLocation
        {
            get
            {
                return this.projectLocation ?? string.Empty;
            }

            set
            {
                this.SetProperty(ref this.projectLocation, value, true);
                this.createCommand?.NotifyCanExecuteChanged();
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must specify a project name.")]
        [File]
        public string ProjectName
        {
            get
            {
                return this.projectName ?? string.Empty;
            }

            set
            {
                this.SetProperty(ref this.projectName, value, true);
                this.createCommand?.NotifyCanExecuteChanged();
            }
        }

        private void Browse()
        {
            string? location = this.userActionRequester.RequestDirectoryLocation();

            if (location == null)
            {
                return;
            }

            this.ProjectLocation = location;
        }

        private void Create(ICloseable? closeable)
        {
            if (closeable == null)
            {
                throw new ArgumentNullException(nameof(closeable));
            }

            try
            {
                this.projectFileHandler.CreateNewProject(this.ProjectName, this.ProjectLocation);
            }
            catch (ProjectExistsException)
            {
                this.userActionRequester.RequestOk("New Project", "A project already exists with that name at the specified location.");
                return;
            }

            this.ProjectCreated?.Invoke(this, new NewProjectEventArgs(this.ProjectName));

            closeable.Close();
        }
    }
}