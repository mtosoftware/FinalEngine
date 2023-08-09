// <copyright file="ManageWindowLayoutsViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Factories;
using FinalEngine.Editor.ViewModels.Interactions;

public sealed class ManageWindowLayoutsViewModel : ObservableObject, IManageWindowLayoutsViewModel
{
    private readonly IFileSystem fileSystem;

    private readonly ILayoutManager layoutManager;

    private readonly IUserActionRequester userActionRequester;

    private IRelayCommand? applyCommand;

    private IRelayCommand? deleteCommand;

    private IEnumerable<string> layoutNames;

    private string? selectedItem;

    private string? title;

    public ManageWindowLayoutsViewModel(
        IFileSystem fileSystem,
        IUserActionRequester userActionRequester,
        ILayoutManagerFactory layoutManagerFactory)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));

        if (layoutManagerFactory == null)
        {
            throw new ArgumentNullException(nameof(layoutManagerFactory));
        }

        this.layoutManager = layoutManagerFactory.CreateManager();
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
        get { return this.layoutNames; }
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
        this.layoutManager.LoadLayout(this.SelectedItem!);
    }

    private bool CanModify()
    {
        return !string.IsNullOrWhiteSpace(this.SelectedItem);
    }

    private void Delete()
    {
        if (!this.userActionRequester.RequestYesNo(
            this.Title,
            $"Are you sure you want to do delete the '{this.SelectedItem}' window layout?"))
        {
            return;
        }

        this.layoutManager.DeleteLayout(this.SelectedItem!);
        this.LayoutNames = this.layoutManager.LoadLayoutNames();
    }
}
