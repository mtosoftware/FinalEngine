// <copyright file="ISceneManager.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using FinalEngine.Editor.Common.Models.Scenes;

/// <summary>
/// Defines an interface that represents a <see cref="Scene"/> manager.
/// </summary>
public interface ISceneManager
{
    /// <summary>
    /// Gets the currently active/loaded scene.
    /// </summary>
    /// <value>
    /// The active/loaded scene.
    /// </value>
    /// <remarks>
    /// The <see cref="ActiveScene"/> property should never return <c>null</c>, an empty scene should be created on application startup.
    /// </remarks>
    IScene ActiveScene { get; }
}
