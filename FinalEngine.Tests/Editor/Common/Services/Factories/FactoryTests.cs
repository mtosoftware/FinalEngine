// <copyright file="FactoryTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Services.Factories;

using System;
using FinalEngine.Editor.Common.Services.Factories;
using NUnit.Framework;

[TestFixture]
public sealed class FactoryTests
{
    private Factory<FactoryTests> factory;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Factory<FactoryTests>(null);
        });
    }

    [Test]
    public void CreateShouldReturnDifferentReferenceWhenInvoked()
    {
        // Arrange
        var expected = this.factory.Create();

        // Act
        var actual = this.factory.Create();

        // Assert
        Assert.That(actual, Is.Not.SameAs(expected));
    }

    [Test]
    public void CreateShouldReturnFactoryTestsWhenInvoked()
    {
        // Act
        var actual = this.factory.Create();

        // Assert
        Assert.That(actual, Is.TypeOf<FactoryTests>());
    }

    [SetUp]
    public void Setup()
    {
        this.factory = new Factory<FactoryTests>(() =>
        {
            return new FactoryTests();
        });
    }
}
