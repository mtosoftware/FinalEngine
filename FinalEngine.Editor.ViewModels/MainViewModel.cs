// <copyright file="MainViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.ViewModels.Interactions;
using Microsoft.Extensions.Logging;

public sealed class MainViewModel : ObservableObject, IMainViewModel
{
    private readonly ILogger<MainViewModel> logger;

    private ICommand? exitCommand;

    private string? title;

    public MainViewModel(
        ILogger<MainViewModel> logger,
        IApplicationContext context)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        this.Title = context.Title;
    }

    public ICommand ExitCommand
    {
        get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Close); }
    }

    public string Title
    {
        get { return this.title ?? string.Empty; }
        private set { this.SetProperty(ref this.title, value); }
    }

    private void Close(ICloseable? closeable)
    {
        if (closeable == null)
        {
            throw new ArgumentNullException(nameof(closeable));
        }

        this.logger.LogDebug($"Closing {nameof(MainViewModel)}...");

        closeable.Close();
    }
}
