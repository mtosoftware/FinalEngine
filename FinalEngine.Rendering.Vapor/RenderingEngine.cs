// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor;

using System;
using System.Drawing;
using System.IO.Abstractions;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly IRenderDevice renderDevice;

    public RenderingEngine(IRenderDevice renderDevice, IFileSystem fileSystem)
    {
        ArgumentNullException.ThrowIfNull(fileSystem, nameof(fileSystem));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

        this.renderDevice.Pipeline.AddShaderHeader("lighting", fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\lighting.glsl"));
        this.renderDevice.Pipeline.AddShaderHeader("material", fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\material.glsl"));
    }

    public void Render()
    {
        this.renderDevice.Clear(Color.Black);
    }
}
