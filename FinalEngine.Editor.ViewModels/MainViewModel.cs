// <copyright file="MainViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Interaction;
using FinalEngine.Utilities.Extensions;
using Microsoft.Extensions.Logging;

/// <summary>
///   Provides a standard implementation of an <see cref="IMainViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject"/>
/// <seealso cref="IMainViewModel"/>
public partial class MainViewModel : ObservableObject, IMainViewModel
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<MainViewModel> logger;

    /// <summary>
    /// The main application title.
    /// </summary>
    [ObservableProperty]
    private string? title;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/> parameter cannot be null.
    /// </exception>
    public MainViewModel(ILogger<MainViewModel> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.Title = $"Final Engine - {Assembly.GetExecutingAssembly().GetVersionString()}";
    }

    /// <summary>
    /// Attempts to the exit the main application.
    /// </summary>
    /// <param name="closeable">
    /// The closeable used to exit the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="closeable"/> parameter cannot be null.
    /// </exception>
    [RelayCommand]
    private void Exit(ICloseable? closeable)
    {
        if (closeable == null)
        {
            throw new ArgumentNullException(nameof(closeable));
        }

        this.logger.LogInformation($"Closing the main view...");

        closeable.Close();
    }
}
