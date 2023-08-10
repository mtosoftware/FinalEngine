// <copyright file="SceneViewPaneViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Panes.Scenes;

using FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;
using NUnit.Framework;

[TestFixture]
public sealed class SceneViewPaneViewModelTests
{
    [Test]
    public void ConstructorShouldSetContentIDToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "SceneView";

        var viewModel = new SceneViewPaneViewModel();

        // Act
        string actual = viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldSetTitleToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Scene View";

        var viewModel = new SceneViewPaneViewModel();

        // Act
        string actual = viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
