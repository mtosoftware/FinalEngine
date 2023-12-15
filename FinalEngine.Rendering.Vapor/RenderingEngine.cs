// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor;

using System;
using System.Drawing;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly IRenderDevice renderDevice;

    public RenderingEngine(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    public void Render()
    {
        this.renderDevice.Clear(Color.Black);
    }
}
