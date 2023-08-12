// <copyright file="SceneViewPaneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Scenes;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="ISceneViewPaneViewModel"/>.
/// </summary>
/// <seealso cref="PaneViewModelBase" />
/// <seealso cref="ISceneViewPaneViewModel" />
public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    private readonly ILogger<SceneViewPaneViewModel> logger;

    private readonly ISceneRenderer sceneRenderer;

    private ICommand? renderCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneViewPaneViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    public SceneViewPaneViewModel(
        ILogger<SceneViewPaneViewModel> logger,
        ISceneRenderer sceneRenderer)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));

        this.Title = "Scene View";
        this.ContentID = "SceneView";

        this.logger.LogInformation($"Initializing {this.Title}...");
    }

    public ICommand RenderCommand
    {
        get { return this.renderCommand ??= new RelayCommand(this.Render); }
    }

    private void Render()
    {
        this.sceneRenderer.Render();
    }
}
