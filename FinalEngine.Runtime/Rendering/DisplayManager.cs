// <copyright file="DisplayManager.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Rendering;

using System;
using System.Drawing;
using FinalEngine.Platform;
using FinalEngine.Rendering;

/// <summary>
/// Provides a standard implementation of an <see cref="IDisplayManager"/>.
/// </summary>
/// <seealso cref="IDisplayManager" />
public class DisplayManager : IDisplayManager
{
    /// <summary>
    /// The rasterizer.
    /// </summary>
    private readonly IRasterizer rasterizer;

    /// <summary>
    /// The window.
    /// </summary>
    private readonly IWindow window;

    /// <summary>
    /// Initializes a new instance of the <see cref="DisplayManager"/> class.
    /// </summary>
    /// <param name="rasterizer">
    /// The rasterizer.
    /// </param>
    /// <param name="window">
    /// The window.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="rasterizer"/> or <paramref name="window"/> parameter cannot be null.
    /// </exception>
    public DisplayManager(IRasterizer rasterizer, IWindow window)
    {
        this.rasterizer = rasterizer ?? throw new ArgumentNullException(nameof(rasterizer));
        this.window = window ?? throw new ArgumentNullException(nameof(window));
    }

    /// <summary>
    /// Gets the display height.
    /// </summary>
    /// <value>
    /// The display height.
    /// </value>
    public int DisplayHeight
    {
        get { return this.rasterizer.GetViewport().Height; }
    }

    /// <summary>
    /// Gets the display width.
    /// </summary>
    /// <value>
    /// The display width.
    /// </value>
    public int DisplayWidth
    {
        get { return this.rasterizer.GetViewport().Width; }
    }

    /// <summary>
    /// Changes the display resolution to the specified <paramref name="resolution" />.
    /// </summary>
    /// <param name="resolution">
    /// The desired resolution.
    /// </param>
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

        this.rasterizer.SetViewport(viewport);
    }
}
