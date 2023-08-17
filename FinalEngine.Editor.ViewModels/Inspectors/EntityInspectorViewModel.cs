// <copyright file="EntityInspectorViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.ECS;
using FinalEngine.Editor.ViewModels.Components;

/// <summary>
/// Provides a standard implementation of an <see cref="IEntityInspectorViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IEntityInspectorViewModel" />
public sealed class EntityInspectorViewModel : ObservableObject, IEntityInspectorViewModel
{
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
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        this.ComponentViewModels = new ObservableCollection<IEntityComponentViewModel>();

        foreach (var component in entity.Components)
        {
            this.ComponentViewModels.Add(new EntityComponentViewModel(component.GetType().Name, component));
        }
    }

    /// <inheritdoc/>
    public ObservableCollection<IEntityComponentViewModel> ComponentViewModels { get; }
}
