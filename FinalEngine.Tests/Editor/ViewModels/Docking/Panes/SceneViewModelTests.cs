// <copyright file="SceneViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Panes;

using System;
using System.Drawing;
using FinalEngine.Editor.Common.Services.Rendering;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneViewModelTests
{
    private Mock<ISceneRenderer> sceneRenderer;

    private SceneViewModel viewModel;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenSceneRendererIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneViewModel(null);
        });
    }

    [Test]
    public void ProjectionSizeShouldInvokeSceneRendererChangeProjectionWhenSet()
    {
        // Arrange
        var projection = new Size(1280, 720);

        // Act
        this.viewModel.ProjectionSize = projection;

        // Assert
        this.sceneRenderer.Verify(x => x.ChangeProjection(1280, 720), Times.Once());
    }

    [Test]
    public void ProjectionSizeShouldReturnHighDefinitionWhenSetToHighDefinition()
    {
        // Arrange
        var expected = new Size(1280, 720);

        // Act
        this.viewModel.ProjectionSize = expected;

        // Assert
        Assert.That(this.viewModel.ProjectionSize, Is.EqualTo(expected));
    }

    [Test]
    public void ProjectionSizeShouldReturnZeroWhenNotSet()
    {
        // Act
        var actual = this.viewModel.ProjectionSize;

        // Assert
        Assert.That(actual, Is.EqualTo(Size.Empty));
    }

    [Test]
    public void RenderCommandShouldInvokeSceneRendererRenderWhenInvoked()
    {
        // Act
        this.viewModel.RenderCommand.Execute(null);

        // Assert
        this.sceneRenderer.Verify(x => x.Render(), Times.Once());
    }

    [SetUp]
    public void Setup()
    {
        this.sceneRenderer = new Mock<ISceneRenderer>();
        this.viewModel = new SceneViewModel(this.sceneRenderer.Object);
    }

    [Test]
    public void TitleShouldReturnSceneViewWhenSet()
    {
        // Arrange
        string expected = "Scene View";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
