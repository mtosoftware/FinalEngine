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
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Interaction;
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

    /// <summary>
    /// The exit command.
    /// </summary>
    private ICommand? exitCommand;

    /// <summary>
    /// The application title.
    /// </summary>
    private string? title;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="sceneViewModelFactory">
    /// The scene view model factory.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/> parameter cannot be null.
    /// </exception>
    public MainViewModel(ILogger<MainViewModel> logger, IFactory<SceneViewModel> sceneViewModelFactory)
    {
        if (sceneViewModelFactory == null)
        {
            throw new ArgumentNullException(nameof(sceneViewModelFactory));
        }

        this.Panes = new List<IPaneViewModel>()
        {
            sceneViewModelFactory.Create(),
        };

        this.Tools = new List<IToolViewModel>();

        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.Title = $"Final Engine - {Assembly.GetExecutingAssembly().GetVersionString()}";
    }

    /// <inheritdoc/>
    public ICommand ExitCommand
    {
        get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Exit); }
    }

    /// <inheritdoc/>
    public IEnumerable<IPaneViewModel> Panes { get; }

    /// <inheritdoc/>
    public string Title
    {
        get { return this.title ?? string.Empty; }
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
}
