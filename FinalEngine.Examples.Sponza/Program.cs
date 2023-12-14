using System;
using System.Diagnostics;
using System.Drawing;
using System.IO.Abstractions;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.Examples.Sponza.Entities;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Vapor;
using FinalEngine.Rendering.Vapor.Components.Geometry;
using FinalEngine.Rendering.Vapor.Geometry;
using FinalEngine.Rendering.Vapor.Loaders.Shaders;
using FinalEngine.Rendering.Vapor.Loaders.Textures;
using FinalEngine.Rendering.Vapor.Primitives;
using FinalEngine.Rendering.Vapor.Systems.Cameras;
using FinalEngine.Rendering.Vapor.Systems.Geometry;
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

            NumberOfSamples = 16,
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

        ResourceManager.Instance.RegisterLoader(new ShaderResourceLoader(fileSystem, renderDevice.Factory));
        ResourceManager.Instance.RegisterLoader(new Texture2DResourceLoader(fileSystem, renderDevice.Factory));

        var watch = new Stopwatch();
        var watchInvoker = new StopwatchInvoker(watch);
        var gameTime = new GameTime(watchInvoker, 120.0d);

        float fieldDepth = 10.0f;
        float fieldWidth = 10.0f;

        MeshVertex[] vertices = [
            new MeshVertex()
            {
                Position = new Vector3(-fieldWidth, 0.0f, -fieldDepth),
                Color = Vector4.One,
                TextureCoordinate = new Vector2(0.0f),
            },

            new MeshVertex()
            {
                Position = new Vector3(-fieldWidth, 0.0f, fieldDepth * 3),
                Color = Vector4.One,
                TextureCoordinate = new Vector2(0.0f, 1.0f),
            },

            new MeshVertex()
            {
                Position = new Vector3(fieldWidth * 3, 0.0f, -fieldDepth),
                Color = Vector4.One,
                TextureCoordinate = new Vector2(1.0f, 0.0f),
            },

            new MeshVertex()
            {
                Position = new Vector3(fieldWidth * 3, 0.0f, fieldDepth * 3),
                Color = Vector4.One,
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

        var vertexShader = renderDevice.Factory.CreateShader(PipelineTarget.Vertex, fileSystem.File.ReadAllText("Resources\\Shaders\\forward-geometry.vert"));
        var fragmentShader = renderDevice.Factory.CreateShader(PipelineTarget.Fragment, fileSystem.File.ReadAllText("Resources\\Shaders\\forward-geometry.frag"));
        var shaderProgram = renderDevice.Factory.CreateShaderProgram(new[] { vertexShader, fragmentShader });

        renderDevice.Pipeline.SetShaderProgram(shaderProgram);

        var mesh = new Mesh(renderDevice.Factory, vertices, indices);
        var material = new Material();

        var world = new EntityWorld();

        var entityFactory = new CameraEntityFactory(window.ClientSize.Width, window.ClientSize.Height);
        dynamic camera = entityFactory.CreateEntity();
        world.AddEntity(camera);

        var cameraSystem = new FlyCameraEntitySystem(keyboard, mouse);
        world.AddSystem(cameraSystem);

        var geometryRenderSystem = new MeshRenderEntitySystem(renderDevice);
        world.AddSystem(geometryRenderSystem);

        var renderingEngine = new RenderingEngine(renderDevice, geometryRenderSystem);

        var entity = new Entity();
        entity.AddComponent(new TransformComponent());
        entity.AddComponent(new ModelComponent()
        {
            Mesh = mesh,
            Material = material,
        });

        world.AddEntity(entity);
        world.RemoveSystem(typeof(MeshRenderEntitySystem));

        bool isRunning = true;

        while (isRunning)
        {
            if (!gameTime.CanProcessNextFrame())
            {
                continue;
            }

            window.Title = $"{GameTime.FrameRate}";

            cameraSystem.Process();

            keyboard.Update();
            mouse.Update();

            renderDevice.Clear(Color.CornflowerBlue);

            //// TODO: Multiple cameras
            renderDevice.Pipeline.SetUniform("u_projection", camera.Perspective.CreateProjectionMatrix());
            renderDevice.Pipeline.SetUniform("u_view", camera.Transform.CreateViewMatrix(Vector3.UnitY));
            renderDevice.Pipeline.SetUniform("u_transform", Matrix4x4.Identity);

            renderingEngine.Render();
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
