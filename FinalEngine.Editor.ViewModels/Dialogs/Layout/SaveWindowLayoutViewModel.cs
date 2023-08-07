// <copyright file="SaveWindowLayoutViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Validation;

public sealed class SaveWindowLayoutViewModel : ObservableValidator, ISaveWindowLayoutViewModel
{
    private string? layoutName;

    private IRelayCommand? saveCommand;

    public SaveWindowLayoutViewModel()
    {
        this.LayoutName = "Layout";
    }

    [FileName(ErrorMessage = "You must provide a valid layout name.")]
    public string LayoutName
    {
        get
        {
            return this.layoutName ?? string.Empty;
        }

        set
        {
            this.SetProperty(ref this.layoutName, value, true);
            this.saveCommand?.NotifyCanExecuteChanged();
        }
    }

    public ICommand SaveCommand
    {
        get { return this.saveCommand ??= new RelayCommand(this.Save, this.CanSave); }
    }

    private bool CanSave()
    {
        return !this.HasErrors;
    }

    private void Save()
    {
        throw new System.NotImplementedException();
    }
}
