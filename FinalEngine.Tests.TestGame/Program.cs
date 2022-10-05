// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.TestGame
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.ECS.Components.Core;
    using FinalEngine.ECS.Components.Rendering;
    using FinalEngine.ECS.Systems.Rendering;
    using FinalEngine.Extensions.Resources.Invocation;
    using FinalEngine.Extensions.Resources.Loaders;
    using FinalEngine.Input;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
    using FinalEngine.Launching;
    using FinalEngine.Launching.Invocation;
    using FinalEngine.Platform.Desktop.OpenTK;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Resources;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    internal class Program
    {
        private static void Main()
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

            var window = new OpenTKWindow(nativeWindow);
            var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
            var keyboard = new Keyboard(keyboardDevice);

            var mouseDevice = new OpenTKMouseDevice(nativeWindow);
            var mouse = new Mouse(mouseDevice);

            var file = new FileInvoker();
            var directory = new DirectoryInvoker();
            var fileSystem = new FileSystem(file, directory);

            var opengl = new OpenGLInvoker();
            var bindings = new GLFWBindingsContext();

            var renderContext = new OpenGLRenderContext(opengl, bindings, nativeWindow.Context);
            var renderDevice = new OpenGLRenderDevice(opengl);

            var image = new ImageInvoker();

            ResourceManager.Instance.RegisterLoader(new Texture2DResourceLoader(fileSystem, renderDevice.Factory, image));
            ResourceManager.Instance.RegisterLoader(new ShaderProgramResourceLoader(renderDevice.Factory, fileSystem));

            var watch = new Stopwatch();
            var watchInvoker = new StopwatchInvoker(watch);
            var gameTime = new GameTime(watchInvoker, 120.0d);

            var world = new EntityWorld();

            world.AddSystem(new MeshRenderEntitySystem(renderDevice));

            var entity = new Entity();

            MeshVertex[] vertices =
            {
                new MeshVertex()
                {
                    Position = new Vector3(-1.0f, -1.0f, 0.0f),
                    Color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                    TextureCoordinate = new Vector2(0.0f, 0.0f),
                },

                new MeshVertex()
                {
                    Position = new Vector3(1.0f, -1.0f, 0.0f),
                    Color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    TextureCoordinate = new Vector2(1.0f, 0.0f),
                },

                new MeshVertex()
                {
                    Position = new Vector3(0.0f, 1.0f, 0.0f),
                    Color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                    TextureCoordinate = new Vector2(0.5f, 1.0f),
                },
            };

            int[] indices =
            {
                0, 1, 2,
            };

            var mesh = new Mesh(renderDevice.Factory, vertices, indices);
            var material = new Material();

            entity.AddComponent(new TransformComponent());
            entity.AddComponent(new ModelComponent()
            {
                Mesh = mesh,
                Material = material,
            });

            world.AddEntity(entity);

            renderDevice.Initialize();

            bool isRunning = true;

            while (isRunning && !window.IsExiting)
            {
                if (!gameTime.CanProcessNextFrame())
                {
                    continue;
                }

                world.ProcessAll(GameLoopType.Update);

                keyboard.Update();
                mouse.Update();

                renderDevice.Clear(Color.Black);
                world.ProcessAll(GameLoopType.Render);

                renderContext.SwapBuffers();
                window.ProcessEvents();
            }

            mesh.Dispose();

            ResourceManager.Instance.Dispose();

            renderDevice.Dispose();
            window.Dispose();
            nativeWindow.Dispose();
        }
    }
}