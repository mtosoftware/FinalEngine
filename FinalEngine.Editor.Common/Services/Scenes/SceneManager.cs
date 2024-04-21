// <copyright file="SceneManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using FinalEngine.Editor.Common.Models.Scenes;
using FinalEngine.Rendering;

public sealed class SceneManager : ISceneManager
{
    private static bool isInitialized;

    private readonly IRenderPipeline renderPipeline;

    public SceneManager(IScene scene, IRenderPipeline renderPipeline)
    {
        this.renderPipeline = renderPipeline ?? throw new ArgumentNullException(nameof(renderPipeline));
        this.ActiveScene = scene ?? throw new ArgumentNullException(nameof(scene));
    }

    public IScene ActiveScene { get; }

    public void Initialize()
    {
        if (!isInitialized)
        {
            this.renderPipeline.Initialize();
            isInitialized = true;
        }
    }
}
