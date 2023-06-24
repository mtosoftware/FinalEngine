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

/// <summary>
/// Provides a desktop implementation of an <see cref="IRuntimeFactory"/>.
/// </summary>
/// <seealso cref="IRuntimeFactory" />
public class DesktopRuntimeFactory : IRuntimeFactory
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

#pragma warning disable CA2000 // Dispose objects before losing scope
        var nativeWindow = new NativeWindowInvoker(settings);
#pragma warning restore CA2000 // Dispose objects before losing scope

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
