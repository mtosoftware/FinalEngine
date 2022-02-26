// <copyright file="MainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.Text.Json;
    using System.Windows.Input;
    using FinalEngine.Editor.Common.Services;
    using FinalEngine.Editor.ViewModels.Interaction;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;

    public class MainViewModel : ObservableObject, IMainViewModel
    {
        private readonly IApplicationContext applicationContext;

        private readonly IProjectFileHandler projectFileHandler;

        private readonly IUserActionRequester userActionRequester;

        private readonly IViewModelFactory viewModelFactory;

        private readonly IViewPresenter viewPresenter;

        private ICommand? exitCommand;

        private ICommand? newProjectCommand;

        private ICommand? openProjectCommand;

        private string projectName;

        public MainViewModel(
            IViewModelFactory viewModelFactory,
            IViewPresenter viewPresenter,
            IApplicationContext applicationContext,
            IUserActionRequester userActionRequester,
            IProjectFileHandler projectFileHandler)
        {
            this.viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
            this.viewPresenter = viewPresenter ?? throw new ArgumentNullException(nameof(viewPresenter));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));
            this.projectFileHandler = projectFileHandler ?? throw new ArgumentNullException(nameof(projectFileHandler));
        }

        public ICommand ExitCommand
        {
            get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Exit); }
        }

        public ICommand NewProjectCommand
        {
            get { return this.newProjectCommand ??= new RelayCommand(this.ShowNewProjectView); }
        }

        public ICommand OpenProjectCommand
        {
            get { return this.openProjectCommand ??= new RelayCommand(this.ShowOpenProjectDialog); }
        }

        public string ProjectName
        {
            get { return this.projectName ?? string.Empty; }
            private set { this.SetProperty(ref this.projectName, value); }
        }

        private void Exit(ICloseable? closeable)
        {
            if (closeable == null)
            {
                throw new ArgumentNullException(nameof(closeable));
            }

            closeable.Close();
        }

        private void ShowNewProjectView()
        {
            this.viewPresenter.ShowNewProjectView(this.viewModelFactory.CreateNewProjectViewModel());
            this.ProjectName = this.applicationContext.Project?.Name ?? string.Empty;
        }

        private void ShowOpenProjectDialog()
        {
            string? file = this.userActionRequester.RequestFileLocation("Please select a project file.", "Final Engine Project File | *.feproj");

            if (file == null)
            {
                return;
            }

            try
            {
                this.projectFileHandler.OpenProject(file);
            }
            catch (JsonException)
            {
                this.userActionRequester.RequestOk("Open Project", "Failed to open project file.");
            }

            this.ProjectName = this.applicationContext.Project?.Name ?? string.Empty;
        }
    }
}