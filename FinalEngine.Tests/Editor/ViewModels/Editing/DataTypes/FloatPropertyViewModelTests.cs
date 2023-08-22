// <copyright file="FloatPropertyViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Editing.DataTypes;

using System;
using FinalEngine.Editor.ViewModels.Editing.DataTypes;
using NUnit.Framework;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;

[TestFixture]
public sealed class FloatPropertyViewModelTests
{
    private FloatPropertyViewModel viewModel;

    public float ComponentProperty { get; set; }

    [SetUp]
    public void Setup()
    {
        this.ComponentProperty = 1.0f;
        this.viewModel = new FloatPropertyViewModel(this, this.GetType().GetProperty(nameof(this.ComponentProperty)));
    }

    [Test]
    public void ValueShouldContainRangeAttribute()
    {
        // Arrange
        var type = typeof(FloatPropertyViewModel);
        var property = type.GetProperty("Value");

        // Act
        bool actual = Attribute.IsDefined(property, typeof(RangeAttribute));

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void ValueShouldReturnOneWhenInvoked()
    {
        // Arrange
        const float expected = 1.0f;

        // Act
        float actual = this.viewModel.Value;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ValueShouldReturnTwoWhenSetToTwo()
    {
        // Arrange
        const float expected = 2.0f;
        this.viewModel.Value = expected;

        // Act
        float actual = this.viewModel.Value;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
