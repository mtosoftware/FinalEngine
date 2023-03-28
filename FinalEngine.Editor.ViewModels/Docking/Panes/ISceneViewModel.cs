// <copyright file="ISceneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

using System.Drawing;
using System.Windows.Input;

/// <summary>
/// Defines an interface that represents a scene view, view model.
/// </summary>
public interface ISceneViewModel : IPaneViewModel
{
    /// <summary>
    /// Gets the size of the projection.
    /// </summary>
    /// <value>
    /// The size of the projection.
    /// </value>
    Size ProjectionSize { get; }

    /// <summary>
    /// Gets the render command.
    /// </summary>
    /// <value>
    /// The render command.
    /// </value>
    ICommand RenderCommand { get; }
}
