// <copyright file="SceneTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Models.Scenes;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Models.Scenes;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneTests
{
    private Scene scene;

    private Mock<IEntityWorld> world;

    [Test]
    public void AddEntityShouldAddEntityToEntitiesWhenInvoked()
    {
        // Arrange
        var entity = new Entity();

        // Act
        this.scene.AddEntity(entity);

        // Assert
        Assert.That(this.scene.Entities.FirstOrDefault(), Is.SameAs(entity));
    }

    [Test]
    public void AddEntityShouldAddEntityToEntityWorldWhenInvoked()
    {
        // Arrange
        var entity = new Entity();

        // Act
        this.scene.AddEntity(entity);

        // Assert
        this.world.Verify(x => x.AddEntity(entity), Times.Once);
    }

    [Test]
    public void AddEntityShouldThrowArgumentNullExceptionWhenEntityIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.scene.AddEntity(null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenWorldIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Scene(null);
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
        this.world = new Mock<IEntityWorld>();
        this.scene = new Scene(this.world.Object);
    }
}
