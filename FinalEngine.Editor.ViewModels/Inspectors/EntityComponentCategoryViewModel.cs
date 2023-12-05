// <copyright file="EntityComponentCategoryViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

public sealed class EntityComponentCategoryViewModel : ObservableObject, IEntityComponentCategoryViewModel
{
    public EntityComponentCategoryViewModel(string name, IEnumerable<IEntityComponentTypeViewModel> componentTypes)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        this.Name = name;
        this.ComponentTypes = componentTypes ?? throw new ArgumentNullException(nameof(componentTypes));
    }

    public IEnumerable<IEntityComponentTypeViewModel> ComponentTypes { get; }

    public string Name { get; }
}
