// <copyright file="SceneHierarchyToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Messages.Entities;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="ISceneHierarchyToolViewModel"/>.
/// </summary>
/// <seealso cref="ToolViewModelBase" />
/// <seealso cref="ISceneHierarchyToolViewModel" />
public sealed class SceneHierarchyToolViewModel : ToolViewModelBase, ISceneHierarchyToolViewModel
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<SceneHierarchyToolViewModel> logger;

    /// <summary>
    /// The messenger.
    /// </summary>
    private readonly IMessenger messenger;

    /// <summary>
    /// The scene manager.
    /// </summary>
    private readonly ISceneManager sceneManager;

    /// <summary>
    /// The delete entity command.
    /// </summary>
    private IRelayCommand? deleteEntityCommand;

    /// <summary>
    /// The selected entity, or <c>null</c> if one is not selected.
    /// </summary>
    private Entity? selectedEntity;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneHierarchyToolViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="messenger">
    /// The messenger.
    /// </param>
    /// <param name="sceneManager">
    /// The scene manager.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/>, <paramref name="messenger"/> or <paramref name="sceneManager"/> parameter cannot be null.
    /// </exception>
    public SceneHierarchyToolViewModel(
        ILogger<SceneHierarchyToolViewModel> logger,
        IMessenger messenger,
        ISceneManager sceneManager)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));

        this.Title = "Scene Hierarchy";
        this.ContentID = "SceneHierarchy";

        this.logger.LogInformation($"Initializing {this.Title}...");
    }

    /// <inheritdoc/>
    public ICommand DeleteEntityCommand
    {
        get { return this.deleteEntityCommand ??= new RelayCommand(this.DeleteEntity, this.CanDeleteEntity); }
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<Entity> Entities
    {
        get { return this.sceneManager.ActiveScene.Entities; }
    }

    /// <inheritdoc/>
    public Entity? SelectedEntity
    {
        get
        {
            return this.selectedEntity;
        }

        set
        {
            this.SetProperty(ref this.selectedEntity, value);
            this.deleteEntityCommand?.NotifyCanExecuteChanged();

            if (this.SelectedEntity != null)
            {
                this.messenger.Send(new EntitySelectedMessage(this.SelectedEntity));
            }
        }
    }

    /// <summary>
    /// Determines whether the <see cref="DeleteEntityCommand"/> can execute and delete the <see cref="SelectedEntity"/>.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the <see cref="DeleteEntityCommand"/> can execute and delete the <see cref="SelectedEntity"/>; otherwise, <c>false</c>.
    /// </returns>
    private bool CanDeleteEntity()
    {
        return this.SelectedEntity != null;
    }

    /// <summary>
    /// Deletes the <see cref="SelectedEntity"/> from the scene.
    /// </summary>
    private void DeleteEntity()
    {
        if (this.SelectedEntity == null)
        {
            return;
        }

        this.sceneManager.ActiveScene.RemoveEntity(this.SelectedEntity.UniqueID);
    }
}
