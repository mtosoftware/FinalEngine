// <copyright file="BoolPropertyViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.Reflection;

public sealed class BoolPropertyViewModel : PropertyViewModel<bool>
{
    public BoolPropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }
}
