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
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Rendering.Geometry;
using Microsoft.Extensions.Logging;

public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    private readonly ICamera camera;

    private readonly IKeyboard keyboard;

    private readonly IMouse mouse;

    private readonly ISceneViewRenderer viewRenderer;

    private IRelayCommand<Size>? adjustRenderSizeCommand;

    private ICommand? renderCommand;

    public SceneViewPaneViewModel(
        ILogger<SceneViewPaneViewModel> logger,
        ISceneViewRenderer viewRenderer,
        IKeyboard keyboard,
        IMouse mouse,
        ICamera camera)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
        this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
        this.camera = camera ?? throw new ArgumentNullException(nameof(camera));
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
        this.camera.Update();
        this.keyboard.Update();
        this.mouse.Update();

        this.viewRenderer.Render();
    }
}
