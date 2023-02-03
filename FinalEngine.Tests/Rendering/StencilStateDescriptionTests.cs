// <copyright file="StencilStateDescriptionTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering;

using FinalEngine.Rendering;
using NUnit.Framework;

public class StencilStateDescriptionTests
{
    private StencilStateDescription description;

    [Test]
    public void ComparisonModeShouldReturnAlwaysWhenDefault()
    {
        // Assert
        Assert.AreEqual(ComparisonMode.Always, this.description.ComparisonMode);
    }

    [Test]
    public void ComparisonModeShouldReturnNeverWhenSetToNever()
    {
        // Act
        this.description.ComparisonMode = ComparisonMode.Never;

        // Assert
        Assert.AreEqual(ComparisonMode.Never, this.description.ComparisonMode);
    }

    [Test]
    public void DepthFailShouldReturnKeepWhenDefault()
    {
        // Assert
        Assert.AreEqual(StencilOperation.Keep, this.description.DepthFail);
    }

    [Test]
    public void DepthFailShouldReturnReplaceWhenSetToReplace()
    {
        // Act
        this.description.DepthFail = StencilOperation.Replace;

        // Assert
        Assert.AreEqual(StencilOperation.Replace, this.description.DepthFail);
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
        var left = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.GreaterEqual,
            DepthFail = StencilOperation.Decrement,
            Enabled = true,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 456,
            ReferenceValue = 34,
            StencilFail = StencilOperation.Zero,
            WriteMask = 2,
        };

        var right = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Greater,
            DepthFail = StencilOperation.Invert,
            Enabled = false,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 46,
            ReferenceValue = 4456,
            StencilFail = StencilOperation.IncrementWrap,
            WriteMask = 76543,
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
        var left = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Greater,
            DepthFail = StencilOperation.Invert,
            Enabled = false,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 46,
            ReferenceValue = 4456,
            StencilFail = StencilOperation.IncrementWrap,
            WriteMask = 76543,
        };

        var right = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Greater,
            DepthFail = StencilOperation.Invert,
            Enabled = false,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 46,
            ReferenceValue = 4456,
            StencilFail = StencilOperation.IncrementWrap,
            WriteMask = 76543,
        };

        // Act
        bool actual = left == right;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void EqualsShouldReturnFalseWhenObjectIsNotStencilStateDescription()
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
        var left = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.GreaterEqual,
            DepthFail = StencilOperation.Decrement,
            Enabled = true,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 456,
            ReferenceValue = 34,
            StencilFail = StencilOperation.Zero,
            WriteMask = 2,
        };

        var right = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Less,
            DepthFail = StencilOperation.IncrementWrap,
            Enabled = true,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 6,
            ReferenceValue = 44,
            StencilFail = StencilOperation.Replace,
            WriteMask = 7543,
        };

        // Act
        bool actual = left.Equals(right);

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void EqualsShouldReturnTrueWhenObjectIsStencilStateDescriptionAndHasSameProperties()
    {
        // Arrange
        var left = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Less,
            DepthFail = StencilOperation.IncrementWrap,
            Enabled = true,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 6,
            ReferenceValue = 44,
            StencilFail = StencilOperation.Replace,
            WriteMask = 7543,
        };

        object right = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Less,
            DepthFail = StencilOperation.IncrementWrap,
            Enabled = true,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 6,
            ReferenceValue = 44,
            StencilFail = StencilOperation.Replace,
            WriteMask = 7543,
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
        var left = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Less,
            DepthFail = StencilOperation.IncrementWrap,
            Enabled = true,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 6,
            ReferenceValue = 44,
            StencilFail = StencilOperation.Replace,
            WriteMask = 7543,
        };

        object right = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Less,
            DepthFail = StencilOperation.IncrementWrap,
            Enabled = true,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 6,
            ReferenceValue = 44,
            StencilFail = StencilOperation.Replace,
            WriteMask = 7543,
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
        var left = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Less,
            DepthFail = StencilOperation.IncrementWrap,
            Enabled = true,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 6,
            ReferenceValue = 44,
            StencilFail = StencilOperation.Replace,
            WriteMask = 7543,
        };

        var right = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Less,
            DepthFail = StencilOperation.IncrementWrap,
            Enabled = true,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 6,
            ReferenceValue = 44,
            StencilFail = StencilOperation.Replace,
            WriteMask = 7543,
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
        var left = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Less,
            DepthFail = StencilOperation.IncrementWrap,
            Enabled = true,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 6,
            ReferenceValue = 44,
            StencilFail = StencilOperation.Replace,
            WriteMask = 7543,
        };

        var right = new StencilStateDescription()
        {
            ComparisonMode = ComparisonMode.Greater,
            DepthFail = StencilOperation.Invert,
            Enabled = false,
            Pass = StencilOperation.DecrementWrap,
            ReadMask = 46,
            ReferenceValue = 4456,
            StencilFail = StencilOperation.IncrementWrap,
            WriteMask = 76543,
        };

        // Act
        bool actual = left != right;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void PassShouldReturnIncrementWhenSetToIncrement()
    {
        // Act
        this.description.Pass = StencilOperation.Increment;

        // Assert
        Assert.AreEqual(StencilOperation.Increment, this.description.Pass);
    }

    [Test]
    public void PassShouldReturnKeepWhenDefault()
    {
        // Assert
        Assert.AreEqual(StencilOperation.Keep, this.description.Pass);
    }

    [Test]
    public void ReadMaskShouldReturnTenWhenSetToTen()
    {
        // Act
        this.description.ReadMask = 10;

        // Assert
        Assert.AreEqual(10, this.description.ReadMask);
    }

    [Test]
    public void ReadMaskShouldReturnZeroWhenDefault()
    {
        // Assert
        Assert.Zero(this.description.ReadMask);
    }

    [Test]
    public void ReferenceValueShouldReturnSevenWhenSetToSeven()
    {
        // Act
        this.description.ReferenceValue = 7;

        // Assert
        Assert.AreEqual(7, this.description.ReferenceValue);
    }

    [Test]
    public void ReferenceValueShouldReturnZeroWhenDefault()
    {
        // Assert
        Assert.Zero(this.description.ReferenceValue);
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.description = default;
    }

    [Test]
    public void StencilFailShouldReturnDecrementWrapWhenSetToDecrementWrap()
    {
        // Act
        this.description.StencilFail = StencilOperation.DecrementWrap;

        // Assert
        Assert.AreEqual(StencilOperation.DecrementWrap, this.description.StencilFail);
    }

    [Test]
    public void StencilFailShouldReturnKeepWhenDefault()
    {
        // Assert
        Assert.AreEqual(StencilOperation.Keep, this.description.StencilFail);
    }

    [Test]
    public void WriteMaskShouldReturnFourWhenSetToFour()
    {
        // Act
        this.description.WriteMask = 4;

        // Assert
        Assert.AreEqual(4, this.description.WriteMask);
    }

    [Test]
    public void WriteShouldReturnNegativeOneWhenDefault()
    {
        // Assert
        Assert.AreEqual(-1, this.description.WriteMask);
    }
}
