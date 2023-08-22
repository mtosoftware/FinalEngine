// <copyright file="IntPropertyViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Editing.DataTypes;

using System;
using FinalEngine.Editor.ViewModels.Editing.DataTypes;
using NUnit.Framework;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;

[TestFixture]
public sealed class IntPropertyViewModelTests
{
    private IntPropertyViewModel viewModel;

    public int ComponentProperty { get; set; }

    [SetUp]
    public void Setup()
    {
        this.ComponentProperty = 1;
        this.viewModel = new IntPropertyViewModel(this, this.GetType().GetProperty(nameof(this.ComponentProperty)));
    }

    [Test]
    public void ValueShouldContainRangeAttribute()
    {
        // Arrange
        var type = typeof(IntPropertyViewModel);
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
        const int expected = 1;

        // Act
        int actual = this.viewModel.Value;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ValueShouldReturnTwoWhenSetToTwo()
    {
        // Arrange
        const int expected = 2;
        this.viewModel.Value = expected;

        // Act
        int actual = this.viewModel.Value;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
