// <copyright file="EntityInspectorViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors;

using System;
using System.Linq;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.Editor.ViewModels.Inspectors;
using NUnit.Framework;

[TestFixture]
public sealed class EntityInspectorViewModelTests
{
    private Entity entity;

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
    public void ConstructorShouldThrowArgumentNullExceptionWhenEntityIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new EntityInspectorViewModel(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.entity = new Entity();

        this.entity.AddComponent(new TagComponent()
        {
            Tag = "Tag",
        });

        this.viewModel = new EntityInspectorViewModel(this.entity);
    }
}
