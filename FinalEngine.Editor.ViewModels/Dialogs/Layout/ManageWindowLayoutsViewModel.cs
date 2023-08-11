// <copyright file="ManageWindowLayoutsViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Services.Actions;
using FinalEngine.Editor.ViewModels.Services.Factories.Layout;
using FinalEngine.Editor.ViewModels.Services.Layout;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="IManageWindowLayoutsViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IManageWindowLayoutsViewModel" />
public sealed class ManageWindowLayoutsViewModel : ObservableObject, IManageWindowLayoutsViewModel
{
    /// <summary>
    /// The layout manager, used to apply and delete the currently selected layout.
    /// </summary>
    private readonly ILayoutManager layoutManager;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<ManageWindowLayoutsViewModel> logger;

    /// <summary>
    /// The user action requester, used to query the user and determine whether they wish to delete the currently selected layout.
    /// </summary>
    private readonly IUserActionRequester userActionRequester;

    /// <summary>
    /// The apply command.
    /// </summary>
    private IRelayCommand? applyCommand;

    /// <summary>
    /// The delete command.
    /// </summary>
    private IRelayCommand? deleteCommand;

    /// <summary>
    /// The layout names.
    /// </summary>
    private IEnumerable<string>? layoutNames;

    /// <summary>
    /// The currently selected item.
    /// </summary>
    private string? selectedItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="ManageWindowLayoutsViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="userActionRequester">
    /// The user action requester, used to query the user and determine whether they wish to delete the currently selected window layout.</param>
    /// <param name="layoutManagerFactory">
    /// The layout manager factory, used to apply and delete window layouts.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="userActionRequester"/> or <paramref name="layoutManagerFactory"/> parameter cannot be null.
    /// </exception>
    public ManageWindowLayoutsViewModel(
        ILogger<ManageWindowLayoutsViewModel> logger,
        IUserActionRequester userActionRequester,
        ILayoutManagerFactory layoutManagerFactory)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));

        if (layoutManagerFactory == null)
        {
            throw new ArgumentNullException(nameof(layoutManagerFactory));
        }

        this.layoutManager = layoutManagerFactory.CreateManager();
        this.LayoutNames = this.layoutManager.LoadLayoutNames();
    }

    /// <inheritdoc/>
    public IRelayCommand ApplyCommand
    {
        get { return this.applyCommand ??= new RelayCommand(this.Apply, this.CanModify); }
    }

    /// <inheritdoc/>
    public IRelayCommand DeleteCommand
    {
        get { return this.deleteCommand ??= new RelayCommand(this.Delete, this.CanModify); }
    }

    /// <inheritdoc/>
    public IEnumerable<string> LayoutNames
    {
        get { return this.layoutNames ?? Array.Empty<string>(); }
        private set { this.SetProperty(ref this.layoutNames, value); }
    }

    /// <inheritdoc/>
    public string? SelectedItem
    {
        get
        {
            return this.selectedItem;
        }

        set
        {
            this.SetProperty(ref this.selectedItem, value);

            this.DeleteCommand.NotifyCanExecuteChanged();
            this.ApplyCommand.NotifyCanExecuteChanged();
        }
    }

    /// <inheritdoc/>
    public string Title
    {
        get { return "Manage Window Layouts"; }
    }

    /// <summary>
    /// Applies the currently selected window layout to the application.
    /// </summary>
    private void Apply()
    {
        this.logger.LogInformation($"Applying selected window layout...");
        this.layoutManager.LoadLayout(this.SelectedItem!);
    }

    /// <summary>
    /// Determines whether the user has selected a window layout.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the user has selected a window layout; otherwise, <c>false</c>.
    /// </returns>
    private bool CanModify()
    {
        return !string.IsNullOrWhiteSpace(this.SelectedItem);
    }

    /// <summary>
    /// Deletes the currently selected window layout from the applications roaming data.
    /// </summary>
    private void Delete()
    {
        this.logger.LogInformation("Deleting selected window layout...");

        if (!this.userActionRequester.RequestYesNo(
            this.Title,
            $"Are you sure you want to do delete the '{this.SelectedItem}' window layout?"))
        {
            this.logger.LogInformation("User cancelled delete operation.");
            return;
        }

        this.layoutManager.DeleteLayout(this.SelectedItem!);
        this.LayoutNames = this.layoutManager.LoadLayoutNames();
    }
}
