// <copyright file="EntityComponentViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.ECS;
using FinalEngine.Editor.ViewModels.Editing;

/// <summary>
/// Provides a standard implementation of an <see cref="IEntityComponentViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IEntityComponentViewModel" />
public sealed class EntityComponentViewModel : ObservableObject, IEntityComponentViewModel
{
    /// <summary>
    /// The component that is being modelled.
    /// </summary>
    private readonly IComponent component;

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
    public EntityComponentViewModel(IComponent component)
    {
        this.component = component ?? throw new ArgumentNullException(nameof(component));
        this.propertyViewModels = new ObservableCollection<ObservableObject>();

        this.Name = component.GetType().Name;

        //// TODO: Only use public properties and also consider attributes (what if the user wants to use a private property or field).
        foreach (var property in component.GetType().GetProperties())
        {
            string name = property.Name;
            var type = property.PropertyType;

            switch (type.Name.ToUpperInvariant())
            {
                case "STRING":
                    this.AddProperty<string?>(property);
                    break;

                default:
                    //// TODO: Throw an exception or log?
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

    /// <summary>
    /// Adds a <see cref="PropertyViewModel{T}"/> of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of property to add.
    /// </typeparam>
    /// <param name="property">
    /// The property.
    /// </param>
    private void AddProperty<T>(PropertyInfo property)
    {
        var getValue = new Func<T?>(() =>
        {
            return (T?)property.GetValue(this.component);
        });

        var setValue = new Action<T?>(x =>
        {
            property.SetValue(this.component, x);
        });

        this.propertyViewModels.Add(new PropertyViewModel<T?>(property.Name, getValue, setValue));
    }
}
