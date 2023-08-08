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
using FinalEngine.Editor.Common.Services.Application;

public sealed class ManageWindowLayoutsViewModel : ObservableObject, IManageWindowLayoutsViewModel
{
    private readonly IApplicationDataContext applicationData;

    private readonly IFileSystem fileSystem;

    private IRelayCommand? deleteCommand;

    private IEnumerable<string> layoutNames;

    private string? selectedItem;

    private string? title;

    public ManageWindowLayoutsViewModel(
        IFileSystem fileSystem,
        IApplicationDataContext applicationData)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.applicationData = applicationData ?? throw new ArgumentNullException(nameof(applicationData));

        this.Title = "Manage Window Layouts";
        this.LayoutNames = this.applicationData.LayoutNames;
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
        }
    }

    public string Title
    {
        get { return this.title ?? string.Empty; }
        private set { this.SetProperty(ref this.title, value); }
    }

    private bool CanDelete()
    {
        return this.SelectedItem != null;
    }

    private void Delete()
    {
        if (!this.applicationData.ContainsLayout(this.SelectedItem!))
        {
            throw new Exception($"The specified {nameof(this.SelectedItem)} was not matched to the layout name: '{this.SelectedItem}'.");
        }

        this.fileSystem.File.Delete(this.applicationData.GetLayoutPath(this.SelectedItem!));
        this.LayoutNames = this.applicationData.LayoutNames;
    }
}
