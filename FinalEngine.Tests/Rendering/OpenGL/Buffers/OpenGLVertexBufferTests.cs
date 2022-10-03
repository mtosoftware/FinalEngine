// <copyright file="OpenGLVertexBufferTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL.Buffers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.OpenGL.Buffers;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Utilities;
    using Moq;
    using NUnit.Framework;
    using OpenTK.Graphics.OpenGL4;

    [SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "This is done in TearDown.")]
    public class OpenGLVertexBufferTests
    {
        private const int ID = 42;

        private const int Stride = 32;

        private readonly int[] data = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };

        private Mock<IOpenGLInvoker> invoker;

        private Mock<IEnumMapper> mapper;

        private OpenGLVertexBuffer<int> vertexBuffer;

        [Test]
        public void BindShouldInvokeBindVertexBufferWhenBufferIsNotDisposed()
        {
            // Act
            this.vertexBuffer.Bind();

            // Assert
            this.invoker.Verify(x => x.BindVertexBuffer(0, ID, IntPtr.Zero, this.vertexBuffer.Stride), Times.Once);
        }

        [Test]
        public void BindShouldThrowObjectDisposedExceptionWhenBufferIsDisposed()
        {
            // Arrange
            this.vertexBuffer.Dispose();

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() => this.vertexBuffer.Bind());
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
            this.invoker.Verify(x => x.NamedBufferData(ID, this.data.Length * sizeof(int), this.data, BufferUsageHint.StreamCopy), Times.Once);
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenDataIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLVertexBuffer<int>(this.invoker.Object, this.mapper.Object, BufferUsageHint.DynamicRead, null, this.data.Length * sizeof(int), 0));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLVertexBuffer<int>(null, this.mapper.Object, BufferUsageHint.DynamicCopy, this.data, this.data.Length * sizeof(int), Stride));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenMapperIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLVertexBuffer<int>(this.invoker.Object, null, BufferUsageHint.StreamDraw, this.data, this.data.Length * sizeof(int), Stride));
        }

        [Test]
        public void DisposeShouldInvokeDeleteBufferWhenBufferIsNotDisposed()
        {
            // Act
            this.vertexBuffer.Dispose();

            // Assert
            this.invoker.Verify(x => x.DeleteBuffer(ID), Times.Once);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.invoker = new Mock<IOpenGLInvoker>();
            this.invoker.Setup(x => x.CreateBuffer()).Returns(ID);

            this.mapper = new Mock<IEnumMapper>();
            this.mapper.Setup(x => x.Reverse<BufferUsageType>(It.IsAny<BufferUsageHint>())).Returns(BufferUsageType.Static);

            this.vertexBuffer = new OpenGLVertexBuffer<int>(this.invoker.Object, this.mapper.Object, BufferUsageHint.StreamCopy, this.data, this.data.Length * sizeof(int), Stride);
        }

        [Test]
        public void StrideShouldReturnSameAsStrideWhenInvoked()
        {
            // Assert
            Assert.AreEqual(Stride, this.vertexBuffer.Stride);
        }

        [TearDown]
        public void Teardown()
        {
            this.vertexBuffer.Dispose();
        }

        [Test]
        public void TypeShouldReturnStaticWhenInvoked()
        {
            // Arrange
            const BufferUsageType Expected = BufferUsageType.Static;

            // Act
            BufferUsageType actual = this.vertexBuffer.Type;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void UpdateSholdThrowArgumentNullExceptionWhenDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.vertexBuffer.Update<int>(null, 0));
        }

        [Test]
        public void UpdateShouldInvokeNamedBufferSubDataWhenInvoked()
        {
            // Arrange
            int[] array = new int[12];

            // Act
            this.vertexBuffer.Update(array, 0);

            // Assert
            this.invoker.Verify(x => x.NamedBufferSubData(ID, IntPtr.Zero, array.Length * Marshal.SizeOf<int>(), array));
        }

        [Test]
        public void UpdateShouldSetStridePropertyToSevenWhenInvoked()
        {
            // Arrange
            const int Expected = 7;

            // Act
            this.vertexBuffer.Update(Array.Empty<int>(), Expected);

            // Assert
            Assert.AreEqual(Expected, this.vertexBuffer.Stride);
        }

        [Test]
        public void UpdateShouldThrowObjectDisposedExceptionWhenDisposed()
        {
            // Arrange
            this.vertexBuffer.Dispose();

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() => this.vertexBuffer.Update(Array.Empty<int>(), 0));
        }
    }
}