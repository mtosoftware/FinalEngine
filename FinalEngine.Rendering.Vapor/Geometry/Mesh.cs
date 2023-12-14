// <copyright file="Mesh.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Geometry;

using System;
using System.Numerics;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Vapor.Primitives;

public sealed class Mesh : IMesh
{
    private readonly IInputLayout inputLayout;

    private IIndexBuffer? indexBuffer;

    private bool isDisposed;

    private IVertexBuffer? vertexBuffer;

    public Mesh(IGPUResourceFactory factory, MeshVertex[] vertices, int[] indices, bool calculateNormals = true)
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(vertices, nameof(vertices));
        ArgumentNullException.ThrowIfNull(indices, nameof(indices));

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

    ~Mesh()
    {
        this.Dispose(false);
    }

    public void Bind(IInputAssembler inputAssembler)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        ArgumentNullException.ThrowIfNull(inputAssembler, nameof(inputAssembler));

        inputAssembler.SetInputLayout(this.inputLayout);
        inputAssembler.SetVertexBuffer(this.vertexBuffer!);
        inputAssembler.SetIndexBuffer(this.indexBuffer!);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Draw(IRenderDevice renderDevice)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        ArgumentNullException.ThrowIfNull(renderDevice, nameof(renderDevice));

        renderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, this.indexBuffer!.Length);
    }

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
