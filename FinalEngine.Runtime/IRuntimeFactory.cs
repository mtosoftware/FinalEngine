// <copyright file="IRuntimeFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime
{
    using FinalEngine.Input.Keyboards;
    using FinalEngine.Input.Mouses;
    using FinalEngine.IO;
    using FinalEngine.Platform;
    using FinalEngine.Rendering;

    public interface IRuntimeFactory
    {
        void InitializeRuntime(
            int width,
            int height,
            string title,
            out IWindow window,
            out IEventsProcessor eventsProcessor,
            out IFileSystem fileSystem,
            out IKeyboard keyboard,
            out IMouse mouse,
            out IRenderContext renderContext,
            out IRenderDevice renderDevice);
    }
}
