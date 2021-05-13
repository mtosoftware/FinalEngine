// <copyright file="IGamePlatformFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Input.Mouse;
    using FinalEngine.IO;
    using FinalEngine.Platform;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;

    public interface IGamePlatformFactory
    {
        void InitializePlatform(
            int width,
            int height,
            string title,
            out IWindow window,
            out IEventsProcessor eventsProcessor,
            out IFileSystem fileSystem,
            out IKeyboard keyboard,
            out IMouse mouse,
            out IRenderContext renderContext,
            out IRenderDevice renderDevice,
            out ITexture2DLoader textureLoader);
    }
}