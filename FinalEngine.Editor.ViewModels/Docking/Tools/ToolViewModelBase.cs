// <copyright file="ToolViewModelBase.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools;

using System;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Messages.Docking;

public abstract class ToolViewModelBase : PaneViewModelBase, IToolViewModel
{
    private bool isVisible;

    private PaneLocation location;

    protected ToolViewModelBase(IMessenger messenger)
    {
        if (messenger == null)
        {
            throw new ArgumentNullException(nameof(messenger));
        }

        this.IsVisible = true;

        messenger.Register<ToggleToolWindowMessage>(this, this.ToggleToolWindow);
    }

    public bool IsVisible
    {
        get { return this.isVisible; }
        set { this.SetProperty(ref this.isVisible, value); }
    }

    public PaneLocation PaneLocation
    {
        get { return this.location; }
        set { this.SetProperty(ref this.location, value); }
    }

    private void ToggleToolWindow(object recipient, ToggleToolWindowMessage message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        if (message.ContentID != this.ContentID)
        {
            return;
        }

        this.IsVisible = !this.IsVisible;
    }
}
