// <copyright file="SceneRendererTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Services.Rendering;

using System;
using System.Drawing;
using FinalEngine.Editor.Common.Services.Rendering;
using FinalEngine.Rendering;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneRendererTests
{
    private Mock<ILogger<SceneRenderer>> logger;

    private Mock<IRenderDevice> renderDevice;

    private SceneRenderer sceneRenderer;

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

        this.sceneRenderer = new SceneRenderer(this.logger.Object, this.renderDevice.Object);
    }
}
