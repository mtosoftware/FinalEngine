// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Input.Mouse;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
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

    internal static class Program
    {
        private static void Main()
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

            var nativeWindow = new NativeWindowInvoker(settings)
            {
                CursorGrabbed = true,
            };

            var window = new OpenTKWindow(nativeWindow);

            var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
            var mouseDevice = new OpenTKMouseDevice(nativeWindow);

            var keyboard = new Keyboard(keyboardDevice);
            var mouse = new Mouse(mouseDevice);

            var file = new FileInvoker();
            var directory = new DirectoryInvoker();

            var fileSystem = new FileSystem(file, directory);

            var opengl = new OpenGLInvoker();
            var bindings = new GLFWBindingsContext();

            var renderContext = new OpenGLRenderContext(opengl, bindings, nativeWindow.Context);
            var renderDevice = new OpenGLRenderDevice(opengl);

            IInputAssembler inputAssembler = renderDevice.InputAssembler;
            IRasterizer rasterizer = renderDevice.Rasterizer;
            IOutputMerger outputMerger = renderDevice.OutputMerger;
            IPipeline pipeline = renderDevice.Pipeline;
            IGPUResourceFactory factory = renderDevice.Factory;

            var batcher = new SpriteBatcher(inputAssembler, 10000);
            var binder = new TextureBinder(pipeline);
            var drawer = new SpriteDrawer(renderDevice, batcher, binder, nativeWindow.ClientSize.X, nativeWindow.ClientSize.Y);

            var image = new ImageInvoker();
            var textureLoader = new Texture2DLoader(fileSystem, factory, image);

            ITexture2D texture = textureLoader.LoadTexture("Resources\\Textures\\default.png");
            ITexture2D texture2 = textureLoader.LoadTexture("Resources\\Textures\\wood.png");

            float rot = 0;
            float x = 0;
            float y = 0;
            const float speed = 4;

            while (!window.IsExiting)
            {
                if (keyboard.IsKeyReleased(Key.Escape))
                {
                    break;
                }

                rot += 0.01f;

                if (keyboard.IsKeyDown(Key.W))
                {
                    y -= speed;
                }
                else if (keyboard.IsKeyDown(Key.S))
                {
                    y += speed;
                }
                else if (keyboard.IsKeyDown(Key.A))
                {
                    x += speed;
                }
                else if (keyboard.IsKeyDown(Key.D))
                {
                    x -= speed;
                }

                keyboard.Update();
                mouse.Update();

                renderDevice.Clear(Color.Black);

                drawer.Transform = Matrix4x4.CreateTranslation(x, y, 0);

                drawer.Begin();

                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        ITexture2D tex = (j + i) % 2 == 0 ? texture : texture2;

                        drawer.Draw(tex, Color.White, new Vector2(32, 32), new Vector2(i * 64, j * 64), rot, new Vector2(64, 64));
                    }
                }

                drawer.End();

                renderContext.SwapBuffers();
                window.ProcessEvents();
            }

            drawer.Dispose();
            renderContext.Dispose();
            window.Dispose();
            nativeWindow.Dispose();
        }
    }
}