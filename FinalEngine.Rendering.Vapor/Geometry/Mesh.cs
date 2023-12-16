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

    public Mesh(IGPUResourceFactory factory, MeshVertex[] vertices, int[] indices, bool calculateNormals = true, bool calculateTangents = true)
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(vertices, nameof(vertices));
        ArgumentNullException.ThrowIfNull(indices, nameof(indices));

        if (calculateNormals)
        {
            CalculateNormals(vertices, indices);
        }

        if (calculateTangents)
        {
            CalculateTangents(vertices, indices);
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
    }

    private static void CalculateTangents(MeshVertex[] vertices, int[] indices)
    {
        for (int i = 0; i < indices.Length; i += 3)
        {
            int i0 = indices[i];
            int i1 = indices[i + 1];
            int i2 = indices[i + 2];

            var edge1 = vertices[i1].Position - vertices[i0].Position;
            var edge2 = vertices[i2].Position - vertices[i0].Position;

            float deltaU1 = vertices[i1].TextureCoordinate.X - vertices[i0].TextureCoordinate.X;
            float deltaU2 = vertices[i2].TextureCoordinate.X - vertices[i0].TextureCoordinate.X;
            float deltaV1 = vertices[i1].TextureCoordinate.Y - vertices[i0].TextureCoordinate.Y;
            float deltaV2 = vertices[i2].TextureCoordinate.Y - vertices[i0].TextureCoordinate.Y;

            float dividend = (deltaU1 * deltaV2) - (deltaU2 * deltaV1);
            float f = dividend == 0.0f ? 0.0f : 1.0f / dividend;

            var tangent = new Vector3(
                f * ((deltaV2 * edge1.X) - (deltaV1 * edge2.X)),
                f * ((deltaV2 * edge1.Y) - (deltaV1 * edge2.Y)),
                f * ((deltaV2 * edge1.Z) - (deltaV1 * edge2.Z)));

            vertices[i0].Tangent = tangent;
            vertices[i1].Tangent = tangent;
            vertices[i2].Tangent = tangent;
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
