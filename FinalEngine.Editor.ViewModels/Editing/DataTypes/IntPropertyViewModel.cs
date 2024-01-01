// <copyright file="IntPropertyViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

public sealed class IntPropertyViewModel : PropertyViewModel<int>
{
    public IntPropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }

    [Range(0, int.MaxValue, ErrorMessage = "You must enter a valid integer.")]
    public override int Value
    {
        get { return base.Value; }
        set { base.Value = value; }
    }
}
