// <copyright file="SceneHierarchyToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

public sealed class SceneHierarchyToolViewModel : ToolViewModelBase, ISceneHierarchyToolViewModel
{
    public SceneHierarchyToolViewModel()
    {
        this.Title = "Scene Hierarchy";
        this.ContentID = "SceneHierarchy";
        this.PaneLocation = PaneLocation.Left;
    }
}
