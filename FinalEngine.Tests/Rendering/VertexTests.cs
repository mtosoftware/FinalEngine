// <copyright file="VertexTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Numerics;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Buffers;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class VertexTests
    {
        [Test]
        public void ColorShouldReturnZeroWhenInvoked()
        {
            // Arrange
            Vector4 expected = Vector4.Zero;
            var vertex = default(Vertex);

            // Act
            Vector4 actual = vertex.Color;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ColoShouldReturnUnitWWhenSet()
        {
            // Arrange
            Vector4 expected = Vector4.UnitW;
            var vertex = default(Vertex);

            // Act
            vertex.Color = expected;

            // Assert
            Assert.AreEqual(expected, vertex.Color);
        }

        [Test]
        public void EqualityOperatorShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            var right = new Vertex()
            {
                Position = new Vector2(2, 2),
                Color = new Vector4(1, 55, 33, 1),
                TextureCoordinate = new Vector2(33, 6),
                TextureSlotIndex = 7.1f,
            };

            // Act
            bool actual = left == right;

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualityOperatorShouldReturnTrueWhenPropertiesDoMatch()
        {
            // Arrange
            var left = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            var right = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            // Act
            bool actual = left == right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNotVertex()
        {
            // Arrange
            var left = default(Vertex);
            object right = new object();

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNull()
        {
            // Act
            bool actual = default(Vertex).Equals(null);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            var right = new Vertex()
            {
                Position = new Vector2(2, 2),
                Color = new Vector4(1, 55, 33, 1),
                TextureCoordinate = new Vector2(33, 6),
                TextureSlotIndex = 7.1f,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnTrueWhenObjectIsVertexAndHasSameProperties()
        {
            // Arrange
            var left = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            object right = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void EqualsShouldReturnTrueWhenPropertiesDoMatch()
        {
            // Arrange
            var left = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            var right = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
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
            var left = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            var right = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            // Act
            int leftHashCode = left.GetHashCode();
            int rightHashCode = right.GetHashCode();

            // Assert
            Assert.AreEqual(leftHashCode, rightHashCode);
        }

        [Test]
        public void InEqualityOperatorShouldReturnFalseWhenPropertiesDoMatch()
        {
            // Arrange
            var left = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            var right = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
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
            var left = new Vertex()
            {
                Position = new Vector2(1, 2),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                TextureSlotIndex = 7.0f,
            };

            var right = new Vertex()
            {
                Position = new Vector2(2, 2),
                Color = new Vector4(1, 55, 33, 1),
                TextureCoordinate = new Vector2(33, 6),
                TextureSlotIndex = 7.1f,
            };

            // Act
            bool actual = left != right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void InputElementsShouldReturnFourElementsWhenInvoked()
        {
            // Arrange
            var expected = new List<InputElement>()
            {
                // Position
                new (0, 2, InputElementType.Float, 0),

                // Color
                new (1, 4, InputElementType.Float, 2 * sizeof(float)),

                // Texture coordinate
                new (2, 2, InputElementType.Float, 6 * sizeof(float)),

                // Texture slot index
                new (3, 1, InputElementType.Float, 8 * sizeof(float)),
            };

            // Act
            IReadOnlyCollection<InputElement> actual = Vertex.InputElements;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PositionShouldReturnUnitXWhenSet()
        {
            // Arrange
            Vector2 expected = Vector2.UnitX;
            var vertex = default(Vertex);

            // Act
            vertex.Position = expected;

            // Assert
            Assert.AreEqual(expected, vertex.Position);
        }

        [Test]
        public void PositionShouldReturnZeroWhenInvoked()
        {
            // Arrange
            Vector2 expected = Vector2.Zero;
            var vertex = default(Vertex);

            // Act
            Vector2 actual = vertex.Position;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SizeInBytesShouldReturnThirtySixWhenInvoked()
        {
            // Arrange
            const int Expected = 36;

            // Act
            int actual = Vertex.SizeInBytes;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void TextureCoordianteShouldReturnZeroWhenInvoked()
        {
            // Arrange
            Vector2 expected = Vector2.Zero;
            var vertex = default(Vertex);

            // Act
            Vector2 actual = vertex.TextureCoordinate;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TextureCoordinateShouldReturnUnitYWhenSet()
        {
            // Arrange
            Vector2 expected = Vector2.UnitY;
            var vertex = default(Vertex);

            // Act
            vertex.TextureCoordinate = expected;

            // Assert
            Assert.AreEqual(expected, vertex.TextureCoordinate);
        }

        [Test]
        public void TextureSlotIndexShouldReturnSevenWhenSet()
        {
            // Arrange
            const float Expected = 7.0f;
            var vertex = default(Vertex);

            // Act
            vertex.TextureSlotIndex = Expected;

            // Assert
            Assert.AreEqual(Expected, vertex.TextureSlotIndex);
        }

        [Test]
        public void TextureSlotIndexShouldReturnZeroWhenInvoked()
        {
            // Arrange
            const float Expected = 0.0f;
            var vertex = default(Vertex);

            // Act
            float actual = vertex.TextureSlotIndex;

            // Assert
            Assert.AreEqual(Expected, actual);
        }
    }
}