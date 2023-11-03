// <copyright file="DeleteEntityMementoCommand.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Commands.Entities;

using System;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Messages.Entities;

public sealed class DeleteEntityMementoCommand : IMementoCommand
{
    /// <summary>
    /// The entity.
    /// </summary>
    private readonly Entity? entity;

    /// <summary>
    /// The messenger.
    /// </summary>
    private readonly IMessenger? messenger;

    /// <summary>
    /// The scene manager.
    /// </summary>
    private readonly ISceneManager? sceneManager;

    public DeleteEntityMementoCommand(Entity? entity, IMessenger? messenger, ISceneManager? sceneManager)
    {
        this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
    }

    /// <inheritdoc/>
    public string ActionName
    {
        get { return "Delete Entity"; }
    }

    /// <inheritdoc/>
    public void Apply()
    {
        this.sceneManager.ActiveScene.RemoveEntity(this.entity.UniqueIdentifier);
        this.messenger.Send(new EntityDeletedMessage());
    }

    /// <inheritdoc/>
    public void Revert()
    {
        this.sceneManager.ActiveScene.AddEntity(this.entity);
    }
}
