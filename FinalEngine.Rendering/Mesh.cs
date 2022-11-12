// <copyright file="Mesh.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Numerics;
    using FinalEngine.Rendering.Buffers;

    public class Mesh : IMesh
    {
        private readonly IInputLayout inputLayout;

        private IIndexBuffer? indexBuffer;

        private IVertexBuffer? vertexBuffer;

        public Mesh(IGPUResourceFactory factory, MeshVertex[] vertices, int[] indices)
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

            this.CalculateNormals(vertices, indices);

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

        protected bool IsDisposed { get; private set; }

        public void Bind(IInputAssembler inputAssembler)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(Mesh));
            }

            if (inputAssembler == null)
            {
                throw new ArgumentNullException(nameof(inputAssembler));
            }

            inputAssembler.SetInputLayout(this.inputLayout);
            inputAssembler.SetIndexBuffer(this.indexBuffer!);
            inputAssembler.SetVertexBuffer(this.vertexBuffer!);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Render(IRenderDevice renderDevice)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(Mesh));
            }

            if (renderDevice == null)
            {
                throw new ArgumentNullException(nameof(renderDevice));
            }

            renderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, this.indexBuffer!.Length);
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

        private void CalculateNormals(MeshVertex[] vertices, int[] indices)
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
    }
}
