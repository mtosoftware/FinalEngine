// <copyright file="OpenGLGPUResourceFactoryTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL
{
    using System;
    using System.Collections.Generic;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Buffers;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.OpenGL.Pipeline;
    using FinalEngine.Rendering.OpenGL.Textures;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Rendering.Textures;
    using FinalEngine.Utilities;
    using Moq;
    using NUnit.Framework;
    using OpenTK.Graphics.OpenGL4;

    public class OpenGLGPUResourceFactoryTests
    {
        private OpenGLGPUResourceFactory factory;

        private Mock<IOpenGLInvoker> invoker;

        private Mock<IEnumMapper> mapper;

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLGPUResourceFactory(null, this.mapper.Object));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenMapperIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLGPUResourceFactory(this.invoker.Object, null));
        }

        [Test]
        public void CreateIndexBufferShouldReturnOpenGLIndexBufferWhenInvoked()
        {
            // Act
            IIndexBuffer actual = this.factory.CreateIndexBuffer(BufferUsageType.Static, Array.Empty<int>(), 0);

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLIndexBuffer<int>), actual);
        }

        [Test]
        public void CreateIndexBufferShouldThrowArgumentNullExceptionWhenDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.factory.CreateIndexBuffer<int>(BufferUsageType.Dynamic, null, 0));
        }

        [Test]
        public void CreateInputLayoutShouldReturnOpenGLInputLayoutWhenInvoked()
        {
            // Act
            IReadOnlyCollection<InputElement> elements = new List<InputElement>();
            IInputLayout actual = this.factory.CreateInputLayout(elements);

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLInputLayout), actual);
        }

        [Test]
        public void CreateInputLayoutShouldThrowArgumentNullExceptionWhenElementsIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.factory.CreateInputLayout(null));
        }

        [Test]
        public void CreateShaderProgramShouldReturnOpenGLShaderProgramWhenInvoked()
        {
            // Act
            IReadOnlyCollection<IOpenGLShader> shaders = new List<IOpenGLShader>();
            IShaderProgram actual = this.factory.CreateShaderProgram(shaders);

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLShaderProgram), actual);
        }

        [Test]
        public void CreateShaderProgramShouldThrowArgumentNullExceptionWhenShadersIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.factory.CreateShaderProgram(null));
        }

        [Test]
        public void CreateShaderProgramShouldThrowInvalidCastExceptionWhenShadersContainsNotOpenGLShader()
        {
            // Arrange
            IReadOnlyCollection<IShader> shaders = new List<IShader>()
            {
                new Mock<IShader>().Object,
                new OpenGLShader(this.invoker.Object, this.mapper.Object, ShaderType.VertexShader, "test"),
            };

            // Act and assert
            Assert.Throws<InvalidCastException>(() => this.factory.CreateShaderProgram(shaders));
        }

        [Test]
        public void CreateShaderShouldReturnOpenGLShaderWhenInvoked()
        {
            // Act
            IShader actual = this.factory.CreateShader(PipelineTarget.Vertex, "test");

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLShader), actual);
        }

        [Test]
        public void CreateShaderShouldThrowArgumentNullExceptionWhenSourceCodeIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.factory.CreateShader(PipelineTarget.Vertex, string.Empty));
        }

        [Test]
        public void CreateShaderShouldThrowArgumentNullExceptionWhenSourceCodeIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.factory.CreateShader(PipelineTarget.Vertex, null));
        }

        [Test]
        public void CreateShaderShouldThrowArgumentNullExceptionWhenSourceCodeIsWhitespace()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.factory.CreateShader(PipelineTarget.Vertex, "\r\n\t"));
        }

        [Test]
        public void CreateTexture2DShouldNotThrowArgumentNullExceptionWhenDataIsNull()
        {
            // Act and assert
            Assert.DoesNotThrow(() => this.factory.CreateTexture2D<int>(default, null));
        }

        [Test]
        public void CreateTexture2DShouldReturnOpenGLTexture2DWhenInvoked()
        {
            // Act
            ITexture2D actual = this.factory.CreateTexture2D(default, Array.Empty<int>());

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLTexture2D), actual);
        }

        [Test]
        public void CreateVertexBufferShouldReturnOpenGLVertexBufferWhenInvoked()
        {
            // Act
            IVertexBuffer actual = this.factory.CreateVertexBuffer(BufferUsageType.Dynamic, Array.Empty<int>(), 0, 0);

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLVertexBuffer<int>), actual);
        }

        [Test]
        public void CreateVertexBufferShouldThrowArgumentNullExceptionWhenDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.factory.CreateVertexBuffer<int>(BufferUsageType.Static, null, 0, 0));
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.invoker = new Mock<IOpenGLInvoker>();
            this.mapper = new Mock<IEnumMapper>();

            this.factory = new OpenGLGPUResourceFactory(this.invoker.Object, this.mapper.Object);
        }
    }
}