// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Geometry;
using FinalEngine.Rendering.Vapor.Renderers;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly IGeometryRenderer geometryRenderer;

    private readonly Dictionary<Model, IEnumerable<Transform>> modelToTransformationMap;

    public RenderingEngine(IGeometryRenderer geometryRenderer)
    {
        this.geometryRenderer = geometryRenderer ?? throw new ArgumentNullException(nameof(geometryRenderer));
        this.modelToTransformationMap = [];
    }

    public void Enqueue(Model model, Transform transform)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));
        ArgumentNullException.ThrowIfNull(transform, nameof(transform));

        if (!this.modelToTransformationMap.TryGetValue(model, out var batch))
        {
            batch = new List<Transform>();
            this.modelToTransformationMap.Add(model, batch);
        }

        ((IList<Transform>)batch).Add(transform);
    }

    public void Render(ICamera camera)
    {
        ArgumentNullException.ThrowIfNull(camera, nameof(camera));

        this.geometryRenderer.Render(this.modelToTransformationMap);

        this.modelToTransformationMap.Clear();
    }
}
