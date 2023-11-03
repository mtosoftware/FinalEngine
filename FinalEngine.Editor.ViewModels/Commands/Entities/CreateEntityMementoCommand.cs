// <copyright file="CreateEntityMementoCommand.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Commands.Entities;

using System;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Messages.Entities;

/// <summary>
/// Provides an <see cref="IMementoCommand"/> implementation that handles creating a new entity for the currently active scene.
/// </summary>
/// <seealso cref="IMementoCommand" />
public sealed class CreateEntityMementoCommand : IMementoCommand
{
    /// <summary>
    /// The entity unique identifier.
    /// </summary>
    private readonly Guid entityGuid;

    /// <summary>
    /// The entity name.
    /// </summary>
    private readonly string entityName;

    /// <summary>
    /// The messenger.
    /// </summary>
    private readonly IMessenger messenger;

    /// <summary>
    /// The scene manager.
    /// </summary>
    private readonly ISceneManager sceneManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEntityMementoCommand"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger.
    /// </param>
    /// <param name="sceneManager">
    /// The scene manager.
    /// </param>
    /// <param name="entityName">
    /// The name of the entity.
    /// </param>
    /// <param name="entityGuid">
    /// The entity unique identifier.
    /// </param>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="entityName"/> cannot be null, empty or consist of only whitespace characters.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="sceneManager"/> parameter cannot be null.
    /// </exception>
    public CreateEntityMementoCommand(
        IMessenger messenger,
        ISceneManager sceneManager,
        string entityName,
        Guid entityGuid)
    {
        if (string.IsNullOrEmpty(entityName))
        {
            throw new ArgumentException($"'{nameof(entityName)}' cannot be null or empty.", nameof(entityName));
        }

        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));

        this.entityName = entityName;
        this.entityGuid = entityGuid;
    }

    /// <inheritdoc/>
    public string ActionName
    {
        get { return "Create Entity"; }
    }

    /// <inheritdoc/>
    public void Apply()
    {
        var scene = this.sceneManager.ActiveScene;

        var entity = new Entity(this.entityGuid);

        entity.AddComponent(new TransformComponent());
        entity.AddComponent(new TagComponent()
        {
            Tag = this.entityName,
        });

        scene.AddEntity(entity);
    }

    /// <inheritdoc/>
    public void Revert()
    {
        this.sceneManager.ActiveScene.RemoveEntity(this.entityGuid);
        this.messenger.Send(new EntityDeletedMessage());
    }
}
