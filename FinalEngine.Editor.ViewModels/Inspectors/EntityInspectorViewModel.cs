// <copyright file="EntityInspectorViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.Editor.ViewModels.Messages.Entities;

/// <summary>
///   Provides a standard implementation of an <see cref="IEntityInspectorViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject"/>
/// <seealso cref="IEntityInspectorViewModel"/>
public sealed class EntityInspectorViewModel : ObservableObject, IEntityInspectorViewModel
{
    /// <summary>
    ///   The component view models.
    /// </summary>
    private readonly ObservableCollection<IEntityComponentViewModel> componentViewModels;

    /// <summary>
    ///   The entity being inspected.
    /// </summary>
    private readonly Entity entity;

    /// <summary>
    ///   The messenger.
    /// </summary>
    private readonly IMessenger messenger;

    /// <summary>
    ///   Initializes a new instance of the <see cref="EntityInspectorViewModel"/> class.
    /// </summary>
    /// <param name="messenger">
    ///   The messenger.
    /// </param>
    /// <param name="entity">
    ///   The entity to be inspected.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="entity"/> parameter cannot be null.
    /// </exception>
    public EntityInspectorViewModel(IMessenger messenger, Entity entity)
    {
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        this.componentViewModels = new ObservableCollection<IEntityComponentViewModel>();

        this.messenger.Register<EntityModifiedMessage>(this, this.HandleEntityModified);

        this.InitializeEntityComponents();
    }

    /// <inheritdoc/>
    public ICollection<IEntityComponentViewModel> ComponentViewModels
    {
        get { return this.componentViewModels; }
    }

    /// <summary>
    ///   Handles the <see cref="EntityModifiedMessage"/> and initializes the entity component view models.
    /// </summary>
    /// <param name="recipient">
    ///   The recipient.
    /// </param>
    /// <param name="message">
    ///   The message.
    /// </param>
    private void HandleEntityModified(object recipient, EntityModifiedMessage message)
    {
        if (!ReferenceEquals(this.entity, message.Entity))
        {
            return;
        }

        this.InitializeEntityComponents();
    }

    /// <summary>
    ///   Initializes the entity component view models for the <see cref="Entity"/>.
    /// </summary>
    private void InitializeEntityComponents()
    {
        this.componentViewModels.Clear();

        foreach (var component in this.entity.Components)
        {
            this.componentViewModels.Add(new EntityComponentViewModel(this.messenger, this.entity, component));
        }
    }
}
