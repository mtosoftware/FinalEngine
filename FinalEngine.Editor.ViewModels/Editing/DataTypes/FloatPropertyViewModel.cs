// <copyright file="FloatPropertyViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

/// <summary>
/// Provides an implementation of a <see cref="PropertyViewModel{T}"/> with a generic type of <c>float</c>.
/// </summary>
/// <seealso cref="PropertyViewModel{T}"/>
public sealed class FloatPropertyViewModel : PropertyViewModel<float>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FloatPropertyViewModel"/> class.
    /// </summary>
    /// <param name="component">
    /// The object that contains the float property.
    /// </param>
    /// <param name="property">
    /// The property.
    /// </param>
    public FloatPropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }

    /// <inheritdoc/>
    [Range(0, float.MaxValue, ErrorMessage = "You must enter a valid float.")]
    public override float Value
    {
        get { return base.Value; }
        set { base.Value = value; }
    }
}
