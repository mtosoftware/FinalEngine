// <copyright file="IMainViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.ViewModels.Interaction;

/// <summary>
/// Defines an interface that represents the main view.
/// </summary>
public interface IMainViewModel
{
    /// <summary>
    /// Gets the exit command.
    /// </summary>
    /// <value>
    /// The exit command.
    /// </value>
    IRelayCommand<ICloseable?> ExitCommand { get; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    string Title { get; }
}
