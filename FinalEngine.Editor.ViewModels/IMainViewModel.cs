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
    /// <summary>
    /// Gets the dock view model.
    /// </summary>
    /// <value>
    /// The dock view model.
    /// </value>
    IDockViewModel DockViewModel { get; }

    /// <summary>
    /// Gets the exit command.
    /// </summary>
    /// <value>
    /// The exit command.
    /// </value>
    /// <remarks>
    /// The <see cref="ExitCommand"/> is used to exit the main application.
    /// </remarks>
    ICommand ExitCommand { get; }

    /// <summary>
    /// Gets the manage window layouts command.
    /// </summary>
    /// <value>
    /// The manage window layouts command.
    /// </value>
    /// <remarks>
    /// The <see cref="ManageWindowLayoutsCommand"/> is used open the manage window layouts view.
    /// </remarks>
    ICommand ManageWindowLayoutsCommand { get; }

    /// <summary>
    /// Gets the reset window layout command.
    /// </summary>
    /// <value>
    /// The reset window layout command.
    /// </value>
    /// <remarks>
    /// The <see cref="ResetWindowLayoutCommand"/> is used to reset the current layout to the default window layout.
    /// </remarks>
    ICommand ResetWindowLayoutCommand { get; }

    /// <summary>
    /// Gets the save window layout command.
    /// </summary>
    /// <value>
    /// The save window layout command.
    /// </value>
    /// <remarks>
    /// The <see cref="SaveWindowLayoutCommand"/> is used to open the save window layout view.
    /// </remarks>
    ICommand SaveWindowLayoutCommand { get; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    string Title { get; }

    /// <summary>
    /// Gets the toggle tool window command.
    /// </summary>
    /// <value>
    /// The toggle tool window command.
    /// </value>
    /// <remarks>
    /// The <see cref="ToggleToolWindowCommand"/> is used to toggle the visiblity of a tool view.
    /// </remarks>
    ICommand ToggleToolWindowCommand { get; }
}
