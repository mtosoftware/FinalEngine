// <copyright file="GeometryRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Core;
using FinalEngine.Rendering.Geometry;

public sealed class GeometryRenderer : IRenderQueue<Model>, IGeometryRenderer
{
    private readonly Dictionary<Model, IEnumerable<Transform>> modelToTransformMap;

    private readonly IRenderDevice renderDevice;

    public GeometryRenderer(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.modelToTransformMap = [];
    }

    public bool CanRender
    {
        get { return this.modelToTransformMap.Count != 0; }
    }

    public void Clear()
    {
        this.modelToTransformMap.Clear();
    }

    public void Enqueue(Model renderable)
    {
        ArgumentNullException.ThrowIfNull(renderable, nameof(renderable));

        if (!this.modelToTransformMap.TryGetValue(renderable, out var batch))
        {
            batch = new List<Transform>();
            this.modelToTransformMap.Add(renderable, batch);
        }

        ((IList<Transform>)batch).Add(renderable.Transform);
    }

    public void Render()
    {
        foreach (var kvp in this.modelToTransformMap)
        {
            var model = kvp.Key;
            var batch = kvp.Value;

            if (!this.TryPrepareModel(model))
            {
                continue;
            }

            foreach (var transform in batch)
            {
                this.UpdateUniforms(transform);
                this.RenderBatchInstance(model.Mesh);
            }
        }
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

    private void UpdateUniforms(Transform transform)
    {
        this.renderDevice.Pipeline.SetUniform("u_transform", transform.CreateTransformationMatrix());
    }
}
