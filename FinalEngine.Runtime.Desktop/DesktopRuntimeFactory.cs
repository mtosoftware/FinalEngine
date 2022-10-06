// <copyright file="DesktopRuntimeFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Desktop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Extensions.Resources.Invocation;
    using FinalEngine.Extensions.Resources.Loaders;
    using FinalEngine.Input;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
    using FinalEngine.Platform;
    using FinalEngine.Platform.Desktop.OpenTK;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Resources;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    public class DesktopRuntimeFactory : IRuntimeFactory
    {
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Invocation Class")]
        public void InitializeRuntime(
            int width,
            int height,
            string title,
            out IWindow window,
            out IEventsProcessor eventsProcessor,
            out IFileSystem fileSystem,
            out IKeyboard keyboard,
            out IMouse mouse,
            out IRenderContext renderContext,
            out IRenderDevice renderDevice)
        {
            var settings = new NativeWindowSettings()
            {
                API = ContextAPI.OpenGL,
                APIVersion = new Version(4, 5),

                Flags = ContextFlags.ForwardCompatible,
                Profile = ContextProfile.Core,

                AutoLoadBindings = false,

                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,

                Size = new OpenTK.Mathematics.Vector2i(1280, 720),

                StartVisible = true,
            };

            var nativeWindow = new NativeWindowInvoker(settings);

            window = new OpenTKWindow(nativeWindow)
            {
                Title = title,
            };

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

            ResourceManager.Instance.RegisterLoader(new Texture2DResourceLoader(fileSystem, renderDevice.Factory, new ImageInvoker()));
            ResourceManager.Instance.RegisterLoader(new ShaderResourceLoader(renderDevice.Factory, fileSystem));
        }
    }
}