// <copyright file="TextureQualitySettingsTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.Settings;

using FinalEngine.Rendering.Textures;
using FinalEngine.Rendering.Vapor.Settings;
using NUnit.Framework;

[TestFixture]
public class TextureQualitySettingsTests
{
    private TextureQualitySettings settings;

    [Test]
    public void EqualityOperatorShouldReturnFalseWhenPropertiesDontMatch()
    {
        // Arrange
        var left = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
        };

        var right = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Bilinear,
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
        var left = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
        };

        var right = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
        };

        // Act
        bool actual = left == right;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void EqualsShouldReturnFalseWhenObjectIsNotTextureQualitySettings()
    {
        // Act
        bool actual = this.settings.Equals(new object());

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void EqualsShouldReturnFalseWhenObjectIsNull()
    {
        // Act
        bool actual = this.settings.Equals(null);

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void EqualsShouldReturnFalseWhenPropertiesDontMatch()
    {
        // Arrange
        var left = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
        };

        var right = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.NearestNeighbour,
        };

        // Act
        bool actual = left.Equals(right);

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void EqualsShouldReturnTrueWhenObjectIsTextureQualitySettingsAndHasSameProperties()
    {
        // Arrange
        var left = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
        };

        object right = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
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
        var left = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
        };

        var right = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
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
        var left = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
        };

        var right = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
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
        var left = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Trilinear,
        };

        var right = new TextureQualitySettings()
        {
            FilterType = TextureFilterType.Bilinear,
        };

        // Act
        bool actual = left != right;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void MagFilterShouldReturnLinearByDefault()
    {
        // Arrange
        this.settings.FilterType = (TextureFilterType)(-1);

        // Act
        var actual = this.settings.MagFilter;

        // Assert
        Assert.That(actual, Is.EqualTo(TextureFilterMode.Linear));
    }

    [Test]
    public void MagFilterShouldReturnLinearWhenFilterTypeBilinear()
    {
        // Arrange
        this.settings.FilterType = TextureFilterType.Bilinear;

        // Act
        var actual = this.settings.MagFilter;

        // Assert
        Assert.That(actual, Is.EqualTo(TextureFilterMode.Linear));
    }

    [Test]
    public void MagFilterShouldReturnLinearWhenFilterTypeTrilinear()
    {
        // Arrange
        this.settings.FilterType = TextureFilterType.Trilinear;

        // Act
        var actual = this.settings.MagFilter;

        // Assert
        Assert.That(actual, Is.EqualTo(TextureFilterMode.Linear));
    }

    [Test]
    public void MagFilterShouldReturnNearestWhenFilterTypeNearestNeighbour()
    {
        // Arrange
        this.settings.FilterType = TextureFilterType.NearestNeighbour;

        // Act
        var actual = this.settings.MagFilter;

        // Assert
        Assert.That(actual, Is.EqualTo(TextureFilterMode.Nearest));
    }

    [Test]
    public void MinFilterShouldReturnLinearMipmapLinearByDefault()
    {
        // Arrange
        this.settings.FilterType = (TextureFilterType)(-1);

        // Act
        var actual = this.settings.MinFilter;

        // Assert
        Assert.That(actual, Is.EqualTo(TextureFilterMode.LinearMipmapLinear));
    }

    [Test]
    public void MinFilterShouldReturnLinearMipmapLinearWhenFilterTypeTrilinear()
    {
        // Arrange
        this.settings.FilterType = TextureFilterType.Trilinear;

        // Act
        var actual = this.settings.MinFilter;

        // Assert
        Assert.That(actual, Is.EqualTo(TextureFilterMode.LinearMipmapLinear));
    }

    [Test]
    public void MinFilterShouldReturnLinearWhenFilterTypeBilinear()
    {
        // Arrange
        this.settings.FilterType = TextureFilterType.Bilinear;

        // Act
        var actual = this.settings.MinFilter;

        // Assert
        Assert.That(actual, Is.EqualTo(TextureFilterMode.Linear));
    }

    [Test]
    public void MinFilterShouldReturnNearestWhenFilterTypeNearestNeighbour()
    {
        // Arrange
        this.settings.FilterType = TextureFilterType.NearestNeighbour;

        // Act
        var actual = this.settings.MinFilter;

        // Assert
        Assert.That(actual, Is.EqualTo(TextureFilterMode.Nearest));
    }

    [SetUp]
    public void Setup()
    {
        this.settings = default;
    }
}
