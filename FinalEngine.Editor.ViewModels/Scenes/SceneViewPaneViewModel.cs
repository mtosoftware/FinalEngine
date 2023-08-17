// <copyright file="SceneViewPaneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="ISceneViewPaneViewModel"/>.
/// </summary>
/// <seealso cref="PaneViewModelBase" />
/// <seealso cref="ISceneViewPaneViewModel" />
public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    /// <summary>
    /// The scene renderer, used to render the current scene.
    /// </summary>
    private readonly ISceneRenderer sceneRenderer;

    /// <summary>
    /// The render command, used to render the current scene.
    /// </summary>
    private ICommand? renderCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneViewPaneViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="sceneRenderer">
    /// The scene renderer used to render the currently active scene.
    /// </param>
    public SceneViewPaneViewModel(
        ILogger<SceneViewPaneViewModel> logger,
        ISceneRenderer sceneRenderer)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));

        this.Title = "Scene View";
        this.ContentID = "SceneView";

        logger.LogInformation($"Initializing {this.Title}...");
    }

    /// <inheritdoc/>
    public ICommand RenderCommand
    {
        get { return this.renderCommand ??= new RelayCommand(this.Render); }
    }

    /// <summary>
    /// Renders the currently active scene.
    /// </summary>
    private void Render()
    {
        this.sceneRenderer.Render();
    }
}
