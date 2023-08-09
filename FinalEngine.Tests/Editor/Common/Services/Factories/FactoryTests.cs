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

    private Func<FactoryTests> testFactory;

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
    public void CreateShouldReturnInstanceOfFactoryTestsWhenInvoked()
    {
        // Act
        var actual = this.factory.Create();

        // Assert
        Assert.That(actual, Is.InstanceOf<FactoryTests>());
    }

    [Test]
    public void CreateShouldReturnNewInstanceOfFactoryTestsWhenInvoked()
    {
        // Act
        var actual = this.factory.Create();

        // Assert
        Assert.That(actual, Is.Not.SameAs(this));
    }

    [SetUp]
    public void Setup()
    {
        this.testFactory = new Func<FactoryTests>(() => new FactoryTests());
        this.factory = new Factory<FactoryTests>(this.testFactory);
    }
}
