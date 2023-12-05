// <copyright file="ManageWindowLayoutsViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Services.Actions;
using FinalEngine.Editor.ViewModels.Services.Layout;
using Microsoft.Extensions.Logging;

public sealed class ManageWindowLayoutsViewModel : ObservableObject, IManageWindowLayoutsViewModel
{
    private readonly ILayoutManager layoutManager;

    private readonly ILogger<ManageWindowLayoutsViewModel> logger;

    private readonly IUserActionRequester userActionRequester;

    private IRelayCommand? applyCommand;

    private IRelayCommand? deleteCommand;

    private IEnumerable<string>? layoutNames;

    private string? selectedItem;

    public ManageWindowLayoutsViewModel(
        ILogger<ManageWindowLayoutsViewModel> logger,
        IUserActionRequester userActionRequester,
        ILayoutManager layoutManager)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));
        this.layoutManager = layoutManager ?? throw new ArgumentNullException(nameof(layoutManager));

        this.LayoutNames = this.layoutManager.LoadLayoutNames();
    }

    public IRelayCommand ApplyCommand
    {
        get { return this.applyCommand ??= new RelayCommand(this.Apply, this.CanModify); }
    }

    public IRelayCommand DeleteCommand
    {
        get { return this.deleteCommand ??= new RelayCommand(this.Delete, this.CanModify); }
    }

    public IEnumerable<string> LayoutNames
    {
        get { return this.layoutNames ?? Array.Empty<string>(); }
        private set { this.SetProperty(ref this.layoutNames, value); }
    }

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

    public string Title
    {
        get { return "Manage Window Layouts"; }
    }

    private void Apply()
    {
        this.logger.LogInformation($"Applying selected window layout...");
        this.layoutManager.LoadLayout(this.SelectedItem!);
    }

    private bool CanModify()
    {
        return !string.IsNullOrWhiteSpace(this.SelectedItem);
    }

    private void Delete()
    {
        this.logger.LogInformation("Deleting selected window layout...");

        if (!this.userActionRequester.RequestYesNo(
            this.Title,
            $"Are you sure you want to delete the '{this.SelectedItem}' window layout?"))
        {
            this.logger.LogInformation("User cancelled delete operation.");
            return;
        }

        this.layoutManager.DeleteLayout(this.SelectedItem!);
        this.LayoutNames = this.layoutManager.LoadLayoutNames();
    }
}
