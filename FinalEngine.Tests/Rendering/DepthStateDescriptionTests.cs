// <copyright file="DepthStateDescriptionTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering
{
    using FinalEngine.Rendering;
    using NUnit.Framework;

    public class DepthStateDescriptionTests
    {
        private DepthStateDescription description;

        [Test]
        public void ComparisonModeShouldReturnLessEqualWhenSetToLessEqual()
        {
            // Act
            this.description.ComparisonMode = ComparisonMode.LessEqual;

            // Assert
            Assert.AreEqual(ComparisonMode.LessEqual, this.description.ComparisonMode);
        }

        [Test]
        public void ComparisonModeShouldReturnLessWhenDefault()
        {
            // Assert
            Assert.AreEqual(ComparisonMode.Less, this.description.ComparisonMode);
        }

        [Test]
        public void EqualityOperatorShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.LessEqual,
                ReadEnabled = false,
                WriteEnabled = false,
            };

            var right = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Greater,
                ReadEnabled = false,
                WriteEnabled = true,
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
            var left = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Greater,
                ReadEnabled = true,
                WriteEnabled = false,
            };

            var right = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Greater,
                ReadEnabled = true,
                WriteEnabled = false,
            };

            // Act
            bool actual = left == right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNotDepthStateDescription()
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
            var left = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Never,
                ReadEnabled = true,
                WriteEnabled = true,
            };

            var right = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.GreaterEqual,
                ReadEnabled = false,
                WriteEnabled = true,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnTrueWhenObjectIsDepthStateDescriptionAndHasSameProperties()
        {
            // Arrange
            var left = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Never,
                ReadEnabled = true,
                WriteEnabled = true,
            };

            object right = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Never,
                ReadEnabled = true,
                WriteEnabled = true,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void GetHashCodeShouldReturnSameAsOtherObjectWhenPropertiesAreEqual()
        {
            // Arrange
            var left = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Greater,
                ReadEnabled = true,
                WriteEnabled = false,
            };

            var right = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Greater,
                ReadEnabled = true,
                WriteEnabled = false,
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
            var left = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Greater,
                ReadEnabled = true,
                WriteEnabled = false,
            };

            var right = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Greater,
                ReadEnabled = true,
                WriteEnabled = false,
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
            var left = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Always,
                ReadEnabled = false,
                WriteEnabled = false,
            };

            var right = new DepthStateDescription()
            {
                ComparisonMode = ComparisonMode.Greater,
                ReadEnabled = true,
                WriteEnabled = false,
            };

            // Act
            bool actual = left != right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void ReadEnabledShouldReturnFalseWhenDefault()
        {
            // Assert
            Assert.False(this.description.ReadEnabled);
        }

        [Test]
        public void ReadEnabledShouldReturnTrueWhenSetToTrue()
        {
            // Act
            this.description.ReadEnabled = true;

            // Assert
            Assert.True(this.description.ReadEnabled);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.description = default;
        }

        [Test]
        public void WriteEnabledShouldReturnTrueWhenDefault()
        {
            // Assert
            Assert.True(this.description.WriteEnabled);
        }

        [Test]
        public void WriteEnabledSohuldReturnFalseWhenSetToFalse()
        {
            // Act
            this.description.WriteEnabled = false;

            // Assert
            Assert.False(this.description.WriteEnabled);
        }
    }
}