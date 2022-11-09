// <copyright file="MeshTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering
{
    using System;
    using System.Collections.Generic;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Buffers;
    using Moq;
    using NUnit.Framework;

    public class MeshTests
    {
        private Mock<IGPUResourceFactory> factory;

        private Mock<IIndexBuffer> indexBuffer;

        private IReadOnlyCollection<int> indices;

        private Mock<IInputAssembler> inputAssembler;

        private Mock<IInputLayout> inputLayout;

        private Mesh mesh;

        private Mock<IRenderDevice> renderDevice;

        private Mock<IVertexBuffer> vertexBuffer;

        private IReadOnlyCollection<MeshVertex> vertices;

        [Test]
        public void BindShouldInvokeInputAssemblerSetIndexBufferWhenInvoked()
        {
            // Act
            this.mesh.Bind(this.inputAssembler.Object);

            // Assert
            this.inputAssembler.Verify(x => x.SetIndexBuffer(this.indexBuffer.Object), Times.Once);
        }

        [Test]
        public void BindShouldInvokeInputAssemblerSetInputLayoutWhenInvoked()
        {
            // Act
            this.mesh.Bind(this.inputAssembler.Object);

            // Assert
            this.inputAssembler.Verify(x => x.SetInputLayout(this.inputLayout.Object), Times.Once);
        }

        [Test]
        public void BindShouldInvokeInputAssemblerSetVertexBufferWhenInvoked()
        {
            // Act
            this.mesh.Bind(this.inputAssembler.Object);

            // Assert
            this.inputAssembler.Verify(x => x.SetVertexBuffer(this.vertexBuffer.Object), Times.Once);
        }

        [Test]
        public void BindShouldThrowArgumentNullExceptionWhenInputAssemblerIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.mesh.Bind(null);
            });
        }

        [Test]
        public void BindShouldThrowObjectDisposedExceptionWhenDisposed()
        {
            // Arrange
            this.mesh.Dispose();

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() =>
            {
                this.mesh.Bind(this.inputAssembler.Object);
            });
        }

        [Test]
        public void ConstructorShouldInvokeFactoryCreateInputLayoutWhenInvoked()
        {
            // Assert
            this.factory.Verify(x => x.CreateInputLayout(MeshVertex.InputElements));
        }

        [Test]
        public void ConstructorShouldInvokeFactoryCreateVertexBufferWhenInvoked()
        {
            // Assert
            this.factory.Verify(x => x.CreateVertexBuffer(BufferUsageType.Static, this.vertices, this.vertices.Count * MeshVertex.SizeInBytes, MeshVertex.SizeInBytes), Times.Once);
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenFactoryIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Mesh(null, this.vertices, this.indices);
            });
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenIndicesIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Mesh(this.factory.Object, this.vertices, null);
            });
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenVerticesIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Mesh(this.factory.Object, null, this.indices);
            });
        }

        [Test]
        public void ConsutrctorShouldFactoryInvokeCreateIndexBufferWhenInvoked()
        {
            // Assert
            this.factory.Verify(x => x.CreateIndexBuffer(BufferUsageType.Static, this.indices, this.indices.Count * sizeof(int)));
        }

        [Test]
        public void DisposeShouldInvokeIndexBufferDisposeWHenBufferIsNotDisposed()
        {
            // Act
            this.mesh.Dispose();

            // Assert
            this.indexBuffer.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public void DisposeShouldInvokeVertexBufferDisposeWhenBufferIsNotDisposed()
        {
            // Act
            this.mesh.Dispose();

            // Assert
            this.vertexBuffer.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public void RenderShouldInvokeRenderDeviceDrawIndicesWhenInvoked()
        {
            // Act
            this.mesh.Render(this.renderDevice.Object);

            // Assert
            this.renderDevice.Verify(x => x.DrawIndices(PrimitiveTopology.Triangle, 0, this.indexBuffer.Object.Length));
        }

        [Test]
        public void RenderShouldThrowArgumentNullExceptionWhenRenderDeviceIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.mesh.Render(null);
            });
        }

        [Test]
        public void RenderShouldThrowObjectDisposedExceptionWhenDisposed()
        {
            // Arrange
            this.mesh.Dispose();

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() =>
            {
                this.mesh.Render(this.renderDevice.Object);
            });
        }

        [SetUp]
        public void Setup()
        {
            this.factory = new Mock<IGPUResourceFactory>();
            this.inputAssembler = new Mock<IInputAssembler>();
            this.renderDevice = new Mock<IRenderDevice>();

            this.vertexBuffer = new Mock<IVertexBuffer>();
            this.indexBuffer = new Mock<IIndexBuffer>();
            this.inputLayout = new Mock<IInputLayout>();

            this.vertices = new List<MeshVertex>()
            {
                default,
                default,
                default,
                default,
            };

            this.indices = new List<int>()
            {
                0,
                0,
                0,
                0,
                0,
            };

            this.indexBuffer.SetupGet(x => x.Length).Returns(this.indices.Count);

            this.factory.Setup(x => x.CreateVertexBuffer(BufferUsageType.Static, this.vertices, this.vertices.Count * MeshVertex.SizeInBytes, MeshVertex.SizeInBytes)).Returns(this.vertexBuffer.Object);
            this.factory.Setup(x => x.CreateIndexBuffer(BufferUsageType.Static, this.indices, this.indices.Count * sizeof(int))).Returns(this.indexBuffer.Object);
            this.factory.Setup(x => x.CreateInputLayout(MeshVertex.InputElements)).Returns(this.inputLayout.Object);

            this.mesh = new Mesh(this.factory.Object, this.vertices, this.indices);
        }

        [TearDown]
        public void Teardown()
        {
            this.mesh.Dispose();
        }
    }
}
