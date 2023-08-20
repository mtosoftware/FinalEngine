// <copyright file="EntityComponentViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors;

using System;
using FinalEngine.ECS.Components.Core;
using FinalEngine.Editor.ViewModels.Exceptions.Inspectors;
using FinalEngine.Editor.ViewModels.Inspectors;
using FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;
using NUnit.Framework;

[TestFixture]
public sealed class EntityComponentViewModelTests
{
    private TagComponent component;

    private EntityComponentViewModel viewModel;

    [Test]
    public void ConstructorShouldAddPropertyViewModelWhenComponentDoesNotHaveBrowsableAttribute()
    {
        // Assert
        Assert.That(this.viewModel.PropertyViewModels, Has.Count.EqualTo(1));
    }

    [Test]
    public void ConstructorShouldAddPropertyViewModelWhenComponentIsBrowseable()
    {
        // Arrange
        var component = new EntityComponentBrowsable();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels, Has.Count.EqualTo(1));
    }

    [Test]
    public void ConstructorShouldNotAddPropertyViewModelWhenComponentNotBrowseable()
    {
        // Arrange
        var component = new EntityComponentNotBrowsable();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels, Is.Empty);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenComponentIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new EntityComponentViewModel(null);
        });
    }

    [Test]
    public void ConstructorShouldThrowPropertyTypeNotFoundExceptionWhenPropertyTypeIsNotSupported()
    {
        // Act and assert
        Assert.Throws<PropertyTypeNotFoundException>(() =>
        {
            new EntityComponentViewModel(new EntityComponentUnsupportedPropertyType());
        });
    }

    [Test]
    public void NameShouldReturnTagComponentWhenInvoked()
    {
        // Arrange
        string expected = this.component.GetType().Name;

        // Act
        string actual = this.viewModel.Name;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [OneTimeSetUp]
    public void Setup()
    {
        this.component = new TagComponent()
        {
            Tag = "Tag",
        };

        this.viewModel = new EntityComponentViewModel(this.component);
    }
}
