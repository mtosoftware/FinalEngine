// <copyright file="IDisplayManager.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Rendering;

/// <summary>
/// Enumerates the available display resolutions for the client.
/// </summary>
public enum DisplayResolution
{
    /// <summary>
    /// Standard Definition (SD - 640 x 480).
    /// </summary>
    StandardDefinition,

    /// <summary>
    /// High Definition (HD - 1280 x 720).
    /// </summary>
    HighDefinition,

    /// <summary>
    /// Full High Definition (FHD - 1920 x 1080).
    /// </summary>
    FullHighDefinition,

    /// <summary>
    /// Ultra High Definition (UHD - 3840 x 2160).
    /// </summary>
    UltaHighDefinition,
}

/// <summary>
/// Defines an interface that provides methods to handle the display resolution.
/// </summary>
public interface IDisplayManager
{
    /// <summary>
    /// Changes the display resolution to the specified <paramref name="resolution"/>.
    /// </summary>
    /// <param name="resolution">
    /// The desired resolution.
    /// </param>
    void ChangeResolution(DisplayResolution resolution);
}
