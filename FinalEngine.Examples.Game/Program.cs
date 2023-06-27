// <copyright file="Program.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.Game;

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using FinalEngine.Extensions.Resources.Invocation;
using FinalEngine.Extensions.Resources.Loaders.Audio;
using FinalEngine.Extensions.Resources.Loaders.Textures;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.IO;
using FinalEngine.IO.Invocation;
using FinalEngine.Maths;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;
using FinalEngine.Runtime;
using FinalEngine.Runtime.Invocation;
using FinalEngine.Runtime.Rendering;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

/// <summary>
/// The test game application.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
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

        var vertexShader = LoadShader(renderDevice.Factory, PipelineTarget.Vertex, "Resources\\Shaders\\forward-geometry.vert");
        var fragmentShader = LoadShader(renderDevice.Factory, PipelineTarget.Fragment, "Resources\\Shaders\\forward-geometry.frag");
        var shaderProgram = renderDevice.Factory.CreateShaderProgram(new[] { vertexShader, fragmentShader });

        renderDevice.Pipeline.SetShaderProgram(shaderProgram);

        renderDevice.Pipeline.SetUniform(
            "u_projection",
            Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(70.0f), 1280.0f / 720.0f, 0.1f, 1000.0f));

        renderDevice.Pipeline.SetUniform(
            "u_view",
            Matrix4x4.CreateLookAt(new Vector3(0, 0, -2), Vector3.Zero, Vector3.UnitY));

        renderDevice.Pipeline.SetUniform(
            "u_transform",
            Matrix4x4.CreateTranslation(Vector3.Zero));

        renderDevice.Pipeline.SetUniform("u_material.diffuseTexture", 0);

        MeshVertex[] vertices =
        {
            new MeshVertex()
            {
                Position = new Vector3(-1, -1, 0),
                Color = new Vector4(1, 0, 0, 1),
                TextureCoordinate = new Vector2(0, 0),
            },

            new MeshVertex()
            {
                Position = new Vector3(1, -1, 0),
                Color = new Vector4(0, 1, 0, 1),
                TextureCoordinate = new Vector2(1, 0),
            },

            new MeshVertex()
            {
                Position = new Vector3(0, 1, 0),
                Color = new Vector4(0, 0, 1, 1),
                TextureCoordinate = new Vector2(0.5f, 1),
            },
        };

        int[] indices =
        {
            0, 1, 2,
        };

        var mesh = new Mesh(renderDevice.Factory, vertices, indices);
        var material = new Material();

        while (!window.IsExiting)
        {
            if (!gameTime.CanProcessNextFrame())
            {
                continue;
            }

            keyboard.Update();
            mouse.Update();

            renderDevice.Clear(Color.CornflowerBlue);

            material.Bind(renderDevice.Pipeline);
            mesh.Bind(renderDevice.InputAssembler);
            mesh.Draw(renderDevice);

            renderContext.SwapBuffers();
            window.ProcessEvents();
        }

        mesh.Dispose();
        shaderProgram.Dispose();
        fragmentShader.Dispose();
        vertexShader.Dispose();
        resourceManager.Dispose();
        renderContext.Dispose();
        mouse.Dispose();
        keyboard.Dispose();
        window.Dispose();
        nativeWindow.Dispose();
    }

    /// <summary>
    /// Loads a shader from the specified <paramref name="filePath"/>.
    /// </summary>
    /// <param name="factory">
    /// The GPU factory.
    /// </param>
    /// <param name="target">
    /// The pipeline target for the shader.
    /// </param>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <returns>
    /// The newly created <see cref="IShader"/> loaded into memory.
    /// </returns>
    private static IShader LoadShader(IGPUResourceFactory factory, PipelineTarget target, string filePath)
    {
        return factory.CreateShader(target, File.ReadAllText(filePath));
    }
}
