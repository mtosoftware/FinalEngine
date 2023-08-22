// <copyright file="IRenderPipeline.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;

/// <summary>
/// Defines an interface that orchestrates the initialization of the rendering pipeline.
/// </summary>
public interface IRenderPipeline : IDisposable
{
    /// <summary>
    /// Initializes the rendering pipeline, paving the way for efficient GPU-driven rendering instructions.
    /// </summary>
    void Initialize();
}
