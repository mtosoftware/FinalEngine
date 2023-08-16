// <copyright file="EntitySystemBaseTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.ECS;

using System;
using System.Linq;
using FinalEngine.ECS;
using FinalEngine.Tests.ECS.Mocks;
using NUnit.Framework;

public class EntitySystemBaseTests
{
    [Test]
    public void AddOrRemoveByAspectByAspectShouldAddEntityToSystemWhenIsMatchReturnsTrueAndEntityHasNotBeenPreviouslyAdded()
    {
        // Arrange
        var expected = new Entity();
        var system = new MockEntitySystemA()
        {
            IsMatchFunction = (_) =>
            {
                return true;
            },
            ProcessFunction = (entities) =>
            {
                // Assert
                Assert.AreSame(expected, entities.FirstOrDefault());
            },
        };

        // Act
        system.AddOrRemoveByAspect(expected);
        system.Process();
    }

    [Test]
    public void AddOrRemoveByAspectShouldRemoveEntityFromSystemWhenForceRemoveIsTrueAndEntityAlreadyAdded()
    {
        // Arrange
        var entity = new Entity();
        var system = new MockEntitySystemA()
        {
            IsMatchFunction = (_) =>
            {
                return true;
            },
        };

        system.AddOrRemoveByAspect(entity);

        system.IsMatchFunction = (_) =>
        {
            return false;
        };
        system.ProcessFunction = (entities) =>
        {
            // Assert
            Assert.False(entities.Contains(entity));
        };

        // Act
        system.AddOrRemoveByAspect(entity, true);
    }

    [Test]
    public void AddOrRemoveByAspectShouldRemoveEntityFromSystemWhenIsMatchReturnsFalseAndEntityPreviouslyAdded()
    {
        // Arrange
        var entity = new Entity();
        var system = new MockEntitySystemA()
        {
            IsMatchFunction = (_) =>
            {
                return true;
            },
        };

        system.AddOrRemoveByAspect(entity);

        system.IsMatchFunction = (_) =>
        {
            return false;
        };
        system.ProcessFunction = (entities) =>
        {
            // Assert
            Assert.False(entities.Contains(entity));
        };

        // Act
        system.AddOrRemoveByAspect(entity);
    }

    [Test]
    public void AddOrRemoveByAspectShouldThrowArgumentNullExceptionWhenEntityIsNull()
    {
        // Arrange
        var system = new MockEntitySystemA();

        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            system.AddOrRemoveByAspect(null);
        });
    }

    [Test]
    public void ProcessShouldInvokeProtectedProcessWhenInvoked()
    {
        // Arrange
        var system = new MockEntitySystemA()
        {
            ProcessFunction = (_) =>
            {
                // Assert
                Assert.Pass();
            },
        };

        // Act
        system.Process();
    }
}
