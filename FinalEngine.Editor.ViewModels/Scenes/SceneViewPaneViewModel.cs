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
using FinalEngine.Rendering;
using FinalEngine.Rendering.Systems;
using Microsoft.Extensions.Logging;

public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    private static bool isInitialized;

    private readonly IRenderDevice renderDevice;

    private readonly ISceneManager sceneManager;

    private ICommand? renderCommand;

    public SceneViewPaneViewModel(
        ILogger<SceneViewPaneViewModel> logger,
        IRenderDevice renderDevice,
        ISceneManager sceneManager)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));

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
        if (!isInitialized)
        {
            this.sceneManager.Initialize();
            this.sceneManager.ActiveScene.AddSystem<SpriteRenderEntitySystem>();
            isInitialized = true;
        }

        this.renderDevice.Clear(Color.Black);
        this.sceneManager.ActiveScene.Render();
    }
}
