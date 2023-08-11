// <copyright file="ApplicationContextTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Services.Application;

using System;
using System.IO.Abstractions.TestingHelpers;
using System.Reflection;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Common.Services.Environment;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class ApplicationContextTests
{
    private ApplicationContext context;

    private Mock<IEnvironmentContext> environment;

    private MockFileSystem fileSystem;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenEnvironmentIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ApplicationContext(this.fileSystem, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFileSystemIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ApplicationContext(null, this.environment.Object);
        });
    }

    [Test]
    public void DataDirectoryShouldCreateDirectoryWhenFinalEngineDirectoryDoesNotExist()
    {
        // Act
        _ = this.context.DataDirectory;

        // Assert
        Assert.That(this.fileSystem.Directory.Exists("AppData\\FinalEngine"), Is.True);
    }

    [Test]
    public void DataDirectoryShouldInvokeEnvironmentGetFolderPathWhenInvoked()
    {
        // Act
        _ = this.context.DataDirectory;

        // Assert
        this.environment.Verify(x => x.GetFolderPath(Environment.SpecialFolder.ApplicationData), Times.Once);
    }

    [Test]
    public void DataDirectoryShouldReturnDirectoryWhenDirectoryDoesNotExist()
    {
        // Arrange
        const string expected = "AppData\\Final Engine";

        // Act
        string actual = this.context.DataDirectory;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DataDirectoryShouldReturnDirectoryWhenDirectoryExists()
    {
        // Arrange
        const string expected = "AppData\\Final Engine";

        this.fileSystem.Directory.CreateDirectory("AppData");
        this.fileSystem.Directory.CreateDirectory(expected);

        // Act
        string actual = this.context.DataDirectory;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [SetUp]
    public void Setup()
    {
        this.fileSystem = new MockFileSystem();
        this.environment = new Mock<IEnvironmentContext>();

        this.environment.Setup(x => x.GetFolderPath(Environment.SpecialFolder.ApplicationData)).Returns("AppData");

        this.context = new ApplicationContext(this.fileSystem, this.environment.Object);
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
