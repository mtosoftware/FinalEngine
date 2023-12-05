// <copyright file="IEntityComponentViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

public interface IEntityComponentViewModel
{
    bool IsVisible { get; }

    string Name { get; }

    ICollection<ObservableObject> PropertyViewModels { get; }

    ICommand RemoveCommand { get; }

    ICommand ToggleCommand { get; }
}
