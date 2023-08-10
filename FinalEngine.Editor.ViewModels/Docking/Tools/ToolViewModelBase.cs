// <copyright file="ToolViewModelBase.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools;

using FinalEngine.Editor.ViewModels.Docking.Panes;

/// <summary>
/// Provides a standard implementation of an <see cref="IToolViewModel"/>.
/// </summary>
/// <seealso cref="PaneViewModelBase" />
/// <seealso cref="IToolViewModel" />
public abstract class ToolViewModelBase : PaneViewModelBase, IToolViewModel
{
    /// <summary>
    /// Indicates whether this view is visible.
    /// </summary>
    private bool isVisible;

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolViewModelBase"/> class.
    /// </summary>
    protected ToolViewModelBase()
    {
        this.IsVisible = true;
    }

    /// <inheritdoc/>
    public bool IsVisible
    {
        get { return this.isVisible; }
        set { this.SetProperty(ref this.isVisible, value); }
    }
}
