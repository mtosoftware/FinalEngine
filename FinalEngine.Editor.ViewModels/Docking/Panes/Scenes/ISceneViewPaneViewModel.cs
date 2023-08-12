// <copyright file="ISceneViewPaneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;

using System.Windows.Input;

/// <summary>
/// Defines an interface that represents a model of the scene view pane.
/// </summary>
/// <seealso cref="IPaneViewModel" />
public interface ISceneViewPaneViewModel : IPaneViewModel
{
    /// <summary>
    /// Gets the render command.
    /// </summary>
    /// <value>
    /// The render command.
    /// </value>
    /// <remarks>
    /// The <see cref="RenderCommand"/> is used to render the currently active scene.
    /// </remarks>
    ICommand RenderCommand { get; }
}
