// <copyright file="NewProjectViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Windows.Input;
    using FinalEngine.Editor.Common.Exceptions;
    using FinalEngine.Editor.Common.Models;
    using FinalEngine.Editor.Common.Services.Projects;
    using FinalEngine.Editor.ViewModels.Interaction;
    using FinalEngine.Editor.ViewModels.Messages;
    using FinalEngine.Editor.ViewModels.Validation;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="INewProjectViewModel"/>.
    /// </summary>
    /// <seealso cref="ObservableValidator"/>
    /// <seealso cref="INewProjectViewModel"/>
    public class NewProjectViewModel : ObservableValidator, INewProjectViewModel
    {
        /// <summary>
        ///   The messenger.
        /// </summary>
        private readonly IMessenger messenger;

        /// <summary>
        ///   The project file handler.
        /// </summary>
        private readonly IProjectFileHandler projectFileHandler;

        /// <summary>
        ///   The user action requester.
        /// </summary>
        private readonly IUserActionRequester userActionRequester;

        /// <summary>
        ///   The browse command.
        /// </summary>
        private ICommand? browseCommand;

        /// <summary>
        ///   The create command.
        /// </summary>
        private IRelayCommand? createCommand;

        /// <summary>
        ///   The project location.
        /// </summary>
        private string? projectLocation;

        /// <summary>
        ///   The project name.
        /// </summary>
        private string? projectName;

        /// <summary>
        ///   Initializes a new instance of the <see cref="NewProjectViewModel"/> class.
        /// </summary>
        /// <param name="userActionRequester">
        ///   The user action requester.
        /// </param>
        /// <param name="projectFileHandler">
        ///   The project file handler.
        /// </param>
        /// <param name="messenger">
        ///   The messanger.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="userActionRequester"/> or <paramref name="projectFileHandler"/> parameter cannot be null.
        /// </exception>
        public NewProjectViewModel(IUserActionRequester userActionRequester, IProjectFileHandler projectFileHandler, IMessenger messenger)
        {
            this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));
            this.projectFileHandler = projectFileHandler ?? throw new ArgumentNullException(nameof(projectFileHandler));
            this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

            this.ProjectName = "My Project";
            this.ProjectLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        /// <summary>
        ///   Gets the browse command.
        /// </summary>
        /// <value>
        ///   The browse command.
        /// </value>
        public ICommand BrowseCommand
        {
            get { return this.browseCommand ??= new RelayCommand(this.Browse); }
        }

        /// <summary>
        ///   Gets the create command.
        /// </summary>
        /// <value>
        ///   The create command.
        /// </value>
        public ICommand CreateCommand
        {
            get { return this.createCommand ??= new RelayCommand<ICloseable>(this.Create, (o) => !this.HasErrors); }
        }

        /// <summary>
        ///   Gets or sets the project location.
        /// </summary>
        /// <value>
        ///   The project location.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must specify a project location.")]
        [Directory(ShouldExist = true)]
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

        /// <summary>
        ///   Gets or sets the name of the project.
        /// </summary>
        /// <value>
        ///   The name of the project.
        /// </value>
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

        /// <summary>
        ///   Browses for a project directory location for the project to be saved.
        /// </summary>
        private void Browse()
        {
            string? location = this.userActionRequester.RequestDirectoryLocation();

            if (location == null)
            {
                return;
            }

            this.ProjectLocation = location;
        }

        /// <summary>
        ///   Creats a new project and then closes the view.
        /// </summary>
        /// <param name="closeable">
        ///   The closeable.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="closeable"/> parameter cannot be null.
        /// </exception>
        private void Create(ICloseable? closeable)
        {
            if (closeable == null)
            {
                throw new ArgumentNullException(nameof(closeable));
            }

            try
            {
                Project project = this.projectFileHandler.CreateNewProject(this.ProjectName, this.ProjectLocation);
                this.messenger.Send(new ProjectChangedMessage(project));
            }
            catch (ProjectExistsException)
            {
                this.userActionRequester.RequestOk("New Project", "A project already exists with that name at the specified location.");
                return;
            }

            closeable.Close();
        }
    }
}