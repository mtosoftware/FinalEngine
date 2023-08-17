// <copyright file="StringPropertyViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.Reflection;

/// <summary>
/// Provides an implementation of a <see cref="PropertyViewModel{T}"/> with a generic type of <c>string</c>.
/// </summary>
/// <seealso cref="PropertyViewModel{T}"/>
public sealed class StringPropertyViewModel : PropertyViewModel<string?>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringPropertyViewModel"/> class.
    /// </summary>
    /// <param name="component">
    /// The component.
    /// </param>
    /// <param name="property">
    /// The property.
    /// </param>
    public StringPropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }
}
