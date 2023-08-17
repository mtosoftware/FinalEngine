// <copyright file="PropertiesToolViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Tools.Inspectors;

using System;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.Editor.ViewModels.Inspectors;
using FinalEngine.Editor.ViewModels.Messages.Entities;
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
            new PropertiesToolViewModel(null, WeakReferenceMessenger.Default);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenMessengerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new PropertiesToolViewModel(this.logger.Object, null);
        });
    }

    [Test]
    public void MessengerPubilshShouldSetCurrentViewToEntityInspectorViewModelWhenInvoked()
    {
        // Arrange
        var entity = new Entity();
        entity.AddComponent<TagComponent>();

        // Act
        WeakReferenceMessenger.Default.Send(new EntitySelectedMessage(entity));

        // Assert
        Assert.That(this.viewModel.CurrentViewModel, Is.TypeOf<EntityInspectorViewModel>());
    }

    [Test]
    public void MessengerPubilshShouldSetTitleToEntityInspectorWhenInvoked()
    {
        // Arrange
        var entity = new Entity();

        entity.AddComponent(new TagComponent()
        {
            Tag = "Tag",
        });

        // Act
        WeakReferenceMessenger.Default.Send(new EntitySelectedMessage(entity));

        // Assert
        Assert.That(this.viewModel.Title, Is.EqualTo("Entity Inspector"));
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<PropertiesToolViewModel>>();
        this.viewModel = new PropertiesToolViewModel(this.logger.Object, WeakReferenceMessenger.Default);
    }
}
