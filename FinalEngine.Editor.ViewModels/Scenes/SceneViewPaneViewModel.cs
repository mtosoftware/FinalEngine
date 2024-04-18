// <copyright file="SceneViewPaneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System;
using System.Drawing;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using Microsoft.Extensions.Logging;

public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    private readonly ISceneViewRenderer viewRenderer;

    private IRelayCommand<Size>? adjustRenderSizeCommand;

    private ICommand? renderCommand;

    public SceneViewPaneViewModel(
        ILogger<SceneViewPaneViewModel> logger,
        ISceneViewRenderer viewRenderer)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.viewRenderer = viewRenderer ?? throw new ArgumentNullException(nameof(viewRenderer));

        this.Title = "Scene View";
        this.ContentID = "SceneView";

        logger.LogInformation($"Initializing {this.Title}...");
    }

    public ICommand AdjustRenderSizeCommand
    {
        get { return this.adjustRenderSizeCommand ??= new RelayCommand<Size>(this.AdjustRenderSize); }
    }

    public ICommand RenderCommand
    {
        get { return this.renderCommand ??= new RelayCommand(this.Render); }
    }

    private void AdjustRenderSize(Size size)
    {
        this.viewRenderer.AdjustView(size.Width, size.Height);
    }

    private void Render()
    {
        this.viewRenderer.Render();
    }
}
