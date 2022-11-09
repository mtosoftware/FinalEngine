// <copyright file="MeshVertexTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering
{
    using System.Collections.Generic;
    using System.Numerics;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Buffers;
    using NUnit.Framework;

    public class MeshVertexTests
    {
        [Test]
        public void ColorShouldReturnZeroWhenInvoked()
        {
            // Arrange
            var expected = Vector4.Zero;
            var vertex = default(MeshVertex);

            // Act
            var actual = vertex.Color;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ColoShouldReturnUnitWWhenSet()
        {
            // Arrange
            var expected = Vector4.UnitW;
            var vertex = default(MeshVertex);

            // Act
            vertex.Color = expected;

            // Assert
            Assert.AreEqual(expected, vertex.Color);
        }

        [Test]
        public void EqualityOperatorShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new MeshVertex()
            {
                Position = new Vector3(1, 2, 3),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(10, 10, 10),
            };

            var right = new MeshVertex()
            {
                Position = new Vector3(2, 2, 3),
                Color = new Vector4(1, 55, 33, 1),
                TextureCoordinate = new Vector2(33, 6),
                Normal = new Vector3(10, 10, 10),
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
            var left = new MeshVertex()
            {
                Position = new Vector3(1, 2, 3),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(12, 13, 14),
            };

            var right = new MeshVertex()
            {
                Position = new Vector3(1, 2, 3),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(12, 13, 14),
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
            var left = default(MeshVertex);
            object right = new MeshVertexTests();

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNull()
        {
            // Act
            bool actual = default(MeshVertex).Equals(null);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new MeshVertex()
            {
                Position = new Vector3(1, 2, 3),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(10, 10, 10),
            };

            var right = new MeshVertex()
            {
                Position = new Vector3(2, 2, 4),
                Color = new Vector4(1, 55, 33, 1),
                TextureCoordinate = new Vector2(33, 6),
                Normal = new Vector3(1, 1, 1),
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
            var left = new MeshVertex()
            {
                Position = new Vector3(1, 2, 3),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(10, 12, 13),
            };

            object right = new MeshVertex()
            {
                Position = new Vector3(1, 2, 3),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(10, 12, 13),
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
            var left = new MeshVertex()
            {
                Position = new Vector3(1, 2, 3),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(14, 15, 16),
            };

            var right = new MeshVertex()
            {
                Position = new Vector3(1, 2, 3),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(14, 15, 16),
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
            var left = new MeshVertex()
            {
                Position = new Vector3(1, 2, 4),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(654, 423, 425),
            };

            var right = new MeshVertex()
            {
                Position = new Vector3(1, 2, 4),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(654, 423, 425),
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
            var left = new MeshVertex()
            {
                Position = new Vector3(1, 2, 1),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(1, 2, 33),
            };

            var right = new MeshVertex()
            {
                Position = new Vector3(1, 2, 1),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(1, 2, 33),
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
            var left = new MeshVertex()
            {
                Position = new Vector3(1, 2, 3),
                Color = new Vector4(1, 2, 3, 4),
                TextureCoordinate = new Vector2(5, 6),
                Normal = new Vector3(10, 10, 10),
            };

            var right = new MeshVertex()
            {
                Position = new Vector3(2, 2, 4),
                Color = new Vector4(1, 55, 33, 1),
                TextureCoordinate = new Vector2(33, 6),
                Normal = new Vector3(10, 2, 5),
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
                new InputElement(0, 3, InputElementType.Float, 0),

                // Color
                new InputElement(1, 4, InputElementType.Float, 3 * sizeof(float)),

                // Texture Coordinate
                new InputElement(2, 2, InputElementType.Float, 7 * sizeof(float)),

                // Normal
                new InputElement(3, 3, InputElementType.Float, 9 * sizeof(float)),
            };

            // Act
            var actual = MeshVertex.InputElements;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NormalShouldReturnUnitYWhenSet()
        {
            // Arrange
            var expected = Vector3.UnitY;
            var vertex = default(MeshVertex);

            // Act
            vertex.Normal = expected;

            // Assert
            Assert.AreEqual(expected, vertex.Normal);
        }

        [Test]
        public void NormalShouldReturnZeroWhenInvoked()
        {
            // Arrange
            var expected = Vector3.Zero;
            var vertex = default(MeshVertex);

            // Act
            var actual = vertex.Normal;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PositionShouldReturnUnitXWhenSet()
        {
            // Arrange
            var expected = Vector3.UnitX;
            var vertex = default(MeshVertex);

            // Act
            vertex.Position = expected;

            // Assert
            Assert.AreEqual(expected, vertex.Position);
        }

        [Test]
        public void PositionShouldReturnZeroWhenInvoked()
        {
            // Arrange
            var expected = Vector3.Zero;
            var vertex = default(MeshVertex);

            // Act
            var actual = vertex.Position;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SizeInBytesShouldReturnThirtySixWhenInvoked()
        {
            // Arrange
            const int expected = 48;

            // Act
            int actual = MeshVertex.SizeInBytes;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TextureCoordianteShouldReturnZeroWhenInvoked()
        {
            // Arrange
            var expected = Vector2.Zero;
            var vertex = default(MeshVertex);

            // Act
            var actual = vertex.TextureCoordinate;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TextureCoordinateShouldReturnUnitYWhenSet()
        {
            // Arrange
            var expected = Vector2.UnitY;
            var vertex = default(MeshVertex);

            // Act
            vertex.TextureCoordinate = expected;

            // Assert
            Assert.AreEqual(expected, vertex.TextureCoordinate);
        }
    }
}
