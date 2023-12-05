// <copyright file="SceneTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Models.Scenes;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.ECS.Exceptions;
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
    public void AddEntityShouldAddEntityToEntitiesWhenInvoked()
    {
        // Act
        this.scene.AddEntity("Tag", Guid.Empty);

        // Assert
        Assert.That(this.scene.Entities.FirstOrDefault(), Is.Not.Null);
    }

    [Test]
    public void AddEntityShouldAddTagComponentToEntityWhenInvoked()
    {
        // Act
        this.scene.AddEntity("Tag", Guid.Empty);

        // Assert
        Assert.That(this.scene.Entities.FirstOrDefault().GetComponent<TagComponent>().Tag, Is.EqualTo("Tag"));
    }

    [Test]
    public void AddEntityShouldInvokeWorldAddEntityWhenInvoked()
    {
        // Act
        this.scene.AddEntity("Tag", Guid.Empty);

        // Assert
        this.world.Verify(x => x.AddEntity(this.scene.Entities.FirstOrDefault()), Times.Once);
    }

    [Test]
    public void AddEntityShouldThrowArgumentExceptionWhenTagIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.scene.AddEntity(string.Empty, Guid.Empty);
        });
    }

    [Test]
    public void AddEntityShouldThrowArgumentExceptionWhenTagIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.scene.AddEntity(null, Guid.Empty);
        });
    }

    [Test]
    public void AddEntityShouldThrowArgumentExceptionWhenTagIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.scene.AddEntity("\t\r\n ", Guid.Empty);
        });
    }

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
    public void RemoveEntityShouldRemoveEntityFromCollectionWhenInvoked()
    {
        // Arrange
        var uniqueIdentifier = Guid.NewGuid();
        this.scene.AddEntity("Tag", uniqueIdentifier);

        // Act
        this.scene.RemoveEntity(uniqueIdentifier);

        // Assert
        Assert.That(this.scene.Entities.Count, Is.EqualTo(0));
    }

    [Test]
    public void RemoveEntityShouldRemoveEntityFromWorldWhenInvoked()
    {
        // Arrange
        var uniqueIdentifier = Guid.NewGuid();
        this.scene.AddEntity("Tag", uniqueIdentifier);

        // Act
        this.scene.RemoveEntity(uniqueIdentifier);

        // Assert
        this.world.Verify(x => x.RemoveEntity(It.IsAny<Entity>()), Times.Once);
    }

    [Test]
    public void RemoveEntityShouldThrowEntityNotFoundExceptionWhenNotInCollection()
    {
        // Act and assert
        Assert.Throws<EntityNotFoundException>(() =>
        {
            this.scene.RemoveEntity(Guid.NewGuid());
        });
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
