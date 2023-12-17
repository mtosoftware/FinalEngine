using System;
using System.Diagnostics;
using System.Drawing;
using System.IO.Abstractions;
using System.Numerics;
using FinalEngine.Examples.Sponza;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using FinalEngine.Rendering.Vapor;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Geometry;
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

        renderDevice.Pipeline.AddShaderHeader("lighting", fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\lighting.glsl"));
        renderDevice.Pipeline.AddShaderHeader("material", fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\material.glsl"));

        var watch = new Stopwatch();
        var watchInvoker = new StopwatchInvoker(watch);
        var gameTime = new GameTime(watchInvoker, 120.0d);

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

        var vertexShader = ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\Lighting\\lighting-main.vert");
        var dirFragmentShader = ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\Lighting\\lighting-directional.frag");
        var pointFragmentShader = ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\Lighting\\lighting-point.frag");
        var spotFragmentShader = ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\Lighting\\lighting-spot.frag");
        var ambientFragmentSahder = ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\Lighting\\lighting-ambient.frag");

        var dirShaderProgram = renderDevice.Factory.CreateShaderProgram(new[] { vertexShader, dirFragmentShader });
        var pointShaderProgram = renderDevice.Factory.CreateShaderProgram(new[] { vertexShader, pointFragmentShader });
        var spotShaderProgram = renderDevice.Factory.CreateShaderProgram(new[] { vertexShader, spotFragmentShader });
        var ambientShaderProgram = renderDevice.Factory.CreateShaderProgram(new[] { vertexShader, ambientFragmentSahder });

        var mesh = new Mesh(renderDevice.Factory, vertices, indices, true);
        var material = new Material()
        {
            NormalTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\bricks_normal.jpg"),
            Shininess = 16.0f,
        };

        bool isRunning = true;

        var camera = new Camera(window.ClientSize.Width, window.ClientSize.Height);

        var geometryRenderer = new GeometryRenderer(renderDevice);
        var renderingEngine = new RenderingEngine(geometryRenderer);

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

            renderDevice.Clear(Color.Black);

            renderDevice.Pipeline.SetShaderProgram(ambientShaderProgram);

            renderDevice.Pipeline.SetUniform("u_projection", camera.Projection);
            renderDevice.Pipeline.SetUniform("u_view", camera.View);
            renderDevice.Pipeline.SetUniform("u_viewPosition", camera.Transform.Position);

            renderDevice.Pipeline.SetUniform("u_light.base.intensity", 1.0f);
            renderDevice.Pipeline.SetUniform("u_light.base.color", Vector3.One);

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    renderingEngine.Enqueue(new Model()
                    {
                        Mesh = mesh,
                        Material = material,
                    },
                    new Transform()
                    {
                        Position = new Vector3(i * 40, 0, j * 40),
                    });
                }
            }

            renderingEngine.Render(camera);

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
