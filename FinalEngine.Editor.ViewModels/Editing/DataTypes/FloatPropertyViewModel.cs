// <copyright file="FloatPropertyViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

public sealed class FloatPropertyViewModel : PropertyViewModel<float>
{
    public FloatPropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }

    [Range(0, float.MaxValue, ErrorMessage = "You must enter a valid float.")]
    public override float Value
    {
        get { return base.Value; }
        set { base.Value = value; }
    }
}
