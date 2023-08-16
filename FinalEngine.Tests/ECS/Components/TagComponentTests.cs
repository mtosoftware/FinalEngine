// <copyright file="TagComponentTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.ECS.Components;

using FinalEngine.ECS.Components.Core;
using NUnit.Framework;

[TestFixture]
public sealed class TagComponentTests
{
    private TagComponent component;

    [SetUp]
    public void Setup()
    {
        this.component = new TagComponent();
    }

    [Test]
    public void TagShouldReturnHelloWorldWhenSetToHelloWorld()
    {
        // Arrange
        string expected = "Hello, World!";
        this.component.Tag = expected;

        // Act
        string actual = this.component.Tag;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TagShouldReturnNullWhenNotSet()
    {
        // Act
        string actual = this.component.Tag;

        // Assert
        Assert.That(actual, Is.Null);
    }
}
