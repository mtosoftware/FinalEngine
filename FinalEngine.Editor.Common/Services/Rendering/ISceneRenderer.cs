// <copyright file="ISceneRenderer.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Rendering;

/// <summary>
/// Defines an interface that represents a scene renderer.
/// </summary>
public interface ISceneRenderer
{
    /// <summary>
    /// Changes the current scene projection the specified <paramref name="projectionWidth"/> and <paramref name="projectionHeight"/>.
    /// </summary>
    /// <param name="projectionWidth">
    /// The width of the projection.
    /// </param>
    /// <param name="projectionHeight">
    /// The height of the projection.
    /// </param>
    void ChangeProjection(int projectionWidth, int projectionHeight);

    /// <summary>
    /// Renders the currently active scene.
    /// </summary>
    void Render();
}
