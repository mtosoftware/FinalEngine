// <copyright file="EntitySystemsToolViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Tools.Scenes;

using System;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class EntitySystemsToolViewModelTests
{
    private Mock<ILogger<EntitySystemsToolViewModel>> logger;

    private EntitySystemsToolViewModel viewModel;

    [Test]
    public void ConstructorShouldSetContentIDToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "EntitySystems";

        // Act
        string actual = this.viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldSetTitleToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Entity Systems";

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
            new EntitySystemsToolViewModel(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<EntitySystemsToolViewModel>>();
        this.viewModel = new EntitySystemsToolViewModel(this.logger.Object);
    }
}
