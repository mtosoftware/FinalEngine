// <copyright file="SceneHierarchyToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

using System;
using System.Collections.Generic;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Services.Scenes;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="ISceneHierarchyToolViewModel"/>.
/// </summary>
/// <seealso cref="ToolViewModelBase" />
/// <seealso cref="ISceneHierarchyToolViewModel" />
public sealed class SceneHierarchyToolViewModel : ToolViewModelBase, ISceneHierarchyToolViewModel
{
    /// <summary>
    /// The scene manager.
    /// </summary>
    private readonly ISceneManager sceneManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneHierarchyToolViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="sceneManager">
    /// The scene manager.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/> or <paramref name="sceneManager"/> parameter cannot be null.
    /// </exception>
    public SceneHierarchyToolViewModel(ILogger<SceneHierarchyToolViewModel> logger, ISceneManager sceneManager)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));

        this.Title = "Scene Hierarchy";
        this.ContentID = "SceneHierarchy";

        logger.LogInformation($"Initializing {this.Title}...");
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<Entity> Entities
    {
        get { return this.sceneManager.ActiveScene.Entities; }
    }
}
