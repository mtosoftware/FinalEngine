// <copyright file="PropertiesToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;

using CommunityToolkit.Mvvm.Messaging;

public sealed class PropertiesToolViewModel : ToolViewModelBase, IPropertiesToolViewModel
{
    public PropertiesToolViewModel(IMessenger messenger)
        : base(messenger)
    {
        this.Title = "Properties";
        this.ContentID = "Properties";
        this.PaneLocation = PaneLocation.Right;
    }
}
