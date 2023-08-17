// <copyright file="PropertyStringViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Components;

using System;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// Provides a standard implementation of an <see cref="IPropertyStringViewModel"/>.
/// </summary>
/// <seealso cref="ObservableValidator" />
/// <seealso cref="IPropertyStringViewModel" />
public sealed class PropertyStringViewModel : ObservableValidator, IPropertyStringViewModel
{
    /// <summary>
    /// The function useed to get the value of the property.
    /// </summary>
    private readonly Func<string?> getValue;

    /// <summary>
    /// The action used to set the value of the property.
    /// </summary>
    private readonly Action<string?> setValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyStringViewModel"/> class.
    /// </summary>
    /// <param name="name">
    /// The name of the property.
    /// </param>
    /// <param name="getValue">
    /// The function used to retrieve the value of the property.
    /// </param>
    /// <param name="setValue">
    /// The action used to set the value of the property.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="getValue"/> or <paramref name="setValue"/> parameter cannot be null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="name"/> parameter cannot be null or whitespace.
    /// </exception>
    public PropertyStringViewModel(string name, Func<string?> getValue, Action<string?> setValue)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new System.ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        this.getValue = getValue ?? throw new ArgumentNullException(nameof(getValue));
        this.setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));

        this.Name = name;
        this.Value = getValue();
    }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public string? Value
    {
        get
        {
            return this.getValue();
        }

        set
        {
            this.setValue(value);
            this.OnPropertyChanged(nameof(this.Value));
        }
    }
}
