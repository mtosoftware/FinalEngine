// <copyright file="EntitySystemsToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

/// <summary>
/// Provides a standard implementation of an <see cref="IEntitySystemsToolViewModel"/>.
/// </summary>
/// <seealso cref="ToolViewModelBase" />
/// <seealso cref="IEntitySystemsToolViewModel" />
public sealed class EntitySystemsToolViewModel : ToolViewModelBase, IEntitySystemsToolViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntitySystemsToolViewModel"/> class.
    /// </summary>
    public EntitySystemsToolViewModel()
    {
        this.Title = "Entity Systems";
        this.ContentID = "EntitySystems";
    }
}
