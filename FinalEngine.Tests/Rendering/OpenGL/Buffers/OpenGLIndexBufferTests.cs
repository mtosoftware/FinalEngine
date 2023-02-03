// <copyright file="OpenGLIndexBufferTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL.Buffers;

using System;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.OpenGL.Buffers;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Utilities;
using Moq;
using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;

public class OpenGLIndexBufferTests
{
    private const int ID = 10;

    private readonly int[] data = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    private OpenGLIndexBuffer<int> indexBuffer;

    private Mock<IOpenGLInvoker> invoker;

    private Mock<IEnumMapper> mapper;

    [Test]
    public void BindShouldInvokeBindBufferIdentifierWhenBufferIsNotDisposed()
    {
        // Arrange
        this.invoker.Reset();

        // Act
        this.indexBuffer.Bind();

        // Assert
        this.invoker.Verify(x => x.BindBuffer(BufferTarget.ElementArrayBuffer, ID), Times.Once);
    }

    [Test]
    public void BindShouldThrowObjectDisposedExceptionWhenBufferIsDisposed()
    {
        // Arrange
        this.indexBuffer.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(this.indexBuffer.Bind);
    }

    [Test]
    public void ConstructorShouldInvokeCreateBufferWhenInvoked()
    {
        // Assert
        this.invoker.Verify(x => x.CreateBuffer(), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeNamedBufferDataWhenInvoked()
    {
        // Assert
        this.invoker.Verify(x => x.NamedBufferData(ID, this.data.Length * sizeof(int), this.data, BufferUsageHint.DynamicRead), Times.Once);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenDataIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLIndexBuffer<int>(this.invoker.Object, this.mapper.Object, BufferUsageHint.DynamicCopy, null, 0);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLIndexBuffer<int>(null, this.mapper.Object, BufferUsageHint.StreamDraw, this.data, 0);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenMapperIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLIndexBuffer<int>(this.invoker.Object, null, BufferUsageHint.StaticRead, this.data, this.data.Length * sizeof(int));
        });
    }

    [Test]
    public void DisposeShouldInvokeDeleteBufferIdentifierWhenBufferIsNotDisposed()
    {
        // Act
        this.indexBuffer.Dispose();

        // Assert
        this.invoker.Verify(x => x.DeleteBuffer(ID), Times.Once);
    }

    [Test]
    public void LengthShouldReturnSameAsDataLength()
    {
        // Assert
        Assert.AreEqual(this.data.Length, this.indexBuffer.Length);
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.invoker = new Mock<IOpenGLInvoker>();
        this.invoker.Setup(x => x.CreateBuffer()).Returns(ID);

        this.mapper = new Mock<IEnumMapper>();
        this.mapper.Setup(x => x.Reverse<BufferUsageType>(It.IsAny<BufferUsageHint>())).Returns(BufferUsageType.Dynamic);

        this.indexBuffer = new OpenGLIndexBuffer<int>(this.invoker.Object, this.mapper.Object, BufferUsageHint.DynamicRead, this.data, this.data.Length * sizeof(int));
    }

    [TearDown]
    public void Teardown()
    {
        this.indexBuffer.Dispose();
    }

    [Test]
    public void TypeShouldReturnDynamicWhenInvoked()
    {
        // Arrange
        const BufferUsageType expected = BufferUsageType.Dynamic;

        // Act
        var actual = this.indexBuffer.Type;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void UpdateShouldInvokeNamedBufferSubDataWhenInvoked()
    {
        // Arrange
        int[] array = new int[12];

        // Act
        this.indexBuffer.Update(array);

        // Assert
        this.invoker.Verify(x => x.NamedBufferSubData(ID, IntPtr.Zero, array.Length * Marshal.SizeOf<int>(), array));
    }

    [Test]
    public void UpdateShouldSetLengthPropertyToTwelveWhenInvoked()
    {
        // Arrange
        const int expected = 12;
        int[] array = new int[12];

        // Act
        this.indexBuffer.Update(array);

        // Assert
        Assert.AreEqual(expected, this.indexBuffer.Length);
    }

    [Test]
    public void UpdateShouldThrowArgumentNullExceptionWhenDataIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.indexBuffer.Update<int>(null);
        });
    }

    [Test]
    public void UpdateShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.indexBuffer.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            this.indexBuffer.Update(Array.Empty<int>());
        });
    }
}
