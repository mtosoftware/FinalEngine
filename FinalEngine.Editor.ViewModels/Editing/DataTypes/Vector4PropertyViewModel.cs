// <copyright file="Vector4PropertyViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.ComponentModel.DataAnnotations;
using System.Numerics;

using System.Reflection;

public sealed class Vector4PropertyViewModel : PropertyViewModel<Vector4>
{
    public Vector4PropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }

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
