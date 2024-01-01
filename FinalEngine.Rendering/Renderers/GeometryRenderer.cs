// <copyright file="GeometryRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Core;
using FinalEngine.Rendering.Geometry;

public sealed class GeometryRenderer : IGeometryRenderer
{
    private readonly IRenderDevice renderDevice;

    public GeometryRenderer(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    public void Render(IDictionary<Model, IEnumerable<Transform>> modelToTransformMap)
    {
        ArgumentNullException.ThrowIfNull(modelToTransformMap, nameof(modelToTransformMap));

        foreach (var kvp in modelToTransformMap)
        {
            var model = kvp.Key;
            var batch = kvp.Value;

            if (!this.TryPrepareModel(model))
            {
                continue;
            }

            foreach (var transform in batch)
            {
                this.PrepareBatchInstance(transform);
                this.RenderBatchInstance(model.Mesh);
            }
        }
    }

    private void PrepareBatchInstance(Transform transform)
    {
        this.renderDevice.Pipeline.SetUniform("u_transform", transform.CreateTransformationMatrix());
    }

    private void RenderBatchInstance(IMesh? mesh)
    {
        if (mesh == null)
        {
            return;
        }

        mesh.Draw(this.renderDevice);
    }

    private bool TryPrepareModel(Model model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var mesh = model.Mesh;
        var material = model.Material;

        if (mesh == null)
        {
            return false;
        }

        material.Bind(this.renderDevice.Pipeline);
        mesh.Bind(this.renderDevice.InputAssembler);

        return true;
    }
}
