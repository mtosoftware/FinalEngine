// <copyright file="MainViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Interactions;
using FinalEngine.Editor.ViewModels.Messages.Docking;
using FinalEngine.Editor.ViewModels.Messages.Layout;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="IMainViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IMainViewModel" />
public sealed class MainViewModel : ObservableObject, IMainViewModel
{
    private readonly IApplicationDataContext applicationDataContext;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<MainViewModel> logger;

    /// <summary>
    /// The messenger.
    /// </summary>
    private readonly IMessenger messenger;

    /// <summary>
    /// The view presenter.
    /// </summary>
    private readonly IViewPresenter viewPresenter;

    /// <summary>
    /// The exit command.
    /// </summary>
    private ICommand? exitCommand;

    private ICommand? resetWindowLayoutCommand;

    private ICommand? saveWindowLayoutCommand;

    /// <summary>
    /// The title of the application.
    /// </summary>
    private string? title;

    /// <summary>
    /// The toggle tool window command.
    /// </summary>
    private ICommand? toggleToolWindowCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="applicationContext">
    /// The application context.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/> or <paramref name="applicationContext"/> parameter cannot be null.
    /// </exception>
    public MainViewModel(
        ILogger<MainViewModel> logger,
        IMessenger messenger,
        IViewPresenter viewPresenter,
        IApplicationContext applicationContext,
        IApplicationDataContext applicationDataContext,
        IFactory<IDockViewModel> dockViewModelFactory)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.viewPresenter = viewPresenter ?? throw new ArgumentNullException(nameof(viewPresenter));
        this.applicationDataContext = applicationDataContext ?? throw new ArgumentNullException(nameof(applicationDataContext));

        if (applicationContext == null)
        {
            throw new ArgumentNullException(nameof(applicationContext));
        }

        if (dockViewModelFactory == null)
        {
            throw new ArgumentNullException(nameof(dockViewModelFactory));
        }

        this.DockViewModel = dockViewModelFactory.Create();

        this.Title = applicationContext.Title;
    }

    public IDockViewModel DockViewModel { get; }

    /// <summary>
    /// Gets the exit command.
    /// </summary>
    /// <value>
    /// The exit command.
    /// </value>
    public ICommand ExitCommand
    {
        get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Close); }
    }

    public ICommand ResetWindowLayoutCommand
    {
        get { return this.resetWindowLayoutCommand ??= new RelayCommand(this.ResetWindowLayout); }
    }

    public ICommand SaveWindowLayoutCommand
    {
        get { return this.saveWindowLayoutCommand = new RelayCommand(this.ShowSaveWindowLayoutView); }
    }

    /// <summary>
    /// Gets the title of the application.
    /// </summary>
    /// <value>
    /// The title of the application.
    /// </value>
    public string Title
    {
        get { return this.title ?? string.Empty; }
        private set { this.SetProperty(ref this.title, value); }
    }

    public ICommand ToggleToolWindowCommand
    {
        get { return this.toggleToolWindowCommand = new RelayCommand<string>(this.ToggleToolWindow); }
    }

    /// <summary>
    /// Closes the main view using the specified <paramref name="closeable"/> interaction.
    /// </summary>
    /// <param name="closeable">
    /// The closeable interaction used to close the main view.
    /// .</param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="closeable"/> parameter cannot be null.
    /// </exception>
    private void Close(ICloseable? closeable)
    {
        if (closeable == null)
        {
            throw new ArgumentNullException(nameof(closeable));
        }

        this.logger.LogDebug($"Closing {nameof(MainViewModel)}...");

        closeable.Close();
    }

    private void ResetWindowLayout()
    {
        this.messenger.Send<ResetWindowLayoutMessage>();
    }

    private void ShowSaveWindowLayoutView()
    {
        this.viewPresenter.ShowView<ISaveWindowLayoutViewModel>();
    }

    private void ToggleToolWindow(string? contentID)
    {
        if (string.IsNullOrWhiteSpace(contentID))
        {
            throw new ArgumentNullException(nameof(contentID));
        }

        this.messenger.Send(new ToggleToolWindowMessage(contentID));
    }
}
