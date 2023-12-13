// <copyright file="SpriteDrawerTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;
using Moq;
using NUnit.Framework;

public class SpriteDrawerTests
{
    private const int ProjectionHeight = 720;

    private const int ProjectionWidth = 1280;

    private Mock<ISpriteBatcher> batcher;

    private Mock<ITextureBinder> binder;

    private SpriteDrawer drawer;

    private Mock<IGPUResourceFactory> factory;

    private Mock<IShader> fragmentShader;

    private Mock<IIndexBuffer> indexBuffer;

    private Mock<IInputAssembler> inputAssembler;

    private Mock<IInputLayout> inputLayout;

    private Mock<IOutputMerger> outputMerger;

    private Mock<IPipeline> pipeline;

    private Mock<IRasterizer> rasterizer;

    private Mock<IRenderDevice> renderDevice;

    private Mock<ResourceLoaderBase<IShader>> resourceLoader;

    private Mock<IShaderProgram> shaderProgram;

    private Mock<ITexture2D> texture;

    private Mock<IVertexBuffer> vertexBuffer;

    private Mock<IShader> vertexShader;

    [Test]
    public void BeginShouldInvokeBatcherResetWhenNotDisposed()
    {
        // Act
        this.drawer.Begin();

        // Assert
        this.batcher.Verify(x => x.Reset(), Times.Once);
    }

    [Test]
    public void BeginShouldInvokeBinderResetWhenNotDisposed()
    {
        // Act
        this.drawer.Begin();

        // Assert
        this.binder.Verify(x => x.Reset(), Times.Once);
    }

    [Test]
    public void BeginShouldInvokeSetShaderProgramWhenNotDisposed()
    {
        // Act
        this.drawer.Begin();

        // Assert
        this.pipeline.Verify(x => x.SetShaderProgram(this.shaderProgram.Object), Times.Once);
    }

    [Test]
    public void BeginShouldInvokeSetUniformProjectionWhenNotDisposed()
    {
        // Act
        this.drawer.Begin();

        // Assert
        this.pipeline.Verify(x => x.SetUniform("u_projection", this.drawer.Projection), Times.Once);
    }

    [Test]
    public void BeginShouldInvokeSetUniformTransformWhenNotDisposed()
    {
        // Act
        this.drawer.Begin();

        // Assert
        this.pipeline.Verify(x => x.SetUniform("u_transform", this.drawer.Transform), Times.Once);
    }

    [Test]
    public void BeginShouldThrowObjectDisposedExceptionWhenDrawerIsDisposed()
    {
        // Arrange
        this.drawer.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(this.drawer.Begin);
    }

    [Test]
    public void ConstructorShouldInvokeCreateIndexBufferWhenInvoked()
    {
        // Assert
        this.factory.Verify(x => x.CreateIndexBuffer(BufferUsageType.Static, It.IsAny<int[]>(), this.batcher.Object.MaxIndexCount * sizeof(int)), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeCreateInputLayoutWhenInvoked()
    {
        // Assert
        this.factory.Verify(x => x.CreateInputLayout(SpriteVertex.InputElements), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeCreateShaderProgramWhenInvoked()
    {
        // Assert
        this.factory.Verify(x => x.CreateShaderProgram(new[] { this.vertexShader.Object, this.fragmentShader.Object }), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeCreateVertexBufferWhenInvoked()
    {
        // Assert
        this.factory.Verify(x => x.CreateVertexBuffer(BufferUsageType.Dynamic, It.IsAny<SpriteVertex[]>(), this.batcher.Object.MaxVertexCount * SpriteVertex.SizeInBytes, SpriteVertex.SizeInBytes), Times.Once);
    }

    [Test]
    public void ConstructorShouldSetProjectionUsingProjectionWidthAndHeightValuesWhenInvoked()
    {
        // Arrange
        var expected = Matrix4x4.CreateOrthographicOffCenter(0, ProjectionWidth, ProjectionHeight, 0, -1, 1);

        // Act
        var actual = this.drawer.Projection;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ConstructorShouldSetTransformToZeroedTranslationWhenInvoked()
    {
        // Arrange
        var expected = Matrix4x4.CreateTranslation(Vector3.Zero);

        // Act
        var actual = this.drawer.Transform;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenBatcherIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SpriteDrawer(this.renderDevice.Object, null, this.binder.Object, ProjectionWidth, ProjectionHeight);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenBinderIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SpriteDrawer(this.renderDevice.Object, this.batcher.Object, null, ProjectionWidth, ProjectionHeight);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenRenderDeviceIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SpriteDrawer(null, this.batcher.Object, this.binder.Object, ProjectionWidth, ProjectionHeight);
        });
    }

    [Test]
    public void DrawShouldInvokeBatchWhenInvoked()
    {
        // Act
        this.drawer.Draw(this.texture.Object, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero);

        // Assert
        this.batcher.Verify(x => x.Batch(It.IsAny<float>(), Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero, 0, 0));
    }

    [Test]
    public void DrawShouldInvokeResetWhenBatcherShouldReset()
    {
        // Arrange
        this.batcher.SetupGet(x => x.ShouldReset).Returns(true);

        // Act
        this.drawer.Draw(this.texture.Object, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero);

        // Assert
        this.batcher.Verify(x => x.Reset(), Times.Once);
    }

    [Test]
    public void DrawShouldInvokeResetWhenBinderShouldReset()
    {
        // Arrange
        this.binder.SetupGet(x => x.ShouldReset).Returns(true);

        // Act
        this.drawer.Draw(this.texture.Object, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero);

        // Assert
        this.binder.Verify(x => x.Reset(), Times.Once);
    }

    [Test]
    public void DrawShouldThrowArgumentNullExceptionWhenTextureIsNullAndNotDisposed()
    {
        // Act
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.drawer.Draw(null, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero);
        });
    }

    [Test]
    public void DrawShouldThrowObjectDisposedExceptionWhenDrawerIsDisposed()
    {
        // Arrange
        this.drawer.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            this.drawer.Draw(null, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero);
        });
    }

    [Test]
    public void EndShouldInvokeDrawIndicesWhenNotDisposedAndVertexBufferIsNotNull()
    {
        // Act
        this.drawer.End();

        // Assert
        this.renderDevice.Verify(x => x.DrawIndices(PrimitiveTopology.Triangle, 0, this.batcher.Object.CurrentIndexCount), Times.Once);
    }

    [Test]
    public void EndShouldInvokeSetIndexBufferWhenNotDisposedAndVertexBufferIsNotNull()
    {
        // Act
        this.drawer.End();

        // Assert
        this.inputAssembler.Verify(x => x.SetIndexBuffer(this.indexBuffer.Object), Times.Once);
    }

    [Test]
    public void EndShouldInvokeSetInputLayoutWhenNotDisposedAndVertexBufferIsNotNull()
    {
        // Act
        this.drawer.End();

        // Assert
        this.inputAssembler.Verify(x => x.SetInputLayout(this.inputLayout.Object), Times.Once);
    }

    [Test]
    public void EndShouldInvokeSetVertexBufferWhenNotDisposedAndVertexBufferIsNotNull()
    {
        // Act
        this.drawer.End();

        // Assert
        this.inputAssembler.Verify(x => x.SetVertexBuffer(this.vertexBuffer.Object), Times.Once);
    }

    [Test]
    public void EndShouldInvokeUpdateBatchWhenNotDisposedAndVertexBufferIsNotNull()
    {
        // Act
        this.drawer.End();

        // Assert
        this.batcher.Verify(x => x.Update(this.vertexBuffer.Object), Times.Once);
    }

    [Test]
    public void EndShouldThrowObjectDisposedExceptionWhenDrawerIsDisposed()
    {
        // Arrange
        this.drawer.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(this.drawer.End);
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        this.vertexShader = new Mock<IShader>();
        this.fragmentShader = new Mock<IShader>();
        this.resourceLoader = new Mock<ResourceLoaderBase<IShader>>();

        this.resourceLoader.Setup(x => x.LoadResource("Resources\\Shaders\\sprite-geometry.vert")).Returns(this.vertexShader.Object);
        this.resourceLoader.Setup(x => x.LoadResource("Resources\\Shaders\\sprite-geometry.frag")).Returns(this.fragmentShader.Object);

        ResourceManager.Instance.RegisterLoader(this.resourceLoader.Object);
    }

    [Test]
    public void ProjectionSetShouldSetProjectionWhenInvoked()
    {
        // Arrange
        var expected = Matrix4x4.Identity;

        // Act
        this.drawer.Projection = expected;

        // Assert
        Assert.AreEqual(expected, this.drawer.Projection);
    }

    [SetUp]
    public void Setup()
    {
        this.renderDevice = new Mock<IRenderDevice>();
        this.batcher = new Mock<ISpriteBatcher>();
        this.binder = new Mock<ITextureBinder>();

        this.batcher.SetupGet(x => x.MaxVertexCount).Returns(1000);
        this.batcher.SetupGet(x => x.MaxIndexCount).Returns(6000);

        this.factory = new Mock<IGPUResourceFactory>();
        this.pipeline = new Mock<IPipeline>();
        this.inputAssembler = new Mock<IInputAssembler>();
        this.rasterizer = new Mock<IRasterizer>();
        this.outputMerger = new Mock<IOutputMerger>();

        this.renderDevice.SetupGet(x => x.Factory).Returns(this.factory.Object);
        this.renderDevice.SetupGet(x => x.Pipeline).Returns(this.pipeline.Object);
        this.renderDevice.SetupGet(x => x.InputAssembler).Returns(this.inputAssembler.Object);
        this.renderDevice.SetupGet(x => x.Rasterizer).Returns(this.rasterizer.Object);
        this.renderDevice.Setup(x => x.OutputMerger).Returns(this.outputMerger.Object);

        this.shaderProgram = new Mock<IShaderProgram>();
        this.inputLayout = new Mock<IInputLayout>();
        this.vertexBuffer = new Mock<IVertexBuffer>();
        this.indexBuffer = new Mock<IIndexBuffer>();

        this.factory.Setup(x => x.CreateShaderProgram(new[] { this.vertexShader.Object, this.fragmentShader.Object })).Returns(this.shaderProgram.Object);
        this.factory.Setup(x => x.CreateInputLayout(SpriteVertex.InputElements)).Returns(this.inputLayout.Object);
        this.factory.Setup(x => x.CreateVertexBuffer(BufferUsageType.Dynamic, It.IsAny<SpriteVertex[]>(), this.batcher.Object.MaxVertexCount * SpriteVertex.SizeInBytes, SpriteVertex.SizeInBytes)).Returns(this.vertexBuffer.Object);
        this.factory.Setup(x => x.CreateIndexBuffer(BufferUsageType.Static, It.IsAny<int[]>(), this.batcher.Object.MaxIndexCount * sizeof(int))).Returns(this.indexBuffer.Object);

        this.texture = new Mock<ITexture2D>();

        this.drawer = new SpriteDrawer(this.renderDevice.Object, this.batcher.Object, this.binder.Object, ProjectionWidth, ProjectionHeight);
    }

    [TearDown]
    public void TearDown()
    {
        this.drawer.Dispose();
    }

    [Test]
    public void TransformSetShouldSetTransformWhenInvoked()
    {
        // Arrange
        var expected = Matrix4x4.CreateTranslation(new Vector3(1, 2, 3));

        // Act
        this.drawer.Transform = expected;

        // Assert
        Assert.AreEqual(expected, this.drawer.Transform);
    }
}
