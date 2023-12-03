// <copyright file="EntityComponentCategoryViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

public sealed class EntityComponentCategoryViewModel : ObservableObject, IEntityComponentCategoryViewModel
{
    private string? name;

    public EntityComponentCategoryViewModel(string name, IEnumerable<IEntityComponentTypeViewModel> componentTypes)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.ComponentTypes = componentTypes ?? throw new ArgumentNullException(nameof(componentTypes));
    }

    public IEnumerable<IEntityComponentTypeViewModel> ComponentTypes { get; }

    public string Name
    {
        get { return this.name ?? string.Empty; }
        private set { this.SetProperty(ref this.name, value); }
    }
}
