// <copyright file="Mesh.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Numerics;
using FinalEngine.Rendering.Buffers;

/// <summary>
/// Provides a static implementation of an <see cref="IMesh"/>.
/// </summary>
/// <seealso cref="IMesh" />
public sealed class Mesh : IMesh
{
    /// <summary>
    /// The input layout.
    /// </summary>
    private readonly IInputLayout inputLayout;

    /// <summary>
    /// The index buffer.
    /// </summary>
    private IIndexBuffer? indexBuffer;

    /// <summary>
    /// Indicates whether this instance is disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// The vertex buffer.
    /// </summary>
    private IVertexBuffer? vertexBuffer;

    /// <summary>
    /// Initializes a new instance of the <see cref="Mesh"/> class.
    /// </summary>
    /// <param name="factory">
    /// The GPU factory used to create the required buffers for this <see cref="Mesh"/>.
    /// </param>
    /// <param name="vertices">
    /// The vertices for this <see cref="Mesh"/>.
    /// </param>
    /// <param name="indices">
    /// The indices for this <see cref="Mesh"/>.
    /// </param>
    /// <param name="calculateNormals">
    /// if set to <c>true</c> normal vectors will be calculated prior to creating the resource buffers.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="factory"/>, <paramref name="vertices"/> or <paramref name="indices"/> parameter cannot be null.
    /// </exception>
    public Mesh(IGPUResourceFactory factory, MeshVertex[] vertices, int[] indices, bool calculateNormals = true)
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        if (vertices == null)
        {
            throw new ArgumentNullException(nameof(vertices));
        }

        if (indices == null)
        {
            throw new ArgumentNullException(nameof(indices));
        }

        if (calculateNormals)
        {
            CalculateNormals(vertices, indices);
        }

        this.vertexBuffer = factory.CreateVertexBuffer(
            BufferUsageType.Static,
            vertices,
            vertices.Length * MeshVertex.SizeInBytes,
            MeshVertex.SizeInBytes);

        this.indexBuffer = factory.CreateIndexBuffer(
            BufferUsageType.Static,
            indices,
            indices.Length * sizeof(int));

        this.inputLayout = factory.CreateInputLayout(MeshVertex.InputElements);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="Mesh"/> class.
    /// </summary>
    ~Mesh()
    {
        this.Dispose(false);
    }

    /// <summary>
    /// Binds this <see cref="IMesh" /> to the GPU using the specified <paramref name="inputAssembler" />.
    /// </summary>
    /// <param name="inputAssembler">
    /// The input assembler used to bind this <see cref="IMesh" /> to the GPU.
    /// </param>
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="Mesh"/> has been disposed.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="inputAssembler"/> parameter cannot be null.
    /// </exception>
    public void Bind(IInputAssembler inputAssembler)
    {
        if (this.isDisposed)
        {
            throw new ObjectDisposedException(nameof(Mesh));
        }

        if (inputAssembler == null)
        {
            throw new ArgumentNullException(nameof(inputAssembler));
        }

        inputAssembler.SetInputLayout(this.inputLayout);
        inputAssembler.SetVertexBuffer(this.vertexBuffer!);
        inputAssembler.SetIndexBuffer(this.indexBuffer!);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Draws this <see cref="IMesh" /> to the currently bound render target using the specified <paramref name="renderDevice" />.
    /// </summary>
    /// <param name="renderDevice">
    /// The render device used to draw this <see cref="IMesh" /> to the currently bound render target.
    /// </param>
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="Mesh"/> has been disposed.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="renderDevice"/> parameter cannot be null.
    /// </exception>
    public void Draw(IRenderDevice renderDevice)
    {
        if (this.isDisposed)
        {
            throw new ObjectDisposedException(nameof(Mesh));
        }

        if (renderDevice == null)
        {
            throw new ArgumentNullException(nameof(renderDevice));
        }

        renderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, this.indexBuffer!.Length);
    }

    /// <summary>
    /// Calculates the surface normals for all triangles by taking the cross-product of two edges for each triangle and normalizing it.
    /// </summary>
    /// <param name="vertices">
    /// The vertices.
    /// </param>
    /// <param name="indices">
    /// The indices.
    /// </param>
    private static void CalculateNormals(MeshVertex[] vertices, int[] indices)
    {
        for (int i = 0; i < indices.Length; i += 3)
        {
            int i0 = indices[i];
            int i1 = indices[i + 1];
            int i2 = indices[i + 2];

            var v1 = vertices[i1].Position - vertices[i0].Position;
            var v2 = vertices[i2].Position - vertices[i0].Position;

            var normal = Vector3.Normalize(Vector3.Cross(v1, v2));

            vertices[i0].Normal = normal;
            vertices[i1].Normal = normal;
            vertices[i2].Normal = normal;
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].Normal = Vector3.Normalize(vertices[i].Normal);
        }
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
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

        this.isDisposed = true;
    }
}
