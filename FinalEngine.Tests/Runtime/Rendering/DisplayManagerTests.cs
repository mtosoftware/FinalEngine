// <copyright file="DisplayManagerTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Runtime.Rendering;

using System;
using System.Drawing;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Runtime.Rendering;
using Moq;
using NUnit.Framework;

[TestFixture]
public class DisplayManagerTests
{
    private DisplayManager displayManager;

    private Mock<IRasterizer> rasterizer;

    private Mock<IWindow> window;

    [Test]
    public void ChangeResolutionShouldInvokeSetViewportWhenSetToFullHighDefinition()
    {
        // Act
        this.displayManager.ChangeResolution(DisplayResolution.FullHighDefinition);

        // Assert
        this.rasterizer.Verify(x => x.SetViewport(new Rectangle(0, 0, 1920, 1080), 0, 1), Times.Once());
    }

    [Test]
    public void ChangeResolutionShouldInvokeSetViewportWhenSetToHighDefinition()
    {
        // Act
        this.displayManager.ChangeResolution(DisplayResolution.HighDefinition);

        // Assert
        this.rasterizer.Verify(x => x.SetViewport(new Rectangle(0, 0, 1280, 720), 0, 1), Times.Once());
    }

    [Test]
    public void ChangeResolutionShouldInvokeSetViewportWhenSetToStandardDefinition()
    {
        // Act
        this.displayManager.ChangeResolution(DisplayResolution.StandardDefinition);

        // Assert
        this.rasterizer.Verify(x => x.SetViewport(new Rectangle(0, 0, 640, 480), 0, 1), Times.Once());
    }

    [Test]
    public void ChangeResolutionShouldInvokeSetViewportWhenSetToUltraHighDefinition()
    {
        // Act
        this.displayManager.ChangeResolution(DisplayResolution.UltraHighDefinition);

        // Assert
        this.rasterizer.Verify(x => x.SetViewport(new Rectangle(0, 0, 3840, 2160), 0, 1), Times.Once());
    }

    [Test]
    public void ChangeResolutionShouldSetWindowSizeWhenSetToFullHighDefinition()
    {
        // Act
        this.displayManager.ChangeResolution(DisplayResolution.FullHighDefinition);

        // Assert
        Assert.That(this.window.Object.Size, Is.EqualTo(new Size(1920, 1080)));
    }

    [Test]
    public void ChangeResolutionShouldSetWindowSizeWhenSetToHighDefinition()
    {
        // Act
        this.displayManager.ChangeResolution(DisplayResolution.HighDefinition);

        // Assert
        Assert.That(this.window.Object.Size, Is.EqualTo(new Size(1280, 720)));
    }

    [Test]
    public void ChangeResolutionShouldSetWindowSizeWhenSetToStandardDefinition()
    {
        // Act
        this.displayManager.ChangeResolution(DisplayResolution.StandardDefinition);

        // Assert
        Assert.That(this.window.Object.Size, Is.EqualTo(new Size(640, 480)));
    }

    [Test]
    public void ChangeResolutionShouldSetWindowSizeWhenSetToUltraHighDefinition()
    {
        // Act
        this.displayManager.ChangeResolution(DisplayResolution.UltraHighDefinition);

        // Assert
        Assert.That(this.window.Object.Size, Is.EqualTo(new Size(3840, 2160)));
    }

    [Test]
    public void ChangeResolutionShouldThrowNotSupportedExceptionWhenSetToInvalidResolution()
    {
        // Act and assert
        Assert.Throws<NotSupportedException>(() =>
        {
            this.displayManager.ChangeResolution((DisplayResolution)(-1));
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenRasterizerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new DisplayManager(null, this.window.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenWindowIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new DisplayManager(this.rasterizer.Object, null);
        });
    }

    [Test]
    public void DisplayHeightShouldInvokeRasterizerGetViewportWhenInvoked()
    {
        // Act
        _ = this.displayManager.DisplayHeight;

        // Assert
        this.rasterizer.Verify(x => x.GetViewport(), Times.Once);
    }

    [Test]
    public void DisplayHeightShouldReturnRasterizerGetViewportHeightWhenInvoked()
    {
        // Arrange
        int expected = this.rasterizer.Object.GetViewport().Height;

        // Act
        int actual = this.displayManager.DisplayHeight;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DisplayWidthShouldInvokeRasterizerGetViewportWhenInvoked()
    {
        // Act
        _ = this.displayManager.DisplayWidth;

        // Assert
        this.rasterizer.Verify(x => x.GetViewport(), Times.Once);
    }

    [Test]
    public void DisplayWidthShouldReturnRasterizerGetViewportWidthWhenInvoked()
    {
        // Arrange
        int expected = this.rasterizer.Object.GetViewport().Width;

        // Act
        int actual = this.displayManager.DisplayWidth;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [SetUp]
    public void Setup()
    {
        this.rasterizer = new Mock<IRasterizer>();

        this.rasterizer.Setup(x => x.GetViewport()).Returns(new Rectangle()
        {
            X = 0,
            Y = 0,
            Width = 1280,
            Height = 720,
        });

        this.window = new Mock<IWindow>();
        this.window.SetupAllProperties();

        this.displayManager = new DisplayManager(this.rasterizer.Object, this.window.Object);
    }
}
