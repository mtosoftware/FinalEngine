// <copyright file="IMainViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System.Windows.Input;
using FinalEngine.Editor.ViewModels.Docking;

/// <summary>
/// Defines an interface that represents a model of the main view.
/// </summary>
public interface IMainViewModel
{
    IDockViewModel DockViewModel { get; }

    /// <summary>
    /// Gets the exit command.
    /// </summary>
    /// <value>
    /// The exit command.
    /// </value>
    ICommand ExitCommand { get; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    string Title { get; }

    ICommand ToggleToolWindowCommand { get; }
}
