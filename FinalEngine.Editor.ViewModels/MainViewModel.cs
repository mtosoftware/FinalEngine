// <copyright file="MainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Dialogs.Entities;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Services;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using FinalEngine.Editor.ViewModels.Services.Layout;
using FinalEngine.Runtime;
using Microsoft.Extensions.Logging;

public sealed class MainViewModel : ObservableObject, IMainViewModel
{
    private readonly ILayoutManager layoutManager;

    private readonly ILogger<MainViewModel> logger;

    private readonly IViewPresenter viewPresenter;

    private ICommand? createEntityCommand;

    private ICommand? exitCommand;

    private ICommand? manageWindowLayoutsCommand;

    private ICommand? resetWindowLayoutCommand;

    private ICommand? saveWindowLayoutCommand;

    private ICommand? toggleToolWindowCommand;

    public MainViewModel(
        ILogger<MainViewModel> logger,
        IViewPresenter viewPresenter,
        IApplicationContext applicationContext,
        ILayoutManager layoutManager,
        IFactory<IDockViewModel> dockViewModelFactory,
        IRuntimeContext runtimeContext)
    {
        ArgumentNullException.ThrowIfNull(applicationContext, nameof(applicationContext));
        ArgumentNullException.ThrowIfNull(dockViewModelFactory, nameof(dockViewModelFactory));
        ArgumentNullException.ThrowIfNull(runtimeContext, nameof(runtimeContext));

        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.viewPresenter = viewPresenter ?? throw new ArgumentNullException(nameof(viewPresenter));
        this.layoutManager = layoutManager ?? throw new ArgumentNullException(nameof(layoutManager));

        this.DockViewModel = dockViewModelFactory.Create();
        this.Title = applicationContext.Title;

        runtimeContext.Initialize();
    }

    public ICommand CreateEntityCommand
    {
        get { return this.createEntityCommand ??= new RelayCommand(this.CreateEntity); }
    }

    public IDockViewModel DockViewModel { get; }

    public ICommand ExitCommand
    {
        get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Close); }
    }

    public ICommand ManageWindowLayoutsCommand
    {
        get { return this.manageWindowLayoutsCommand ??= new RelayCommand(this.ShowManageWindowLayoutsView); }
    }

    public ICommand ResetWindowLayoutCommand
    {
        get { return this.resetWindowLayoutCommand ??= new RelayCommand(this.ResetWindowLayout); }
    }

    public ICommand SaveWindowLayoutCommand
    {
        get { return this.saveWindowLayoutCommand ??= new RelayCommand(this.ShowSaveWindowLayoutView); }
    }

    public string Title { get; }

    public ICommand ToggleToolWindowCommand
    {
        get { return this.toggleToolWindowCommand ??= new RelayCommand<string>(this.ToggleToolWindow); }
    }

    private void Close(ICloseable? closeable)
    {
        ArgumentNullException.ThrowIfNull(closeable, nameof(closeable));

        this.logger.LogInformation($"Closing {nameof(MainViewModel)}...");

        closeable.Close();
    }

    private void CreateEntity()
    {
        this.viewPresenter.ShowView<ICreateEntityViewModel>();
    }

    private void ResetWindowLayout()
    {
        this.layoutManager.ResetLayout();
    }

    private void ShowManageWindowLayoutsView()
    {
        this.viewPresenter.ShowView<IManageWindowLayoutsViewModel>();
    }

    private void ShowSaveWindowLayoutView()
    {
        this.viewPresenter.ShowView<ISaveWindowLayoutViewModel>();
    }

    private void ToggleToolWindow(string? contentID)
    {
        this.logger.LogInformation($"Toggling tool view with ID: '{contentID}'...");

        if (string.IsNullOrWhiteSpace(contentID))
        {
            throw new ArgumentException($"'{nameof(contentID)}' cannot be null or whitespace.", nameof(contentID));
        }

        this.layoutManager.ToggleToolWindow(contentID);
    }
}
