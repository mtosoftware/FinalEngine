// <copyright file="StringPropertyViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.Reflection;
using FinalEngine.ECS;

/// <summary>
/// Provides an implementation of a <see cref="PropertyViewModel{T}"/> with a generic type of <c>string</c>.
/// </summary>
/// <seealso cref="PropertyViewModel{T}"/>
public sealed class StringPropertyViewModel : PropertyViewModel<string?>
{
    public StringPropertyViewModel(IEntityComponent component, PropertyInfo property)
        : base(component, property)
    {
    }
}
