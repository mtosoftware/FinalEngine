// <copyright file="SceneHierarchyToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

using System;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="ISceneHierarchyToolViewModel"/>.
/// </summary>
/// <seealso cref="ToolViewModelBase" />
/// <seealso cref="ISceneHierarchyToolViewModel" />
public sealed class SceneHierarchyToolViewModel : ToolViewModelBase, ISceneHierarchyToolViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SceneHierarchyToolViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    public SceneHierarchyToolViewModel(ILogger<SceneHierarchyToolViewModel> logger)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        this.Title = "Scene Hierarchy";
        this.ContentID = "SceneHierarchy";

        logger.LogInformation($"Initializing {this.Title}...");
    }
}
