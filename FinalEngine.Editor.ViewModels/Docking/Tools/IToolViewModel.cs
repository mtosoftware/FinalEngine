// <copyright file="IToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools;

using FinalEngine.Editor.ViewModels.Docking.Panes;

/// <summary>
/// Defines an interface that represents a model of a tool view.
/// </summary>
/// <seealso cref="IPaneViewModel" />
public interface IToolViewModel : IPaneViewModel
{
    /// <summary>
    /// Gets or sets a value indicating whether this view is visible.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this view is visible; otherwise, <c>false</c>.
    /// </value>
    bool IsVisible { get; set; }
}
