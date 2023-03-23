// <copyright file="MainViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Interaction;
using FinalEngine.Utilities.Extensions;

/// <summary>
///   Provides a standard implementation of an <see cref="IMainViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject"/>
/// <seealso cref="IMainViewModel"/>
public sealed class MainViewModel : ObservableObject, IMainViewModel
{
    /// <summary>
    /// The exit command.
    /// </summary>
    private ICommand? exitCommand;

    /// <summary>
    /// The title.
    /// </summary>
    private string? title;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    public MainViewModel()
    {
        this.Title = $"Final Engine - {Assembly.GetExecutingAssembly().GetVersionString()}";
    }

    /// <inheritdoc/>
    public ICommand ExitCommand
    {
        get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Exit); }
    }

    /// <inheritdoc/>
    public string Title
    {
        get { return this.title ?? string.Empty; }
        private set { this.SetProperty(ref this.title, value); }
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
    private void Exit(ICloseable? closeable)
    {
        if (closeable == null)
        {
            throw new ArgumentNullException(nameof(closeable));
        }

        closeable.Close();
    }
}
