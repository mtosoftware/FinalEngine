// <copyright file="PaneViewModelBase.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// Provides a standard base implementation of an <see cref="IPaneViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IPaneViewModel" />
public abstract class PaneViewModelBase : ObservableObject, IPaneViewModel
{
    /// <summary>
    /// The content identifier.
    /// </summary>
    private string? contentID;

    /// <summary>
    /// Indicates whether this view is active.
    /// </summary>
    private bool isActive;

    /// <summary>
    /// Indicates whether this view is selected.
    /// </summary>
    private bool isSelected;

    /// <summary>
    /// The title of the view.
    /// </summary>
    private string? title;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaneViewModelBase"/> class.
    /// </summary>
    protected PaneViewModelBase()
    {
        this.IsActive = true;
    }

    /// <inheritdoc/>
    public string ContentID
    {
        get { return this.contentID ?? string.Empty; }
        set { this.SetProperty(ref this.contentID, value); }
    }

    /// <inheritdoc/>
    public bool IsActive
    {
        get { return this.isActive; }
        set { this.SetProperty(ref this.isActive, value); }
    }

    /// <inheritdoc/>
    public bool IsSelected
    {
        get { return this.isSelected; }
        set { this.SetProperty(ref this.isSelected, value); }
    }

    /// <inheritdoc/>
    public string Title
    {
        get { return this.title ?? string.Empty; }
        set { this.SetProperty(ref this.title, value); }
    }
}
