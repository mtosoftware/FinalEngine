// <copyright file="GameContainerBaseTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Runtime;

using System;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.IO;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Resources;
using FinalEngine.Runtime;
using FinalEngine.Runtime.Rendering;
using FinalEngine.Tests.Runtime.Mocks;
using Moq;
using NUnit.Framework;

[TestFixture]
public class GameContainerBaseTests
{
    private Mock<IDisplayManager> displayManager;

    private Mock<IEventsProcessor> eventsProcessor;

    private Mock<IFileSystem> fileSystem;

    private GameContainerMock gameContainer;

    private Mock<IGameTime> gameTime;

    private Mock<IKeyboardDevice> keyboardDevice;

    private Mock<IMouseDevice> mouseDevice;

    private Mock<IRenderContext> renderContext;

    private Mock<IRenderDevice> renderDevice;

    private Mock<IResourceManager> resourceManager;

    private Mock<IRuntimeFactory> runtimeFactory;

    private Mock<IWindow> window;

    [Test]
    public void ConstructorShouldCreateKeyboard()
    {
        // Assert
        Assert.That(this.gameContainer.KeyboardObject, Is.Not.Null);
    }

    [Test]
    public void ConstructorShouldCreateMouse()
    {
        // Assert
        Assert.That(this.gameContainer.MouseObject, Is.Not.Null);
    }

    [Test]
    public void ConstructorShouldInvokeDisplayManagerChangeResolutionWhenInvoked()
    {
        // Assert
        this.displayManager.Verify(x => x.ChangeResolution(DisplayResolution.HighDefinition), Times.Once());
    }

    [Test]
    public void ConstructorShouldSetDisplayManagerToDisplayManager()
    {
        // Assert
        Assert.That(this.displayManager.Object, Is.SameAs(this.gameContainer.DisplayManagerObject));
    }

    [Test]
    public void ConstructorShouldSetFileSystemToFileSystem()
    {
        // Assert
        Assert.That(this.fileSystem.Object, Is.SameAs(this.gameContainer.FileSystemObject));
    }

    [Test]
    public void ConstructorShouldSetRenderDeviceToRenderDevice()
    {
        // Assert
        Assert.That(this.renderDevice.Object, Is.SameAs(this.gameContainer.RenderDeviceObject));
    }

    [Test]
    public void ConstructorShouldSetResourceManagerToResourceManager()
    {
        // Assert
        Assert.That(this.resourceManager.Object, Is.SameAs(this.gameContainer.ResourceManagerObject));
    }

    [Test]
    public void ConstructorShouldSetWindowToWindow()
    {
        // Assert
        Assert.That(this.window.Object, Is.SameAs(this.gameContainer.WindowObject));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenRuntimeFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new GameContainerMock(null);
        });
    }

    [Test]
    public void DisposeShouldNotDisposeTwice()
    {
        // Arrange
        this.gameContainer.Dispose();

        // Act
        this.gameContainer.Dispose();

        // Assert
        this.window.Verify(x => x.Dispose(), Times.Once());
    }

    [Test]
    public void DisposeShouldSetResourceManagerToNull()
    {
        // Act
        this.gameContainer.Dispose();

        // Assert
        Assert.That(this.gameContainer.ResourceManagerObject, Is.Null);
    }

    [Test]
    public void DisposeShouldSetWindowToNull()
    {
        // Act
        this.gameContainer.Dispose();

        // Assert
        Assert.That(this.gameContainer.WindowObject, Is.Null);
    }

    [Test]
    public void ExitShouldSetIsRunningToFalseWhenRunning()
    {
        // Arrange
        this.gameContainer.InitializeActin = this.gameContainer.Exit;

        // Act
        this.gameContainer.Run(this.gameTime.Object);

        // Assert
        Assert.That(this.gameContainer.IsRunningBoolean, Is.False);
    }

    [Test]
    public void IsRunningShouldReturnFalseWhenNotRunning()
    {
        // Act
        bool actual = this.gameContainer.IsRunningBoolean;

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void RunShouldInvokeCanProcessNextFrameFiftyTimes()
    {
        // Arrange
        this.gameTime.Setup(x => x.CanProcessNextFrame()).Returns(true);

        int currentCycle = 0;
        int cycles = 50;

        this.gameContainer.UpdateAction = () =>
        {
            currentCycle++;

            if (currentCycle >= cycles)
            {
                this.gameContainer.Exit();
            }
        };

        // Act
        this.gameContainer.Run(this.gameTime.Object);

        // Assert
        this.gameTime.Verify(x => x.CanProcessNextFrame(), Times.Exactly(50));
    }

    [Test]
    public void RunShouldInvokeEventsProcessorProcessEventsFiftyTimes()
    {
        // Arrange
        this.gameTime.Setup(x => x.CanProcessNextFrame()).Returns(true);

        int currentCycle = 0;
        int cycles = 50;

        this.gameContainer.RenderAction = () =>
        {
            currentCycle++;

            if (currentCycle >= cycles)
            {
                this.gameContainer.Exit();
            }
        };

        // Act
        this.gameContainer.Run(this.gameTime.Object);

        // Assert
        this.eventsProcessor.Verify(x => x.ProcessEvents(), Times.Exactly(cycles));
    }

    [Test]
    public void RunShouldInvokeInitialize()
    {
        // Arrange
        this.gameContainer.InitializeActin = Assert.Pass;

        // Act
        this.gameContainer.Run(this.gameTime.Object);
    }

    [Test]
    public void RunShouldInvokeLoadResources()
    {
        // Arrange
        this.gameContainer.LoadResourcesAction = Assert.Pass;

        // Act
        this.gameContainer.Run(this.gameTime.Object);
    }

    [Test]
    public void RunShouldInvokeRenderContextSwapBuffersFiftyTimes()
    {
        // Arrange
        this.gameTime.Setup(x => x.CanProcessNextFrame()).Returns(true);

        int currentCycle = 0;
        int cycles = 50;

        this.gameContainer.RenderAction = () =>
        {
            currentCycle++;

            if (currentCycle >= cycles)
            {
                this.gameContainer.Exit();
            }
        };

        // Act
        this.gameContainer.Run(this.gameTime.Object);

        // Assert
        this.renderContext.Verify(x => x.SwapBuffers(), Times.Exactly(cycles));
    }

    [Test]
    public void RunShouldInvokeRenderFiftyTimes()
    {
        // Arrange
        this.gameTime.Setup(x => x.CanProcessNextFrame()).Returns(true);

        int currentCycle = 0;
        int cycles = 50;

        this.gameContainer.RenderAction = () =>
        {
            currentCycle++;

            if (currentCycle >= cycles)
            {
                this.gameContainer.Exit();
            }
        };

        // Act
        this.gameContainer.Run(this.gameTime.Object);

        // Assert
        Assert.That(this.gameContainer.RenderCount, Is.EqualTo(cycles));
    }

    [Test]
    public void RunShouldInvokeUpdateFiftyTimes()
    {
        // Arrange
        this.gameTime.Setup(x => x.CanProcessNextFrame()).Returns(true);

        int currentCycle = 0;
        int cycles = 50;

        this.gameContainer.UpdateAction = () =>
        {
            currentCycle++;

            if (currentCycle >= cycles)
            {
                this.gameContainer.Exit();
            }
        };

        // Act
        this.gameContainer.Run(this.gameTime.Object);

        // Assert
        Assert.That(this.gameContainer.UpdateCount, Is.EqualTo(cycles));
    }

    [Test]
    public void RunShouldNotInvokeUpdateWhenCanProcessNextFrameReturnsFalse()
    {
        // Arrange
        int currentCycle = 0;
        int cycles = 1;

        this.gameTime.Setup(x => x.CanProcessNextFrame()).Returns(() =>
        {
            if (currentCycle >= cycles)
            {
                this.gameContainer.Exit();
            }

            currentCycle++;

            return false;
        });

        // Act
        this.gameContainer.Run(this.gameTime.Object);

        // Assert
        this.eventsProcessor.Verify(x => x.ProcessEvents(), Times.Never());
    }

    [SetUp]
    public void Setup()
    {
        this.window = new Mock<IWindow>();
        this.eventsProcessor = new Mock<IEventsProcessor>();
        this.keyboardDevice = new Mock<IKeyboardDevice>();
        this.mouseDevice = new Mock<IMouseDevice>();
        this.fileSystem = new Mock<IFileSystem>();
        this.renderContext = new Mock<IRenderContext>();
        this.renderDevice = new Mock<IRenderDevice>();
        this.displayManager = new Mock<IDisplayManager>();
        this.resourceManager = new Mock<IResourceManager>();
        this.runtimeFactory = new Mock<IRuntimeFactory>();
        this.gameTime = new Mock<IGameTime>();

        var windowObject = this.window.Object;
        var eventsProcessorObject = this.eventsProcessor.Object;
        var keyboardDeviceObject = this.keyboardDevice.Object;
        var mouseDeviceObject = this.mouseDevice.Object;
        var fileSystemObject = this.fileSystem.Object;
        var renderContextObject = this.renderContext.Object;
        var renderDeviceObject = this.renderDevice.Object;
        var displayManagerObject = this.displayManager.Object;
        var resourceManagerObject = this.resourceManager.Object;

        this.runtimeFactory.Setup(x => x.InitializeRuntime(
            out windowObject,
            out eventsProcessorObject,
            out keyboardDeviceObject,
            out mouseDeviceObject,
            out fileSystemObject,
            out renderContextObject,
            out renderDeviceObject,
            out displayManagerObject,
            out resourceManagerObject));

        this.gameContainer = new GameContainerMock(this.runtimeFactory.Object);
    }
}
