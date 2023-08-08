// <copyright file="ManageWindowLayoutsViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Windows.Input;
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

        this.Title = "Manage Window Layouts";
        this.LayoutNames = this.layoutManager.LoadLayoutNames();
    }

    public ICommand ApplyCommand
    {
        get { return this.applyCommand ??= new RelayCommand(this.Apply, this.CanApply); }
    }

    public ICommand DeleteCommand
    {
        get { return this.deleteCommand ??= new RelayCommand(this.Delete, this.CanDelete); }
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

            this.deleteCommand?.NotifyCanExecuteChanged();
            this.applyCommand?.NotifyCanExecuteChanged();
        }
    }

    public string Title
    {
        get { return this.title ?? string.Empty; }
        private set { this.SetProperty(ref this.title, value); }
    }

    private void Apply()
    {
        this.layoutManager.LoadLayout(this.SelectedItem!);
    }

    private bool CanApply()
    {
        return this.SelectedItem != null;
    }

    private bool CanDelete()
    {
        return this.SelectedItem != null;
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
