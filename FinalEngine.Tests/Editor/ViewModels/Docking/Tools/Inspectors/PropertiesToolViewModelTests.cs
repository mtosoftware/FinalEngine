// <copyright file="PropertiesToolViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Tools.Inspectors;

using System;
using FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class PropertiesToolViewModelTests
{
    private Mock<ILogger<PropertiesToolViewModel>> logger;

    private PropertiesToolViewModel viewModel;

    [Test]
    public void ConstructorShouldSetContentIDToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Properties";

        // Act
        string actual = this.viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldSetTitleToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Properties";

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
            new PropertiesToolViewModel(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<PropertiesToolViewModel>>();
        this.viewModel = new PropertiesToolViewModel(this.logger.Object);
    }
}
