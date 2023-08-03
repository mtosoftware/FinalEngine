// <copyright file="ApplicationContextTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Services.Application;

using System.Reflection;
using FinalEngine.Editor.Common.Services.Application;
using NUnit.Framework;

[TestFixture]
public sealed class ApplicationContextTests
{
    private ApplicationContext context;

    [Test]
    public void ConstructorShouldNotThrowExceptionWhenInvoked()
    {
        // Act and assert
        Assert.DoesNotThrow(() =>
        {
            new ApplicationContext();
        });
    }

    [SetUp]
    public void Setup()
    {
        this.context = new ApplicationContext();
    }

    [Test]
    public void TitleShouldReturnFinalEngineAndVersionWhenInvoked()
    {
        // Arrange
        string expected = $"Final Engine - {Assembly.GetExecutingAssembly().GetName().Version}";

        // Act
        string actual = this.context.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void VersionShouldReturnAssemblyVersionWhenInvoked()
    {
        // Arrange
        var expected = Assembly.GetExecutingAssembly().GetName().Version;

        // Act
        var actual = this.context.Version;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
