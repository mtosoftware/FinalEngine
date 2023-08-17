// <copyright file="EntityComponentViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.ECS;
using FinalEngine.Editor.ViewModels.Editing.DataTypes;

/// <summary>
/// Provides a standard implementation of an <see cref="IEntityComponentViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IEntityComponentViewModel" />
public sealed class EntityComponentViewModel : ObservableObject, IEntityComponentViewModel
{
    /// <summary>
    /// The property view models associated with this component model.
    /// </summary>
    private readonly ObservableCollection<ObservableObject> propertyViewModels;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityComponentViewModel"/> class.
    /// </summary>
    /// <param name="component">
    /// The component to be modelled.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="component"/> parameter cannot be null.
    /// </exception>
    public EntityComponentViewModel(IEntityComponent component)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        this.propertyViewModels = new ObservableCollection<ObservableObject>();

        this.Name = component.GetType().Name;

        //// TODO: Only use public properties and also consider attributes (what if the user wants to use a private property or field).
        foreach (var property in component.GetType().GetProperties())
        {
            var type = property.PropertyType;
            var browsable = property.GetCustomAttribute<BrowsableAttribute>();

            if (browsable != null && !browsable.Browsable)
            {
                continue;
            }

            switch (type.Name.ToUpperInvariant())
            {
                case "STRING":
                    this.propertyViewModels.Add(new StringPropertyViewModel(component, property));
                    break;

                default:
                    //// TODO: Log a warning message here.
                    break;
            }
        }
    }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public ICollection<ObservableObject> PropertyViewModels
    {
        get { return this.propertyViewModels; }
    }
}
