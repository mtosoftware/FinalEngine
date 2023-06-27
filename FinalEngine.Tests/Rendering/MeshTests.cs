// <copyright file="MeshTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering;

using System;
using System.Collections.Generic;
using System.Numerics;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Buffers;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class MeshTests
{
    private Mock<IGPUResourceFactory> factory;

    private Mock<IIndexBuffer> indexBuffer;

    private int[] indices;

    private Mock<IInputAssembler> inputAssembler;

    private Mock<IInputLayout> inputLayout;

    private Mesh mesh;

    private Mock<IRenderDevice> renderDevice;

    private Mock<IVertexBuffer> vertexBuffer;

    private MeshVertex[] vertices;

    [Test]
    public void BindShouldInvokeSetIndexBufferWhenInvoked()
    {
        // Act
        this.mesh.Bind(this.inputAssembler.Object);

        // Assert
        this.inputAssembler.Verify(x => x.SetIndexBuffer(this.indexBuffer.Object), Times.Once);
    }

    [Test]
    public void BindShouldInvokeSetInputLayoutWhenInvoked()
    {
        // Act
        this.mesh.Bind(this.inputAssembler.Object);

        // Assert
        this.inputAssembler.Verify(x => x.SetInputLayout(this.inputLayout.Object), Times.Once);
    }

    [Test]
    public void BindShouldInvokeSetVertexBufferWhenInvoked()
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
    public void ConstructorShouldInvokeCreateIndexBufferWhenInvoked()
    {
        // Assert
        this.factory.Verify(x => x.CreateIndexBuffer(BufferUsageType.Static, this.indices, this.indices.Length * sizeof(int)), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeCreateInputLayoutWhenInvoked()
    {
        // Assert
        this.factory.Verify(x => x.CreateInputLayout(MeshVertex.InputElements), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeCreateVertexBufferWhenInvoked()
    {
        // Assert
        this.factory.Verify(x => x.CreateVertexBuffer(BufferUsageType.Static, this.vertices, this.vertices.Length * MeshVertex.SizeInBytes, MeshVertex.SizeInBytes), Times.Once);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Mesh(null, Array.Empty<MeshVertex>(), Array.Empty<int>());
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenIndicesIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Mesh(this.factory.Object, Array.Empty<MeshVertex>(), null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenVerticesIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Mesh(this.factory.Object, null, Array.Empty<int>());
        });
    }

    [Test]
    public void DisposeShouldInvokeIndexBufferDisposeWhenNotDisposed()
    {
        // Act
        this.mesh.Dispose();

        // Assert
        this.indexBuffer.Verify(x => x.Dispose(), Times.Once);
    }

    [Test]
    public void DisposeShouldInvokeVertexBufferDisposeWhenNotDisposed()
    {
        // Act
        this.mesh.Dispose();

        // Assert
        this.vertexBuffer.Verify(x => x.Dispose(), Times.Once);
    }

    [Test]
    public void DisposeShouldNotInvokeIndexBufferDisposeWhenAlreadyDisposed()
    {
        // Arrange
        this.mesh.Dispose();

        // Act
        this.mesh.Dispose();

        // Assert
        this.indexBuffer.Verify(x => x.Dispose(), Times.Once);
    }

    [Test]
    public void DisposeShouldNotInvokeVertexBufferDisposeWhenAlreadyDisposed()
    {
        // Arrange
        this.mesh.Dispose();

        // Act
        this.mesh.Dispose();

        // Assert
        this.vertexBuffer.Verify(x => x.Dispose(), Times.Once);
    }

    [Test]
    public void DrawShouldInvokeDrawIndicesWhenInvoked()
    {
        // Act
        this.mesh.Draw(this.renderDevice.Object);

        // Assert
        this.renderDevice.Verify(x => x.DrawIndices(PrimitiveTopology.Triangle, 0, this.indices.Length), Times.Once);
    }

    [Test]
    public void DrawShouldThrowArgumentNullExceptionWhenRenderDeviceIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.mesh.Draw(null);
        });
    }

    [Test]
    public void DrawShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.mesh.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            this.mesh.Draw(this.renderDevice.Object);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.factory = new Mock<IGPUResourceFactory>();
        this.renderDevice = new Mock<IRenderDevice>();
        this.inputAssembler = new Mock<IInputAssembler>();
        this.vertexBuffer = new Mock<IVertexBuffer>();
        this.indexBuffer = new Mock<IIndexBuffer>();
        this.inputLayout = new Mock<IInputLayout>();

        this.vertices = new MeshVertex[]
        {
            new MeshVertex()
            {
                Position = new Vector3(-1, -1, 0),
                Color = new Vector4(1, 0, 0, 1),
                TextureCoordinate = new Vector2(0, 0),
            },

            new MeshVertex()
            {
                Position = new Vector3(1, -1, 0),
                Color = new Vector4(0, 1, 0, 1),
                TextureCoordinate = new Vector2(1, 0),
            },

            new MeshVertex()
            {
                Position = new Vector3(0, 1, 0),
                Color = new Vector4(0, 0, 1, 1),
                TextureCoordinate = new Vector2(0.5f, 1),
            },
        };

        this.indices = new int[]
        {
            0, 1, 2,
        };

        this.indexBuffer.Setup(x => x.Length).Returns(this.indices.Length);

        this.factory.Setup(x => x.CreateIndexBuffer<int>(It.IsAny<BufferUsageType>(), It.IsAny<IReadOnlyCollection<int>>(), It.IsAny<int>())).Returns(this.indexBuffer.Object);
        this.factory.Setup(x => x.CreateVertexBuffer<MeshVertex>(It.IsAny<BufferUsageType>(), It.IsAny<IReadOnlyCollection<MeshVertex>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(this.vertexBuffer.Object);
        this.factory.Setup(x => x.CreateInputLayout(It.IsAny<IReadOnlyCollection<InputElement>>())).Returns(this.inputLayout.Object);

        this.mesh = new Mesh(this.factory.Object, this.vertices, this.indices);
    }
}
