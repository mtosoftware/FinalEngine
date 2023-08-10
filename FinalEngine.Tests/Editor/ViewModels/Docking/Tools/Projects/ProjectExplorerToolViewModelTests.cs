// <copyright file="ProjectExplorerToolViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Tools.Projects;

using FinalEngine.Editor.ViewModels.Docking.Tools.Projects;
using NUnit.Framework;

[TestFixture]
public sealed class ProjectExplorerToolViewModelTests
{
    [Test]
    public void ConstructorShouldSetContentIDToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "ProjectExplorer";

        var viewModel = new ProjectExplorerToolViewModel();

        // Act
        string actual = viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldSetTitleToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Project Explorer";

        var viewModel = new ProjectExplorerToolViewModel();

        // Act
        string actual = viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
