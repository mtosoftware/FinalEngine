// <copyright file="EntityComponentViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Components;

using System;
using System.Collections.ObjectModel;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.ECS;

/// <summary>
/// Provides a standard implementation of an <see cref="IEntityComponentViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IEntityComponentViewModel" />
public sealed class EntityComponentViewModel : ObservableObject, IEntityComponentViewModel
{
    /// <summary>
    /// The component.
    /// </summary>
    private readonly IComponent component;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityComponentViewModel"/> class.
    /// </summary>
    /// <param name="name">
    /// The name of the component.
    /// </param>
    /// <param name="component">
    /// The component.
    /// </param>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="name"/> parameter cannot be null or whitespace.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="component"/> parameter cannot be null.
    /// </exception>
    public EntityComponentViewModel(string name, IComponent component)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new System.ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        this.component = component ?? throw new ArgumentNullException(nameof(component));

        this.Name = name;

        this.PropertyViewModels = new ObservableCollection<ObservableObject>();

        foreach (var property in component.GetType().GetProperties())
        {
            switch (property.PropertyType.Name.ToUpperInvariant())
            {
                case "STRING":
                    this.AddStringProperty(property);
                    break;

                default:
                    //// TODO: Log an error here.
                    break;
            }
        }
    }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public ObservableCollection<ObservableObject> PropertyViewModels { get; }

    /// <summary>
    /// Adds a <c>string</c> property view model.
    /// </summary>
    /// <param name="property">
    /// The property information.
    /// </param>
    private void AddStringProperty(PropertyInfo property)
    {
        var getValue = new Func<string?>(() =>
        {
            return (string?)property.GetValue(this.component);
        });

        var setValue = new Action<string?>(x =>
        {
            property.SetValue(this.component, x);
        });

        this.PropertyViewModels.Add(new PropertyStringViewModel(property.Name, getValue, setValue));
    }
}
