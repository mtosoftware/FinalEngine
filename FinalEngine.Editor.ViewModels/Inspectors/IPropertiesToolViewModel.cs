// <copyright file="IPropertiesToolViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.Editor.ViewModels.Docking.Tools;

public interface IPropertiesToolViewModel : IToolViewModel
{
    ObservableObject? CurrentViewModel { get; }
}
