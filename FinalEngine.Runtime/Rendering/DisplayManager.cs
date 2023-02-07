// <copyright file="DisplayManager.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Rendering;

using System;
using System.Drawing;
using FinalEngine.Platform;
using FinalEngine.Rendering;

public class DisplayManager : IDisplayManager
{
    private readonly IRasterizer rasterizer;

    private readonly IWindow window;

    public DisplayManager(IRasterizer rasterizer, IWindow window)
    {
        this.rasterizer = rasterizer ?? throw new ArgumentNullException(nameof(rasterizer));
        this.window = window ?? throw new ArgumentNullException(nameof(window));
    }

    public int DisplayHeight
    {
        get { return this.rasterizer.GetViewport().Height; }
    }

    public int DisplayWidth
    {
        get { return this.rasterizer.GetViewport().Width; }
    }

    public void ChangeResolution(DisplayResolution resolution)
    {
        var viewport = resolution switch
        {
            DisplayResolution.StandardDefinition => new Rectangle(0, 0, 640, 480),
            DisplayResolution.HighDefinition => new Rectangle(0, 0, 1280, 720),
            DisplayResolution.FullHighDefinition => new Rectangle(0, 0, 1920, 1080),
            DisplayResolution.UltraHighDefinition => new Rectangle(0, 0, 3840, 2160),
            _ => throw new NotSupportedException($"The specified {nameof(resolution)} is not supported."),
        };

        this.window.Size = new Size(
            viewport.Width,
            viewport.Height);

        // glViewport
        this.rasterizer.SetViewport(viewport);
    }
}
