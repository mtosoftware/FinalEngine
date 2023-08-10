// <copyright file="IPaneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

/// <summary>
/// Defines an interface that represents a pane view.
/// </summary>
public interface IPaneViewModel
{
    /// <summary>
    /// Gets or sets the content identifier.
    /// </summary>
    /// <value>
    /// The content identifier.
    /// </value>
    string ContentID { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this view is active.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this view is active; otherwise, <c>false</c>.
    /// </value>
    bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this view is selected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this view is selected; otherwise, <c>false</c>.
    /// </value>
    bool IsSelected { get; set; }

    /// <summary>
    /// Gets or sets the title of the view.
    /// </summary>
    /// <value>
    /// The title of the view.
    /// </value>
    string Title { get; set; }
}
