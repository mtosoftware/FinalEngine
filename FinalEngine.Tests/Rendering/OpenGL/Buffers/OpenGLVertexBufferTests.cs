﻿// <copyright file="OpenGLVertexBufferTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL.Buffers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Rendering.OpenGL.Buffers;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using Moq;
    using NUnit.Framework;
    using OpenTK.Graphics.OpenGL4;

    [ExcludeFromCodeCoverage]
    public class OpenGLVertexBufferTests
    {
        private int[] data;

        private int id;

        private Mock<IOpenGLInvoker> invoker;

        private OpenGLVertexBuffer<int> vertexBuffer;

        [Test]
        public void BindShouldInvokeBindVertexBufferWhenVertexBufferIsNotDisposed()
        {
            // Act
            this.vertexBuffer.Bind();

            // Assert
            this.invoker.Verify(x => x.BindVertexBuffer(0, this.id, IntPtr.Zero, this.vertexBuffer.Stride), Times.Once);
        }

        [Test]
        public void BindShouldThrowObjectDisposedExceptionWhenVertexBufferIsDisposed()
        {
            // Arrange
            this.vertexBuffer.Dispose();

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() => this.vertexBuffer.Bind());
        }

        [Test]
        public void ConstructorShouldInvokeBindBufferIdentifierWhenParametersAreNotNull()
        {
            // Assert
            this.invoker.Verify(x => x.BindBuffer(BufferTarget.ArrayBuffer, this.id), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeBindBufferNoneWhenParametersAreNotNull()
        {
            // Assert
            this.invoker.Verify(x => x.BindBuffer(BufferTarget.ArrayBuffer, 0), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeBufferDataWhenParametersAreNotNulL()
        {
            // Assert
            this.invoker.Verify(x => x.BufferData<int>(BufferTarget.ArrayBuffer, this.data.Length * sizeof(int), this.data, BufferUsageHint.StaticDraw), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeGenBufferWhenParametersAreNotNull()
        {
            // Assert
            this.invoker.Verify(x => x.GenBuffer(), Times.Once);
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenDataIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLVertexBuffer<int>(new Mock<IOpenGLInvoker>().Object, null, 0, 0));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLVertexBuffer<int>(null, Array.Empty<int>(), 0, 0));
        }

        [SetUp]
        public void Setup()
        {
            this.id = 42;

            this.invoker = new Mock<IOpenGLInvoker>();
            this.invoker.Setup(x => x.GenBuffer()).Returns(this.id);

            this.data = new int[]
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
            };

            this.vertexBuffer = new OpenGLVertexBuffer<int>(this.invoker.Object, this.data, this.data.Length * sizeof(int), 1);
        }

        [Test]
        public void StrideShouldReturnSameAsConstructorInputWhenInvoked()
        {
            // Act
            int actual = this.vertexBuffer.Stride;

            // Assert
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void Teardown()
        {
            this.vertexBuffer.Dispose();

            // Dispose once more just to be safe.
            this.vertexBuffer.Dispose();
        }
    }
}