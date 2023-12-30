using System;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Numerics;
using FinalEngine.Examples.Sponza;
using FinalEngine.Examples.Sponza.Loaders;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Maths;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Textures;
using FinalEngine.Rendering.Vapor;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Geometry;
using FinalEngine.Rendering.Vapor.Lighting;
using FinalEngine.Rendering.Vapor.Loaders.Shaders;
using FinalEngine.Rendering.Vapor.Loaders.Textures;
using FinalEngine.Rendering.Vapor.Primitives;
using FinalEngine.Rendering.Vapor.Renderers;
using FinalEngine.Resources;
using FinalEngine.Runtime;
using FinalEngine.Runtime.Invocation;
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
            APIVersion = new Version(4, 6),

            Flags = ContextFlags.ForwardCompatible,
            Profile = ContextProfile.Core,

            AutoLoadBindings = false,

            WindowBorder = WindowBorder.Fixed,
            WindowState = WindowState.Normal,

            Size = new OpenTK.Mathematics.Vector2i(1280, 720),

            StartVisible = true,

            NumberOfSamples = 8,
        };

        var nativeWindow = new NativeWindowInvoker(settings);

        var window = new OpenTKWindow(nativeWindow)
        {
            Title = "Final Engine - Vapor",
        };

        var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
        var keyboard = new Keyboard(keyboardDevice);

        var mouseDevice = new OpenTKMouseDevice(nativeWindow);
        var mouse = new Mouse(mouseDevice);

        var fileSystem = new FileSystem();

        var opengl = new OpenGLInvoker();
        var bindings = new GLFWBindingsContext();

        var renderContext = new OpenGLRenderContext(opengl, bindings, nativeWindow.Context);
        var renderDevice = new OpenGLRenderDevice(opengl);
        var renderPipeline = new OpenGLRenderPipeline();

        renderPipeline.Initialize();

        ResourceManager.Instance.RegisterLoader(new ShaderResourceLoader(fileSystem, renderDevice));
        ResourceManager.Instance.RegisterLoader(new Texture2DResourceLoader(fileSystem, renderDevice.Factory));
        ResourceManager.Instance.RegisterLoader(new ShaderProgramResourceLoader(ResourceManager.Instance, renderDevice, fileSystem));
        ResourceManager.Instance.RegisterLoader(new ModelResourceLoader(renderDevice, fileSystem));

        renderDevice.Pipeline.AddShaderHeader("lighting", fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\lighting.glsl"));
        renderDevice.Pipeline.AddShaderHeader("material", fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\material.glsl"));

        var watch = new Stopwatch();
        var watchInvoker = new StopwatchInvoker(watch);
        var gameTime = new GameTime(watchInvoker, 120.0f);

        float fieldDepth = 10.0f;
        float fieldWidth = 10.0f;

        MeshVertex[] vertices = [
            new MeshVertex()
            {
                Position = new Vector3(-fieldWidth, 0.0f, -fieldDepth),
                TextureCoordinate = new Vector2(0.0f),
            },

            new MeshVertex()
            {
                Position = new Vector3(-fieldWidth, 0.0f, fieldDepth * 3),
                TextureCoordinate = new Vector2(0.0f, 1.0f),
            },

            new MeshVertex()
            {
                Position = new Vector3(fieldWidth * 3, 0.0f, -fieldDepth),
                TextureCoordinate = new Vector2(1.0f, 0.0f),
            },

            new MeshVertex()
            {
                Position = new Vector3(fieldWidth * 3, 0.0f, fieldDepth * 3),
                TextureCoordinate = new Vector2(1.0f, 1.0f),
            },
        ];

        int[] indices = [
            0,
            1,
            2,
            2,
            1,
            3
        ];

        var mesh = new Mesh(renderDevice.Factory, vertices, indices, true);

        var material = new Material()
        {
            Shininess = 16.0f,
        };

        bool isRunning = true;

        var camera = new Camera(window.ClientSize.Width, window.ClientSize.Height);

        var geometryRenderer = new GeometryRenderer(renderDevice);
        var lightRenderer = new LightRenderer(renderDevice.Pipeline);
        var renderingEngine = new RenderingEngine(renderDevice, geometryRenderer, lightRenderer);

        var light1 = new Light()
        {
            Color = new Vector3(1.0f, 0.0f, 0.0f),
        };

        var light2 = new Light()
        {
            Color = new Vector3(0.0f, 1.0f, 0.0f),
        };

        var light3 = new Light()
        {
            Color = new Vector3(0.0f, 0.0f, 1.0f),
        };

        var light4 = new Light()
        {
            Color = new Vector3(1.0f, 0.0f, 1.0f),
        };

        float move = 0.0f;

        var modelResource = ResourceManager.Instance.LoadResource<ModelResource>("Resources\\Models\\Dabrovic\\Sponza.obj");

        var colorTarget = renderDevice.Factory.CreateTexture2D<byte>(
            new Texture2DDescription()
            {
                GenerateMipmaps = false,
                Height = window.ClientSize.Height,
                Width = window.ClientSize.Width,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Linear,
                PixelType = PixelType.Byte,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Clamp,
            },
            null);

        var renderTarget = renderDevice.Factory.CreateFrameBuffer(
            new[] { colorTarget });

        while (isRunning)
        {
            if (!gameTime.CanProcessNextFrame())
            {
                continue;
            }

            window.Title = $"{GameTime.FrameRate}";

            camera.Update(renderDevice.Pipeline, keyboard, mouse);

            keyboard.Update();
            mouse.Update();

            //renderDevice.Pipeline.SetFrameBuffer(renderTarget);

            foreach (var model in modelResource.Models)
            {
                renderingEngine.Enqueue(model, new Transform()
                {
                    Scale = new Vector3(5),
                });
            }

            //renderingEngine.Enqueue(new Model()
            //{
            //    Mesh = mesh,
            //    Material = material,
            //}, new Transform()
            //{
            //    Scale = new Vector3(20, 20, 20),
            //});

            renderingEngine.Enqueue(new Light()
            {
                Type = LightType.Spot,
                Color = new Vector3(1.0f),
                Intensity = 0.8f,
                Position = camera.Transform.Position,
                Direction = camera.Transform.Forward,
                Attenuation = new Attenuation()
                {
                    Linear = 0.014f,
                    Quadratic = 0.0007f,
                }
            });

            move += 0.4f;

            float cos = MathF.Cos(MathHelper.DegreesToRadians(move));

            light1.Position = new Vector3(cos * 40, 10, -cos * 40);
            light2.Position = new Vector3(-cos * 40, 10, cos * 40);
            light3.Position = new Vector3(-cos * 40, 10, -cos * 40);
            light4.Position = new Vector3(cos * 40, 10, cos * 40);

            renderingEngine.Enqueue(light1);
            renderingEngine.Enqueue(light2);
            renderingEngine.Enqueue(light3);
            renderingEngine.Enqueue(light4);

            renderingEngine.Render(camera);

            //renderDevice.Pipeline.SetFrameBuffer(null);

            //renderingEngine.Enqueue(new Model()
            //{
            //    Mesh = mesh,
            //    Material = new Material()
            //    {
            //        DiffuseTexture = renderTarget.ColorTargets.FirstOrDefault(),
            //    },
            //}, new Transform());

            //renderingEngine.Render(camera);

            renderContext.SwapBuffers();
            window.ProcessEvents();
        }

        ResourceManager.Instance.Dispose();

        renderPipeline.Dispose();
        mouse.Dispose();
        keyboard.Dispose();
        window.Dispose();
        nativeWindow.Dispose();
    }
}
