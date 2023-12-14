// <copyright file="MeshVertexTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.Vapor.Primitives;

using System.Linq;
using System.Numerics;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Geometry;
using NUnit.Framework;

[TestFixture]
public sealed class MeshVertexTests
{
    private MeshVertex vertex;

    [Test]
    public void ColorShouldReturnTwoWhenSetToTwo()
    {
        // Arrange
        var expected = new Vector4(2, 2, 2, 2);

        // Act
        this.vertex.Color = expected;

        // Assert
        Assert.That(this.vertex.Color, Is.EqualTo(expected));
    }

    [Test]
    public void ColorShouldReturnZeroWhenNotSet()
    {
        // Arrange
        var expected = Vector4.Zero;

        // Act
        var actual = this.vertex.Color;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void EqualityOperatorShouldReturnFalseWhenPropertiesDontMatch()
    {
        // Arrange
        var left = new MeshVertex()
        {
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
        };

        var right = new MeshVertex()
        {
            Position = new Vector3(2, 2, 2),
            Color = new Vector4(1, 55, 33, 1),
            TextureCoordinate = new Vector2(33, 6),
            Normal = new Vector3(1, 1, 2),
            Tangent = new Vector3(3, 4, 4),
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
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
        };

        var right = new MeshVertex()
        {
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
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
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
        };

        var right = new MeshVertex()
        {
            Position = new Vector3(1, 12, 2),
            Color = new Vector4(1, 2, 33, 4),
            TextureCoordinate = new Vector2(455, 6),
            Tangent = new Vector3(1, 12, 3),
            Normal = new Vector3(3, 4, 5),
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
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
        };

        object right = new MeshVertex()
        {
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
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
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
        };

        var right = new MeshVertex()
        {
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
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
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
        };

        var right = new MeshVertex()
        {
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
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
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
        };

        var right = new MeshVertex()
        {
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
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
            Position = new Vector3(1, 2, 2),
            Color = new Vector4(1, 2, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 2, 3),
            Normal = new Vector3(3, 4, 5),
        };

        var right = new MeshVertex()
        {
            Position = new Vector3(31, 2, 2),
            Color = new Vector4(1, 42, 3, 4),
            TextureCoordinate = new Vector2(5, 6),
            Tangent = new Vector3(1, 12, 3),
            Normal = new Vector3(3, 4, 15),
        };

        // Act
        bool actual = left != right;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void InputElementsShouldReturnColorElementWhenInvoked()
    {
        // Arrange
        var expected = new InputElement(1, 4, InputElementType.Float, 3 * sizeof(float));

        // Act
        var actual = MeshVertex.InputElements.OrderBy(x =>
        {
            return x.Index;
        }).ToArray()[1];

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void InputElementsShouldReturnNormalElementWhenInvoked()
    {
        // Arrange
        var expected = new InputElement(3, 3, InputElementType.Float, 9 * sizeof(float));

        // Act
        var actual = MeshVertex.InputElements.OrderBy(x =>
        {
            return x.Index;
        }).ToArray()[3];

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void InputElementsShouldReturnPositionElementWhenInvoked()
    {
        // Arrange
        var expected = new InputElement(0, 3, InputElementType.Float, 0);

        // Act
        var actual = MeshVertex.InputElements.OrderBy(x =>
        {
            return x.Index;
        }).First();

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void InputElementsShouldReturnTangentElementWhenInvoked()
    {
        // Arrange
        var expected = new InputElement(4, 3, InputElementType.Float, 12 * sizeof(float));

        // Act
        var actual = MeshVertex.InputElements.OrderBy(x =>
        {
            return x.Index;
        }).ToArray()[4];

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void InputElementsShouldReturnTextureCoordinateElementWhenInvoked()
    {
        // Arrange
        var expected = new InputElement(2, 2, InputElementType.Float, 7 * sizeof(float));

        // Act
        var actual = MeshVertex.InputElements.OrderBy(x =>
        {
            return x.Index;
        }).ToArray()[2];

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void NormalShouldReturnTwoWhenSetToTwo()
    {
        // Arrange
        var expected = new Vector3(2, 2, 2);

        // Act
        this.vertex.Normal = expected;

        // Assert
        Assert.That(this.vertex.Normal, Is.EqualTo(expected));
    }

    [Test]
    public void NormalShouldReturnZeroWhenNotSet()
    {
        // Arrange
        var expected = Vector3.Zero;

        // Act
        var actual = this.vertex.Normal;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void PositionShouldReturnTwoWhenSetToTwo()
    {
        // Arrange
        var expected = new Vector3(2, 2, 2);

        // Act
        this.vertex.Position = expected;

        // Assert
        Assert.That(this.vertex.Position, Is.EqualTo(expected));
    }

    [Test]
    public void PositionShouldReturnZeroWhenNotSet()
    {
        // Arrange
        var expected = Vector3.Zero;

        // Act
        var actual = this.vertex.Position;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [SetUp]
    public void Setup()
    {
        this.vertex = default;
    }

    [Test]
    public void SizeInBytesShouldReturnSixtyBytesWhenInvoked()
    {
        // Arrange
        const int expected = 60;

        // Act
        int actual = MeshVertex.SizeInBytes;

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void TangentShouldReturnTwoWhenSetToTwo()
    {
        // Arrange
        var expected = new Vector3(2, 2, 2);

        // Act
        this.vertex.Tangent = expected;

        // Assert
        Assert.That(this.vertex.Tangent, Is.EqualTo(expected));
    }

    [Test]
    public void TangentShouldReturnZeroWhenNotSet()
    {
        // Arrange
        var expected = Vector3.Zero;

        // Act
        var actual = this.vertex.Tangent;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TextureCoordinateShouldReturnTwoWhenSetToTwo()
    {
        // Arrange
        var expected = new Vector2(2, 2);

        // Act
        this.vertex.TextureCoordinate = expected;

        // Assert
        Assert.That(this.vertex.TextureCoordinate, Is.EqualTo(expected));
    }

    [Test]
    public void TextureCoordinateShouldReturnZeroWhenNotSet()
    {
        // Arrange
        var expected = Vector2.Zero;

        // Act
        var actual = this.vertex.TextureCoordinate;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
