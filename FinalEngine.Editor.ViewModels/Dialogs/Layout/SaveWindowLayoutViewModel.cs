// <copyright file="SaveWindowLayoutViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Interactions;
using FinalEngine.Editor.ViewModels.Services.Actions;
using FinalEngine.Editor.ViewModels.Services.Factories.Layout;
using FinalEngine.Editor.ViewModels.Services.Layout;
using FinalEngine.Editor.ViewModels.Validation;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="ISaveWindowLayoutViewModel"/>.
/// </summary>
/// <seealso cref="ObservableValidator" />
/// <seealso cref="ISaveWindowLayoutViewModel" />
public sealed class SaveWindowLayoutViewModel : ObservableValidator, ISaveWindowLayoutViewModel
{
    /// <summary>
    /// The layout manager, used to save the current window layout.
    /// </summary>
    private readonly ILayoutManager layoutManager;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<SaveWindowLayoutViewModel> logger;

    /// <summary>
    /// The user action requester, used to query the user and determine whether they wish to overwrite a layout that already exists.
    /// </summary>
    private readonly IUserActionRequester userActionRequester;

    /// <summary>
    /// The layout name.
    /// </summary>
    private string? layoutName;

    /// <summary>
    /// The save command.
    /// </summary>
    private IRelayCommand? saveCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaveWindowLayoutViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="layoutManagerFactory">
    /// The layout manager factory, used to create an <see cref="ILayoutManager"/> to handle saving the current window layout.
    /// </param>
    /// <param name="userActionRequester">
    /// The user action requester, used to query the user and determine whether they wish to overwrite a layout that already exists.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="layoutManagerFactory"/> or <paramref name="userActionRequester"/> parameter cannot be null.
    /// </exception>
    public SaveWindowLayoutViewModel(
        ILogger<SaveWindowLayoutViewModel> logger,
        ILayoutManagerFactory layoutManagerFactory,
        IUserActionRequester userActionRequester)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));

        if (layoutManagerFactory == null)
        {
            throw new ArgumentNullException(nameof(layoutManagerFactory));
        }

        this.layoutManager = layoutManagerFactory.CreateManager();
        this.LayoutName = "Layout Name";
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public string Title
    {
        get { return "Save Window Layout"; }
    }

    /// <summary>
    /// Determines whether the <see cref="LayoutName"/> has any naming errors and if the layout can be saved.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the <see cref="LayoutName"/> has any errors and if the layout can be saved; otherwise, <c>false</c>.
    /// </returns>
    private bool CanSave()
    {
        return !this.HasErrors;
    }

    /// <summary>
    /// Saves the current layout using the <see cref="LayoutName"/> to applications roaming data.
    /// </summary>
    /// <param name="closeable">
    /// The closeable, used to close the view once the operation has finished.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="closeable"/> parameter cannot be null.
    /// </exception>
    private void Save(ICloseable? closeable)
    {
        this.logger.LogInformation("Saving current window layout...");

        if (closeable == null)
        {
            throw new ArgumentNullException(nameof(closeable));
        }

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
