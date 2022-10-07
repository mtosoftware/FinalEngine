// <copyright file="Mesh.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Collections.Generic;
    using FinalEngine.Rendering.Buffers;

    public class Mesh : IMesh
    {
        private readonly IInputLayout inputLayout;

        private IIndexBuffer? indexBuffer;

        private IVertexBuffer? vertexBuffer;

        public Mesh(IGPUResourceFactory factory, IReadOnlyCollection<MeshVertex> vertices, IReadOnlyCollection<int> indices)
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

            this.vertexBuffer = factory.CreateVertexBuffer(
                BufferUsageType.Static,
                vertices,
                vertices.Count * MeshVertex.SizeInBytes,
                MeshVertex.SizeInBytes);

            this.indexBuffer = factory.CreateIndexBuffer(
                BufferUsageType.Static,
                indices,
                indices.Count * sizeof(int));

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
            inputAssembler.SetVertexBuffer(this.vertexBuffer!);
            inputAssembler.SetIndexBuffer(this.indexBuffer!);
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
    }
}
