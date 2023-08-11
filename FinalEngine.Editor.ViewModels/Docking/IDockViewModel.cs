// <copyright file="IDockViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking;

using System.Collections.Generic;
using System.Windows.Input;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Tools;

/// <summary>
/// Defines an interface that represents a model of a dock view.
/// </summary>
public interface IDockViewModel
{
    /// <summary>
    /// Gets the load command.
    /// </summary>
    /// <value>
    /// The load command.
    /// </value>
    /// <remarks>
    /// The <see cref="LoadCommand"/> loads the last state of the window layout.
    /// </remarks>
    ICommand LoadCommand { get; }

    /// <summary>
    /// Gets the panes.
    /// </summary>
    /// <value>
    /// The panes.
    /// </value>
    /// <remarks>
    /// The <see cref="Panes"/> property refers to all the document views that are part of this view.
    /// </remarks>
    IEnumerable<IPaneViewModel> Panes { get; }

    /// <summary>
    /// Gets the tools.
    /// </summary>
    /// <value>
    /// The tools.
    /// </value>
    /// <remarks>
    /// The <see cref="Tools"/> property refers to all the tool views that are part of the view.
    /// </remarks>
    IEnumerable<IToolViewModel> Tools { get; }

    /// <summary>
    /// Gets the unload command.
    /// </summary>
    /// <value>
    /// The unload command.
    /// </value>
    /// <remarks>
    /// The <see cref="UnloadCommand"/> saves the current window layout to be used when the application is started once again.
    /// </remarks>
    ICommand UnloadCommand { get; }
}
