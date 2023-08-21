// <copyright file="Vector4PropertyViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.ComponentModel.DataAnnotations;
using System.Numerics;

using System.Reflection;

/// <summary>
/// Provides an implementation of a <see cref="PropertyViewModel{T}"/> with a generic type of <see cref="Vector4"/>.
/// </summary>
/// <seealso cref="PropertyViewModel{T}"/>
public sealed class Vector4PropertyViewModel : PropertyViewModel<Vector4>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Vector4PropertyViewModel"/> class.
    /// </summary>
    /// <param name="component">
    /// The object that contains the <see cref="Vector4"/> property.
    /// </param>
    /// <param name="property">
    /// The property.
    /// </param>
    public Vector4PropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }

    /// <summary>
    /// Gets or sets the W component.
    /// </summary>
    /// <value>
    /// The W component.
    /// </value>
    [Range(0, float.MaxValue, ErrorMessage = "You must enter a valid float.")]
    public float W
    {
        get
        {
            return this.Value.W;
        }

        set
        {
            var temp = this.Value;
            temp.W = value;
            this.Value = temp;
        }
    }

    /// <summary>
    /// Gets or sets the X component.
    /// </summary>
    /// <value>
    /// The x component.
    /// </value>
    [Range(0, float.MaxValue, ErrorMessage = "You must enter a valid float.")]
    public float X
    {
        get
        {
            return this.Value.X;
        }

        set
        {
            var temp = this.Value;
            temp.X = value;
            this.Value = temp;
        }
    }

    /// <summary>
    /// Gets or sets the X component.
    /// </summary>
    /// <value>
    /// The x component.
    /// </value>
    [Range(0, float.MaxValue, ErrorMessage = "You must enter a valid float.")]
    public float Y
    {
        get
        {
            return this.Value.Y;
        }

        set
        {
            var temp = this.Value;
            temp.Y = value;
            this.Value = temp;
        }
    }

    /// <summary>
    /// Gets or sets the Z component.
    /// </summary>
    /// <value>
    /// The Z component.
    /// </value>
    [Range(0, float.MaxValue, ErrorMessage = "You must enter a valid float.")]
    public float Z
    {
        get
        {
            return this.Value.Z;
        }

        set
        {
            var temp = this.Value;
            temp.Z = value;
            this.Value = temp;
        }
    }
}
