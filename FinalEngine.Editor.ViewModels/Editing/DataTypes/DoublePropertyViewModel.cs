// <copyright file="DoublePropertyViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

/// <summary>
/// Provides an implementation of a <see cref="PropertyViewModel{T}"/> with a generic type of <c>double</c>.
/// </summary>
/// <seealso cref="PropertyViewModel{T}"/>
public sealed class DoublePropertyViewModel : PropertyViewModel<double>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoublePropertyViewModel"/> class.
    /// </summary>
    /// <param name="component">
    /// The object that contains the double property.
    /// </param>
    /// <param name="property">
    /// The property.
    /// </param>
    public DoublePropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }

    /// <inheritdoc/>
    [Range(0, double.MaxValue, ErrorMessage = "You must enter a valid double.")]
    public override double Value
    {
        get { return base.Value; }
        set { base.Value = value; }
    }
}
