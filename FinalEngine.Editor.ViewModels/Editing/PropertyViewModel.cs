// <copyright file="PropertyViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing;

using System;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// Provides a standard implementation of an <see cref="IPropertyViewModel{T}"/>.
/// </summary>
/// <typeparam name="T">
/// The type of the property.
/// </typeparam>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IPropertyViewModel{T}"/>
public sealed class PropertyViewModel<T> : ObservableObject, IPropertyViewModel<T>
{
    /// <summary>
    /// The function used to retrieve the property value.
    /// </summary>
    private readonly Func<T> getValue;

    /// <summary>
    /// The action used to set the property value.
    /// </summary>
    private readonly Action<T> setValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyViewModel{T}"/> class.
    /// </summary>
    /// <param name="name">
    /// The name of the property.
    /// </param>
    /// <param name="getValue">
    /// The function used to retrieve the property value.
    /// </param>
    /// <param name="setValue">
    /// The function used to set the property value.
    /// </param>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="name"/> parameter cannot be null or whitespace.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="getValue"/> or <paramref name="setValue"/> parameter cannot be null.
    /// </exception>
    public PropertyViewModel(string name, Func<T> getValue, Action<T> setValue)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        this.getValue = getValue ?? throw new ArgumentNullException(nameof(getValue));
        this.setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));

        this.Name = name;
        this.Value = getValue();
    }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public T Value
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
