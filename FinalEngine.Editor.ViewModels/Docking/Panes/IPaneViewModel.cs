// <copyright file="IPaneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

/// <summary>
///   Defines an interface that represents a pane view model.
/// </summary>
/// <remarks>
///   A pane view is a view which is used as part of a dockable layout system. It represents any element that can be docked to a dockable layout.
/// </remarks>
public interface IPaneViewModel
{
    /// <summary>
    ///   Gets or sets the content identifier.
    /// </summary>
    /// <value>
    ///   The content identifier.
    /// </value>
    string? ContentID { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether this instance is active.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    bool IsActive { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether this instance is selected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
    /// </value>
    bool IsSelected { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    string Title { get; set; }
}
