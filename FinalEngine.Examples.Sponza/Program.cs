using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Abstractions;
using System.Numerics;
using FinalEngine.Examples.Sponza;
using FinalEngine.Examples.Sponza.Loaders;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Maths;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering.Batching;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Loaders.Shaders;
using FinalEngine.Rendering.Loaders.Textures;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Primitives;
using FinalEngine.Resources;
using FinalEngine.Runtime;
using FinalEngine.Runtime.Invocation;
using OpenTK.Graphics.OpenGL4;
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
            Profile = ContextProfile.Compatability,

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

        float fieldDepth = 5.0f;
        float fieldWidth = 5.0f;

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

        MeshVertex[] cubeVertices = {
            new MeshVertex() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2( -1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2( -1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2( -1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2( -1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2( -1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2( -1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(  1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(  1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(  1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(  1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(  1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(  1.0f,  0.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f, -1.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f, -1.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f, -1.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f, -1.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f, -1.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f, -1.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f,  1.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f,  1.0f , 1.0f), TextureCoordinate = new Vector2(  0.0f,  1.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f,  1.0f)},
            new MeshVertex() { Position = new Vector3( 1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f,  1.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(  0.0f,  1.0f)},
            new MeshVertex() { Position = new Vector3(-1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(  0.0f,  1.0f)},
        };

        var cubeIndices = new List<int>();

        for (var i = 0; i < cubeIndices.Count; i++)
        {
            cubeIndices.Add(i);
        }

        var cubeMesh = new Mesh(renderDevice.Factory, cubeVertices, [.. cubeIndices]);

        const int ShadowResolution = 1024;

        int fbo = GL.GenFramebuffer();
        int tex = GL.GenTexture();

        GL.BindTexture(TextureTarget.Texture2D, tex);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, ShadowResolution, ShadowResolution, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, tex, 0);

        GL.DrawBuffer(DrawBufferMode.None);
        GL.ReadBuffer(ReadBufferMode.None);

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        var lightPos = new Vector3(-10000.0f, 400.0f, -10000.0f);

        void Render()
        {
            var model = Matrix4x4.CreateTranslation(Vector3.Zero);

            renderDevice.Pipeline.SetUniform("u_transform", model);
            renderDevice.Pipeline.SetUniform("model", model);

            material.Bind(renderDevice.Pipeline);
            mesh.Bind(renderDevice.InputAssembler);
            mesh.Draw(renderDevice);

            model = Matrix4x4.CreateTranslation(0, 5, 0) * Matrix4x4.CreateRotationX(MathHelper.DegreesToRadians(45.0f)) * Matrix4x4.CreateScale(0.25f);

            renderDevice.Pipeline.SetUniform("u_transform", model);
            renderDevice.Pipeline.SetUniform("model", model);

            mesh.Draw(renderDevice);
        }

        var binder = new TextureBinder(renderDevice.Pipeline);
        var batcher = new SpriteBatcher(renderDevice.InputAssembler);
        var drawer = new SpriteDrawer(renderDevice, batcher, binder, window.ClientSize.Width, window.ClientSize.Height);

        var depthShader = ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\depth.fesp");
        var standardShader = ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\standard-geometry.fesp");
        var debugShader = ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\debug.fesp");

        GL.Enable(EnableCap.DepthTest);

        MeshVertex[] quadVertices =
        {
            new MeshVertex() { Position = new Vector3( 0.5f,  0.5f, 0.0f), TextureCoordinate = new Vector2(   1.0f, 1.0f) },   // top right
            new MeshVertex() { Position = new Vector3( 0.5f, -0.5f, 0.0f), TextureCoordinate = new Vector2(   1.0f, 0.0f) },   // bottom right
            new MeshVertex() { Position = new Vector3(-0.5f, -0.5f, 0.0f), TextureCoordinate = new Vector2(   0.0f, 0.0f) },   // bottom left
            new MeshVertex() { Position = new Vector3(-0.5f,  0.5f, 0.0f), TextureCoordinate = new Vector2(   0.0f, 1.0f) },   // top left
        };

        int[] quadIndices =
        {
            0, 1, 3, 1, 2, 3,
        };

        var quadMesh = new Mesh(renderDevice.Factory, quadVertices, quadIndices, false, false);

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

            GL.ClearColor(Color.Blue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var lightProjection = camera.Projection; //Matrix4x4.CreateOrthographicOffCenter(-100000.0f, 100000.0f, -100000.0f, 100000.0f, 1.0f, 7.5f);
            var lightView = camera.Transform.CreateViewMatrix(Vector3.UnitY);
            var lightSpace = lightProjection * lightView;

            renderDevice.Pipeline.SetShaderProgram(depthShader);
            renderDevice.Pipeline.SetUniform("lightSpaceMatrix", lightSpace);

            GL.Viewport(0, 0, ShadowResolution, ShadowResolution);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            Render();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Viewport(0, 0, window.ClientSize.Width, window.ClientSize.Height);

            renderDevice.Pipeline.SetShaderProgram(standardShader);

            renderDevice.Pipeline.SetUniform("u_projection", camera.Projection);
            renderDevice.Pipeline.SetUniform("u_view", camera.View);

            Render();

            renderDevice.Pipeline.SetShaderProgram(debugShader);

            renderDevice.Pipeline.SetUniform("depthMap", 0);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            quadMesh.Bind(renderDevice.InputAssembler);
            quadMesh.Draw(renderDevice);

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
