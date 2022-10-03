// <copyright file="TransformComponentTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.ECS.Components
{
    using System.Numerics;
    using FinalEngine.ECS.Components;
    using NUnit.Framework;

    [TestFixture]
    public class TransformComponentTests
    {
        private TransformComponent component;

        [Test]
        public void ConstructorShouldNotThrowExceptionWhenInvoked()
        {
            // Act and assert
            Assert.DoesNotThrow(() => new TransformComponent());
        }

        [Test]
        public void ConstructorShouldSetScaleToVector2OneWhenInvoked()
        {
            // Arrange
            var expected = Vector2.One;
            var component = new TransformComponent();

            // Act
            var actual = component.Scale;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PositionShouldReturnSameAsXWhenInvoked()
        {
            // Arrange
            const float Expected = 122;
            this.component.X = Expected;

            // Act
            var actual = this.component.Position.X;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void PositionShouldReturnSameAsYWhenInvoked()
        {
            // Arrange
            const float Expected = 4122;
            this.component.Y = Expected;

            // Act
            var actual = this.component.Position.Y;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void PositionShouldSetXWhenSet()
        {
            // Arrange
            const float Expected = 3493;
            this.component.Position = new Vector2(Expected, 0);

            // Act
            var actual = this.component.X;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void PositionShouldSetYWhenSet()
        {
            // Arrange
            const float Expected = 451;
            this.component.Position = new Vector2(0, Expected);

            // Act
            var actual = this.component.Y;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void RotationShouldReturnTwelveWhenSetToFive()
        {
            // Arrange
            const float Expected = 12;
            this.component.Rotation = Expected;

            // Act
            var actual = this.component.Rotation;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void RotationShouldReturnZeroWhenInvoked()
        {
            // Arrange
            const float Expected = 0;

            // Act
            var actual = this.component.Rotation;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void ScaleShouldReturnOneWhenInvoked()
        {
            // Arrange
            var expected = Vector2.One;

            // Act
            var actual = this.component.Scale;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ScaleShouldReturnSameAsXWhenInvoked()
        {
            // Arrange
            const float Expected = 122;
            this.component.ScaleX = Expected;

            // Act
            var actual = this.component.Scale.X;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void ScaleShouldReturnSameAsYWhenInvoked()
        {
            // Arrange
            const float Expected = 4122;
            this.component.ScaleY = Expected;

            // Act
            var actual = this.component.Scale.Y;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void ScaleShouldSetXWhenSet()
        {
            // Arrange
            const float Expected = 3493;
            this.component.Scale = new Vector2(Expected, 0);

            // Act
            var actual = this.component.ScaleX;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void ScaleShouldSetYWhenSet()
        {
            // Arrange
            const float Expected = 451;
            this.component.Scale = new Vector2(0, Expected);

            // Act
            var actual = this.component.ScaleY;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void ScaleXShouldReturnFiveWhenSetToFive()
        {
            // Arrange
            const float Expected = 5;
            this.component.ScaleX = Expected;

            // Act
            var actual = this.component.ScaleX;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void ScaleXShouldReturnOneWhenInvoked()
        {
            // Arrange
            const float Expected = 1;

            // Act
            var actual = this.component.ScaleX;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void ScaleYShouldReturnFourWhenSetToFive()
        {
            // Arrange
            const float Expected = 4;
            this.component.ScaleY = Expected;

            // Act
            var actual = this.component.ScaleY;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void ScaleYShouldReturnOneWhenInvoked()
        {
            // Arrange
            const float Expected = 1;

            // Act
            var actual = this.component.ScaleY;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.component = new TransformComponent();
        }

        [Test]
        public void XShouldReturnFiveWhenSetToFive()
        {
            // Arrange
            const float Expected = 5;
            this.component.X = Expected;

            // Act
            var actual = this.component.X;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void XShouldReturnZeroWhenInvoked()
        {
            // Arrange
            const float Expected = 0;

            // Act
            var actual = this.component.X;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void YShouldReturnFourWhenSetToFive()
        {
            // Arrange
            const float Expected = 4;
            this.component.Y = Expected;

            // Act
            var actual = this.component.Y;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void YShouldReturnZeroWhenInvoked()
        {
            // Arrange
            const float Expected = 0;

            // Act
            var actual = this.component.Y;

            // Assert
            Assert.AreEqual(Expected, actual);
        }
    }
}