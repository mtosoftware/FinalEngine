// <copyright file="SceneHierarchyToolViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Tools.Scenes;

using System;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneHierarchyToolViewModelTests
{
    private Mock<ILogger<SceneHierarchyToolViewModel>> logger;

    private SceneHierarchyToolViewModel viewModel;

    [Test]
    public void ConstructorShouldSetContentIDToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "SceneHierarchy";

        // Act
        string actual = this.viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldSetTitleToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Scene Hierarchy";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneHierarchyToolViewModel(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<SceneHierarchyToolViewModel>>();
        this.viewModel = new SceneHierarchyToolViewModel(this.logger.Object);
    }
}
