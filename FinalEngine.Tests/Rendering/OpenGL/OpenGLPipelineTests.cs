// <copyright file="OpenGLPipelineTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL;

using System;
using System.Numerics;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.OpenGL.Pipeline;
using FinalEngine.Rendering.OpenGL.Textures;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using Moq;
using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;

public class OpenGLPipelineTests
{
    private Mock<IOpenGLInvoker> invoker;

    private OpenGLPipeline pipeline;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLPipeline(null);
        });
    }

    [Test]
    public void MaxTextureSlotsShouldInvokeGetIntegerWhenInvoked()
    {
        // Act
        _ = this.pipeline.MaxTextureSlots;

        // Assert
        this.invoker.Verify(x => x.GetInteger(GetPName.MaxTextureImageUnits), Times.Once);
    }

    [Test]
    public void MaxTextureSlotsShouldReturnSameAsGetIntegerWhenInvoked()
    {
        // Arrange
        const int expected = 10;
        this.invoker.Setup(x => x.GetInteger(GetPName.MaxTextureImageUnits)).Returns(expected);

        // Act
        int actual = this.pipeline.MaxTextureSlots;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void SetShaderProgramShouldInvokeProgramBindWhenProgramIsOpenGLShaderProgram()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();

        // Act
        this.pipeline.SetShaderProgram(program.Object);

        // Assert
        program.Verify(x => x.Bind(), Times.Once);
    }

    [Test]
    public void SetShaderProgramShouldThrowArgumentNullExceptionWhenProgramIsNotOpenGLShaderProgram()
    {
        // Arrange
        var program = new Mock<IShaderProgram>();

        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetShaderProgram(program.Object);
        });
    }

    [Test]
    public void SetShaderProgramShouldThrowArgumentNullExceptionWhenProgramIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.pipeline.SetShaderProgram(null);
        });
    }

    [Test]
    public void SetTextureShouldInvokeBindWhenInvoked()
    {
        // Arrange
        var texture = new Mock<IOpenGLTexture>();

        // Act
        this.pipeline.SetTexture(texture.Object);

        // Assert
        texture.Verify(x => x.Bind(0), Times.Once);
    }

    [Test]
    public void SetTextureShouldThrowArgumentExceptionWhenTextureIsNotIOpenGLTexture()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetTexture(new Mock<ITexture>().Object);
        });
    }

    [Test]
    public void SetTextureShouldThrowArgumentNullExceptionWhenTextureIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.pipeline.SetTexture(null);
        });
    }

    [Test]
    public void SetUniformBoolShouldInvokeUniform1WithOneAsParameterWhenGetUniformLocationReturnsPositiveInteger()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(0);

        this.pipeline.SetShaderProgram(program.Object);

        // Act
        this.pipeline.SetUniform("name", true);

        // Assert
        this.invoker.Verify(x => x.Uniform1(0, 1), Times.Once);
    }

    [Test]
    public void SetUniformBoolShouldInvokeUniform1WithZeroAsParameterWhenGetUniformLocationReturnsPositiveInteger()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(0);

        this.pipeline.SetShaderProgram(program.Object);

        // Act
        this.pipeline.SetUniform("name", false);

        // Assert
        this.invoker.Verify(x => x.Uniform1(0, 0), Times.Once);
    }

    [Test]
    public void SetUniformBoolShouldNotInvokeUniform1WhenBoundProgramIsNull()
    {
        // Act
        this.pipeline.SetUniform("name", true);

        // Assert
        this.invoker.Verify(x => x.Uniform1(0, 1), Times.Never);
    }

    [Test]
    public void SetUniformBoolShouldThrowArgumentExceptionWhenNameIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(string.Empty, true);
        });
    }

    [Test]
    public void SetUniformBoolShouldThrowArgumentExceptionWhenNameIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(null, true);
        });
    }

    [Test]
    public void SetUniformBoolShouldThrowArgumentExceptionWhenNameIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform("\t\r\n", true);
        });
    }

    [Test]
    public void SetUniformBoolShouldThrowUniformNotLocatedExceptionWhenGetUniformLocationReturnsNegativeOne()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(-1);

        this.pipeline.SetShaderProgram(program.Object);

        // Act and assert
        Assert.Throws<UniformNotLocatedException>(() =>
        {
            this.pipeline.SetUniform("name", true);
        });
    }

    [Test]
    public void SetUniformDoubleShouldInvokeUniform1WhenGetUniformLocationReturnsPositiveInteger()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(0);

        this.pipeline.SetShaderProgram(program.Object);

        // Act
        this.pipeline.SetUniform("name", 0.0d);

        // Assert
        this.invoker.Verify(x => x.Uniform1(0, 0.0d), Times.Once);
    }

    [Test]
    public void SetUniformDoubleShouldNotInvokeUniform1WhenBoundProgramIsNull()
    {
        // Act
        this.pipeline.SetUniform("name", 0.0d);

        // Assert
        this.invoker.Verify(x => x.Uniform1(0, 0.0d), Times.Never);
    }

    [Test]
    public void SetUniformDoubleShouldThrowArgumentExceptionWhenNameIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(string.Empty, 0.0d);
        });
    }

    [Test]
    public void SetUniformDoubleShouldThrowArgumentExceptionWhenNameIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(null, 0.0d);
        });
    }

    [Test]
    public void SetUniformDoubleShouldThrowArgumentExceptionWhenNameIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform("\t\r\n", 0.0d);
        });
    }

    [Test]
    public void SetUniformDoubleShouldThrowUniformNotLocatedExceptionWhenGetUniformLocatioReturnsNegativeOne()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(-1);

        this.pipeline.SetShaderProgram(program.Object);

        // Act and assert
        Assert.Throws<UniformNotLocatedException>(() =>
        {
            this.pipeline.SetUniform("name", 0.0d);
        });
    }

    [Test]
    public void SetUniformFloatShouldInvokeUniform1WhenGetUniformLocationReturnsPositiveInteger()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(0);

        this.pipeline.SetShaderProgram(program.Object);

        // Act
        this.pipeline.SetUniform("name", 1.0f);

        // Assert
        this.invoker.Verify(x => x.Uniform1(0, 1.0f), Times.Once);
    }

    [Test]
    public void SetUniformFloatShouldNotInvokeUniform1WhenBoundProgramIsNull()
    {
        // Act
        this.pipeline.SetUniform("name", 1.0f);

        // Assert
        this.invoker.Verify(x => x.Uniform1(0, 1.0f), Times.Never);
    }

    [Test]
    public void SetUniformFloatShouldThrowArgumentExceptionWhenNameIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(string.Empty, 1.0f);
        });
    }

    [Test]
    public void SetUniformFloatShouldThrowArgumentExceptionWhenNameIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(null, 1.0f);
        });
    }

    [Test]
    public void SetUniformFloatShouldThrowArgumentExceptionWhenNameIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform("\t\r\n", 1.0f);
        });
    }

    [Test]
    public void SetUniformFloatShouldThrowUniformNotLocatedExceptionWhenGetUniformLocationReturnsNegativeOne()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(-1);

        this.pipeline.SetShaderProgram(program.Object);

        // Act and assert
        Assert.Throws<UniformNotLocatedException>(() =>
        {
            this.pipeline.SetUniform("name", 1.0f);
        });
    }

    [Test]
    public void SetUniformIntShouldInvokeUniform1WhenGetUniformLocationReturnsPositiveInteger()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(0);

        this.pipeline.SetShaderProgram(program.Object);

        // Act
        this.pipeline.SetUniform("name", 0);

        // Assert
        this.invoker.Verify(x => x.Uniform1(0, 0), Times.Once);
    }

    [Test]
    public void SetUniformIntShouldNotInvokeUniform1WhenBoundProgramIsNull()
    {
        // Act
        this.pipeline.SetUniform("name", 0);

        // Assert
        this.invoker.Verify(x => x.Uniform1(0, 0), Times.Never);
    }

    [Test]
    public void SetUniformIntShouldThrowArgumentExceptionWhenNameIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(string.Empty, 0);
        });
    }

    [Test]
    public void SetUniformIntShouldThrowArgumentExceptionWhenNameIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(null, 0);
        });
    }

    [Test]
    public void SetUniformIntShouldThrowArgumentExceptionWhenNameIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform("\t\r\n", 0);
        });
    }

    [Test]
    public void SetUniformIntShouldThrowUniformNotLocatedExceptionWhenGetUniformLocationReturnsNegativeOne()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(-1);

        this.pipeline.SetShaderProgram(program.Object);

        // Act and assert
        Assert.Throws<UniformNotLocatedException>(() =>
        {
            this.pipeline.SetUniform("name", 0);
        });
    }

    [Test]
    public void SetUniformMatrix4x4ShouldInvokeUniformMatrix4WhenGetUniformLocationReturnsPositiveInteger()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(0);

        this.pipeline.SetShaderProgram(program.Object);

        var value = Matrix4x4.Identity;

        float[] values =
        {
            value.M11, value.M12, value.M13, value.M14,
            value.M21, value.M22, value.M23, value.M24,
            value.M31, value.M32, value.M33, value.M34,
            value.M41, value.M42, value.M43, value.M44,
        };

        // Act
        this.pipeline.SetUniform("name", Matrix4x4.Identity);

        // Assert
        this.invoker.Verify(x => x.UniformMatrix4(0, 1, false, values), Times.Once);
    }

    [Test]
    public void SetUniformMatrix4x4ShouldNotInvokeUniformMatrix4WhenBoundProgramIsNull()
    {
        // Arrange
        var value = Matrix4x4.Identity;

        float[] values =
        {
            value.M11, value.M12, value.M13, value.M14,
            value.M21, value.M22, value.M23, value.M24,
            value.M31, value.M32, value.M33, value.M34,
            value.M41, value.M42, value.M43, value.M44,
        };

        // Act
        this.pipeline.SetUniform("name", Matrix4x4.Identity);

        // Assert
        this.invoker.Verify(x => x.UniformMatrix4(0, 1, false, values), Times.Never);
    }

    [Test]
    public void SetUniformMatrix4x4ShouldThrowArgumentExceptionWhenNameIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(string.Empty, Matrix4x4.Identity);
        });
    }

    [Test]
    public void SetUniformMatrix4x4ShouldThrowArgumentExceptionWhenNameIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(null, Matrix4x4.Identity);
        });
    }

    [Test]
    public void SetUniformMatrix4x4ShouldThrowArgumentExceptionWhenNameIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform("\t\r\n", Matrix4x4.Identity);
        });
    }

    [Test]
    public void SetUniformMatrix4x4ShouldThrowUniformNotLocatedExceptionWhenGetUniformLocationReturnsNegativeOne()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(-1);

        this.pipeline.SetShaderProgram(program.Object);

        // Act and assert
        Assert.Throws<UniformNotLocatedException>(() =>
        {
            this.pipeline.SetUniform("name", Matrix4x4.Identity);
        });
    }

    [Test]
    public void SetUniformVector2ShouldInvokeUniform2WhenGetUniformLocationReturnsPositiveInteger()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(0);

        this.pipeline.SetShaderProgram(program.Object);

        // Act
        this.pipeline.SetUniform("name", new Vector2(1.0f, 0.0f));

        // Assert
        this.invoker.Verify(x => x.Uniform2(0, 1.0f, 0.0f), Times.Once);
    }

    [Test]
    public void SetUniformVector2ShouldNotInvokeUniform2WhenBoundProgramIsNull()
    {
        // Act
        this.pipeline.SetUniform("name", new Vector2(1.0f, 0.0f));

        // Assert
        this.invoker.Verify(x => x.Uniform2(0, 1.0f, 0.0f), Times.Never);
    }

    [Test]
    public void SetUniformVector2ShouldThrowArgumentExceptionWhenNameIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(string.Empty, new Vector2(1.0f, 0.0f));
        });
    }

    [Test]
    public void SetUniformVector2ShouldThrowArgumentExceptionWhenNameIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(null, new Vector2(1.0f, 0.0f));
        });
    }

    [Test]
    public void SetUniformVector2ShouldThrowArgumentExceptionWhenNameIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform("\t\r\n", new Vector2(1.0f, 0.0f));
        });
    }

    [Test]
    public void SetUniformVector2ShouldThrowUniformNotLocatedExceptionWhenGetUniformLocationReturnsNegativeOne()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(-1);

        this.pipeline.SetShaderProgram(program.Object);

        // Act and assert
        Assert.Throws<UniformNotLocatedException>(() =>
        {
            this.pipeline.SetUniform("name", new Vector2(1.0f, 0.0f));
        });
    }

    [Test]
    public void SetUniformVector3ShouldInvokeUniform3WhenGetUniformLocationReturnsPositiveInteger()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(0);

        this.pipeline.SetShaderProgram(program.Object);

        // Act
        this.pipeline.SetUniform("name", new Vector3(1.0f, 0.0f, 1.0f));

        // Assert
        this.invoker.Verify(x => x.Uniform3(0, 1.0f, 0.0f, 1.0f), Times.Once);
    }

    [Test]
    public void SetUniformVector3ShouldNotInvokeUniform3WhenBoundProgramIsNull()
    {
        // Act
        this.pipeline.SetUniform("name", new Vector3(1.0f, 0.0f, 1.0f));

        // Assert
        this.invoker.Verify(x => x.Uniform3(0, 1.0f, 0.0f, 1.0f), Times.Never);
    }

    [Test]
    public void SetUniformVector3ShouldThrowArgumentExceptionWhenNameIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(string.Empty, new Vector3(1.0f, 0.0f, 1.0f));
        });
    }

    [Test]
    public void SetUniformVector3ShouldThrowArgumentExceptionWhenNameIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(null, new Vector3(1.0f, 0.0f, 1.0f));
        });
    }

    [Test]
    public void SetUniformVector3ShouldThrowArgumentExceptionWhenNameIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform("\t\r\n", new Vector3(1.0f, 0.0f, 1.0f));
        });
    }

    [Test]
    public void SetUniformVector3ShouldThrowUniformNotLocatedExceptionWhenGetUniformLocationReturnsNegativeOne()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(-1);

        this.pipeline.SetShaderProgram(program.Object);

        // Act and assert
        Assert.Throws<UniformNotLocatedException>(() =>
        {
            this.pipeline.SetUniform("name", new Vector3(1.0f, 0.0f, 1.0f));
        });
    }

    [Test]
    public void SetUniformVector4ShouldInvokeUniform4WhenGetUniformLocationReturnsPositiveInteger()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(0);

        this.pipeline.SetShaderProgram(program.Object);

        // Act
        this.pipeline.SetUniform("name", new Vector4(1.0f, 0.0f, 1.0f, 1.0f));

        // Assert
        this.invoker.Verify(x => x.Uniform4(0, 1.0f, 0.0f, 1.0f, 1.0f), Times.Once);
    }

    [Test]
    public void SetUniformVector4ShouldNotInvokeUniform4WhenBoundProgramIsNull()
    {
        // Act
        this.pipeline.SetUniform("name", new Vector4(1.0f, 0.0f, 1.0f, 1.0f));

        // Assert
        this.invoker.Verify(x => x.Uniform4(0, 1.0f, 0.0f, 1.0f, 1.0f), Times.Never);
    }

    [Test]
    public void SetUniformVector4ShouldThrowArgumentExceptionWhenNameIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(string.Empty, new Vector4(1.0f, 0.0f, 1.0f, 1.0f));
        });
    }

    [Test]
    public void SetUniformVector4ShouldThrowArgumentExceptionWhenNameIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform(null, new Vector4(1.0f, 0.0f, 1.0f, 1.0f));
        });
    }

    [Test]
    public void SetUniformVector4ShouldThrowArgumentExceptionWhenNameIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.pipeline.SetUniform("\t\r\n", new Vector4(1.0f, 0.0f, 1.0f, 1.0f));
        });
    }

    [Test]
    public void SetUniformVector4ShouldThrowUniformNotLocatedExceptionWhenGetUniformLocationReturnsNegativeOne()
    {
        // Arrange
        var program = new Mock<IOpenGLShaderProgram>();
        program.Setup(x => x.GetUniformLocation(It.IsAny<string>())).Returns(-1);

        this.pipeline.SetShaderProgram(program.Object);

        // Act and assert
        Assert.Throws<UniformNotLocatedException>(() =>
        {
            this.pipeline.SetUniform("name", new Vector4(1.0f, 0.0f, 1.0f, 1.0f));
        });
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.invoker = new Mock<IOpenGLInvoker>();
        this.pipeline = new OpenGLPipeline(this.invoker.Object);
    }
}
