// <copyright file="IRuntimeFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.IO;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Resources;
using FinalEngine.Runtime.Rendering;

public interface IRuntimeFactory
{
    void InitializeRuntime(
        out IWindow window,
        out IEventsProcessor eventsProcessor,
        out IKeyboardDevice keyboardDevice,
        out IMouseDevice mouseDevice,
        out IFileSystem fileSystem,
        out IRenderContext renderContext,
        out IRenderDevice renderDevice,
        out IDisplayManager displayManager,
        out IResourceManager resourceManager);
}
