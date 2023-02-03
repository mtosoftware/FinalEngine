// <copyright file="OpenGLRenderContextTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL;

using System;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using Moq;
using NUnit.Framework;
using OpenTK;
using OpenTK.Windowing.Common;

public class OpenGLRenderContextTests
{
    private const int VertexArrayID = 5094;

    private Mock<IBindingsContext> bindingsContext;

    private Mock<IGraphicsContext> graphicsContext;

    private Mock<IOpenGLInvoker> invoker;

    private OpenGLRenderContext renderContext;

    [Test]
    public void ConstructorShouldInvokeLoadBindingsWhenInvoked()
    {
        // Assert
        this.invoker.Verify(x => x.LoadBindings(this.bindingsContext.Object), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeMakeCurrentWhenInvoked()
    {
        // Assert
        this.graphicsContext.Verify(x => x.MakeCurrent(), Times.Once);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenBindingsIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLRenderContext(this.invoker.Object, null, this.graphicsContext.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenContextIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLRenderContext(this.invoker.Object, this.bindingsContext.Object, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenGLRenderContext(null, this.bindingsContext.Object, this.graphicsContext.Object);
        });
    }

    [Test]
    public void DisposeShouldNotExecuteWhenAlreadyDisposed()
    {
        // Arrange
        this.renderContext.Dispose();

        // Act
        this.renderContext.Dispose();

        // Assert
        this.invoker.Verify(x => x.DeleteVertexArray(VertexArrayID), Times.Once);
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.invoker = new Mock<IOpenGLInvoker>();

        this.invoker.Setup(x => x.GenVertexArray()).Returns(VertexArrayID);

        this.bindingsContext = new Mock<IBindingsContext>();
        this.graphicsContext = new Mock<IGraphicsContext>();

        this.renderContext = new OpenGLRenderContext(this.invoker.Object, this.bindingsContext.Object, this.graphicsContext.Object);
    }

    [Test]
    public void SwapBuffersShouldInvokeSwapBuffersWhenContextIsCurrent()
    {
        // Arrange
        this.graphicsContext.SetupGet(x => x.IsCurrent).Returns(true);

        // Act
        this.renderContext.SwapBuffers();

        // Assert
        this.graphicsContext.Verify(x => x.SwapBuffers(), Times.Once);
    }

    [Test]
    public void SwapBuffersShouldThrowRenderContextExceptionWhenContextIsNotCurrent()
    {
        // Arrange
        this.graphicsContext.SetupGet(x => x.IsCurrent).Returns(false);

        // Act and assert
        Assert.Throws<RenderContextException>(this.renderContext.SwapBuffers);
    }

    [TearDown]
    public void TearDown()
    {
        this.renderContext.Dispose();
    }
}
