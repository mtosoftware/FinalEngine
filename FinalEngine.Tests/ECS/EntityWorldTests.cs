// <copyright file="EntityWorldTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.ECS;

using System;
using System.Collections.Generic;
using System.Linq;
using FinalEngine.ECS;
using FinalEngine.ECS.Exceptions;
using FinalEngine.Tests.ECS.Mocks;
using NUnit.Framework;

public class EntityWorldTests
{
    private EntityWorld world;

    [Test]
    public void AddEntityShouldHookOntoEntityOnComponentsChangedWhenInvoked()
    {
        // Arrange
        var entity = new Entity();

        // Act
        this.world.AddEntity(entity);

        // Assert
        Assert.AreEqual(entity.OnComponentsChanged.GetInvocationList().Length, 1);
    }

    [Test]
    public void AddEntityShouldInvokeSystemAddOrRemoveByAspectWhenSystemIsAdded()
    {
        // Arrange
        var entity = new Entity();

        var system = new MockEntitySystemA()
        {
            IsMatchFunction = (_) =>
            {
                return true;
            },
            ProcessFunction = (entities) =>
            {
                // Assert
                Assert.AreEqual(entity, entities.FirstOrDefault());
            },
        };

        this.world.AddSystem(system);

        // Act
        this.world.AddEntity(entity);
        system.Process();
    }

    [Test]
    public void AddEntityShouldThrowArgumentExceptionWhenEntityHasAlreadyBeenAdded()
    {
        // Arrange
        var entity = new Entity();
        this.world.AddEntity(entity);

        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.world.AddEntity(entity);
        });
    }

    [Test]
    public void AddEntityShouldThrowArgumentNullExceptionWhenEntityIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.world.AddEntity(null);
        });
    }

    [Test]
    public void AddSystemShouldInvokeSystemAddOrRemveByAspectForEachEntityAddedWhenInvoked()
    {
        // Arrange
        var entities = new List<Entity>();

        for (int i = 0; i < 10; i++)
        {
            var entity = new Entity();

            this.world.AddEntity(entity);
            entities.Add(entity);
        }

        var system = new MockEntitySystemA()
        {
            IsMatchFunction = (_) =>
            {
                return true;
            },
            ProcessFunction = (collection) =>
            {
                // Assert
                for (int i = 0; i < collection.Count(); i++)
                {
                    Assert.AreSame(collection.ElementAt(i), entities[i]);
                }
            },
        };

        // Act
        this.world.AddSystem(system);
        system.Process();
    }

    [Test]
    public void AddSystemShouldNotThrowExceptionWhenSystemOfSameTypeHasPreviouslyBeenRemoved()
    {
        // Arrange
        this.world.AddSystem(new MockEntitySystemA());
        this.world.AddSystem(new MockEntitySystemB());

        // Act
        this.world.RemoveSystem(typeof(MockEntitySystemA));

        // Assert
        Assert.DoesNotThrow(() =>
        {
            this.world.AddSystem(new MockEntitySystemA());
        });
    }

    [Test]
    public void AddSystemShouldThrowArgumentExceptionWhenSystemOfSameTypeIsAlreadyAdded()
    {
        // Arrange
        var systemA = new MockEntitySystemA();
        var systemB = new MockEntitySystemB();
        var systemC = new MockEntitySystemA();

        this.world.AddSystem(systemA);
        this.world.AddSystem(systemB);

        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.world.AddSystem(systemC);
        });
    }

    [Test]
    public void AddSystemShouldThrowArgumentNullExceptionWhenSystemIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.world.AddSystem(null);
        });
    }

    [Test]
    public void EntityOnComponentsChangedShouldInvokeSystemAddOrRemoveByAspectWhenInvoked()
    {
        // Arrange
        var entity = new Entity();

        var systemA = new MockEntitySystemA()
        {
            IsMatchFunction = (_) =>
            {
                return true;
            },
            ProcessFunction = (entities) =>
            {
                Assert.AreSame(entity, entities.FirstOrDefault());
            },
        };

        var systemB = new MockEntitySystemB();

        this.world.AddSystem(systemA);
        this.world.AddSystem(systemB);

        this.world.AddEntity(entity);

        // Act
        entity.OnComponentsChanged.Invoke(entity, EventArgs.Empty);
    }

    [Test]
    public void EntityOnComponentsChangedShouldThrowArgumentExceptionWhenSenderIsNotEntity()
    {
        // Arrange
        var entity = new Entity();
        this.world.AddEntity(entity);

        // Act
        Assert.Throws<ArgumentException>(() =>
        {
            entity.OnComponentsChanged.Invoke(this, EventArgs.Empty);
        });
    }

    [Test]
    public void ProcessAllShouldInvokeSystemProcessWhenInvoked()
    {
        // Arrange
        var entity = new Entity();
        var system = new MockEntitySystemA()
        {
            ProcessFunction = (entities) =>
            {
                Assert.Pass();
            },

            IsMatchFunction = (entity) =>
            {
                return true;
            },
        };

        this.world.AddSystem(system);
        this.world.AddEntity(entity);

        // Act
        this.world.ProcessAll(GameLoopType.Update);
    }

    [Test]
    public void ProcessAllShouldNotProcessSystemWithUpdateAttributeWhenProcessingRenderSystems()
    {
        // Arrange
        var entity = new Entity();
        var systemA = new MockEntitySystemA()
        {
            ProcessFunction = (_) =>
            {
                // Fail if update system is processed.
                Assert.Fail();
            },
        };

        var systemB = new MockEntitySystemB();

        this.world.AddEntity(entity);
        this.world.AddSystem(systemA);
        this.world.AddSystem(systemB);

        // Act
        this.world.ProcessAll(GameLoopType.Render);

        // Assert
        Assert.Pass();
    }

    [Test]
    public void ProcessAllShouldProcessExecutionTypeFromAttributeWhenInvoked()
    {
        // Arrange
        var entity = new Entity();
        var system = new MockEntitySystemB()
        {
            IsMatchFunction = (_) =>
            {
                return true;
            },

            ProcessFunction = (_) =>
            {
                Assert.Pass();
            },
        };

        this.world.AddEntity(entity);
        this.world.AddSystem(system);

        // Act
        this.world.ProcessAll(GameLoopType.Render);
    }

    [Test]
    public void RemoveEntityShouldEntityNotFoundExceptionWhenEntityIsNotAdded()
    {
        // Act and assert
        Assert.Throws<EntityNotFoundException>(() =>
        {
            this.world.RemoveEntity(new Entity());
        });
    }

    [Test]
    public void RemoveEntityShouldInvokeSystemAddOrRemoveByAspectWhenInvoked()
    {
        // Arrange
        var systemA = new MockEntitySystemA();
        var systemB = new MockEntitySystemB()
        {
            IsMatchFunction = (_) =>
            {
                return true;
            },
            ProcessFunction = (entities) =>
            {
                // Assert
                Assert.Zero(entities.Count());
            },
        };

        this.world.AddSystem(systemA);
        this.world.AddSystem(systemB);

        var entity = new Entity();
        this.world.AddEntity(entity);

        // Act
        this.world.RemoveEntity(entity);
    }

    [Test]
    public void RemoveEntityShouldThrowArgumentNulLExceptionWhenEntityIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.world.RemoveEntity(null);
        });
    }

    [Test]
    public void RemoveEntityShouldUnhookFromEntityOnComponentsChangedWhenInvoked()
    {
        // Arrange
        var entity = new Entity();
        this.world.AddEntity(entity);

        // Act
        this.world.RemoveEntity(entity);

        // Assert
        Assert.Null(entity.OnComponentsChanged);
    }

    [Test]
    public void RemoveSystemShouldThrowArgumentExceptionWhenSystemHasNotBeenAddedToWorld()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.world.RemoveSystem(typeof(MockEntitySystemA));
        });
    }

    [Test]
    public void RemoveSystemShouldThrowArgumentExceptionWhenTypeDoesNotInheritFromEntitySYstemBase()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.world.RemoveSystem(typeof(EntityWorldTests));
        });
    }

    [Test]
    public void RemoveSystemShouldThrowArgumentNullExceptionWhenSystemIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.world.RemoveSystem(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.world = new EntityWorld();
    }
}
