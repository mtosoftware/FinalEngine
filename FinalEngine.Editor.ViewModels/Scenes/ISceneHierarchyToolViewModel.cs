// <copyright file="ISceneHierarchyToolViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.ECS;
using FinalEngine.Editor.ViewModels.Docking.Tools;

public interface ISceneHierarchyToolViewModel : IToolViewModel
{
    IRelayCommand DeleteEntityCommand { get; }

    IReadOnlyCollection<Entity> Entities { get; }

    Entity? SelectedEntity { get; set; }
}
