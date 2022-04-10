// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.HelloTriangle
{
    using System;
    using System.Drawing;
    using System.IO;
    using FinalEngine.IO;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.Pipeline;

    public sealed class Program : GameContainer
    {
        private readonly IInputLayout inputLayout;

        private IShader? fragmentShader;

        private IIndexBuffer? indexBuffer;

        private IShaderProgram? shaderProgram;

        private IVertexBuffer? vertexBuffer;

        private IShader? vertexShader;

        public Program()
        {
            this.vertexShader = this.RenderDevice!.Factory.CreateShader(PipelineTarget.Vertex, ReadAllTextFromFile(this.FileSystem, "Resources\\Shaders\\vertexShader.vert"));
            this.fragmentShader = this.RenderDevice!.Factory.CreateShader(PipelineTarget.Fragment, ReadAllTextFromFile(this.FileSystem, "Resources\\Shaders\\fragmentShader.frag"));
            this.shaderProgram = this.RenderDevice!.Factory.CreateShaderProgram(new[] { this.vertexShader, this.fragmentShader });

            this.inputLayout = this.RenderDevice!.Factory.CreateInputLayout(
                new[]
                {
                    new InputElement(0, 3, InputElementType.Float, 0),
                    new InputElement(1, 4, InputElementType.Float, 3 * sizeof(float)),
                });

            float[] vertices =
            {
                -1.0f, -1.0f, 0.0f,     1, 0, 0, 1,
                1.0f, -1.0f, 0.0f,      0, 1, 0, 1,
                0.0f, 1.0f, 0.0f,       0, 0, 1, 0,
            };

            int[] indices =
            {
                0, 1, 2,
            };

            this.vertexBuffer = this.RenderDevice!.Factory.CreateVertexBuffer(BufferUsageType.Static, vertices, vertices.Length * sizeof(float), 7 * sizeof(float));
            this.indexBuffer = this.RenderDevice!.Factory.CreateIndexBuffer(BufferUsageType.Static, indices, indices.Length * sizeof(int));

            this.RenderDevice.Pipeline.SetShaderProgram(this.shaderProgram);
            this.RenderDevice.InputAssembler.SetVertexBuffer(this.vertexBuffer);
            this.RenderDevice.InputAssembler.SetIndexBuffer(this.indexBuffer);
            this.RenderDevice.InputAssembler.SetInputLayout(this.inputLayout);
        }

        protected override void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.vertexShader != null)
                {
                    this.vertexShader.Dispose();
                    this.vertexShader = null;
                }

                if (this.fragmentShader != null)
                {
                    this.fragmentShader.Dispose();
                    this.fragmentShader = null;
                }

                if (this.shaderProgram != null)
                {
                    this.shaderProgram.Dispose();
                    this.shaderProgram = null;
                }

                if (this.vertexBuffer != null)
                {
                    this.vertexBuffer.Dispose();
                    this.vertexBuffer = null;
                }

                if (this.indexBuffer != null)
                {
                    this.indexBuffer.Dispose();
                    this.indexBuffer = null;
                }
            }

            base.Dispose(disposing);
        }

        protected override void Render()
        {
            this.RenderDevice!.Clear(Color.CornflowerBlue);
            this.RenderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, 3);

            base.Render();
        }

        private static void Main()
        {
            using (var game = new Program())
            {
                game.Launch(120.0d);
            }
        }

        private static string ReadAllTextFromFile(IFileSystem fileSystem, string filePath)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!fileSystem.FileExists(filePath))
            {
                throw new FileNotFoundException($"Failed to locate file at path: {filePath}", filePath);
            }

            using (Stream? stream = fileSystem.OpenFile(filePath, FileAccessMode.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}