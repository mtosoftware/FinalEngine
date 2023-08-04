// <copyright file="ScenePaneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;

public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    public SceneViewPaneViewModel()
    {
        this.Title = "Scene View";
        this.ContentID = "SceneView";
    }
}
