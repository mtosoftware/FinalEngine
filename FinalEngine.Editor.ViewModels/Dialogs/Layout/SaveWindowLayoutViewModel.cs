// <copyright file="SaveWindowLayoutViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Factories;
using FinalEngine.Editor.ViewModels.Interactions;
using FinalEngine.Editor.ViewModels.Validation;

public sealed class SaveWindowLayoutViewModel : ObservableValidator, ISaveWindowLayoutViewModel
{
    private readonly ILayoutManager layoutManager;

    private readonly IUserActionRequester userActionRequester;

    private string? layoutName;

    private ICommand? saveCommand;

    private string? title;

    public SaveWindowLayoutViewModel(
        ILayoutManagerFactory layoutManagerFactory,
        IUserActionRequester userActionRequester)
    {
        this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));

        if (layoutManagerFactory == null)
        {
            throw new ArgumentNullException(nameof(layoutManagerFactory));
        }

        this.layoutManager = layoutManagerFactory.CreateManager();

        this.Title = "Save Window Layout";
        this.LayoutName = "Layout Name";
    }

    [FileName(ErrorMessage = "You must provide a valid layout name.")]
    public string LayoutName
    {
        get { return this.layoutName ?? string.Empty; }
        set { this.SetProperty(ref this.layoutName, value, true); }
    }

    public ICommand SaveCommand
    {
        get
        {
            return this.saveCommand ??= new RelayCommand<ICloseable>(this.Save, (o) =>
            {
                return !this.HasErrors;
            });
        }
    }

    public string Title
    {
        get { return this.title ?? string.Empty; }
        private set { this.SetProperty(ref this.title, value); }
    }

    private void Save(ICloseable? closeable)
    {
        if (closeable == null)
        {
            throw new ArgumentNullException(nameof(closeable));
        }

        if (this.layoutManager.ContainsLayout(this.LayoutName))
        {
            if (!this.userActionRequester.RequestYesNo(
                this.Title,
                $"A window layout named {this.LayoutName} already exists. Do you want to replace it?"))
            {
                return;
            }
        }

        this.layoutManager.SaveLayout(this.LayoutName);
        closeable.Close();
    }
}
