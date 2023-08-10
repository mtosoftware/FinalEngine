// <copyright file="ConsoleToolViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Tools.Inspectors;

using FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;
using NUnit.Framework;

[TestFixture]
public sealed class ConsoleToolViewModelTests
{
    [Test]
    public void ConstructorShouldSetContentIDToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Console";

        var viewModel = new ConsoleToolViewModel();

        // Act
        string actual = viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldSetTitleToSceneViewWhenInvoked()
    {
        // Arrange
        const string expected = "Console";

        var viewModel = new ConsoleToolViewModel();

        // Act
        string actual = viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
