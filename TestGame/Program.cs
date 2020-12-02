﻿// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    //// TODO: Make sure that all OpenGL unit tests are checking whether the delete calls are being invoked inside their Dispose methods.

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Input.Mouse;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
    using FinalEngine.Platform.Desktop.OpenTK;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.Pipeline;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    [StructLayout(LayoutKind.Explicit)]
    public struct Vertex
    {
        public static readonly int SizeInBytes = Marshal.SizeOf<Vertex>();

        [FieldOffset(0)]
        private Vector3 position;

        [FieldOffset(12)]
        private Vector4 color;

        public Vertex(Vector3 position, Vector4 color)
        {
            this.position = position;
            this.color = color;
        }

        public Vector3 Position
        {
            get { return this.position; }
        }

        public Vector4 Color
        {
            get { return this.color; }
        }
    }

    /// <summary>
    ///   The main program.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///   Defines the entry point of the application.
        /// </summary>
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

                Size = new Vector2i(1024, 768),

                StartVisible = true,
            };

            var nativeWindow = new NativeWindowInvoker(settings);
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

            IRasterizer rasterizer = renderDevice.Rasterizer;
            IPipeline pipeline = renderDevice.Pipeline;
            IInputAssembler inputAssembler = renderDevice.InputAssembler;

            IGPUResourceFactory factory = renderDevice.Factory;

            rasterizer.SetRasterState(default);

            IEnumerable<IShader> shaders = new List<IShader>()
            {
                factory.CreateShader(PipelineTarget.Vertex, File.ReadAllText("Resources\\Shaders\\shader.vert")),
                factory.CreateShader(PipelineTarget.Fragment, File.ReadAllText("Resources\\Shaders\\shader.frag")),
            };

            IShaderProgram program = factory.CreateShaderProgram(shaders);
            pipeline.SetShaderProgram(program);

            Vertex[] vertices =
            {
                new Vertex(new Vector3(-0.9f, -0.5f, 0.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f)),
                new Vertex(new Vector3(-0.0f, -0.5f, 0.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f)),
                new Vertex(new Vector3(-0.45f, 0.5f, 0.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)),
            };

            Vertex[] vertices2 =
            {
                new Vertex(new Vector3(0.0f, -0.5f, 0.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f)),
                new Vertex(new Vector3(0.9f, -0.5f, 0.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f)),
                new Vertex(new Vector3(0.45f, 0.5f, 0.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f)),
            };

            int[] indices =
            {
                0, 1, 2,
            };

            var inputElements = new List<InputElement>()
            {
                new InputElement(0, 3, InputElementType.Float, Marshal.OffsetOf<Vertex>("position").ToInt32()),
                new InputElement(1, 4, InputElementType.Float, Marshal.OffsetOf<Vertex>("color").ToInt32()),
            };

            IInputLayout inputLayout = factory.CreateInputLayout(inputElements);
            inputAssembler.SetInputLayout(inputLayout);

            IVertexBuffer vertexBuffer = factory.CreateVertexBuffer(vertices, vertices.Length * Vertex.SizeInBytes, Vertex.SizeInBytes);
            IVertexBuffer vertexBuffer2 = factory.CreateVertexBuffer(vertices2, vertices2.Length * Vertex.SizeInBytes, Vertex.SizeInBytes);

            IIndexBuffer indexBuffer = factory.CreateIndexBuffer(indices, indices.Length * sizeof(int));
            inputAssembler.SetIndexBuffer(indexBuffer);

            while (!window.IsExiting)
            {
                keyboard.Update();
                mouse.Update();

                renderDevice.Clear(Color.Black);

                inputAssembler.SetVertexBuffer(vertexBuffer);
                renderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, indices.Length);

                inputAssembler.SetVertexBuffer(vertexBuffer2);
                renderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, indices.Length);

                renderContext.SwapBuffers();
                window.ProcessEvents();
            }

            program.Dispose();

            foreach (IShader shader in shaders)
            {
                shader.Dispose();
            }

            renderContext.Dispose();
            window.Dispose();
            nativeWindow.Dispose();
        }
    }
}