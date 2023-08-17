// <copyright file="SceneViewPaneViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Scenes;

using System;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Scenes;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneViewPaneViewModelTests
{
    private Mock<ILogger<SceneViewPaneViewModel>> logger;

    private Mock<ISceneRenderer> sceneRenderer;

    private SceneViewPaneViewModel viewModel;

    [Test]
    public void ConstructorShouldSetContentIDToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "SceneView";

        // Act
        string actual = this.viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldSetTitleToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Scene View";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneViewPaneViewModel(null, this.sceneRenderer.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenSceneRendererIsNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneViewPaneViewModel(this.logger.Object, null);
        });
    }

    [Test]
    public void RenderCommandExecuteShouldInvokeSceneRendererRenderWhenInvoked()
    {
        // Act
        this.viewModel.RenderCommand.Execute(null);

        // Assert
        this.sceneRenderer.Verify(x => x.Render(), Times.Once);
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<SceneViewPaneViewModel>>();
        this.sceneRenderer = new Mock<ISceneRenderer>();
        this.viewModel = new SceneViewPaneViewModel(this.logger.Object, this.sceneRenderer.Object);
    }
}
