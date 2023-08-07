// <copyright file="EntitySystemsToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

using CommunityToolkit.Mvvm.Messaging;

public sealed class EntitySystemsToolViewModel : ToolViewModelBase, IEntitySystemsToolViewModel
{
    public EntitySystemsToolViewModel(IMessenger messenger)
        : base(messenger)
    {
        this.Title = "Entity Systems";
        this.ContentID = "EntitySystems";
        this.PaneLocation = PaneLocation.Right;
    }
}
