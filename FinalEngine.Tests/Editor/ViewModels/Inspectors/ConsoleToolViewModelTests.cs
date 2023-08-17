// <copyright file="ConsoleToolViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors;

using System;
using FinalEngine.Editor.ViewModels.Inspectors;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class ConsoleToolViewModelTests
{
    private Mock<ILogger<ConsoleToolViewModel>> logger;

    private ConsoleToolViewModel viewModel;

    [Test]
    public void ConstructorShouldSetContentIDToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Console";

        // Act
        string actual = this.viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldSetTitleToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Console";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ConsoleToolViewModel(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<ConsoleToolViewModel>>();
        this.viewModel = new ConsoleToolViewModel(this.logger.Object);
    }
}
