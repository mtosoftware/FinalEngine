// <copyright file="StringPropertyViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.Reflection;

public sealed class StringPropertyViewModel : PropertyViewModel<string>
{
    public StringPropertyViewModel(object component, PropertyInfo property)
        : base(component, property)
    {
    }
}
