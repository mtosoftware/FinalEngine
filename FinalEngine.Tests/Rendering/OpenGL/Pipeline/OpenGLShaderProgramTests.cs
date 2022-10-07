// <copyright file="OpenGLShaderProgramTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Rendering.Exceptions;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.OpenGL.Pipeline;
    using Moq;
    using NUnit.Framework;

    public class OpenGLShaderProgramTests
    {
        private const int ID = 567;

        private Mock<IOpenGLInvoker> invoker;

        private OpenGLShaderProgram program;

        private Mock<IOpenGLShader> shader;

        private IReadOnlyCollection<IOpenGLShader> shaders;

        [Test]
        public void BindShouldInvokeUseProgramWhenProgramIsNotDisposed()
        {
            // Act
            this.program.Bind();

            // Assert
            this.invoker.Verify(x => x.UseProgram(ID), Times.Once);
        }

        [Test]
        public void BindShouldThrowObjectDisposedExceptionWhenProgramIsDisposed()
        {
            // Arrange
            this.program.Dispose();

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() =>
            {
                this.program.Bind();
            });
        }

        [Test]
        public void ConstructorShouldInvokeAttachWhenInvoked()
        {
            // Assert
            this.shader.Verify(x => x.Attach(ID), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeCreateProgramWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.CreateProgram(), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeGetProgramInfoLogWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.GetProgramInfoLog(ID), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeLinkProgramWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.LinkProgram(ID), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeValidateProgramWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.ValidateProgram(ID), Times.Once);
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OpenGLShaderProgram(null, this.shaders);
            });
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenShadersIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OpenGLShaderProgram(this.invoker.Object, null);
            });
        }

        [Test]
        public void ConstructorShouldThrowProgramLinkingErrorExceptionWhenGetProgramInfoLogReturnsNotNullEmptyOrWhitspace()
        {
            // Arrange
            this.invoker.Setup(x => x.GetProgramInfoLog(ID)).Returns("test");

            // Act and assert
            Assert.Throws<ProgramLinkingErrorException>(() =>
            {
                new OpenGLShaderProgram(this.invoker.Object, this.shaders);
            });
        }

        [Test]
        public void DisposeShouldInvokeDeleteProgramWhenProgramIsNotDisposed()
        {
            // Act
            this.program.Dispose();

            // Assert
            this.invoker.Verify(x => x.DeleteProgram(ID), Times.Once);
        }

        [Test]
        public void GetUniformLocationShouldInvokeGetUniformLocationWhenNameIsNotNullAndProgramIsNotDisposed()
        {
            // Act
            this.program.GetUniformLocation("test");

            // Assert
            this.invoker.Verify(x => x.GetUniformLocation(ID, "test"), Times.Once);
        }

        [Test]
        public void GetUniformLocationShouldThrowArgumentNullExceptionWhenNameIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.program.GetUniformLocation(string.Empty);
            });
        }

        [Test]
        public void GetUniformLocationShouldThrowArgumentNullExceptionWhenNameIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.program.GetUniformLocation(null);
            });
        }

        [Test]
        public void GetUniformLocationShouldThrowArgumentNullExceptionWhenNameIsWhitespace()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.program.GetUniformLocation("\t\r\n");
            });
        }

        [Test]
        public void GetUniformLocationShouldThrowObjectDisposedExceptionWhenProgramIsDisposed()
        {
            // Arrange
            this.program.Dispose();

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() =>
            {
                this.program.GetUniformLocation(null);
            });
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.invoker = new Mock<IOpenGLInvoker>();
            this.invoker.Setup(x => x.CreateProgram()).Returns(ID);

            this.shader = new Mock<IOpenGLShader>();

            this.shaders = new List<IOpenGLShader>()
            {
                null,
                this.shader.Object,
            };

            this.program = new OpenGLShaderProgram(this.invoker.Object, this.shaders);
        }

        [TearDown]
        public void Teardown()
        {
            this.program.Dispose();
        }
    }
}
