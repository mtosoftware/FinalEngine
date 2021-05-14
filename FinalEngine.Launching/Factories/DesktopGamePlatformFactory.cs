// <copyright file="DesktopGamePlatformFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching.Factories
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Input.Mouse;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
    using FinalEngine.Platform;
    using FinalEngine.Platform.Desktop.OpenTK;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Invocation;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.Textures;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    [ExcludeFromCodeCoverage]
    public class DesktopGamePlatformFactory : IGamePlatformFactory
    {
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Handled by Game Container.")]
        public void InitializePlatform(
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
            out ITexture2DLoader textureLoader)
        {
            var settings = new NativeWindowSettings()
            {
                API = ContextAPI.OpenGL,
                APIVersion = new Version(4, 6),

                Flags = ContextFlags.ForwardCompatible,
                Profile = ContextProfile.Core,

                AutoLoadBindings = false,

                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,

                Size = new OpenTK.Mathematics.Vector2i(1280, 720),

                StartVisible = true,
            };

            var nativeWindow = new NativeWindowInvoker(settings);

            window = new OpenTKWindow(nativeWindow);
            eventsProcessor = (IEventsProcessor)window;

            var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
            keyboard = new Keyboard(keyboardDevice);

            var mouseDevice = new OpenTKMouseDevice(nativeWindow);
            mouse = new Mouse(mouseDevice);

            var file = new FileInvoker();
            var directory = new DirectoryInvoker();
            fileSystem = new FileSystem(file, directory);

            var opengl = new OpenGLInvoker();
            var bindings = new GLFWBindingsContext();

            renderContext = new OpenGLRenderContext(opengl, bindings, nativeWindow.Context);
            renderDevice = new OpenGLRenderDevice(opengl);

            var image = new ImageInvoker();
            textureLoader = new Texture2DLoader(fileSystem, renderDevice.Factory, image);
        }
    }
}