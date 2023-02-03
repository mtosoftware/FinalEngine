// <copyright file="ResolutionManager.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Drawing;
using FinalEngine.Input.Displays;

public class ResolutionManager : IResolutionManager
{
    private readonly IDisplay display;

    private readonly IRasterizer rasterizer;

    public ResolutionManager(IRasterizer rasterizer, IDisplay display)
    {
        this.rasterizer = rasterizer ?? throw new ArgumentNullException(nameof(rasterizer));
        this.display = display ?? throw new ArgumentNullException(nameof(display));
    }

    public void ChangeResolution(DisplayResolution resolution)
    {
        var viewport = resolution switch
        {
            DisplayResolution.StandardDefinition => new Rectangle(0, 0, 640, 480),
            DisplayResolution.HighDefinition => new Rectangle(0, 0, 1280, 720),
            DisplayResolution.FullHighDefinition => new Rectangle(0, 0, 1920, 1080),
            DisplayResolution.UltaHighDefinition => new Rectangle(0, 0, 3840, 2160),
            _ => throw new NotSupportedException($"The specified {nameof(resolution)} is not supported."),
        };

        this.display.Width = viewport.Width;
        this.display.Height = viewport.Height;

        this.rasterizer.SetViewport(viewport);
    }
}
