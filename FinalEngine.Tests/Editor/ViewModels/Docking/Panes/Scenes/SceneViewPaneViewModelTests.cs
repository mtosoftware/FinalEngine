// <copyright file="SceneViewPaneViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Panes.Scenes;

using System;
using FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneViewPaneViewModelTests
{
    private Mock<ILogger<SceneViewPaneViewModel>> logger;

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
            new SceneViewPaneViewModel(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<SceneViewPaneViewModel>>();
        this.viewModel = new SceneViewPaneViewModel(this.logger.Object);
    }
}
