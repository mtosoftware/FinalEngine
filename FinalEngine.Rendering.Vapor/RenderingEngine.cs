// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor;

using System;
using System.Drawing;
using FinalEngine.Rendering.Vapor.Renderers;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly IGeometryRenderer geometryRenderer;

    private readonly IRenderDevice renderDevice;

    public RenderingEngine(IRenderDevice renderDevice, IGeometryRenderer geometryRenderer)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.geometryRenderer = geometryRenderer ?? throw new ArgumentNullException(nameof(geometryRenderer));
    }

    public void Render()
    {
        this.renderDevice.Clear(Color.Black);
        this.geometryRenderer.Render();
    }
}
