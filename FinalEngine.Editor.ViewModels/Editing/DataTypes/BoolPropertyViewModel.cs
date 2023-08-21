// <copyright file="BoolPropertyViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.Reflection;

/// <summary>
/// Provides an implementation of a <see cref="PropertyViewModel{T}"/> with a generic type of <c>bool</c>.
/// </summary>
/// <seealso cref="PropertyViewModel{T}"/>
public sealed class BoolPropertyViewModel : PropertyViewModel<bool>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoolPropertyViewModel"/> class.
    /// </summary>
    /// <param name="component">
    /// The object that contains the boolean property.
    /// </param>
    /// <param name="property">
    /// The property.
    /// </param>
    public BoolPropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }
}
