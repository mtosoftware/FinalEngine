// <copyright file="BlendStateDescriptionTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering
{
    using System.Drawing;
    using FinalEngine.Rendering;
    using NUnit.Framework;

    public class BlendStateDescriptionTests
    {
        private BlendStateDescription description;

        [Test]
        public void ColorShouldReturnBlackWhenDefault()
        {
            // Assert
            Assert.AreEqual(Color.Black, this.description.Color);
        }

        [Test]
        public void ColorShouldReturnCornflowerBlueWhenSetToCornflowerBlue()
        {
            // Act
            this.description.Color = Color.CornflowerBlue;

            // Assert
            Assert.AreEqual(Color.CornflowerBlue, this.description.Color);
        }

        [Test]
        public void DestinationModeShouldReturnZeroWhenDefault()
        {
            // Assert
            Assert.AreEqual(BlendMode.Zero, this.description.DestinationMode);
        }

        [Test]
        public void DestinationModeShouldReturnZeroWhenSetToZero()
        {
            // Act
            this.description.DestinationMode = BlendMode.Zero;

            // Assert
            Assert.AreEqual(BlendMode.Zero, this.description.DestinationMode);
        }

        [Test]
        public void EnabledShouldReturnFalseWhenDefault()
        {
            // Assert
            Assert.False(this.description.Enabled);
        }

        [Test]
        public void EnabledShouldReturnTrueWhenSetToTrue()
        {
            // Act
            this.description.Enabled = true;

            // Assert
            Assert.True(this.description.Enabled);
        }

        [Test]
        public void EqualityOperatorShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new BlendStateDescription()
            {
                SourceMode = BlendMode.Zero,
                Color = Color.Green,
                DestinationMode = BlendMode.DestinationColor,
                Enabled = false,
                EquationMode = BlendEquationMode.Min,
            };

            var right = new BlendStateDescription()
            {
                SourceMode = BlendMode.DestinationAlpha,
                Color = Color.Gainsboro,
                DestinationMode = BlendMode.OneMinusDestinationColor,
                Enabled = true,
                EquationMode = BlendEquationMode.Subtract,
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
            var left = new BlendStateDescription()
            {
                SourceMode = BlendMode.Zero,
                Color = Color.Green,
                DestinationMode = BlendMode.DestinationColor,
                Enabled = false,
                EquationMode = BlendEquationMode.Min,
            };

            var right = new BlendStateDescription()
            {
                SourceMode = BlendMode.Zero,
                Color = Color.Green,
                DestinationMode = BlendMode.DestinationColor,
                Enabled = false,
                EquationMode = BlendEquationMode.Min,
            };

            // Act
            bool actual = left == right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNotBlendStateDescription()
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
            var left = new BlendStateDescription()
            {
                SourceMode = BlendMode.Zero,
                Color = Color.Green,
                DestinationMode = BlendMode.DestinationColor,
                Enabled = false,
                EquationMode = BlendEquationMode.Min,
            };

            var right = new BlendStateDescription()
            {
                SourceMode = BlendMode.DestinationAlpha,
                Color = Color.Gainsboro,
                DestinationMode = BlendMode.OneMinusDestinationColor,
                Enabled = true,
                EquationMode = BlendEquationMode.Subtract,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnTrueWhenObjectIsBlendStateDescriptionAndHasSameProperties()
        {
            // Arrange
            var left = new BlendStateDescription()
            {
                SourceMode = BlendMode.DestinationAlpha,
                Color = Color.Gainsboro,
                DestinationMode = BlendMode.OneMinusDestinationColor,
                Enabled = true,
                EquationMode = BlendEquationMode.Subtract,
            };

            object right = new BlendStateDescription()
            {
                SourceMode = BlendMode.DestinationAlpha,
                Color = Color.Gainsboro,
                DestinationMode = BlendMode.OneMinusDestinationColor,
                Enabled = true,
                EquationMode = BlendEquationMode.Subtract,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void EquationModeShouldReturnAddWhenDefault()
        {
            // Assert
            Assert.AreEqual(BlendEquationMode.Add, this.description.EquationMode);
        }

        [Test]
        public void EquationModeShouldReturnReverseSubtractWhenSetToReverseSubtract()
        {
            // Act
            this.description.EquationMode = BlendEquationMode.ReverseSubtract;

            // Assert
            Assert.AreEqual(BlendEquationMode.ReverseSubtract, this.description.EquationMode);
        }

        [Test]
        public void GetHashCodeShouldReturnSameAsOtherObjectWhenPropertiesAreEqual()
        {
            // Arrange
            var left = new BlendStateDescription()
            {
                SourceMode = BlendMode.DestinationAlpha,
                Color = Color.Gainsboro,
                DestinationMode = BlendMode.OneMinusDestinationColor,
                Enabled = true,
                EquationMode = BlendEquationMode.Subtract,
            };

            object right = new BlendStateDescription()
            {
                SourceMode = BlendMode.DestinationAlpha,
                Color = Color.Gainsboro,
                DestinationMode = BlendMode.OneMinusDestinationColor,
                Enabled = true,
                EquationMode = BlendEquationMode.Subtract,
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
            var left = new BlendStateDescription()
            {
                SourceMode = BlendMode.Zero,
                Color = Color.Green,
                DestinationMode = BlendMode.DestinationColor,
                Enabled = false,
                EquationMode = BlendEquationMode.Min,
            };

            var right = new BlendStateDescription()
            {
                SourceMode = BlendMode.Zero,
                Color = Color.Green,
                DestinationMode = BlendMode.DestinationColor,
                Enabled = false,
                EquationMode = BlendEquationMode.Min,
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
            var left = new BlendStateDescription()
            {
                SourceMode = BlendMode.DestinationAlpha,
                Color = Color.Green,
                DestinationMode = BlendMode.DestinationColor,
                Enabled = false,
                EquationMode = BlendEquationMode.Min,
            };

            var right = new BlendStateDescription()
            {
                SourceMode = BlendMode.OneMinusDestinationAlpha,
                Color = Color.Aqua,
                DestinationMode = BlendMode.SourceAlpha,
                Enabled = true,
                EquationMode = BlendEquationMode.Max,
            };

            // Act
            bool actual = left != right;

            // Assert
            Assert.True(actual);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.description = default;
        }

        [Test]
        public void SourceModeShouldReturnOneMinusSourceColorWhenSetToOneMinusSourceColor()
        {
            // Act
            this.description.SourceMode = BlendMode.OneMinusSourceColor;

            // Assert
            Assert.AreEqual(BlendMode.OneMinusSourceColor, this.description.SourceMode);
        }

        [Test]
        public void SourceModeShouldReturnOneWhenDefault()
        {
            // Assert
            Assert.AreEqual(BlendMode.One, this.description.SourceMode);
        }
    }
}
