// <copyright file="SaveWindowLayoutViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Services.Actions;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using FinalEngine.Editor.ViewModels.Services.Layout;
using FinalEngine.Editor.ViewModels.Validation;
using Microsoft.Extensions.Logging;

public sealed class SaveWindowLayoutViewModel : ObservableValidator, ISaveWindowLayoutViewModel
{
    private readonly ILayoutManager layoutManager;

    private readonly ILogger<SaveWindowLayoutViewModel> logger;

    private readonly IUserActionRequester userActionRequester;

    private string? layoutName;

    private IRelayCommand? saveCommand;

    public SaveWindowLayoutViewModel(
        ILogger<SaveWindowLayoutViewModel> logger,
        ILayoutManager layoutManager,
        IUserActionRequester userActionRequester)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));
        this.layoutManager = layoutManager ?? throw new ArgumentNullException(nameof(layoutManager));

        this.LayoutName = "Layout Name";
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
            this.SaveCommand.NotifyCanExecuteChanged();
        }
    }

    public IRelayCommand SaveCommand
    {
        get
        {
            return this.saveCommand ??= new RelayCommand<ICloseable>(this.Save, (o) =>
            {
                return this.CanSave();
            });
        }
    }

    public string Title
    {
        get { return "Save Window Layout"; }
    }

    private bool CanSave()
    {
        return !this.HasErrors;
    }

    private void Save(ICloseable? closeable)
    {
        ArgumentNullException.ThrowIfNull(closeable, nameof(closeable));

        this.logger.LogInformation("Saving current window layout...");

        string requestMessage = $"A window layout named '{this.LayoutName}' already exists. Do you want to replace it?";

        if (this.layoutManager.ContainsLayout(this.LayoutName) && !this.userActionRequester.RequestYesNo(this.Title, requestMessage))
        {
            this.logger.LogInformation("User or manager cancelled the save operation.");
            return;
        }

        this.layoutManager.SaveLayout(this.LayoutName);

        this.logger.LogInformation($"Closing the {this.Title} view...");

        closeable.Close();
    }
}
