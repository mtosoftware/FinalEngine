// <copyright file="DoublePropertyViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

public sealed class DoublePropertyViewModel : PropertyViewModel<double>
{
    public DoublePropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }

    [Range(0, double.MaxValue, ErrorMessage = "You must enter a valid double.")]
    public override double Value
    {
        get { return base.Value; }
        set { base.Value = value; }
    }
}
