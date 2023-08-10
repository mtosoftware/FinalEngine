// <copyright file="EntitySystemsToolViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Tools.Scenes;

using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;
using NUnit.Framework;

[TestFixture]
public sealed class EntitySystemsToolViewModelTests
{
    [Test]
    public void ConstructorShouldSetContentIDToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "EntitySystems";

        var viewModel = new EntitySystemsToolViewModel();

        // Act
        string actual = viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldSetTitleToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Entity Systems";

        var viewModel = new EntitySystemsToolViewModel();

        // Act
        string actual = viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
