// <copyright file="PaneViewModelBase.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// The base class to be implemented for a document view.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IPaneViewModel" />
public partial class PaneViewModelBase : ObservableObject, IPaneViewModel
{
    /// <summary>
    ///   The content identifier.
    /// </summary>
    private string? contentID;

    /// <summary>
    ///   Indicates whether this instance is active.
    /// </summary>
    private bool isActive;

    /// <summary>
    ///   Indicates whether this instance is selected.
    /// </summary>
    private bool isSelected;

    /// <summary>
    ///   The title.
    /// </summary>
    private string? title;

    /// <summary>
    ///   Gets or sets the content identifier.
    /// </summary>
    /// <value>
    ///   The content identifier.
    /// </value>
    public string? ContentID
    {
        get { return this.contentID ?? string.Empty; }
        set { this.SetProperty(ref this.contentID, value); }
    }

    /// <summary>
    ///   Gets or sets a value indicating whether this instance is active.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    public bool IsActive
    {
        get { return this.isActive; }
        set { this.SetProperty(ref this.isActive, value); }
    }

    /// <summary>
    ///   Gets or sets a value indicating whether this instance is selected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
    /// </value>
    public bool IsSelected
    {
        get { return this.isSelected; }
        set { this.SetProperty(ref this.isSelected, value); }
    }

    /// <summary>
    ///   Gets or sets the title.
    /// </summary>
    /// <value>
    ///   The title.
    /// </value>
    public string Title
    {
        get { return this.title ?? string.Empty; }
        set { this.SetProperty(ref this.title, value); }
    }
}
