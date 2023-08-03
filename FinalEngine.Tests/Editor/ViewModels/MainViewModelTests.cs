// <copyright file="MainViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels;

using System;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.ViewModels;
using FinalEngine.Editor.ViewModels.Interactions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class MainViewModelTests
{
    private Mock<IApplicationContext> context;

    private Mock<ILogger<MainViewModel>> logger;

    private MainViewModel viewModel;

    [Test]
    public void ConstructorShouldSetTitleToApplicationContextTitleWhenInvoked()
    {
        // Arrange
        const string expected = "Final Engine";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldSetTitleToStringEmptyWhenApplicationContextTitleIsNull()
    {
        // Arrange
        string expected = string.Empty;
        this.context.Setup(x => x.Title).Returns(() =>
        {
            return null;
        });

        var viewModel = new MainViewModel(this.logger.Object, this.context.Object);

        // Act
        string actual = viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenContextIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MainViewModel(this.logger.Object, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MainViewModel(null, this.context.Object);
        });
    }

    [Test]
    public void ExitCommandExecuteShouldInvokeCloseableCloseWhenInvoked()
    {
        // Arrange
        var closeable = new Mock<ICloseable>();

        // Act
        this.viewModel.ExitCommand.Execute(closeable.Object);

        // Assert
        closeable.Verify(x => x.Close(), Times.Once);
    }

    [Test]
    public void ExitCommandExecuteShouldThrowArgumentNullExceptionWhenCloseableIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.viewModel.ExitCommand.Execute(null);
        });
    }

    [Test]
    public void ExitCommandShouldReturnNewRelayCommandWhenInvoked()
    {
        // Act
        var command = this.viewModel.ExitCommand;

        // Assert
        Assert.That(command, Is.InstanceOf<RelayCommand<ICloseable>>());
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<MainViewModel>>();

        this.context = new Mock<IApplicationContext>();
        this.context.Setup(x => x.Title).Returns("Final Engine");

        this.viewModel = new MainViewModel(this.logger.Object, this.context.Object);
    }
}
