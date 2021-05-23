// <copyright file="GameTimeInfoTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Launching
{
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Launching;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class GameTimeInfoTests
    {
        [Test]
        public void DeltaFShouldReturnSameAsInputWhenInvoked()
        {
            // Arrange
            const float Expected = 43.0f;

            var info = new GameTimeInfo()
            {
                Delta = Expected,
            };

            // Act
            float actual = info.DeltaF;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void DeltaShouldReturnSameAsInputWhenInvoked()
        {
            // Arrange
            const double Expected = 12.0d;

            var info = new GameTimeInfo()
            {
                Delta = Expected,
            };

            // Act
            double actual = info.Delta;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void EqualityOperatorShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new GameTimeInfo()
            {
                Delta = 12.0d,
                FrameRate = 43.0d,
            };

            var right = new GameTimeInfo()
            {
                Delta = 4325.0d,
                FrameRate = 432.0d,
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
            var left = new GameTimeInfo()
            {
                Delta = 12.0d,
                FrameRate = 43.0d,
            };

            var right = new GameTimeInfo()
            {
                Delta = 12.0d,
                FrameRate = 43.0d,
            };

            // Act
            bool actual = left == right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNotGameTimeInfo()
        {
            // Arrange
            var left = new GameTimeInfo()
            {
                Delta = 1243.0d,
                FrameRate = 43254.0d,
            };

            object right = new object();

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNull()
        {
            // Arrange
            var left = new GameTimeInfo()
            {
                Delta = 1243.0d,
                FrameRate = 43254.0d,
            };

            object right = null;

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new GameTimeInfo()
            {
                Delta = 12.0d,
                FrameRate = 43.0d,
            };

            var right = new GameTimeInfo()
            {
                Delta = 4325.0d,
                FrameRate = 432.0d,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnTrueWhenObjectIsGameTimeInfoAndHasSameProperties()
        {
            // Arrange
            var left = new GameTimeInfo()
            {
                Delta = 4325.0d,
                FrameRate = 432.0d,
            };

            object right = new GameTimeInfo()
            {
                Delta = 4325.0d,
                FrameRate = 432.0d,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void FrameRateFShouldReturnSameAsInputWhenInvoked()
        {
            // Arrange
            const float Expected = 1343.0f;

            var info = new GameTimeInfo()
            {
                FrameRate = Expected,
            };

            // Act
            float actual = info.FrameRateF;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void FrameRateShouldReturnSameAsInputWhenInvoked()
        {
            // Arrange
            const double Expected = 120.0d;

            var info = new GameTimeInfo()
            {
                FrameRate = Expected,
            };

            // Act
            double actual = info.FrameRate;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void GetHashCodeShouldNotReturnSameAsOtherObjectWhenPropertiesAreEqual()
        {
            // Arrange
            var left = new GameTimeInfo()
            {
                Delta = 4325.0d,
                FrameRate = 432.0d,
            };

            var right = new GameTimeInfo()
            {
                Delta = 44325.0d,
                FrameRate = 454332.0d,
            };

            // Act
            int leftHashCode = left.GetHashCode();
            int rightHashCode = right.GetHashCode();

            // Assert
            Assert.AreNotEqual(leftHashCode, rightHashCode);
        }

        [Test]
        public void GetHashCodeShouldReturnSameAsOtherObjectWhenPropertiesAreEqual()
        {
            // Arrange
            var left = new GameTimeInfo()
            {
                Delta = 4325.0d,
                FrameRate = 432.0d,
            };

            var right = new GameTimeInfo()
            {
                Delta = 4325.0d,
                FrameRate = 432.0d,
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
            var left = new GameTimeInfo()
            {
                Delta = 4325.0d,
                FrameRate = 432.0d,
            };

            var right = new GameTimeInfo()
            {
                Delta = 4325.0d,
                FrameRate = 432.0d,
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
            var left = new GameTimeInfo()
            {
                Delta = 4325.0d,
                FrameRate = 432.0d,
            };

            var right = new GameTimeInfo()
            {
                Delta = 4543325.0d,
                FrameRate = 43862.0d,
            };

            // Act
            bool actual = left != right;

            // Assert
            Assert.True(actual);
        }
    }
}