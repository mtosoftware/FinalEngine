// <copyright file="EntityTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.ECS
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Dynamic;
    using FinalEngine.ECS;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class EntityTests
    {
        private Entity entity;

        [Test]
        public void AddComponentGenericShouldRaiseOnComponentsChangedWhenComponentIsAdded()
        {
            // Arrange
            this.entity.OnComponentsChanged += (s, e) =>
            {
                // Assert
                Assert.AreSame(this.entity, s);
            };

            // Act
            this.entity.AddComponent<MockComponentA>();
        }

        [Test]
        public void AddComponentGenericShouldThrowArgumentExceptionWhenComponentOfSameTypeAlreadyAdded()
        {
            // Arrange
            var componentA = new MockComponentA();
            var componentB = new MockComponentA();

            this.entity.AddComponent(componentA);

            // Act and assert
            Assert.Throws<ArgumentException>(() => this.entity.AddComponent<MockComponentA>());
        }

        [Test]
        public void AddComponentShouldRaiseOnComponentsChangedWhenComponentIsAdded()
        {
            // Arrange
            var component = new MockComponentA();

            this.entity.OnComponentsChanged += (s, e) =>
            {
                // Assert
                Assert.AreSame(this.entity, s);
            };

            // Act
            this.entity.AddComponent(component);
        }

        [Test]
        public void AddComponentShouldThrowArgumentExceptionWhenComponentOfSameTypeAlreadyAdded()
        {
            // Arrange
            var componentA = new MockComponentA();
            var componentB = new MockComponentA();

            this.entity.AddComponent(componentA);

            // Act and assert
            Assert.Throws<ArgumentException>(() => this.entity.AddComponent(componentB));
        }

        [Test]
        public void AddComponentShouldThrowArgumentNullExceptionWhenComponentIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.entity.AddComponent(null));
        }

        [Test]
        public void ContainsComponentGenericShouldReturnFalseWhenComponentTypeHasNotBeenAdded()
        {
            // Arrange
            this.entity.AddComponent<MockComponentB>();

            // Act
            bool actual = this.entity.ContainsComponent<MockComponentA>();

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void ContainsComponentGenericShouldReturnTrueWhenComponentTypeHasBeenAdded()
        {
            // Arrange
            this.entity.AddComponent<MockComponentA>();

            // Act
            bool actual = this.entity.ContainsComponent<MockComponentA>();

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void ContainsComponentInstanceShouldReturnFalseWhenComponentHasNotBeenAdded()
        {
            // Arrange
            var component = new MockComponentA();

            // Act
            bool actual = this.entity.ContainsComponent(component);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void ContainsComponentInstanceShouldReturnTrueWhenComponentHasBeenAdded()
        {
            // Arrange
            var componentA = new MockComponentA();
            var componentB = new MockComponentB();

            this.entity.AddComponent(componentA);
            this.entity.AddComponent(componentB);

            // Act
            bool actual = this.entity.ContainsComponent(componentB);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void ContainsComponentInstanceShouldThrowArgumentNullExceptionWhenComponentIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.entity.ContainsComponent(component: null));
        }

        [Test]
        public void ContainsComponentTypeShouldReturnFalseWhenComponentTypeHasNotBeenAdded()
        {
            // Arrange
            this.entity.AddComponent(new MockComponentB());

            // Act
            bool actual = this.entity.ContainsComponent(typeof(MockComponentA));

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void ContainsComponentTypeShouldReturnTrueWhenComponentTypeHasBeenAdded()
        {
            // Arrange
            this.entity.AddComponent(new MockComponentA());

            // Act
            bool actual = this.entity.ContainsComponent(typeof(MockComponentA));

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void ContainsComponentTypeShouldThrowArgumentExceptionWhenTypeDoesNotImplementIComponent()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() => this.entity.ContainsComponent(this.GetType()));
        }

        [Test]
        public void ContainsComponentTypeShouldThrowArgumentNullExceptionWhenTypeIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.entity.ContainsComponent(type: null));
        }

        [Test]
        public void GetComponentGenericShouldReturnSameComponentWhenComponentTypeIsAdded()
        {
            // Arrange
            var expected = new MockComponentA();
            this.entity.AddComponent(expected);

            // Act
            MockComponentA actual = this.entity.GetComponent<MockComponentA>();

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GetComponentGenericShouldThrowArgumentExceptionWhenComponentTypeIsNotAdded()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() => this.entity.GetComponent<MockComponentA>());
        }

        [Test]
        public void GetComponentShouldReturnSameComponentWhenComponentTypeIsAdded()
        {
            // Arrange
            var expected = new MockComponentA();
            this.entity.AddComponent(expected);

            // Act
            IComponent actual = this.entity.GetComponent(typeof(MockComponentA));

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GetComponentShouldThrowArgumentExceptionWhenComponentTypeIsNotAdded()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() => this.entity.GetComponent(typeof(MockComponentA)));
        }

        [Test]
        public void GetComponentShouldThrowArgumentExceptionWhenTypeDoesNotImplementIComponent()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() => this.entity.GetComponent(typeof(EntityTests)));
        }

        [Test]
        public void GetComponentShouldThrowArgumentNullExceptionWhenTypeIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.entity.GetComponent(null));
        }

        [Test]
        public void RemoveComponentGenericShouldRemoveComponentWhenComponentWasAdded()
        {
            // Arrange
            this.entity.AddComponent<MockComponentA>();

            // Act
            this.entity.RemoveComponent<MockComponentA>();

            // Assert
            Assert.False(this.entity.ContainsComponent<MockComponentA>());
        }

        [Test]
        public void RemoveComponentGenericShouldThrowArgumentExceptionWhenComponentTypeIsNotAdded()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() => this.entity.RemoveComponent<MockComponentA>());
        }

        [Test]
        public void RemoveComponentInstanceShouldRaiseOnComponentsChangedWhenComponentIsRemoved()
        {
            // Arrange
            var component = new MockComponentA();
            this.entity.AddComponent(component);

            this.entity.OnComponentsChanged += (s, e) =>
            {
                // Assert
                Assert.AreSame(this.entity, s);
            };

            // Act
            this.entity.RemoveComponent(component);
        }

        [Test]
        public void RemoveComponentInstanceShouldThrowArgumentNullExceptionWhenComponentIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.entity.RemoveComponent(component: null));
        }

        [Test]
        public void RemoveComponentShouldRemoveComponentWhenComponentWasAdded()
        {
            // Arrange
            var component = new MockComponentA();
            this.entity.AddComponent(component);

            // Act
            this.entity.RemoveComponent(component);

            // Assert
            Assert.False(this.entity.ContainsComponent(component));
        }

        [Test]
        public void RemoveComponentShouldThrowArgumentExceptionWhenComponentNotAdded()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() => this.entity.RemoveComponent(new MockComponentA()));
        }

        [Test]
        public void RemoveComponentTypeShouldRaiseOnComponentsChangedWhenComponentIsRemoved()
        {
            // Arrange
            this.entity.AddComponent<MockComponentA>();

            this.entity.OnComponentsChanged += (s, e) =>
            {
                // Assert
                Assert.AreSame(this.entity, s);
            };

            // Act
            this.entity.RemoveComponent(typeof(MockComponentA));
        }

        [Test]
        public void RemoveComponentTypeShouldThrowArgumentExceptionWhenComponenetTypeHasNotBeenAdded()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() => this.entity.RemoveComponent(typeof(MockComponentA)));
        }

        [Test]
        public void RemoveComponentTypeShouldThrowArgumentExceptionWhenTypeDoesNotImplementIComponent()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() => this.entity.RemoveComponent(typeof(EntityTests)));
        }

        [Test]
        public void RemoveComponentTypeShouldThrowArgumentNullExceptionWhenTypeIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.entity.RemoveComponent(type: null));
        }

        [Test]
        public void RemoveGenericShouldRaiseOnComponentsChangedWhenComponentIsRemoved()
        {
            // Arrange
            this.entity.AddComponent<MockComponentA>();

            this.entity.OnComponentsChanged += (s, e) => Assert.AreSame(this.entity, s);

            // Act
            this.entity.RemoveComponent<MockComponentA>();
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.entity = new Entity();
        }

        [Test]
        public void TryGetMemberShouldOutComponentWhenComponentTypeNameDoesMatchMemberNameExcludingComponentIfEndsWithComponent()
        {
            // Arrange
            var binder = new GetMemberBinderMock("Mock");
            var expected = new MockComponent();

            this.entity.AddComponent(expected);
            this.entity.AddComponent<MockComponentA>();

            // Act
            _ = this.entity.TryGetMember(binder, out object actual);

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void TryGetMemberShouldOutNullWhenComponentTypeNameDoesMatchMemberNameIncludingComponent()
        {
            // Arrange
            var binder = new GetMemberBinderMock("MockComponent");

            this.entity.AddComponent<MockComponent>();
            this.entity.AddComponent<MockComponentA>();

            // Act
            _ = this.entity.TryGetMember(binder, out object actual);

            // Assert
            Assert.Null(actual);
        }

        [Test]
        public void TryGetMemberShouldReturnFalseWhenComponentTypeNameDoesMatchMemberNameIncludingComponent()
        {
            // Arrange
            var binder = new GetMemberBinderMock("MockComponent");

            this.entity.AddComponent<MockComponent>();
            this.entity.AddComponent<MockComponentA>();

            // Act
            bool result = this.entity.TryGetMember(binder, out _);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void TryGetMemberShouldReturnTrueWhenComponentTypeNameDoesMatchMemberNameExcludingComponentIfEndsWithComponent()
        {
            // Arrange
            var binder = new GetMemberBinderMock("Mock");

            this.entity.AddComponent<MockComponent>();
            this.entity.AddComponent<MockComponentA>();

            // Act
            bool result = this.entity.TryGetMember(binder, out _);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void TryGetMemberShouldThrowArgumentNullExceptionWhenBinderIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.entity.TryGetMember(null, out _));
        }

        private class GetMemberBinderMock : GetMemberBinder
        {
            public GetMemberBinderMock(string name)
                 : base(name, false)
            {
            }

            public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
            {
                Assert.Fail();
                return null;
            }
        }

        private class MockComponent : IComponent
        {
        }

        private class MockComponentA : IComponent
        {
        }

        private class MockComponentB : IComponent
        {
        }
    }
}