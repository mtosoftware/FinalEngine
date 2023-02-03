// <copyright file="OpenGLShaderTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL.Pipeline;

using System;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.OpenGL.Pipeline;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Utilities;
using Moq;
using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;

public class OpenGLShaderTests
{
    private const int ID = 34;

    private readonly string sourceCode = "source code";

    private Mock<IOpenGLInvoker> invoker;

    private Mock<IEnumMapper> mapper;

    private OpenGLShader shader;

    [Test]
    public void AttachShouldInvokeAttachShaderWhenShaderIsNotDisposed()
    {
        // Act
        this.shader.Attach(17);

        // Assert
        this.invoker.Verify(x => x.AttachShader(17, ID), Times.Once);
    }

    [Test]
    public void AttachShouldThrowObjectDisposedExceptionWhenShaderIsDisposed()
    {
        // Arrange
        this.shader.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            this.shader.Attach(0);
        });
    }

    [Test]
    public void ConstructorShouldInvokeCompileShaderWhenInvoked()
    {
        // Assert
        this.invoker.Verify(x => x.CompileShader(ID), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeCreateShaderWhenInvoked()
    {
        // Assert
        this.invoker.Verify(x => x.CreateShader(ShaderType.VertexShader), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeGetShaderInfoLogWhenInvoked()
    {
        // Assert
        this.invoker.Verify(x => x.GetShaderInfoLog(ID), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeReversePipelineTargetWhenInvoked()
    {
        // Assert
        this.mapper.Verify(x => x.Reverse<PipelineTarget>(ShaderType.VertexShader), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeShaderSourceWhenInvoked()
    {
        // Assert
        this.invoker.Verify(x => x.ShaderSource(ID, this.sourceCode), Times.Once);
    }

    [Test]
    public void ConstructorShouldThrowArgumentExceptionWhenSourceCodeIsEmpty()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            new OpenGLShader(this.invoker.Object, this.mapper.Object, ShaderType.VertexShader, string.Empty);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentExceptionWhenSourceCodeIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            new OpenGLShader(this.invoker.Object, this.mapper.Object, ShaderType.VertexShader, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentExceptionWhenSourceCodeIsWhitespace()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            new OpenGLShader(this.invoker.Object, this.mapper.Object, ShaderType.VertexShader, "\t\r\n");
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLShader(null, this.mapper.Object, ShaderType.VertexShader, this.sourceCode);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenMapperIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLShader(this.invoker.Object, null, ShaderType.VertexShader, this.sourceCode);
        });
    }

    [Test]
    public void ConstructorShouldThrowShaderCompilationErrorExceptionWhenGetShaderInfoLogReturnsNotNullEmptyOrWhitspace()
    {
        // Arrange
        this.invoker.Setup(x => x.GetShaderInfoLog(ID)).Returns("test");

        // Act and assert
        Assert.Throws<ShaderCompilationErrorException>(() =>
        {
            new OpenGLShader(this.invoker.Object, this.mapper.Object, ShaderType.VertexShader, this.sourceCode);
        });
    }

    [Test]
    public void DisposeShouldInvokeDeleteShaderWhenShaderIsNotDisposed()
    {
        // Act
        this.shader.Dispose();

        // Assert
        this.invoker.Verify(x => x.DeleteShader(ID), Times.Once);
    }

    [Test]
    public void EntryPointShouldReturnVertexWhenInvoked()
    {
        // Act
        var actual = this.shader.EntryPoint;

        // Assert
        Assert.AreEqual(PipelineTarget.Vertex, actual);
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.invoker = new Mock<IOpenGLInvoker>();
        this.invoker.Setup(x => x.CreateShader(ShaderType.VertexShader)).Returns(ID);

        this.mapper = new Mock<IEnumMapper>();
        this.mapper.Setup(x => x.Forward<PipelineTarget>(ShaderType.VertexShader)).Returns(PipelineTarget.Vertex);

        this.shader = new OpenGLShader(this.invoker.Object, this.mapper.Object, ShaderType.VertexShader, this.sourceCode);
    }

    [TearDown]
    public void Teardown()
    {
        this.shader.Dispose();
    }
}
