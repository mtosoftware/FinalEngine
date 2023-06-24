// <copyright file="IRuntimeFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.IO;
using FinalEngine.Platform;
using FinalEngine.Rendering;

/// <summary>
/// Defines an interface that provides a function to initialize the engine runtime.
/// </summary>
public interface IRuntimeFactory
{
    /// <summary>
    /// Initializes the runtime.
    /// </summary>
    /// <param name="window">
    /// The window.
    /// </param>
    /// <param name="eventsProcessor">
    /// The events processor.
    /// </param>
    /// <param name="keyboardDevice">
    /// The keyboard device.
    /// </param>
    /// <param name="mouseDevice">
    /// The mouse device.
    /// </param>
    /// <param name="fileSystem">
    /// The file system.
    /// </param>
    /// <param name="renderContext">
    /// The render context.
    /// </param>
    /// <param name="renderDevice">
    /// The render device.
    /// </param>
    void InitializeRuntime(
        out IWindow window,
        out IEventsProcessor eventsProcessor,
        out IKeyboardDevice keyboardDevice,
        out IMouseDevice mouseDevice,
        out IFileSystem fileSystem,
        out IRenderContext renderContext,
        out IRenderDevice renderDevice);
}
