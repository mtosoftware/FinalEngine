// <copyright file="EntitySystemBaseTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.ECS
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using FinalEngine.ECS;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class EntitySystemBaseTests
    {
        private readonly EntitySystemBase system;

        [Test]
        public void AddOrRemoveByAspectByAspectShouldAddEntityToSystemWhenIsMatchReturnsTrueAndEntityHasNotBeenPreviouslyAdded()
        {
            // Arrange
            var expected = new Entity();
            var system = new MockEntitySystem(GameLoopType.Update)
            {
                IsMatchFunction = (_) => true,
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
            var system = new MockEntitySystem(GameLoopType.Update)
            {
                IsMatchFunction = (_) => true,
            };

            system.AddOrRemoveByAspect(entity);

            system.IsMatchFunction = (_) => false;
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
            var system = new MockEntitySystem(GameLoopType.Update)
            {
                IsMatchFunction = (_) => true,
            };

            system.AddOrRemoveByAspect(entity);

            system.IsMatchFunction = (_) => false;
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
            var system = new MockEntitySystem(GameLoopType.Update);

            // Act and assert
            Assert.Throws<ArgumentNullException>(() => system.AddOrRemoveByAspect(null));
        }

        [Test]
        public void LoopTypeShouldReturnSameAsConstructorWhenInvoked()
        {
            // Arrange
            GameLoopType expected = GameLoopType.Update;
            var system = new MockEntitySystem(expected);

            // Act
            GameLoopType actual = system.LoopType;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ProcessShouldInvokeProtectedProcessWhenInvoked()
        {
            // Arrange
            var system = new MockEntitySystem(GameLoopType.Update)
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

        private class MockEntitySystem : EntitySystemBase
        {
            public MockEntitySystem(GameLoopType type)
            {
                this.LoopType = type;
            }

            public Predicate<IReadOnlyEntity> IsMatchFunction { get; set; }

            public override GameLoopType LoopType { get; }

            public Action<IEnumerable<Entity>> ProcessFunction { get; set; }

            protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
            {
                return this.IsMatchFunction?.Invoke(entity) ?? false;
            }

            protected override void Process([NotNull] IEnumerable<Entity> entities)
            {
                this.ProcessFunction?.Invoke(entities);
            }
        }
    }
}