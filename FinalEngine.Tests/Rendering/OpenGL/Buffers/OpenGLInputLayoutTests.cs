// <copyright file="OpenGLInputLayoutTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL.Buffers;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.OpenGL.Buffers;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Utilities;
using Moq;
using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;

public class OpenGLInputLayoutTests
{
    private readonly IReadOnlyCollection<InputElement> elements = new List<InputElement>()
    {
        new InputElement(0, 3, InputElementType.Float, 0),
        new InputElement(1, 4, InputElementType.Byte, 12),
        new InputElement(2, 3, InputElementType.Short, 28),
        new InputElement(3, 3, InputElementType.Double, 22),
        new InputElement(4, 5, InputElementType.Int, 121),
    };

    private Mock<IOpenGLInvoker> invoker;

    private OpenGLInputLayout layout;

    private Mock<IEnumMapper> mapper;

    [Test]
    public void BindShouldInvokeEnableVertexAttribArrayWhenInvoked()
    {
        // Act
        this.layout.Bind();

        // Assert
        this.invoker.Verify(x => x.EnableVertexAttribArray(It.IsAny<int>()), Times.Exactly(this.elements.Count));
    }

    [Test]
    public void BindShouldInvokeVertexAttribBindingWhenInvoked()
    {
        // Act
        this.layout.Bind();

        // Assert
        this.invoker.Verify(x => x.VertexAttribBinding(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(this.elements.Count));
    }

    [Test]
    public void BindShouldInvokeVertexAttribFormatWhenInvoked()
    {
        // Act
        this.layout.Bind();

        // Assert
        this.invoker.Verify(x => x.VertexAttribFormat(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<VertexAttribType>(), false, It.IsAny<int>()), Times.Exactly(this.elements.Count));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenElementsIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLInputLayout(this.invoker.Object, this.mapper.Object, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLInputLayout(null, this.mapper.Object, this.elements);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenMapperIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLInputLayout(this.invoker.Object, null, this.elements);
        });
    }

    [Test]
    public void ElementsShouldReturnSameAsElementsWhenInvoked()
    {
        // Assert
        Assert.AreEqual(this.elements, this.layout.Elements);
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.invoker = new Mock<IOpenGLInvoker>();
        this.mapper = new Mock<IEnumMapper>();
        this.layout = new OpenGLInputLayout(this.invoker.Object, this.mapper.Object, this.elements);
    }

    [Test]
    public void UnbindShouldInvokeDisableVertexAttribArrayWhenInvoked()
    {
        // Act
        this.layout.Unbind();

        // Assert
        this.invoker.Verify(x => x.DisableVertexAttribArray(It.IsAny<int>()), Times.Exactly(this.elements.Count));
    }
}
