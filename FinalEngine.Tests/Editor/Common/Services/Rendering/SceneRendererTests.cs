// <copyright file="SceneRendererTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Services.Rendering;

using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Numerics;
using FinalEngine.Editor.Common.Services.Rendering;
using FinalEngine.Rendering;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneRendererTests
{
    private Mock<ILogger<SceneRenderer>> logger;

    private Mock<IPipeline> pipeline;

    private Mock<IRasterizer> rasterizer;

    private Mock<IRenderDevice> renderDevice;

    private SceneRenderer sceneRenderer;

    [Test]
    public void ChangeProjectionShouldSetUniformProjectionWhenInvoked()
    {
        // Arrange
        const int width = 1280;
        const int height = 720;

        var projection = Matrix4x4.CreateOrthographicOffCenter(0, width, 0, height, -1, 1);

        Expression<Func<Matrix4x4, bool>> match = (mat) => mat.Equals(projection);

        // Act
        this.sceneRenderer.ChangeProjection(width, height);

        // Assert
        this.pipeline.Verify(x => x.SetUniform("u_projection", It.Is<Matrix4x4>(match)), Times.Once);
    }

    [Test]
    public void ChangeProjectionShouldSetViewportWhenInvoked()
    {
        // Arrange
        const int width = 1280;
        const int height = 720;

        Expression<Func<Rectangle, bool>> match = (rect) =>
            rect.X == 0 &&
            rect.Y == 0 &&
            rect.Width == width
            && rect.Height == height;

        // Act
        this.sceneRenderer.ChangeProjection(width, height);

        // Assert
        this.rasterizer.Verify(x => x.SetViewport(It.Is<Rectangle>(match), 0, 1), Times.Once);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneRenderer(null, this.renderDevice.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenRenderDeviceIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneRenderer(this.logger.Object, null);
        });
    }

    [Test]
    public void RenderShouldInvokeClearWhenInvoked()
    {
        // Act
        this.sceneRenderer.Render();

        // Assert
        this.renderDevice.Verify(x => x.Clear(It.IsAny<Color>(), 1, 0), Times.Once);
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<SceneRenderer>>();
        this.renderDevice = new Mock<IRenderDevice>();
        this.pipeline = new Mock<IPipeline>();
        this.rasterizer = new Mock<IRasterizer>();

        this.renderDevice.SetupGet(x => x.Pipeline).Returns(this.pipeline.Object);
        this.renderDevice.SetupGet(x => x.Rasterizer).Returns(this.rasterizer.Object);

        this.sceneRenderer = new SceneRenderer(this.logger.Object, this.renderDevice.Object);
    }
}
