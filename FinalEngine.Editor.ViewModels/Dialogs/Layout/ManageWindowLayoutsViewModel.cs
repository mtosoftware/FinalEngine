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
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.ViewModels.Interactions;
using FinalEngine.Editor.ViewModels.Messages.Layout;

public sealed class ManageWindowLayoutsViewModel : ObservableObject, IManageWindowLayoutsViewModel
{
    private readonly IApplicationDataContext applicationData;

    private readonly IFileSystem fileSystem;

    private readonly IUserActionRequester userActionRequester;

    private IRelayCommand? applyCommand;

    private IRelayCommand? deleteCommand;

    private IEnumerable<string> layoutNames;

    private IMessenger messenger;

    private string? selectedItem;

    private string? title;

    public ManageWindowLayoutsViewModel(
        IMessenger messenger,
        IFileSystem fileSystem,
        IApplicationDataContext applicationData,
        IUserActionRequester userActionRequester)
    {
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.applicationData = applicationData ?? throw new ArgumentNullException(nameof(applicationData));
        this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));

        this.Title = "Manage Window Layouts";
        this.LayoutNames = this.applicationData.LoadLayoutNames();
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
        this.messenger.Send(new LoadWindowLayoutMessage(this.applicationData.GetLayoutPath(this.SelectedItem!)));
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
        if (!this.applicationData.ContainsLayout(this.SelectedItem!))
        {
            throw new Exception($"The specified {nameof(this.SelectedItem)} was not matched to the layout name: '{this.SelectedItem}'.");
        }

        if (!this.userActionRequester.RequestYesNo(
            this.Title,
            $"Are you sure you want to do delete the '{this.SelectedItem}' window layout?"))
        {
            return;
        }

        this.fileSystem.File.Delete(this.applicationData.GetLayoutPath(this.SelectedItem!));

        //// TODO: Instead of re-loading layout names once one is removed, just remove it from the list.
        this.LayoutNames = this.applicationData.LoadLayoutNames();
    }
}
