// <copyright file="EntityInspectorViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.ECS;

/// <summary>
/// Provides a standard implementation of an <see cref="IEntityInspectorViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IEntityInspectorViewModel" />
public sealed class EntityInspectorViewModel : ObservableObject, IEntityInspectorViewModel
{
    /// <summary>
    /// The component view models.
    /// </summary>
    private readonly ObservableCollection<IEntityComponentViewModel> componentViewModels;

    /// <summary>
    /// The entity being inspected.
    /// </summary>
    private readonly Entity entity;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityInspectorViewModel"/> class.
    /// </summary>
    /// <param name="entity">
    /// The entity to be inspected.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="entity"/> parameter cannot be null.
    /// </exception>
    public EntityInspectorViewModel(Entity entity)
    {
        this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        this.componentViewModels = new ObservableCollection<IEntityComponentViewModel>();

        foreach (var component in this.entity.Components)
        {
            this.componentViewModels.Add(new EntityComponentViewModel(component));
        }
    }

    /// <inheritdoc/>
    public ICollection<IEntityComponentViewModel> ComponentViewModels
    {
        get { return this.componentViewModels; }
    }
}
