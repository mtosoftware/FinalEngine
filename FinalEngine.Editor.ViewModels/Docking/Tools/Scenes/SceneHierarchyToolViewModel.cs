// <copyright file="SceneHierarchyToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

using System;
using System.Collections.Generic;
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
    /// The messenger.
    /// </summary>
    private readonly IMessenger messenger;

    /// <summary>
    /// The scene manager.
    /// </summary>
    private readonly ISceneManager sceneManager;

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
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));

        this.Title = "Scene Hierarchy";
        this.ContentID = "SceneHierarchy";

        logger.LogInformation($"Initializing {this.Title}...");
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

            if (this.SelectedEntity != null)
            {
                this.messenger.Send(new EntitySelectedMessage(this.SelectedEntity));
            }
        }
    }
}
