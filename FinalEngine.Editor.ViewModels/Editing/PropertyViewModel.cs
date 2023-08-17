// <copyright file="PropertyViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing;

using System;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// Provides a standard implementation of an <see cref="IPropertyViewModel{T}"/>.
/// </summary>
/// <typeparam name="T">
/// The type of the property.
/// </typeparam>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IPropertyViewModel{T}"/>
public abstract class PropertyViewModel<T> : ObservableObject, IPropertyViewModel<T>
{
    /// <summary>
    /// The object that contains the property.
    /// </summary>
    private readonly object component;

    /// <summary>
    /// The function used to retrieve the property value.
    /// </summary>
    private readonly Func<T?> getValue;

    /// <summary>
    /// The action used to set the property value.
    /// </summary>
    private readonly Action<T?> setValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyViewModel{T}"/> class.
    /// </summary>
    protected PropertyViewModel(object component, PropertyInfo property)
    {
        if (property == null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        this.component = component ?? throw new ArgumentNullException(nameof(component));

        this.getValue = new Func<T?>(() =>
        {
            return (T?)property.GetValue(this.component);
        });

        this.setValue = new Action<T?>(x =>
        {
            property.SetValue(this.component, x);
        });

        this.Name = property.Name;
        this.Value = this.getValue();
    }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public T? Value
    {
        get
        {
            return this.getValue();
        }

        set
        {
            this.OnPropertyChanging(nameof(this.Value));
            this.setValue(value);
            this.OnPropertyChanged(nameof(this.Value));
        }
    }
}
