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
    private readonly IViewRenderer viewRenderer;

    private ICommand? renderCommand;

    public SceneViewPaneViewModel(
        ILogger<SceneViewPaneViewModel> logger,
        IViewRenderer viewRenderer)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.viewRenderer = viewRenderer ?? throw new ArgumentNullException(nameof(viewRenderer));

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
        //// TODO: Add issue to adjust view only if the width/height changes.
        this.viewRenderer.AdjustView(this.Width, this.Height);
        this.viewRenderer.Render();
    }
}
