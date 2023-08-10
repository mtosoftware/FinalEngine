// <copyright file="SceneViewPaneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;

/// <summary>
/// Provides a standard implementation of an <see cref="ISceneViewPaneViewModel"/>.
/// </summary>
/// <seealso cref="PaneViewModelBase" />
/// <seealso cref="ISceneViewPaneViewModel" />
public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SceneViewPaneViewModel"/> class.
    /// </summary>
    public SceneViewPaneViewModel()
    {
        this.Title = "Scene View";
        this.ContentID = "SceneView";
    }
}
