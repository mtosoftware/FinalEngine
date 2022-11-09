// <copyright file="OpenGLInputAssemblerTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL
{
    using System;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Buffers;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using Moq;
    using NUnit.Framework;

    public class OpenGLInputAssemblerTests
    {
        private Mock<IOpenGLIndexBuffer> indexBuffer;

        private OpenGLInputAssembler inputAssembler;

        private Mock<IOpenGLInvoker> invoker;

        private Mock<IOpenGLVertexBuffer> vertexBuffer;

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OpenGLInputAssembler(null);
            });
        }

        [Test]
        public void SetIndexBufferShouldInvokeBindWhenInvoked()
        {
            // Arrange
            var indexBuffer = new Mock<IOpenGLIndexBuffer>();

            // Act
            this.inputAssembler.SetIndexBuffer(indexBuffer.Object);

            // Assert
            indexBuffer.Verify(x => x.Bind(), Times.Once);
        }

        [Test]
        public void SetIndexBufferShouldThrowArgumentExceptionWhenBufferIsNotIOpenGLIndexBuffer()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.inputAssembler.SetIndexBuffer(Mock.Of<IIndexBuffer>());
            });
        }

        [Test]
        public void SetIndexBufferShouldThrowArgumentNullExceptionWhenBufferIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.inputAssembler.SetIndexBuffer(null);
            });
        }

        [Test]
        public void SetInputLayoutShouldInvokeBindWhenInvoked()
        {
            // Arrange
            var layout = new Mock<IOpenGLInputLayout>();

            // Act
            this.inputAssembler.SetInputLayout(layout.Object);

            // Assert
            layout.Verify(x => x.Bind(), Times.Once);
        }

        [Test]
        public void SetInputLayoutShouldInvokeBoundLayoutResetWhenInvokedAndLayoutHasBeenPreviouslyBound()
        {
            // Arrange
            var layout = new Mock<IOpenGLInputLayout>();

            this.inputAssembler.SetInputLayout(layout.Object);

            layout.Reset();

            // Act
            this.inputAssembler.SetInputLayout(layout.Object);

            // Assert
            layout.Verify(x => x.Unbind(), Times.Once);
        }

        [Test]
        public void SetInputLayoutShouldInvokeBoundLayoutResetWhenLayoutIsNullAndLayoutHasPreviouslyBeenBound()
        {
            // Arrange
            var layout = new Mock<IOpenGLInputLayout>();

            this.inputAssembler.SetInputLayout(layout.Object);

            layout.Reset();

            // Act
            this.inputAssembler.SetInputLayout(null);

            // Assert
            layout.Verify(x => x.Unbind(), Times.Once);
        }

        [Test]
        public void SetInputLayoutShouldNotInvokeBoundLayoutResetWhenLayoutIsNullAndLayoutHasNotBeenPreviouslyBound()
        {
            // Act
            this.inputAssembler.SetInputLayout(null);

            // Assert
            Assert.Pass();
        }

        [Test]
        public void SetInputLayoutShouldThrowArgumentExceptionWhenLayoutIsNotIOpenGLInputLayout()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.inputAssembler.SetInputLayout(Mock.Of<IInputLayout>());
            });
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.invoker = new Mock<IOpenGLInvoker>();
            this.vertexBuffer = new Mock<IOpenGLVertexBuffer>();
            this.indexBuffer = new Mock<IOpenGLIndexBuffer>();

            this.inputAssembler = new OpenGLInputAssembler(this.invoker.Object);
        }

        [Test]
        public void SetVertexBufferShouldInvokeBindWhenInvoked()
        {
            // Arrange
            var vertexBuffer = new Mock<IOpenGLVertexBuffer>();

            // Act
            this.inputAssembler.SetVertexBuffer(vertexBuffer.Object);

            // Assert
            vertexBuffer.Verify(x => x.Bind(), Times.Once);
        }

        [Test]
        public void SetVertexBufferShouldThrowArgumentExceptionWhenBufferIsNotIOpenGLVertexBuffer()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.inputAssembler.SetVertexBuffer(Mock.Of<IVertexBuffer>());
            });
        }

        [Test]
        public void SetVertexBufferShouldThrowArgumentNullExceptionWhenBufferIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.inputAssembler.SetVertexBuffer(null);
            });
        }

        [Test]
        public void UpdateIndexBufferShouldInvokeIndexBufferUpdate()
        {
            // Act
            this.inputAssembler.UpdateIndexBuffer(this.indexBuffer.Object, Array.Empty<int>());

            // Assert
            this.indexBuffer.Verify(x => x.Update(Array.Empty<int>()));
        }

        [Test]
        public void UpdateIndexBufferShouldThrowArgumentExceptionWhenBufferIsNotOpenGLIndexBuffer()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.inputAssembler.UpdateIndexBuffer(Mock.Of<IIndexBuffer>(), Array.Empty<int>());
            });
        }

        [Test]
        public void UpdateIndexBufferShouldThrowArgumentNullExceptionWhenBufferIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.inputAssembler.UpdateIndexBuffer(null, Array.Empty<int>());
            });
        }

        [Test]
        public void UpdateIndexBufferShouldThrowArgumentNullExceptionWhenDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.inputAssembler.UpdateIndexBuffer<int>(this.indexBuffer.Object, null);
            });
        }

        [Test]
        public void UpdateVertexBufferShouldInvokeVertexBufferUpdate()
        {
            // Act
            this.inputAssembler.UpdateVertexBuffer(this.vertexBuffer.Object, Array.Empty<int>(), 2);

            // Assert
            this.vertexBuffer.Verify(x => x.Update(Array.Empty<int>(), 2));
        }

        [Test]
        public void UpdateVertexBufferShouldThrowArgumentExceptionWhenBufferIsNotOpenGLVertexBuffer()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.inputAssembler.UpdateVertexBuffer(Mock.Of<IVertexBuffer>(), Array.Empty<int>(), 0);
            });
        }

        [Test]
        public void UpdateVertexBufferShouldThrowArgumentNullExceptionWhenBufferIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.inputAssembler.UpdateVertexBuffer(null, Array.Empty<int>(), 0);
            });
        }

        [Test]
        public void UpdateVertexBufferShouldThrowArgumentNullExceptionWhenDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.inputAssembler.UpdateVertexBuffer<int>(this.vertexBuffer.Object, null, 0);
            });
        }
    }
}
