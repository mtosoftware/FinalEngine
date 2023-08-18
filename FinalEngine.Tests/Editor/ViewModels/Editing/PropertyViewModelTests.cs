// <copyright file="PropertyViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Editing;

using System;
using FinalEngine.Editor.ViewModels.Editing;
using NUnit.Framework;

[TestFixture]
public sealed class PropertyViewModelTests
{
    private PropertyViewModel<string> viewModel;

    public string ComponentProperty { get; set; }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenComponentIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new PropertyViewModel<string>(null, this.GetType().GetProperty(nameof(this.ComponentProperty)));
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenPropertyIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new PropertyViewModel<string>(this, null);
        });
    }

    [Test]
    public void NameShouldReturnPropertyNameWhenInvoked()
    {
        // Arrange
        string expected = nameof(this.ComponentProperty);

        // Act
        string actual = this.viewModel.Name;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [SetUp]
    public void Setup()
    {
        this.ComponentProperty = "Hello, World!";
        this.viewModel = new PropertyViewModel<string>(this, this.GetType().GetProperty(nameof(this.ComponentProperty)));
    }

    [Test]
    public void ValueShouldReturnHelloWorldWhenInvoked()
    {
        // Arrange
        const string expected = "Hello, World!";

        // Act
        string actual = this.viewModel.Value;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ValueShouldReturnTestWhenSetToTest()
    {
        // Arrange
        const string expceted = "Test";
        this.viewModel.Value = expceted;

        // Act
        string actual = this.viewModel.Value;

        // Assert
        Assert.That(actual, Is.EqualTo(expceted));
    }
}
