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

public sealed class MainViewModel : ObservableObject, IMainViewModel
{
    private ICommand? exitCommand;

    private string? title;

    public MainViewModel()
    {
        this.Title = $"Final Engine - {Assembly.GetExecutingAssembly().GetVersionString()}";
    }

    public ICommand ExitCommand
    {
        get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Exit); }
    }

    public string Title
    {
        get { return this.title ?? string.Empty; }
        private set { this.SetProperty(ref this.title, value); }
    }

    private void Exit(ICloseable? closeable)
    {
        if (closeable == null)
        {
            throw new ArgumentNullException(nameof(closeable));
        }

        closeable.Close();
    }
}
