// <copyright file="EntitySystemsToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

public sealed class EntitySystemsToolViewModel : ToolViewModelBase, IEntitySystemsToolViewModel
{
    public EntitySystemsToolViewModel()
    {
        this.Title = "Entity Systems";
        this.ContentID = "EntitySystems";
        this.PaneLocation = PaneLocation.Right;
    }
}
