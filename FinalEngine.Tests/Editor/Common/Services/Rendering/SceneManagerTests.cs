// <copyright file="SceneManagerTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Services.Rendering;

using System;
using FinalEngine.Editor.Common.Models.Scenes;
using FinalEngine.Editor.Common.Services.Scenes;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneManagerTests
{
    private Mock<IScene> scene;

    private SceneManager sceneManager;

    [Test]
    public void ActiveSceneShouldNotBeenNullWhenInvoked()
    {
        // Act
        var actual = this.sceneManager.ActiveScene;

        // Assert
        Assert.That(actual, Is.Not.Null);
    }

    [Test]
    public void ActiveSceneShouldReturnSceneWhenInvoked()
    {
        // Act
        var actual = this.sceneManager.ActiveScene;

        // Assert
        Assert.That(actual, Is.SameAs(this.scene.Object));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenSceneIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneManager(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.scene = new Mock<IScene>();
        this.sceneManager = new SceneManager(this.scene.Object);
    }
}
