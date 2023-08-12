// <copyright file="Program.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.Game;

using System;
using System.Drawing;
using System.IO.Abstractions;
using System.Numerics;
using FinalEngine.Extensions.Resources.Loaders.Audio;
using FinalEngine.Extensions.Resources.Loaders.Shaders;
using FinalEngine.Extensions.Resources.Loaders.Textures;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Maths;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;
using FinalEngine.Runtime;
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

        var fileSystem = new FileSystem();

        var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
        var keyboard = new Keyboard(keyboardDevice);

        var mouseDevice = new OpenTKMouseDevice(nativeWindow);
        var mouse = new Mouse(mouseDevice);

        var bindings = new GLFWBindingsContext();
        var renderContext = new OpenGLRenderContext(bindings, nativeWindow.Context);
        var renderDevice = new OpenGLRenderDevice();

        var displayManager = new DisplayManager(renderDevice.Rasterizer, window);
        var resourceManager = ResourceManager.Instance;

        var gameTime = new GameTime(120.0d);

        resourceManager.RegisterLoader(new SoundResourceLoader(fileSystem));
        resourceManager.RegisterLoader(new ShaderResourceLoader(fileSystem, renderDevice.Factory));
        resourceManager.RegisterLoader(new Texture2DResourceLoader(fileSystem, renderDevice.Factory));

        displayManager.ChangeResolution(DisplayResolution.HighDefinition);

        var vertexShader = resourceManager.LoadResource<IShader>("Resources\\Shaders\\forward-geometry.vert");
        var fragmentShader = resourceManager.LoadResource<IShader>("Resources\\Shaders\\forward-geometry.frag");
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

        var batcher = new SpriteBatcher(renderDevice.InputAssembler);
        var binder = new TextureBinder(renderDevice.Pipeline);
        var drawer = new SpriteDrawer(renderDevice, batcher, binder, window.ClientSize.Width, window.ClientSize.Height);

        while (!window.IsExiting)
        {
            if (!gameTime.CanProcessNextFrame())
            {
                continue;
            }

            keyboard.Update();
            mouse.Update();

            renderDevice.Clear(Color.CornflowerBlue);

            drawer.Begin();

            drawer.Draw(
                material.DiffuseTexture,
                Color.Red,
                Vector2.Zero,
                Vector2.Zero,
                0,
                new Vector2(1, 1));

            drawer.End();

            renderDevice.Pipeline.SetShaderProgram(shaderProgram);

            material.Bind(renderDevice.Pipeline);
            mesh.Bind(renderDevice.InputAssembler);
            mesh.Draw(renderDevice);

            renderContext.SwapBuffers();
            window.ProcessEvents();
        }

        drawer.Dispose();
        mesh.Dispose();
        shaderProgram.Dispose();

        ResourceManager.Instance.UnloadResource(vertexShader);
        ResourceManager.Instance.UnloadResource(fragmentShader);

        resourceManager.Dispose();
        renderContext.Dispose();
        mouse.Dispose();
        keyboard.Dispose();
        window.Dispose();
        nativeWindow.Dispose();
    }
}
