﻿// <copyright file="RasterStateDescriptionTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering
{
    using FinalEngine.Rendering;
    using NUnit.Framework;

    public class RasterStateDescriptionTests
    {
        private RasterStateDescription description;

        [Test]
        public void CullEnabledShouldReturnFalseWhenDefault()
        {
            // Act
            bool actual = this.description.CullEnabled;

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void CullEnabledShouldReturnTrueWhenSetToTrue()
        {
            // Act
            this.description.CullEnabled = true;

            // Assert
            Assert.True(this.description.CullEnabled);
        }

        [Test]
        public void CullModeShouldReturnBackWhenDefault()
        {
            // Arrange
            const FaceCullMode Expected = FaceCullMode.Back;

            // Act
            FaceCullMode actual = this.description.CullMode;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void CullModeShouldReturnFrontWhenSetToFront()
        {
            // Arrange
            const FaceCullMode Expected = FaceCullMode.Front;

            // Act
            this.description.CullMode = Expected;

            // Assert
            Assert.AreEqual(Expected, this.description.CullMode);
        }

        [Test]
        public void EqualityOperatorShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            var right = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Solid,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            // Act
            bool actual = left == right;

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualityOperatorShouldReturnTrueWhenPropertiesMatch()
        {
            // Arrange
            var left = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            var right = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            // Act
            bool actual = left == right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNotRasterStateDescription()
        {
            // Act
            bool actual = this.description.Equals(new object());

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNull()
        {
            // Act
            bool actual = this.description.Equals(null);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            var right = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Back,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnTrueWhenObjectIsRasterStateDescriptionAndHasSameProperties()
        {
            // Arrange
            var left = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            object right = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void FillModeShouldReturnSolidWhenDefault()
        {
            // Arrange
            const RasterMode Expected = RasterMode.Solid;

            // Act
            RasterMode actual = this.description.FillMode;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void FillModeShouldReturnWireframeWhenSetToWireframe()
        {
            // Arrange
            const RasterMode Expected = RasterMode.Wireframe;

            // Act
            this.description.FillMode = RasterMode.Wireframe;

            // Assert
            Assert.AreEqual(Expected, this.description.FillMode);
        }

        [Test]
        public void GetHashCodeShouldReturnSameAsOtherObjectWhenPropertiesAreEqual()
        {
            // Arrange
            var left = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            var right = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            // Act
            int leftHashCode = left.GetHashCode();
            int rightHashCode = right.GetHashCode();

            // Assert
            Assert.AreEqual(leftHashCode, rightHashCode);
        }

        [Test]
        public void InEqualityOperatorShouldReturnFalseWhenPropertiesMatch()
        {
            // Arrange
            var left = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            var right = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            // Act
            bool actual = left != right;

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void InEqualityOperatorShouldReturnTrueWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Wireframe,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            var right = new RasterStateDescription()
            {
                CullEnabled = false,
                CullMode = FaceCullMode.Front,
                FillMode = RasterMode.Solid,
                ScissorEnabled = true,
                WindingDirection = WindingDirection.Clockwise,
            };

            // Act
            bool actual = left != right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void ScissorEnabledShouldReturnFalseWhenDefault()
        {
            // Act
            bool actual = this.description.ScissorEnabled;

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void ScissorEnableShouldReturnTrueWhenSetToTrue()
        {
            // Act
            this.description.ScissorEnabled = true;

            // Assert
            Assert.True(this.description.ScissorEnabled);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.description = default;
        }

        [Test]
        public void WindingDirectionShouldReturnClockwiseWhenSetToClockwise()
        {
            // Arrange
            const WindingDirection Expected = WindingDirection.Clockwise;

            // Act
            this.description.WindingDirection = Expected;

            // Assert
            Assert.AreEqual(Expected, this.description.WindingDirection);
        }

        [Test]
        public void WindingDirectionShouldReturnCounterClockwiseWhenDefault()
        {
            // Arrange
            const WindingDirection Expected = WindingDirection.CounterClockwise;

            // Act
            WindingDirection actual = this.description.WindingDirection;

            // Assert
            Assert.AreEqual(Expected, actual);
        }
    }
}