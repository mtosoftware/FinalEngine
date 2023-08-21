// <copyright file="EntityComponentViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors;

using System;
using System.Linq;
using FinalEngine.ECS.Components.Core;
using FinalEngine.Editor.ViewModels.Editing.DataTypes;
using FinalEngine.Editor.ViewModels.Exceptions.Inspectors;
using FinalEngine.Editor.ViewModels.Inspectors;
using FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;
using NUnit.Framework;

[TestFixture]
public sealed class EntityComponentViewModelTests
{
    [Test]
    public void ConstructorShouldAddBooleanPropertyViewModelWhenComponentHasBoolean()
    {
        // Arrange
        var component = new EntityComponenBoolean();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels.FirstOrDefault(), Is.TypeOf<BoolPropertyViewModel>());
    }

    [Test]
    public void ConstructorShouldAddDoublePropertyViewModelWhenComponentHasDouble()
    {
        // Arrange
        var component = new EntityComponentDouble();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels.FirstOrDefault(), Is.TypeOf<DoublePropertyViewModel>());
    }

    [Test]
    public void ConstructorShouldAddFloatPropertyViewModelWhenComponentHasFloat()
    {
        // Arrange
        var component = new EntityComponentFloat();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels.FirstOrDefault(), Is.TypeOf<FloatPropertyViewModel>());
    }

    [Test]
    public void ConstructorShouldAddIntPropertyViewModelWhenComponentHasInteger()
    {
        // Arrange
        var component = new EntityComponentInteger();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels.FirstOrDefault(), Is.TypeOf<IntPropertyViewModel>());
    }

    [Test]
    public void ConstructorShouldAddPropertyViewModelWhenComponentDoesNotHaveBrowsableAttribute()
    {
        // Arrange
        var component = new TagComponent();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels, Has.Count.EqualTo(1));
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
    public void ConstructorShouldAddStringPropertyViewModelWhenComponentHasString()
    {
        // Arrange
        var component = new EntityComponentString();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels.FirstOrDefault(), Is.TypeOf<StringPropertyViewModel>());
    }

    [Test]
    public void ConstructorShouldAddVector2PropertyViewModelWhenComponentHasVector2()
    {
        // Arrange
        var component = new EntityComponentVector2();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels.FirstOrDefault(), Is.TypeOf<Vector2PropertyViewModel>());
    }

    [Test]
    public void ConstructorShouldAddVector3PropertyViewModelWhenComponentHasVector3()
    {
        // Arrange
        var component = new EntityComponentVector3();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels.FirstOrDefault(), Is.TypeOf<Vector3PropertyViewModel>());
    }

    [Test]
    public void ConstructorShouldAddVector4PropertyViewModelWhenComponentHasVector4()
    {
        // Arrange
        var component = new EntityComponentVector4();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels.FirstOrDefault(), Is.TypeOf<Vector4PropertyViewModel>());
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
    public void ConstructorShouldNotAddPropertyViewModelWhenPropertyHasNonPublicGetter()
    {
        // Arrange
        var component = new EntityComponentPrivateGetter();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels.Count, Is.Zero);
    }

    [Test]
    public void ConstructorShouldNotAddPropertyViewModelWhenPropertyHasNonPublicSetter()
    {
        // Arrange
        var component = new EntityComponentPrivateSetter();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.PropertyViewModels.Count, Is.Zero);
    }

    [Test]
    public void ConstructorShouldSetIsVisibleToTrueWhenInvoked()
    {
        // Arrange
        var component = new TagComponent();

        // Act
        var viewModel = new EntityComponentViewModel(component);

        // Assert
        Assert.That(viewModel.IsVisible, Is.True);
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
        var component = new TagComponent()
        {
            Tag = "Tag Test",
        };

        string expected = component.GetType().Name;
        var viewModel = new EntityComponentViewModel(component);

        // Act
        string actual = viewModel.Name;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToggleCommandShouldSetIsVisibleToFalseWhenIsVisibleIsTrue()
    {
        // Arrange
        var component = new TagComponent();
        var viewModel = new EntityComponentViewModel(component);

        // Act
        viewModel.ToggleCommand.Execute(null);

        // Assert
        Assert.That(viewModel.IsVisible, Is.False);
    }

    [Test]
    public void ToggleCommandShouldSetIsVisibleToTrueWhenIsVisibleIsFalse()
    {
        // Arrange
        var component = new TagComponent();
        var viewModel = new EntityComponentViewModel(component);

        viewModel.ToggleCommand.Execute(null);

        // Act
        viewModel.ToggleCommand.Execute(null);

        // Assert
        Assert.That(viewModel.IsVisible, Is.True);
    }
}
