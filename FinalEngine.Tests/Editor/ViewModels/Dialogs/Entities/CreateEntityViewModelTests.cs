// <copyright file="CreateEntityViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Dialogs.Entities;

using System;
using FinalEngine.Editor.Common.Models.Scenes;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Dialogs.Entities;
using FinalEngine.Editor.ViewModels.Interactions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class CreateEntityViewModelTests
{
    private Mock<ICloseable> closeable;

    private Mock<ILogger<CreateEntityViewModel>> logger;

    private Mock<IScene> scene;

    private Mock<ISceneManager> sceneManager;

    private CreateEntityViewModel viewModel;

    [Test]
    public void ConstructorShouldSetEntityGuidToNewGuidWhenInvoked()
    {
        // Act
        var actual = this.viewModel.EntityGuid;

        // Assert
        Assert.That(actual, Is.Not.Null);
    }

    [Test]
    public void ConstructorShouldSetEntityNameToEntityWhenInvoked()
    {
        // Act
        string actual = this.viewModel.EntityName;

        // Assert
        Assert.That(actual, Is.EqualTo("Entity"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new CreateEntityViewModel(null, this.sceneManager.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenSceneManagerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new CreateEntityViewModel(this.logger.Object, null);
        });
    }

    [Test]
    public void CreateCommandShouldExecuteWhenStringIsNotEmptyOrWhitespace()
    {
        // Arrange
        this.viewModel.EntityName = "Entity";

        // Act
        bool actual = this.viewModel.CreateCommand.CanExecute(this.closeable.Object);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void CreateCommandShouldInvokeCloseableCloseWhenInvoked()
    {
        // Act
        this.viewModel.CreateCommand.Execute(this.closeable.Object);

        // Assert
        this.closeable.Verify(x => x.Close(), Times.Once);
    }

    [Test]
    public void CreateCommandShouldInvokeSceneAddEntityWhenInvoked()
    {
        // Act
        this.viewModel.CreateCommand.Execute(this.closeable.Object);

        // Assert
        this.scene.Verify(x => x.AddEntity(this.viewModel.EntityName, this.viewModel.EntityGuid), Times.Once);
    }

    [Test]
    public void CreateCommandShouldInvokeSceneManagerActiveWhenInvoked()
    {
        // Act
        this.viewModel.CreateCommand.Execute(this.closeable.Object);

        // Assert
        this.sceneManager.VerifyGet(x => x.ActiveScene, Times.Once);
    }

    [Test]
    public void CreateCommandShouldNotExecuteWhenEntityNameIsEmpty()
    {
        // Arrange
        this.viewModel.EntityName = string.Empty;

        // Act
        bool actual = this.viewModel.CreateCommand.CanExecute(this.closeable.Object);

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void CreateCommandShouldNotExecuteWhenEntityNameIsNull()
    {
        // Arrange
        this.viewModel.EntityName = null;

        // Act
        bool actual = this.viewModel.CreateCommand.CanExecute(this.closeable.Object);

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void CreateCommandShouldNotExecuteWhenEntityNameIsWhitespace()
    {
        // Arrange
        this.viewModel.EntityName = "\r\n\t ";

        // Act
        bool actual = this.viewModel.CreateCommand.CanExecute(this.closeable.Object);

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void CreateCommandShouldThrowArgumentNullExceptionWhenCloseableIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.viewModel.CreateCommand.Execute(null);
        });
    }

    [Test]
    public void EntityNameShouldReturnHelloWorldWhenSetToHelloWorld()
    {
        // Arrange
        string expected = "Hello, World!";
        this.viewModel.EntityName = expected;

        // Act
        string actual = this.viewModel.EntityName;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void EntityNameShouldReturnStringEmptyWhenSetToNull()
    {
        // Act
        this.viewModel.EntityName = null;

        // Assert
        Assert.That(this.viewModel.EntityName, Is.Empty);
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<CreateEntityViewModel>>();
        this.sceneManager = new Mock<ISceneManager>();
        this.scene = new Mock<IScene>();
        this.closeable = new Mock<ICloseable>();

        this.sceneManager.SetupGet(x => x.ActiveScene).Returns(this.scene.Object);

        this.viewModel = new CreateEntityViewModel(this.logger.Object, this.sceneManager.Object);
    }

    [Test]
    public void TitleShouldReturnCreateNewEntityWhenInvoked()
    {
        // Arrange
        string expected = "Create New Entity";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
