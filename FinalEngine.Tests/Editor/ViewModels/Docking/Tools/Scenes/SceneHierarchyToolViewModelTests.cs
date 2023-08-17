// <copyright file="SceneHierarchyToolViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Tools.Scenes;

using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Models.Scenes;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneHierarchyToolViewModelTests
{
    private IList<Entity> entities;

    private Mock<ILogger<SceneHierarchyToolViewModel>> logger;

    private Mock<IMessenger> messenger;

    private Mock<IScene> scene;

    private Mock<ISceneManager> sceneManager;

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
            new SceneHierarchyToolViewModel(null, WeakReferenceMessenger.Default, this.sceneManager.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenMessengerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneHierarchyToolViewModel(this.logger.Object, null, this.sceneManager.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenSceneManagerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneHierarchyToolViewModel(this.logger.Object, WeakReferenceMessenger.Default, null);
        });
    }

    [Test]
    public void EntitiesShouldReturnActiveEntitiesWhenInvoked()
    {
        // Act
        var actual = this.viewModel.Entities;

        // Assert
        Assert.That(actual, Is.SameAs(this.entities));
    }

    [Test]
    public void SelectedEntityShouldReturnEntityWhenSetToEntity()
    {
        // Arrange
        var expected = new Entity();
        this.viewModel.SelectedEntity = expected;

        // Act
        var actual = this.viewModel.SelectedEntity;

        // Assert
        Assert.That(actual, Is.SameAs(expected));
    }

    [Test]
    public void SelectedEntityShouldReturnNullWhenNotSet()
    {
        // Act
        var actual = this.viewModel.SelectedEntity;

        // Assert
        Assert.That(actual, Is.Null);
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<SceneHierarchyToolViewModel>>();
        this.sceneManager = new Mock<ISceneManager>();
        this.scene = new Mock<IScene>();
        this.messenger = new Mock<IMessenger>();

        this.entities = new List<Entity>()
        {
            new Entity(),
            new Entity(),
            new Entity(),
        };

        this.sceneManager.SetupGet(x => x.ActiveScene).Returns(this.scene.Object);
        this.scene.SetupGet(x => x.Entities).Returns((IReadOnlyCollection<Entity>)this.entities);

        this.viewModel = new SceneHierarchyToolViewModel(this.logger.Object, this.messenger.Object, this.sceneManager.Object);
    }
}
