// <copyright file="SceneViewPaneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using Microsoft.Extensions.Logging;

public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    private readonly IViewRenderer sceneRenderer;

    private ICommand? renderCommand;

    public SceneViewPaneViewModel(
        ILogger<SceneViewPaneViewModel> logger,
        IViewRenderer sceneRenderer)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));

        this.Title = "Scene View";
        this.ContentID = "SceneView";

        logger.LogInformation($"Initializing {this.Title}...");
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
