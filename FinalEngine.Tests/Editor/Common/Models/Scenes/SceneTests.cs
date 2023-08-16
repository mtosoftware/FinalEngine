// <copyright file="SceneTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Models.Scenes;

using System;
using System.Collections.ObjectModel;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Models.Scenes;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneTests
{
    private Mock<ILogger<Scene>> logger;

    private Scene scene;

    private Mock<IEntityWorld> world;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenWorldIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Scene(this.logger.Object, null);
        });
    }

    [Test]
    public void EntitiesShouldNotReturnNullWhenInvoked()
    {
        // Act
        var actual = this.scene.Entities;

        // Assert
        Assert.That(actual, Is.Not.Null);
    }

    [Test]
    public void EntitiesShouldReturnReadOnlyObservableCollectionWhenInvoked()
    {
        // Act
        var actual = this.scene.Entities;

        // Assert
        Assert.That(actual, Is.InstanceOf<ObservableCollection<Entity>>());
    }

    [Test]
    public void RenderShouldInvokeWorldProcessAllRenderWhenInvoked()
    {
        // Act
        this.scene.Render();

        // Assert
        this.world.Verify(x => x.ProcessAll(GameLoopType.Render), Times.Once);
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<Scene>>();
        this.world = new Mock<IEntityWorld>();
        this.scene = new Scene(this.logger.Object, this.world.Object);
    }
}
