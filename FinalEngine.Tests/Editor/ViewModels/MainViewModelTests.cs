// <copyright file="MainViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels;

using System;
using System.Reflection;
using FinalEngine.Editor.ViewModels;
using FinalEngine.Editor.ViewModels.Interaction;
using FinalEngine.Utilities.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class MainViewModelTests
{
    private Mock<ILogger<MainViewModel>> logger;

    private MainViewModel viewModel;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MainViewModel(null);
        });
    }

    [Test]
    public void ExitCommandShouldInvokeCloseWhenCloseableIsPassed()
    {
        // Arrange
        var closeable = new Mock<ICloseable>();

        // Act
        this.viewModel.ExitCommand.Execute(closeable.Object);

        // Assert
        closeable.Verify(x => x.Close(), Times.Once);
    }

    [Test]
    public void ExitCommandShouldThrowArgumentNullExceptionWhenCloseableIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.viewModel.ExitCommand.Execute(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<MainViewModel>>();
        this.viewModel = new MainViewModel(this.logger.Object);
    }

    [Test]
    public void TitleShouldReturnCorrectTitleOnStartup()
    {
        // Arrange
        string expected = $"Final Engine - {Assembly.GetExecutingAssembly().GetVersionString()}";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
