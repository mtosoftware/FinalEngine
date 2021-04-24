// <copyright file="SpriteDrawer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Rendering.Textures;

    public class SpriteDrawer : ISpriteDrawer, IDisposable
    {
        private readonly ISpriteBatcher batcher;

        private readonly ITextureBinder binder;

        private readonly string fragmentShaderSource =
            @"#version 460

              layout (location = 0) in vec4 in_color;
              layout (location = 1) in vec2 in_texCoord;
              layout (location = 2) in float in_textureID;

              out vec4 out_color;

              uniform sampler2D u_textures[32];

              void main()
              {
                  vec4 color = in_color;

                  switch(int(in_textureID))
                  {
                      case 0: color *= texture(u_textures[0], in_texCoord); break;
                      case 1: color *= texture(u_textures[1], in_texCoord); break;
                      case 2: color *= texture(u_textures[2], in_texCoord); break;
                      case 3: color *= texture(u_textures[3], in_texCoord); break;
                      case 4: color *= texture(u_textures[4], in_texCoord); break;
                      case 5: color *= texture(u_textures[5], in_texCoord); break;
                      case 6: color *= texture(u_textures[6], in_texCoord); break;
                      case 7: color *= texture(u_textures[7], in_texCoord); break;
                      case 8: color *= texture(u_textures[8], in_texCoord); break;
                      case 9: color *= texture(u_textures[9], in_texCoord); break;
                      case 10: color *= texture(u_textures[10], in_texCoord); break;
                      case 11: color *= texture(u_textures[11], in_texCoord); break;
                      case 12: color *= texture(u_textures[12], in_texCoord); break;
                      case 13: color *= texture(u_textures[13], in_texCoord); break;
                      case 14: color *= texture(u_textures[14], in_texCoord); break;
                      case 15: color *= texture(u_textures[15], in_texCoord); break;
                      case 16: color *= texture(u_textures[16], in_texCoord); break;
                      case 17: color *= texture(u_textures[17], in_texCoord); break;
                      case 18: color *= texture(u_textures[18], in_texCoord); break;
                      case 19: color *= texture(u_textures[19], in_texCoord); break;
                      case 20: color *= texture(u_textures[20], in_texCoord); break;
                      case 21: color *= texture(u_textures[21], in_texCoord); break;
                      case 22: color *= texture(u_textures[22], in_texCoord); break;
                      case 23: color *= texture(u_textures[23], in_texCoord); break;
                      case 24: color *= texture(u_textures[24], in_texCoord); break;
                      case 25: color *= texture(u_textures[25], in_texCoord); break;
                      case 26: color *= texture(u_textures[26], in_texCoord); break;
                      case 27: color *= texture(u_textures[27], in_texCoord); break;
                      case 28: color *= texture(u_textures[28], in_texCoord); break;
                      case 29: color *= texture(u_textures[29], in_texCoord); break;
                      case 30: color *= texture(u_textures[30], in_texCoord); break;
                      case 31: color *= texture(u_textures[31], in_texCoord); break;
                  }

                  out_color = color;
              }";

        private readonly IInputLayout inputLayout;

        private readonly IRenderDevice renderDevice;

        private readonly string vertexShaderSource =
            @"#version 460

              layout(location = 0) in vec2 in_position;
              layout(location = 1) in vec4 in_color;
              layout(location = 2) in vec2 in_texCoord;
              layout(location = 3) in float in_textureID;

              layout(location = 0) out vec4 out_color;
              layout(location = 1) out vec2 out_texCoord;
              layout(location = 2) out float out_textureID;

              uniform mat4 u_projection;
              uniform mat4 u_view;

              void main()
              {
                  out_color = in_color;
                  out_texCoord = in_texCoord;
                  out_textureID = in_textureID;

                  gl_Position = u_projection * u_view * vec4(in_position, 0.0, 1.0);
              }";

        private IShader? fragmentShader;

        private IIndexBuffer? indexBuffer;

        private IShaderProgram? shaderProgram;

        private IVertexBuffer? vertexBuffer;

        private IShader? vertexShader;

        public SpriteDrawer(IRenderDevice renderDevice, ISpriteBatcher batcher, ITextureBinder binder, int projectionWidth, int projectionHeight)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice), $"The specified {nameof(renderDevice)} parameter cannot be null.");
            this.batcher = batcher ?? throw new ArgumentNullException(nameof(batcher), $"The specified {nameof(batcher)} parameter cannot be null.");
            this.binder = binder ?? throw new ArgumentNullException(nameof(binder), $"The specified {nameof(binder)} parameter cannot be null.");

            this.vertexShader = renderDevice.Factory.CreateShader(PipelineTarget.Vertex, this.vertexShaderSource);
            this.fragmentShader = renderDevice.Factory.CreateShader(PipelineTarget.Fragment, this.fragmentShaderSource);
            this.shaderProgram = renderDevice.Factory.CreateShaderProgram(new[] { this.vertexShader, this.fragmentShader });

            this.inputLayout = renderDevice.Factory.CreateInputLayout(
                new[]
                {
                    new InputElement(0, 2, InputElementType.Float, 0),
                    new InputElement(1, 4, InputElementType.Float, 2 * sizeof(float)),
                    new InputElement(2, 2, InputElementType.Float, 6 * sizeof(float)),
                    new InputElement(3, 1, InputElementType.Float, 8 * sizeof(float)),
                });

            this.vertexBuffer = renderDevice.Factory.CreateVertexBuffer(
                BufferUsageType.Dynamic,
                Array.Empty<Vertex>(),
                batcher.MaxVertexCount * Vertex.SizeInBytes,
                Vertex.SizeInBytes);

            int[] indices = new int[batcher.MaxIndexCount];

            int offset = 0;

            for (int i = 0; i < batcher.MaxIndexCount; i += 6)
            {
                indices[i] = offset;
                indices[i + 1] = 1 + offset;
                indices[i + 2] = 2 + offset;

                indices[i + 3] = 2 + offset;
                indices[i + 4] = 3 + offset;
                indices[i + 5] = 0 + offset;

                offset += 4;
            }

            this.indexBuffer = renderDevice.Factory.CreateIndexBuffer(
                BufferUsageType.Static,
                indices,
                indices.Length * sizeof(int));

            this.Projection = Matrix4x4.CreateOrthographic(projectionWidth, projectionHeight, -1, 1);
            this.Transform = Matrix4x4.CreateTranslation(Vector3.Zero);
        }

        ~SpriteDrawer()
        {
            this.Dispose(false);
        }

        public Matrix4x4 Projection { get; set; }

        public Matrix4x4 Transform { get; set; }

        protected bool IsDisposed { get; private set; }

        public void Begin()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(SpriteDrawer));
            }

            this.renderDevice.Pipeline.SetShaderProgram(this.shaderProgram);

            this.renderDevice.Pipeline.SetUniform("u_projection", this.Projection);
            this.renderDevice.Pipeline.SetUniform("u_view", this.Transform);

            this.batcher.Reset();
            this.binder.Reset();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Draw(ITexture2D texture, Color color, Vector2 origin, Vector2 position, float rotation, Vector2 scale)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(SpriteDrawer));
            }

            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture), $"The specified {nameof(texture)} parameter cannot be null.");
            }

            if (this.batcher.ShouldReset || this.binder.ShouldReset)
            {
                this.End();
                this.Begin();
            }

            this.batcher.Batch(this.binder.GetTextureSlotIndex(texture), color, origin, position, rotation, scale);
        }

        public void End()
        {
            if (this.IsDisposed || this.vertexBuffer == null)
            {
                throw new ObjectDisposedException(nameof(SpriteDrawer));
            }

            this.batcher.UpdateBatch(this.vertexBuffer);

            this.renderDevice.InputAssembler.SetInputLayout(this.inputLayout);
            this.renderDevice.InputAssembler.SetVertexBuffer(this.vertexBuffer);
            this.renderDevice.InputAssembler.SetIndexBuffer(this.indexBuffer);

            this.renderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, this.batcher.CurrentIndexCount);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.indexBuffer != null)
                {
                    this.indexBuffer.Dispose();
                    this.indexBuffer = null;
                }

                if (this.vertexBuffer != null)
                {
                    this.vertexBuffer.Dispose();
                    this.vertexBuffer = null;
                }

                if (this.shaderProgram != null)
                {
                    this.shaderProgram.Dispose();
                    this.shaderProgram = null;
                }

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
            }

            this.IsDisposed = true;
        }
    }
}