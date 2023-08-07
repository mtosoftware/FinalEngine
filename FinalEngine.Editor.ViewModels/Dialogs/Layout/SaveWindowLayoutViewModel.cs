// <copyright file="SaveWindowLayoutViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.ViewModels.Interactions;
using FinalEngine.Editor.ViewModels.Messages.Layout;
using FinalEngine.Editor.ViewModels.Validation;
using Microsoft.Extensions.Logging;

public sealed class SaveWindowLayoutViewModel : ObservableValidator, ISaveWindowLayoutViewModel
{
    private readonly IApplicationDataContext context;

    private readonly ILogger<SaveWindowLayoutViewModel> logger;

    private readonly IMessenger messenger;

    private readonly IUserActionRequester userActionRequester;

    private string? layoutName;

    private ICommand? saveCommand;

    private string? title;

    public SaveWindowLayoutViewModel(
        ILogger<SaveWindowLayoutViewModel> logger,
        IMessenger messenger,
        IApplicationDataContext context,
        IUserActionRequester userActionRequester)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));

        this.Title = "Save Window Layout";
    }

    [FileName(ErrorMessage = "You must provide a valid layout file name.")]
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

        if (this.context.ContainsLayout(this.LayoutName))
        {
            if (!this.userActionRequester.RequestYesNo(
                this.Title,
                $"A window layout named {this.LayoutName} already exists. Do you want to replace it?"))
            {
                return;
            }
        }

        string filePath = this.context.GetLayoutPath(this.LayoutName);
        this.messenger.Send(new SaveWindowLayoutMessage(filePath));

        closeable.Close();
    }
}
