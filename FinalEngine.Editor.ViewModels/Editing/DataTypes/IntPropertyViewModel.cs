// <copyright file="IntPropertyViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

/// <summary>
/// Provides an implementation of a <see cref="PropertyViewModel{T}"/> with a generic type of <c>int</c>.
/// </summary>
/// <seealso cref="PropertyViewModel{T}"/>
public sealed class IntPropertyViewModel : PropertyViewModel<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntPropertyViewModel"/> class.
    /// </summary>
    /// <param name="component">
    /// The object that contains the integer property.
    /// </param>
    /// <param name="property">
    /// The property.
    /// </param>
    public IntPropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }

    /// <inheritdoc/>
    [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid integer.")]
    public override int Value
    {
        get
        {
            return base.Value;
        }

        set
        {
            base.Value = value;
        }
    }
}
