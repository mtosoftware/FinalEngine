// <copyright file="IMainViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System.Collections.Generic;
using System.Windows.Input;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Tools;

/// <summary>
/// Defines an interface that represents the main view.
/// </summary>
public interface IMainViewModel : IViewModel
{
    /// <summary>
    /// Gets the exit command.
    /// </summary>
    /// <value>
    /// The exit command.
    /// </value>
    ICommand ExitCommand { get; }

    ICommand NewProjectCommand { get; }

    /// <summary>
    ///   Gets the documents attached to this <see cref="IMainViewModel"/>.
    /// </summary>
    /// <value>
    ///   The documents attached to this <see cref="IMainViewModel"/>.
    /// </value>
    IEnumerable<IPaneViewModel> Panes { get; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    string Title { get; }

    /// <summary>
    ///   Gets the tool windows attached to this <see cref="IMainViewModel"/>.
    /// </summary>
    /// <value>
    ///   The tool windows attached to this <see cref="IMainViewModel"/>.
    /// </value>
    IEnumerable<IToolViewModel> Tools { get; }
}
