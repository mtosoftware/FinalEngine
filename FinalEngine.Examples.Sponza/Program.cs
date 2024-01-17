using System;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Numerics;
using FinalEngine.Examples.Sponza;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Batching;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.Loaders.Models;
using FinalEngine.Rendering.Loaders.Shaders;
using FinalEngine.Rendering.Loaders.Textures;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Primitives;
using FinalEngine.Rendering.Renderers;
using FinalEngine.Rendering.Textures;
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
        ResourceManager.Instance.RegisterLoader(new Texture2DResourceLoader(fileSystem, renderDevice));
        ResourceManager.Instance.RegisterLoader(new ShaderProgramResourceLoader(ResourceManager.Instance, renderDevice, fileSystem));
        ResourceManager.Instance.RegisterLoader(new ModelResourceLoader(fileSystem, renderDevice));

        var watch = new Stopwatch();
        var watchInvoker = new StopwatchInvoker(watch);
        var gameTime = new GameTime(watchInvoker, 10000.0f);

        float fieldDepth = 10.0f;
        float fieldWidth = 10.0f;

        MeshVertex[] vertices = [
            new MeshVertex()
            {
                Position = new Vector3(-fieldWidth, 0.0f, -fieldDepth),
                TextureCoordinate = new Vector2(0.0f),
            },

            new MeshVertex() { Position = new Vector3(-fieldWidth, 0.0f, fieldDepth * 3), TextureCoordinate = new Vector2(0.0f, 1.0f), },

            new MeshVertex() { Position = new Vector3(fieldWidth * 3, 0.0f, -fieldDepth), TextureCoordinate = new Vector2(1.0f, 0.0f), },

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

        MeshVertex.CalculateNormals(vertices, indices);
        MeshVertex.CalculateTangents(vertices, indices);

        var mesh = new Mesh<MeshVertex>(
            renderDevice.Factory,
            vertices,
            indices,
            MeshVertex.InputElements,
            MeshVertex.SizeInBytes);

        var material = new Material()
        {
            DiffuseTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\Bricks\\bricks_diffuse.tga"),
            SpecularTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\Bricks\\bricks_specular.tga"),
            NormalTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\Bricks\\bricks_normal.tga"),
            Shininess = 16.0f,
        };

        var model = ResourceManager.Instance.LoadResource<Model>("Resources\\Models\\Sponza\\sponza.obj");

        // Currently there is no parent-child relationship with transform, so we have to do this to scale and translate.
        if (model.RenderModel != null)
        {
            model.RenderModel.Transform.Scale = new Vector3(0.2f);
        }

        foreach (var child in model.Children)
        {
            if (child.RenderModel != null)
            {
                child.RenderModel.Transform.Scale = new Vector3(0.2f);
            }
        }

        bool isRunning = true;

        var camera = new Camera(window.ClientSize.Width, window.ClientSize.Height);

        var geometryRenderer = new GeometryRenderer(renderDevice);
        var lightRenderer = new LightRenderer(renderDevice);
        var skyboxRenderer = new SkyboxRenderer(renderDevice);
        var sceneRenderer = new SceneRenderer(renderDevice, geometryRenderer);

        var renderCoordinator = new RenderCoordinator(geometryRenderer, lightRenderer);

        var renderingEngine = new RenderingEngine(
            fileSystem,
            renderDevice,
            lightRenderer,
            skyboxRenderer,
            sceneRenderer,
            renderCoordinator);

        var right = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\Skybox\\default_right.png");
        var left = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\Skybox\\default_left.png");
        var top = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\Skybox\\default_top.png");
        var bottom = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\Skybox\\default_bottom.png");
        var front = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\Skybox\\default_front.png");
        var back = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\Skybox\\default_back.png");

        var cubeTexture = renderDevice.Factory.CreateCubeTexture(
            new TextureCubeDescription()
            {
                Width = right.Description.Width,
                Height = right.Description.Height,
                WrapR = TextureWrapMode.Clamp,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Clamp,
            },
            right,
            left,
            top,
            bottom,
            back,
            front);

        skyboxRenderer.SetSkybox(cubeTexture);

        var controller = new ImGuiController(window.ClientSize.Width, window.ClientSize.Height);

        var dirLight = new Light()
        {
            Type = LightType.Directional,
            Intensity = 0.4f,
            Color = new Vector3(0.4f),
            Direction = new Vector3(-1),
        };

        var binder = new TextureBinder(renderDevice.Pipeline);
        var batcher = new SpriteBatcher(renderDevice.InputAssembler);
        var drawer = new SpriteDrawer(renderDevice, batcher, binder, window.ClientSize.Width, window.ClientSize.Height);

        while (isRunning)
        {
            if (!gameTime.CanProcessNextFrame())
            {
                continue;
            }

            window.Title = $"{GameTime.FrameRate}";

            camera.Update(renderDevice.Pipeline, keyboard, mouse);

            controller.Update(keyboard, mouse, GameTime.Delta);

            keyboard.Update();
            mouse.Update();

            geometryRenderer.Enqueue(model);

            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    lightRenderer.Enqueue(new Light()
                    {
                        Type = LightType.Point,
                        Position = new Vector3((i * 20) - 100, 4, (j * 20) - 50),
                    });
                }
            }

            renderingEngine.Render(camera);

            controller.Render();

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
