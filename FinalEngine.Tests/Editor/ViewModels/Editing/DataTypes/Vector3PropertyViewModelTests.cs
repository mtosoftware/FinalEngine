// <copyright file="Vector3PropertyViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Editing.DataTypes;

using System;
using System.Numerics;
using FinalEngine.Editor.ViewModels.Editing.DataTypes;
using NUnit.Framework;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;

[TestFixture]
public sealed class Vector3PropertyViewModelTests
{
    private Vector3PropertyViewModel viewModel;

    public Vector3 ComponentProperty { get; set; }

    [SetUp]
    public void Setup()
    {
        this.ComponentProperty = new Vector3(1.0f, 2.0f, 3.0f);
        this.viewModel = new Vector3PropertyViewModel(this, this.GetType().GetProperty(nameof(this.ComponentProperty)));
    }

    [Test]
    public void XShouldContainRangeAttribute()
    {
        // Arrange
        var type = typeof(Vector3PropertyViewModel);
        var property = type.GetProperty("X");

        // Act
        bool actual = Attribute.IsDefined(property, typeof(RangeAttribute));

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void XShouldReturnOneWhenInvoked()
    {
        // Arrange
        const float expected = 1.0f;

        // Act
        float actual = this.viewModel.X;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void XShouldReturnTwoWhenSetToTwo()
    {
        // Arrange
        const float expected = 2.0f;
        this.viewModel.X = expected;

        // Act
        float actual = this.viewModel.X;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void YShouldContainRangeAttribute()
    {
        // Arrange
        var type = typeof(Vector3PropertyViewModel);
        var property = type.GetProperty("Y");

        // Act
        bool actual = Attribute.IsDefined(property, typeof(RangeAttribute));

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void YShouldReturnOneWhenSetToOne()
    {
        // Arrange
        const float expected = 1.0f;
        this.viewModel.Y = expected;

        // Act
        float actual = this.viewModel.Y;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void YShouldReturnTwoWhenInvoked()
    {
        // Arrange
        const float expected = 2.0f;

        // Act
        float actual = this.viewModel.Y;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ZShouldContainRangeAttribute()
    {
        // Arrange
        var type = typeof(Vector3PropertyViewModel);
        var property = type.GetProperty("Z");

        // Act
        bool actual = Attribute.IsDefined(property, typeof(RangeAttribute));

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void ZShouldReturnThreeWhenInvoked()
    {
        // Arrange
        const float expected = 3.0f;

        // Act
        float actual = this.viewModel.Z;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ZShouldReturnTwoWhenSetToTwo()
    {
        // Arrange
        const float expected = 2.0f;
        this.viewModel.Z = expected;

        // Act
        float actual = this.viewModel.Z;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
