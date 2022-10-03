// <copyright file="SpriteRenderSystemTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Linq;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.ECS.Components;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Components;
    using FinalEngine.Rendering.Systems;
    using FinalEngine.Rendering.Textures;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class SpriteRenderSystemTests
    {
        private Mock<ISpriteDrawer> drawer;

        private SpriteRenderSystemProtectedMethods system;

        private Mock<ITexture2D> texture;

        [Test]
        public void ConstructorShouldNotThrowExceptionWhenInvoked()
        {
            // Act and assert
            Assert.DoesNotThrow(() => new SpriteRenderSystem(this.drawer.Object));
        }

        [Test]
        public void ConstructorShouldSetLoopTypeToRenderWhenInvoked()
        {
            // Act
            var actual = this.system.LoopType;

            // Assert
            Assert.AreEqual(GameLoopType.Render, actual);
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenSpriteDrawerIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => new SpriteRenderSystem(null));
        }

        [Test]
        public void IsMatchShouldReturnFalseWHenEntityDoesNotHaveSpriteComponent()
        {
            // Arrange
            var entity = new Entity();

            entity.AddComponent<TransformComponent>();

            // Act
            var actual = this.system.InvokeIsMatch(entity);

            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void IsMatchShouldReturnFalseWHenEntityDoesNotHaveTransformComponent()
        {
            // Arrange
            var entity = new Entity();

            entity.AddComponent(new SpriteComponent());

            // Act
            var actual = this.system.InvokeIsMatch(entity);

            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void IsMatchShouldReturnTrueWhenEntityIsMatched()
        {
            // Arrange
            var entity = new Entity();

            entity.AddComponent<TransformComponent>();
            entity.AddComponent(new SpriteComponent());

            // Act
            var actual = this.system.InvokeIsMatch(entity);

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ProcessShouldInvokeDrawerBeginWhenInvoked()
        {
            // Act
            this.system.InvokeProcess(Enumerable.Empty<Entity>());

            // Assert
            this.drawer.Verify(x => x.Begin(), Times.Once);
        }

        [Test]
        public void ProcessShouldInvokeDrawerDrawWhenInvoked()
        {
            // Arrange
            var entity = new Entity();

            entity.AddComponent<TransformComponent>();
            entity.AddComponent(new SpriteComponent(this.texture.Object));

            var emptyEntity = new Entity();

            emptyEntity.AddComponent<TransformComponent>();
            emptyEntity.AddComponent(new SpriteComponent());

            var entities = new List<Entity>()
            {
                entity,
                emptyEntity,
            };

            // Act
            this.system.InvokeProcess(entities);

            // Assert
            this.drawer.Verify(
                x => x.Draw(
                this.texture.Object,
                Color.White,
                It.IsAny<Vector2>(),
                It.IsAny<Vector2>(),
                0,
                It.IsAny<Vector2>()),
                Times.Exactly(1));
        }

        [Test]
        public void ProcessShouldInvokeDrawerEndWhenInvoked()
        {
            // Act
            this.system.InvokeProcess(Enumerable.Empty<Entity>());

            // Assert
            this.drawer.Verify(x => x.End(), Times.Once);
        }

        [SetUp]
        public void Setup()
        {
            this.drawer = new Mock<ISpriteDrawer>();

            this.texture = new Mock<ITexture2D>();
            this.texture.Setup(x => x.Description).Returns(new Texture2DDescription()
            {
                Width = 256,
                Height = 512,
            });

            this.system = new SpriteRenderSystemProtectedMethods(this.drawer.Object);
        }

        private class SpriteRenderSystemProtectedMethods : SpriteRenderSystem
        {
            public SpriteRenderSystemProtectedMethods(ISpriteDrawer spriteDrawer)
                : base(spriteDrawer)
            {
            }

            public bool InvokeIsMatch([NotNull] IReadOnlyEntity entity)
            {
                return this.IsMatch(entity);
            }

            public void InvokeProcess([NotNull] IEnumerable<Entity> entities)
            {
                this.Process(entities);
            }
        }
    }
}