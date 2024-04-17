// <copyright file="PaneViewModelBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

using CommunityToolkit.Mvvm.ComponentModel;

public abstract class PaneViewModelBase : ObservableObject, IPaneViewModel
{
    private string? contentID;

    private bool isActive;

    private bool isSelected;

    private string? title;

    protected PaneViewModelBase()
    {
        this.IsActive = true;
    }

    public string ContentID
    {
        get { return this.contentID ?? string.Empty; }
        set { this.SetProperty(ref this.contentID, value); }
    }

    public bool IsActive
    {
        get { return this.isActive; }
        set { this.SetProperty(ref this.isActive, value); }
    }

    public bool IsSelected
    {
        get { return this.isSelected; }
        set { this.SetProperty(ref this.isSelected, value); }
    }

    public string Title
    {
        get { return this.title ?? string.Empty; }
        set { this.SetProperty(ref this.title, value); }
    }
}
