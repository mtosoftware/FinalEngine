// <copyright file="SceneRendererTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Services.Rendering;

using System;
using System.Drawing;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Rendering;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneRendererTests
{
    private Mock<IRenderDevice> renderDevice;

    private SceneRenderer sceneRenderer;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenRenderDeviceIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneRenderer(null);
        });
    }

    [Test]
    public void RenderShouldInvokeRenderDeviceClearWhenInvoked()
    {
        // Act
        this.sceneRenderer.Render();

        // Assert
        this.renderDevice.Verify(x => x.Clear(Color.FromArgb(255, 30, 30, 30), 1, 0), Times.Once);
    }

    [SetUp]
    public void Setup()
    {
        this.renderDevice = new Mock<IRenderDevice>();
        this.sceneRenderer = new SceneRenderer(this.renderDevice.Object);
    }
}
