// <copyright file="MainViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Interactions;
using FinalEngine.Editor.ViewModels.Services.Factories.Layout;
using FinalEngine.Editor.ViewModels.Services.Layout;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="IMainViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IMainViewModel" />
public sealed class MainViewModel : ObservableObject, IMainViewModel
{
    /// <summary>
    /// The layout manager factory, used to create an <see cref="ILayoutManager"/> to handle reseting the current window layout.
    /// </summary>
    private readonly ILayoutManagerFactory layoutManagerFactory;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<MainViewModel> logger;

    /// <summary>
    /// The view presenter.
    /// </summary>
    private readonly IViewPresenter viewPresenter;

    /// <summary>
    /// The exit command.
    /// </summary>
    private ICommand? exitCommand;

    /// <summary>
    /// The manage window layouts command.
    /// </summary>
    private ICommand? manageWindowLayoutsCommand;

    /// <summary>
    /// The reset window layout command.
    /// </summary>
    private ICommand? resetWindowLayoutCommand;

    /// <summary>
    /// The save window layout command.
    /// </summary>
    private ICommand? saveWindowLayoutCommand;

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
    /// <param name="viewPresenter">
    /// The view presenter, used to display views.
    /// </param>
    /// <param name="applicationContext">
    /// The application context, used to get the title of the view.
    /// </param>
    /// <param name="layoutManagerFactory">
    /// The layout manager factory, used to create an <see cref="ILayoutManager"/> to handle reseting the current window layout.
    /// </param>
    /// <param name="dockViewModelFactory">
    /// The dock view model factory used to create an <see cref="IDockViewModel"/> to handle docking of tool and pane views.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/>, <paramref name="viewPresenter"/>, <paramref name="applicationContext"/>, <paramref name="layoutManagerFactory"/> or <paramref name="dockViewModelFactory"/> parameter cannot be null.
    /// </exception>
    public MainViewModel(
        ILogger<MainViewModel> logger,
        IViewPresenter viewPresenter,
        IApplicationContext applicationContext,
        ILayoutManagerFactory layoutManagerFactory,
        IFactory<IDockViewModel> dockViewModelFactory)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.viewPresenter = viewPresenter ?? throw new ArgumentNullException(nameof(viewPresenter));
        this.layoutManagerFactory = layoutManagerFactory ?? throw new ArgumentNullException(nameof(layoutManagerFactory));

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

    /// <inheritdoc/>
    public IDockViewModel DockViewModel { get; }

    /// <inheritdoc/>
    public ICommand ExitCommand
    {
        get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Close); }
    }

    /// <inheritdoc/>
    public ICommand ManageWindowLayoutsCommand
    {
        get { return this.manageWindowLayoutsCommand ??= new RelayCommand(this.ShowManageWindowLayoutsView); }
    }

    /// <inheritdoc/>
    public ICommand ResetWindowLayoutCommand
    {
        get { return this.resetWindowLayoutCommand ??= new RelayCommand(this.ResetWindowLayout); }
    }

    /// <inheritdoc/>
    public ICommand SaveWindowLayoutCommand
    {
        get { return this.saveWindowLayoutCommand ??= new RelayCommand(this.ShowSaveWindowLayoutView); }
    }

    /// <inheritdoc/>
    public string Title { get; }

    /// <inheritdoc/>
    public ICommand ToggleToolWindowCommand
    {
        get { return this.toggleToolWindowCommand ??= new RelayCommand<string>(this.ToggleToolWindow); }
    }

    /// <summary>
    /// Closes the main view using the specified <paramref name="closeable"/> interaction.
    /// </summary>
    /// <param name="closeable">
    /// The closeable interaction used to close the main view.
    /// </param>
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

    /// <summary>
    /// Resets the window layout to the default window layout.
    /// </summary>
    private void ResetWindowLayout()
    {
        this.layoutManagerFactory.CreateManager().ResetLayout();
    }

    /// <summary>
    /// Shows the manage window layouts view.
    /// </summary>
    private void ShowManageWindowLayoutsView()
    {
        this.viewPresenter.ShowView<IManageWindowLayoutsViewModel>();
    }

    /// <summary>
    /// Shows the save window layout view.
    /// </summary>
    private void ShowSaveWindowLayoutView()
    {
        this.viewPresenter.ShowView<ISaveWindowLayoutViewModel>();
    }

    /// <summary>
    /// Toggles the visibility of a tool view that matches the specified <paramref name="contentID"/>.
    /// </summary>
    /// <param name="contentID">
    /// The content identifier of tool view to toggle.
    /// </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="contentID"/> cannot be null or whitespace.
    /// </exception>
    private void ToggleToolWindow(string? contentID)
    {
        this.logger.LogDebug($"Toggling tool view with ID: '{contentID}'...");

        if (string.IsNullOrWhiteSpace(contentID))
        {
            throw new ArgumentException($"'{nameof(contentID)}' cannot be null or whitespace.", nameof(contentID));
        }

        this.layoutManagerFactory.CreateManager().ToggleToolWindow(contentID);
    }
}
