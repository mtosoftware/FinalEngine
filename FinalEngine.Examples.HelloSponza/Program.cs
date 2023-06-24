// <copyright file="Program.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.HelloSponza;

using System;
using System.Diagnostics;
using System.Drawing;
using FinalEngine.Extensions.Resources.Invocation;
using FinalEngine.Extensions.Resources.Loaders.Audio;
using FinalEngine.Extensions.Resources.Loaders.Textures;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.IO;
using FinalEngine.IO.Invocation;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Resources;
using FinalEngine.Runtime;
using FinalEngine.Runtime.Invocation;
using FinalEngine.Runtime.Rendering;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

internal static class Program
{
    internal static void Main()
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

            StartVisible = true,
        };

        var nativeWindow = new NativeWindowInvoker(settings);
        var window = new OpenTKWindow(nativeWindow);

        var file = new FileInvoker();
        var directory = new DirectoryInvoker();
        var fileSystem = new FileSystem(file, directory);

        var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
        var keyboard = new Keyboard(keyboardDevice);

        var mouseDevice = new OpenTKMouseDevice(nativeWindow);
        var mouse = new Mouse(mouseDevice);

        var opengl = new OpenGLInvoker();
        var bindings = new GLFWBindingsContext();

        var renderContext = new OpenGLRenderContext(opengl, bindings, nativeWindow.Context);
        var renderDevice = new OpenGLRenderDevice(opengl);

        var displayManager = new DisplayManager(renderDevice.Rasterizer, window);
        var resourceManager = ResourceManager.Instance;

        var watch = new Stopwatch();
        var watchInvoker = new StopwatchInvoker(watch);
        var gameTime = new GameTime(watchInvoker, 120.0d);

        resourceManager.RegisterLoader(new SoundResourceLoader(fileSystem));
        resourceManager.RegisterLoader(new Texture2DResourceLoader(fileSystem, renderDevice.Factory, new ImageInvoker()));

        displayManager.ChangeResolution(DisplayResolution.HighDefinition);

        while (!window.IsExiting)
        {
            if (!gameTime.CanProcessNextFrame())
            {
                continue;
            }

            keyboard.Update();
            mouse.Update();

            renderDevice.Clear(Color.CornflowerBlue);

            renderContext.SwapBuffers();
            window.ProcessEvents();
        }

        resourceManager.Dispose();
        renderContext.Dispose();

        mouse.Dispose();
        keyboard.Dispose();

        window.Dispose();
        nativeWindow.Dispose();
    }
}
