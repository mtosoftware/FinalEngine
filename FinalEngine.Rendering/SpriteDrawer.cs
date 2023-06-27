// <copyright file="SpriteDrawer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

/// <summary>
///   Provides a standard implementation of an <see cref="ISpriteDrawer"/>, which assumes batch rendering.
/// </summary>
/// <seealso cref="ISpriteDrawer"/>
/// <seealso cref="IDisposable"/>
public class SpriteDrawer : ISpriteDrawer, IDisposable
{
    /// <summary>
    ///   The sprite batcher.
    /// </summary>
    private readonly ISpriteBatcher batcher;

    /// <summary>
    ///   The texture binder.
    /// </summary>
    private readonly ITextureBinder binder;

    /// <summary>
    ///   The input layout.
    /// </summary>
    private readonly IInputLayout inputLayout;

    /// <summary>
    ///   The projection height.
    /// </summary>
    private readonly int projectionHeight;

    /// <summary>
    ///   The projection width.
    /// </summary>
    private readonly int projectionWidth;

    /// <summary>
    ///   The render device.
    /// </summary>
    private readonly IRenderDevice renderDevice;

    /// <summary>
    ///   The fragment shader.
    /// </summary>
    private IShader? fragmentShader;

    /// <summary>
    ///   The index buffer.
    /// </summary>
    private IIndexBuffer? indexBuffer;

    /// <summary>
    ///   The shader program.
    /// </summary>
    private IShaderProgram? shaderProgram;

    /// <summary>
    ///   The vertex buffer.
    /// </summary>
    private IVertexBuffer? vertexBuffer;

    /// <summary>
    ///   The vertex shader.
    /// </summary>
    private IShader? vertexShader;

    /// <summary>
    ///   Initializes a new instance of the <see cref="SpriteDrawer"/> class.
    /// </summary>
    /// <param name="renderDevice">
    ///   The render device.
    /// </param>
    /// <param name="batcher">
    ///   The sprite batcher, used to batch draw calls to be rendered.
    /// </param>
    /// <param name="binder">
    ///   The texture binder, used to bind textures to texture slots.
    /// </param>
    /// <param name="projectionWidth">
    ///   The initial width of the projection.
    /// </param>
    /// <param name="projectionHeight">
    ///   The initial height of the projection.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="renderDevice"/>, <paramref name="batcher"/> or <paramref name="binder"/> parameter cannot be null.
    /// </exception>
    public SpriteDrawer(IRenderDevice renderDevice, ISpriteBatcher batcher, ITextureBinder binder, int projectionWidth, int projectionHeight)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.batcher = batcher ?? throw new ArgumentNullException(nameof(batcher));
        this.binder = binder ?? throw new ArgumentNullException(nameof(binder));

        this.vertexShader = ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\sprite-geometry.vert");
        this.fragmentShader = ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\sprite-geometry.frag");

        this.shaderProgram = renderDevice.Factory.CreateShaderProgram(new[] { this.vertexShader, this.fragmentShader });
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

    /// <summary>
    ///   Finalizes an instance of the <see cref="SpriteDrawer"/> class.
    /// </summary>
    ~SpriteDrawer()
    {
        this.Dispose(false);
    }

    /// <summary>
    ///   Gets or sets the projection.
    /// </summary>
    /// <value>
    ///   The projection.
    /// </value>
    public Matrix4x4 Projection { get; set; }

    /// <summary>
    ///   Gets or sets the transform.
    /// </summary>
    /// <value>
    ///   The transform.
    /// </value>
    public Matrix4x4 Transform { get; set; }

    /// <summary>
    ///   Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDisposed { get; private set; }

    /// <summary>
    ///   Initializes the drawer, this must be called <c>before</c> you invoke the <see cref="Draw(ITexture2D, Color, Vector2, Vector2, float, Vector2)"/> method.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    ///   The <see cref="SpriteDrawer"/> has been disposed.
    /// </exception>
    public void Begin()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(SpriteDrawer));
        }

        this.renderDevice.Pipeline.SetShaderProgram(this.shaderProgram!);

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

    /// <summary>
    ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Draws the specified texture, blended with the specified <paramref name="color"/>, with the specified <paramref name="origin"/>, at the specified <paramref name="position"/>, <paramref name="rotation"/> and <paramref name="scale"/>.
    /// </summary>
    /// <param name="texture">
    ///   The texture to draw.
    /// </param>
    /// <param name="color">
    ///   The color of the texture.
    /// </param>
    /// <param name="origin">
    ///   The origin of the texture.
    /// </param>
    /// <param name="position">
    ///   The position of the texture, relative to it's origin.
    /// </param>
    /// <param name="rotation">
    ///   The rotation of the texture, relative to it's origin.
    /// </param>
    /// <param name="scale">
    ///   The size of the texture in pixels.
    /// </param>
    /// <exception cref="ObjectDisposedException">
    ///   The <see cref="SpriteDrawer"/> has been disposed.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="texture"/> parameter cannot be null.
    /// </exception>
    public void Draw(ITexture2D texture, Color color, Vector2 origin, Vector2 position, float rotation, Vector2 scale)
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(SpriteDrawer));
        }

        if (texture == null)
        {
            throw new ArgumentNullException(nameof(texture));
        }

        if (this.batcher.ShouldReset || this.binder.ShouldReset)
        {
            this.End();
            this.Begin();
        }

        this.batcher.Batch(this.binder.GetTextureSlotIndex(texture), color, origin, position, rotation, scale, texture.Description.Width, texture.Description.Height);
    }

    /// <summary>
    ///   Flushes the contents of the drawer to the unspecified surface.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    ///   The <see cref="SpriteDrawer"/> has been disposed - or - the internal <see cref="vertexBuffer"/> is null.
    /// </exception>
    /// <remarks>
    ///   This must be called <c>after</c> you've made a call too <see cref="Draw(ITexture2D, Color, Vector2, Vector2, float, Vector2)"/> as otherwise the drawer might behave incorrectly.
    /// </remarks>
    public void End()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(SpriteDrawer));
        }

        this.batcher.Update(this.vertexBuffer!);

        this.renderDevice.InputAssembler.SetInputLayout(this.inputLayout);
        this.renderDevice.InputAssembler.SetVertexBuffer(this.vertexBuffer!);
        this.renderDevice.InputAssembler.SetIndexBuffer(this.indexBuffer!);

        this.renderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, this.batcher.CurrentIndexCount);
    }

    /// <summary>
    ///   Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
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
                ResourceManager.Instance.UnloadResource(this.vertexShader);
                this.vertexShader = null;
            }

            if (this.fragmentShader != null)
            {
                ResourceManager.Instance.UnloadResource(this.fragmentShader);
                this.fragmentShader = null;
            }
        }

        this.IsDisposed = true;
    }
}
