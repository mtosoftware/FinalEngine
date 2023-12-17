// <copyright file="SpriteDrawer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Batching;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using FinalEngine.Rendering.Vapor.Primitives;
using FinalEngine.Resources;

public class SpriteDrawer : ISpriteDrawer, IDisposable
{
    private readonly ISpriteBatcher batcher;

    private readonly ITextureBinder binder;

    private readonly IInputLayout inputLayout;

    private readonly int projectionHeight;

    private readonly int projectionWidth;

    private readonly IRenderDevice renderDevice;

    private IIndexBuffer? indexBuffer;

    private IShaderProgram? shaderProgram;

    private IVertexBuffer? vertexBuffer;

    public SpriteDrawer(IRenderDevice renderDevice, ISpriteBatcher batcher, ITextureBinder binder, int projectionWidth, int projectionHeight)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.batcher = batcher ?? throw new ArgumentNullException(nameof(batcher));
        this.binder = binder ?? throw new ArgumentNullException(nameof(binder));

        this.inputLayout = renderDevice.Factory.CreateInputLayout(SpriteVertex.InputElements);

        this.vertexBuffer = renderDevice.Factory.CreateVertexBuffer(
            BufferUsageType.Dynamic,
            Array.Empty<SpriteVertex>(),
            batcher.MaxVertexCount * SpriteVertex.SizeInBytes,
            SpriteVertex.SizeInBytes);

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

        this.Projection = Matrix4x4.CreateOrthographicOffCenter(0, projectionWidth, projectionHeight, 0, -1, 1);
        this.Transform = Matrix4x4.CreateTranslation(Vector3.Zero);

        this.projectionWidth = projectionWidth;
        this.projectionHeight = projectionHeight;
    }

    ~SpriteDrawer()
    {
        this.Dispose(false);
    }

    public Matrix4x4 Projection { get; set; }

    public Matrix4x4 Transform { get; set; }

    protected bool IsDisposed { get; private set; }

    private IShaderProgram ShaderProgram
    {
        get { return this.shaderProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Batching\\sprite-geometry.fesp"); }
    }

    public void Begin()
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);

        this.renderDevice.Pipeline.SetShaderProgram(this.ShaderProgram!);

        this.renderDevice.Pipeline.SetUniform("u_projection", this.Projection);
        this.renderDevice.Pipeline.SetUniform("u_transform", this.Transform);
        this.renderDevice.Rasterizer.SetViewport(new Rectangle(0, 0, this.projectionWidth, this.projectionHeight));

        this.renderDevice.OutputMerger.SetBlendState(
            new BlendStateDescription()
            {
                Enabled = true,
                SourceMode = BlendMode.SourceAlpha,
                DestinationMode = BlendMode.OneMinusSourceAlpha,
            });

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
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
        ArgumentNullException.ThrowIfNull(texture, nameof(texture));

        if (this.batcher.ShouldReset || this.binder.ShouldReset)
        {
            this.End();
            this.Begin();
        }

        this.batcher.Batch(this.binder.GetTextureSlotIndex(texture), color, origin, position, rotation, scale, texture.Description.Width, texture.Description.Height);
    }

    public void End()
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);

        this.batcher.Update(this.vertexBuffer!);

        this.renderDevice.InputAssembler.SetInputLayout(this.inputLayout);
        this.renderDevice.InputAssembler.SetVertexBuffer(this.vertexBuffer!);
        this.renderDevice.InputAssembler.SetIndexBuffer(this.indexBuffer!);

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
        }

        this.IsDisposed = true;
    }
}
