// <copyright file="MainViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Factories;
using FinalEngine.Editor.ViewModels.Interaction;
using FinalEngine.Editor.ViewModels.Projects;
using FinalEngine.Utilities.Extensions;
using Microsoft.Extensions.Logging;

/// <summary>
///   Provides a standard implementation of an <see cref="IMainViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject"/>
/// <seealso cref="IMainViewModel"/>
public partial class MainViewModel : ObservableObject, IMainViewModel
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<MainViewModel> logger;

    private readonly IViewModelFactory viewModelFactory;

    /// <summary>
    /// The exit command.
    /// </summary>
    private ICommand? exitCommand;

    private ICommand? newProjectCommand;

    /// <summary>
    /// The application title.
    /// </summary>
    private string? title;

    private IViewPresenter viewPresenter;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/> parameter cannot be null.
    /// </exception>
    public MainViewModel(
        ILogger<MainViewModel> logger,
        IViewPresenter viewPresenter,
        IViewModelFactory viewModelFactory)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.viewPresenter = viewPresenter ?? throw new ArgumentNullException(nameof(viewPresenter));
        this.viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

        this.Panes = new List<IPaneViewModel>()
        {
            this.viewModelFactory.CreateViewModel<ISceneViewModel>(),
            this.viewModelFactory.CreateViewModel<ICodeViewModel>(),
        };

        this.Tools = new List<IToolViewModel>();
        this.Title = $"Final Engine - {Assembly.GetExecutingAssembly().GetVersionString()}";
    }

    /// <inheritdoc/>
    public ICommand ExitCommand
    {
        get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Exit); }
    }

    public ICommand NewProjectCommand
    {
        get { return this.newProjectCommand ??= new RelayCommand(this.ShowNewProjectView); }
    }

    /// <inheritdoc/>
    public IEnumerable<IPaneViewModel> Panes { get; }

    /// <inheritdoc/>
    public string Title
    {
        get { return this.title!; }
        private set { this.SetProperty(ref this.title, value); }
    }

    /// <inheritdoc/>
    public IEnumerable<IToolViewModel> Tools { get; }

    /// <summary>
    /// Attempts to the exit the main application.
    /// </summary>
    /// <param name="closeable">
    /// The closeable used to exit the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="closeable"/> parameter cannot be null.
    /// </exception>
    private void Exit(ICloseable? closeable)
    {
        if (closeable == null)
        {
            throw new ArgumentNullException(nameof(closeable));
        }

        this.logger.LogInformation($"Closing the main view...");

        closeable.Close();
    }

    private void ShowNewProjectView()
    {
        var viewModel = this.viewModelFactory.CreateViewModel<INewProjectViewModel>();
        this.viewPresenter.ShowNewProjectView(viewModel);
    }
}
