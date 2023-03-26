// <copyright file="MainViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels;

using System;
using System.Linq;
using System.Reflection;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.Common.Services.Rendering;
using FinalEngine.Editor.ViewModels;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Interaction;
using FinalEngine.Utilities.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class MainViewModelTests
{
    private Mock<ILogger<MainViewModel>> logger;

    private Mock<ISceneRenderer> sceneRenderer;

    private Mock<IFactory<SceneViewModel>> sceneViewModelFactory;

    private MainViewModel viewModel;

    [Test]
    public void ConstructorShouldInvokeSceneViewModelFactoryCreateWhenInvoked()
    {
        // Assert
        this.sceneViewModelFactory.Verify(x => x.Create(), Times.Once());
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MainViewModel(null, this.sceneViewModelFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenSceneViewModelFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MainViewModel(this.logger.Object, null);
        });
    }

    [Test]
    public void ExitCommandShouldInvokeCloseWhenCloseableIsPassed()
    {
        // Arrange
        var closeable = new Mock<ICloseable>();

        // Act
        this.viewModel.ExitCommand.Execute(closeable.Object);

        // Assert
        closeable.Verify(x => x.Close(), Times.Once);
    }

    [Test]
    public void ExitCommandShouldThrowArgumentNullExceptionWhenCloseableIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.viewModel.ExitCommand.Execute(null);
        });
    }

    [Test]
    public void PanesShouldContainSceneViewModelWhenCreated()
    {
        // Act
        var actual = this.viewModel.Panes.FirstOrDefault(x =>
        {
            return x.GetType() == typeof(SceneViewModel);
        });

        // Assert
        Assert.That(actual, Is.Not.Null);
    }

    [Test]
    public void PanesShouldNotBeNullWhenCreated()
    {
        // Act
        var actual = this.viewModel.Panes;

        // Assert
        Assert.That(actual, Is.Not.Null);
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<MainViewModel>>();
        this.sceneViewModelFactory = new Mock<IFactory<SceneViewModel>>();
        this.sceneRenderer = new Mock<ISceneRenderer>();
        this.sceneViewModelFactory.Setup(x => x.Create()).Returns(new SceneViewModel(this.sceneRenderer.Object));
        this.viewModel = new MainViewModel(this.logger.Object, this.sceneViewModelFactory.Object);
    }

    [Test]
    public void TitleShouldReturnCorrectTitleOnStartup()
    {
        // Arrange
        string expected = $"Final Engine - {Assembly.GetExecutingAssembly().GetVersionString()}";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToolsShouldNotBeNullWhenCreated()
    {
        // Act
        var actual = this.viewModel.Tools;

        // Assert
        Assert.That(actual, Is.Not.Null);
    }
}
