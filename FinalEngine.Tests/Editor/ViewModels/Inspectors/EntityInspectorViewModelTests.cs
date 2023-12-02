// <copyright file="EntityInspectorViewModelTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors;

using System;
using System.Linq;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.Editor.ViewModels.Inspectors;
using FinalEngine.Editor.ViewModels.Messages.Entities;
using FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;
using NUnit.Framework;

[TestFixture]
public sealed class EntityInspectorViewModelTests
{
    private Entity entity;

    private IMessenger messenger;

    private EntityInspectorViewModel viewModel;

    [Test]
    public void ComponentViewModelsShouldNotReturnNull()
    {
        // Act
        var actual = this.viewModel.ComponentViewModels;

        // Assert
        Assert.That(actual, Is.Not.Null);
    }

    [Test]
    public void ComponentViewModelsShouldReturnEntityComponentViewModels()
    {
        // Act
        var actual = this.viewModel.ComponentViewModels.FirstOrDefault();

        // Assert
        Assert.That(actual, Is.AssignableFrom<EntityComponentViewModel>());
    }

    [Test]
    public void ConstructorShouldRegisterEntityModifiedMessageWhenInvoked()
    {
        // Assert
        this.messenger.IsRegistered<EntityModifiedMessage>(this.viewModel);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenEntityIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new EntityInspectorViewModel(this.messenger, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenMessengerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new EntityInspectorViewModel(null, this.entity);
        });
    }

    [Test]
    public void HandleEntityModifiedShouldNotRefreshComponentsWhenEntityIsNotSameEntity()
    {
        // Arrange
        var entity = new Entity();
        entity.AddComponent(new EntityComponentBoolean());

        // Act
        this.messenger.Send(new EntityModifiedMessage(entity));

        // Assert
        Assert.That(this.viewModel.ComponentViewModels.Count, Is.EqualTo(1));
    }

    [Test]
    public void HandleEntityModifiedShouldRefreshComponentsWhenEntityIsNotSameEntity()
    {
        // Arrange
        this.entity.AddComponent(new EntityComponentBoolean());

        // Act
        this.messenger.Send(new EntityModifiedMessage(this.entity));

        // Assert
        Assert.That(this.viewModel.ComponentViewModels.Count, Is.EqualTo(2));
    }

    [SetUp]
    public void Setup()
    {
        this.entity = new Entity();

        this.entity.AddComponent(new TagComponent()
        {
            Tag = "Tag",
        });

        this.messenger = WeakReferenceMessenger.Default;

        this.viewModel = new EntityInspectorViewModel(this.messenger, this.entity);
    }
}
