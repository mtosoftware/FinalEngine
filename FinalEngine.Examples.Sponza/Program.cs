using System;
using System.Diagnostics;
using System.Drawing;
using System.IO.Abstractions;
using System.Numerics;
using FinalEngine.Examples.Sponza;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Maths;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Geometry;
using FinalEngine.Rendering.Vapor.Loaders.Shaders;
using FinalEngine.Rendering.Vapor.Loaders.Textures;
using FinalEngine.Rendering.Vapor.Primitives;
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

        var vertexShader = renderDevice.Factory.CreateShader(PipelineTarget.Vertex, fileSystem.File.ReadAllText("Resources\\Shaders\\Lighting\\lighting-directional.vert"));
        var fragmentShader = renderDevice.Factory.CreateShader(PipelineTarget.Fragment, fileSystem.File.ReadAllText("Resources\\Shaders\\Lighting\\lighting-directional.frag"));
        var shaderProgram = renderDevice.Factory.CreateShaderProgram(new[] { vertexShader, fragmentShader });

        renderDevice.Pipeline.SetShaderProgram(shaderProgram);

        var mesh = new Mesh(renderDevice.Factory, vertices, indices, true);
        var material = new Material();

        bool isRunning = true;

        var camera = new Camera(window.ClientSize.Width, window.ClientSize.Height);

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

            renderDevice.Clear(Color.CornflowerBlue);

            var t = new Transform();
            t.Rotate(Vector3.UnitX, MathHelper.DegreesToRadians(45.0f));

            renderDevice.Pipeline.SetUniform("u_light.direction", new Vector3(-1, -1, -1));
            renderDevice.Pipeline.SetUniform("u_light.base.diffuseColor", new Vector3(0.1f, 0.1f, 0.1f));
            renderDevice.Pipeline.SetUniform("u_light.base.specularColor", new Vector3(0.1f, 0.1f, 0.1f));

            renderDevice.Pipeline.SetUniform("u_plight.position", new Vector3(-1, 3, -1));
            renderDevice.Pipeline.SetUniform("u_plight.base.diffuseColor", new Vector3(0.0f, 0.0f, 0.5f));
            renderDevice.Pipeline.SetUniform("u_plight.base.specularColor", new Vector3(1.0f, 0.2f, 0.5f));
            renderDevice.Pipeline.SetUniform("u_plight.attenuation.constant", 1.0f);
            renderDevice.Pipeline.SetUniform("u_plight.attenuation.linear", 0.09f);
            renderDevice.Pipeline.SetUniform("u_plight.attenuation.quadratic", 0.032f);

            renderDevice.Pipeline.SetUniform("u_slight.position", camera.Transform.Position);
            renderDevice.Pipeline.SetUniform("u_slight.direction", camera.Transform.Forward);
            renderDevice.Pipeline.SetUniform("u_slight.base.diffuseColor", new Vector3(1.0f, 0.0f, 1.0f));
            renderDevice.Pipeline.SetUniform("u_slight.base.specularColor", new Vector3(1.0f, 0.2f, 0.5f));
            renderDevice.Pipeline.SetUniform("u_slight.radius", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            renderDevice.Pipeline.SetUniform("u_slight.outerRadius", MathF.Cos(MathHelper.DegreesToRadians(17.5f)));
            renderDevice.Pipeline.SetUniform("u_slight.attenuation.constant", 1.0f);
            renderDevice.Pipeline.SetUniform("u_slight.attenuation.linear", 0.09f);
            renderDevice.Pipeline.SetUniform("u_slight.attenuation.quadratic", 0.032f);

            renderDevice.Pipeline.SetUniform("u_transform", Matrix4x4.CreateTranslation(0, -2, 0));

            material.Bind(renderDevice.Pipeline);
            mesh.Bind(renderDevice.InputAssembler);
            mesh.Draw(renderDevice);

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
