// <copyright file="DoublePropertyViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Editing.DataTypes;

using System;
using FinalEngine.Editor.ViewModels.Editing.DataTypes;
using NUnit.Framework;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;

[TestFixture]
public sealed class DoublePropertyViewModelTests
{
    private DoublePropertyViewModel viewModel;

    public double ComponentProperty { get; set; }

    [SetUp]
    public void Setup()
    {
        this.ComponentProperty = 1.0d;
        this.viewModel = new DoublePropertyViewModel(this, this.GetType().GetProperty(nameof(this.ComponentProperty)));
    }

    [Test]
    public void ValueShouldContainRangeAttribute()
    {
        // Arrange
        var type = typeof(DoublePropertyViewModel);
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
        const double expected = 1.0d;

        // Act
        double actual = this.viewModel.Value;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ValueShouldReturnTwoWhenSetToTwo()
    {
        // Arrange
        const double expected = 2.0d;
        this.viewModel.Value = expected;

        // Act
        double actual = this.viewModel.Value;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
