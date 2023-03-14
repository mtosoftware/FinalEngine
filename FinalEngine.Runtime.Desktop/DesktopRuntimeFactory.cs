// <copyright file="DesktopRuntimeFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Desktop;

using System;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.IO;
using FinalEngine.IO.Invocation;
using FinalEngine.Platform;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class DesktopRuntimeFactory : IRuntimeFactory
{
    public void InitializeRuntime(
        out IWindow window,
        out IEventsProcessor eventsProcessor,
        out IKeyboardDevice keyboardDevice,
        out IMouseDevice mouseDevice,
        out IFileSystem fileSystem,
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
        var file = new FileInvoker();
        var directory = new DirectoryInvoker();
        var opengl = new OpenGLInvoker();
        var bindings = new GLFWBindingsContext();

        window = new OpenTKWindow(nativeWindow);
        eventsProcessor = (IEventsProcessor)window;
        fileSystem = new FileSystem(file, directory);
        keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
        mouseDevice = new OpenTKMouseDevice(nativeWindow);
        renderContext = new OpenGLRenderContext(opengl, bindings, nativeWindow.Context);
        renderDevice = new OpenGLRenderDevice(opengl);
    }
}
