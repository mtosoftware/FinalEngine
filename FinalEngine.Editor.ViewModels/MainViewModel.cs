// <copyright file="MainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using System.Windows.Input;
    using FinalEngine.Editor.Common.Events;
    using FinalEngine.Editor.Common.Services;
    using FinalEngine.Editor.ViewModels.Docking;
    using FinalEngine.Editor.ViewModels.Interaction;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IMainViewModel"/>.
    /// </summary>
    /// <seealso cref="ObservableObject"/>
    /// <seealso cref="IMainViewModel"/>
    public sealed class MainViewModel : ObservableObject, IMainViewModel, IDisposable
    {
        /// <summary>
        ///   The project file handler.
        /// </summary>
        private readonly IProjectFileHandler projectFileHandler;

        /// <summary>
        ///   The user action requester.
        /// </summary>
        private readonly IUserActionRequester userActionRequester;

        /// <summary>
        ///   The view model factory.
        /// </summary>
        private readonly IViewModelFactory viewModelFactory;

        /// <summary>
        ///   The view presenter.
        /// </summary>
        private readonly IViewPresenter viewPresenter;

        /// <summary>
        ///   The exit command.
        /// </summary>
        private ICommand? exitCommand;

        /// <summary>
        ///   Indicates whether this instance is disposed.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        ///   The new project command.
        /// </summary>
        private ICommand? newProjectCommand;

        /// <summary>
        ///   The open proejct command.
        /// </summary>
        private ICommand? openProejctCommand;

        /// <summary>
        ///   The project name.
        /// </summary>
        private string? projectName;

        /// <summary>
        ///   The toggle tool window command.
        /// </summary>
        private ICommand? toggleToolWindowCommand;

        /// <summary>
        ///   Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="viewModelFactory">
        ///   The view model factory.
        /// </param>
        /// <param name="viewPresenter">
        ///   The view presenter.
        /// </param>
        /// <param name="userActionRequester">
        ///   The user action requester.
        /// </param>
        /// <param name="projectFileHandler">
        ///   The project file handler.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="viewModelFactory"/>, <paramref name="viewPresenter"/>, <paramref name="userActionRequester"/> or <paramref name="projectFileHandler"/> parameter cannot be null.
        /// </exception>
        public MainViewModel(
            IViewModelFactory viewModelFactory,
            IViewPresenter viewPresenter,
            IUserActionRequester userActionRequester,
            IProjectFileHandler projectFileHandler)
        {
            this.viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
            this.viewPresenter = viewPresenter ?? throw new ArgumentNullException(nameof(viewPresenter));
            this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));
            this.projectFileHandler = projectFileHandler ?? throw new ArgumentNullException(nameof(projectFileHandler));

            this.DockViewModel = this.viewModelFactory.CreateDockViewModel();

            this.projectFileHandler.ProjectChanged += this.ProjectFileHandler_ProjectChanged;
        }

        /// <summary>
        ///   Finalizes an instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        ~MainViewModel()
        {
            this.Dispose(false);
        }

        /// <summary>
        ///   Gets the dock view model.
        /// </summary>
        /// <value>
        ///   The dock view model.
        /// </value>
        public IDockViewModel DockViewModel { get; }

        /// <summary>
        ///   Gets the exit command.
        /// </summary>
        /// <value>
        ///   The exit command.
        /// </value>
        public ICommand ExitCommand
        {
            get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Exit); }
        }

        /// <summary>
        ///   Gets the new project command.
        /// </summary>
        /// <value>
        ///   The new project command.
        /// </value>
        public ICommand NewProjectCommand
        {
            get { return this.newProjectCommand ??= new RelayCommand(this.ShowNewProjectView); }
        }

        /// <summary>
        ///   Gets the open project command.
        /// </summary>
        /// <value>
        ///   The open project command.
        /// </value>
        public ICommand OpenProjectCommand
        {
            get { return this.openProejctCommand ??= new RelayCommand(this.ShowOpenProjectDialog); }
        }

        /// <summary>
        ///   Gets the name of the project that is currently open.
        /// </summary>
        /// <value>
        ///   The name of the project that is currently open.
        /// </value>
        public string ProjectName
        {
            get { return this.projectName ?? string.Empty; }
            private set { this.SetProperty(ref this.projectName, value); }
        }

        /// <summary>
        ///   Gets the toggle tool window command.
        /// </summary>
        /// <value>
        ///   The toggle tool window command.
        /// </value>
        public ICommand ToggleToolWindowCommand
        {
            get { return this.toggleToolWindowCommand ??= new RelayCommand<string>(this.ToggleToolWindow); }
        }

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.projectFileHandler.ProjectChanged -= this.ProjectFileHandler_ProjectChanged;
            }

            this.isDisposed = true;
        }

        /// <summary>
        ///   Exits the main application.
        /// </summary>
        /// <param name="closeable">
        ///   The closeable used to exit he application.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="closeable"/> parameter cannot be null.
        /// </exception>
        private void Exit(ICloseable? closeable)
        {
            if (closeable == null)
            {
                throw new ArgumentNullException(nameof(closeable));
            }

            closeable.Close();
        }

        /// <summary>
        ///   Handles the <see cref="IProjectFileHandler.ProjectChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///   The sender.
        /// </param>
        /// <param name="e">
        ///   The <see cref="ProjectChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void ProjectFileHandler_ProjectChanged(object? sender, ProjectChangedEventArgs e)
        {
            this.ProjectName = e.Name;
        }

        /// <summary>
        ///   Shows a new project view.
        /// </summary>
        private void ShowNewProjectView()
        {
            this.viewPresenter.ShowNewProjectView(this.viewModelFactory.CreateNewProjectViewModel());
        }

        /// <summary>
        ///   Shows the open project dialog.
        /// </summary>
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
        }

        /// <summary>
        ///   Toggles the tool window visibility that matches the specified <paramref name="contentID"/>.
        /// </summary>
        /// <param name="contentID">
        ///   The content identifier of the tool window to toggle.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="contentID"/> parametr cannot be null, empty or consist of whitespace characters.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///   Failed to locate a tool window that matches the specified <paramref name="contentID"/>.
        /// </exception>
        private void ToggleToolWindow(string? contentID)
        {
            if (string.IsNullOrWhiteSpace(contentID))
            {
                throw new ArgumentNullException(nameof(contentID));
            }

            IToolViewModel? tool = this.DockViewModel.Tools.FirstOrDefault(x => x.ContentID == contentID);

            if (tool == null)
            {
                throw new ArgumentException($"Failed to locate tool window with content ID: {contentID}.", nameof(contentID));
            }

            tool.IsVisible = !tool.IsVisible;
        }
    }
}