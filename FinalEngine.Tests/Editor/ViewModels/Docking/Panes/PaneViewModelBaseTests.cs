// <copyright file="PaneViewModelBaseTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Panes;

using FinalEngine.Tests.Editor.ViewModels.Docking.Panes.Mocks;
using NUnit.Framework;

[TestFixture]
public sealed class PaneViewModelBaseTests
{
    private MockPaneViewModel viewModel;

    [Test]
    public void ContentIDShouldReturnHelloWorldWhenSetToHelloWorld()
    {
        // Arrange
        const string expected = "Hello, World!";

        this.viewModel.ContentID = expected;

        // Act
        string actual = this.viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ContentIDShouldReturnStringEmptyWhenInvoked()
    {
        // Act
        string actual = this.viewModel.ContentID;

        // Assert
        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void IsActiveShouldReturnFalseWhenSetToFalse()
    {
        // Arrange
        this.viewModel.IsActive = false;

        // Act
        bool actual = this.viewModel.IsActive;

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void IsActiveShouldReturnTrueWhenInvoked()
    {
        // Act
        bool actual = this.viewModel.IsActive;

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void IsSelectedShouldReturnFalseWhenInvoked()
    {
        // Act
        bool actual = this.viewModel.IsSelected;

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void IsSelectedShouldReturnTrueWhenSetToTrue()
    {
        // Arrange
        this.viewModel.IsSelected = true;

        // Act
        bool actual = this.viewModel.IsSelected;

        // Assert
        Assert.That(actual, Is.True);
    }

    [SetUp]
    public void Setup()
    {
        this.viewModel = new MockPaneViewModel();
    }

    [Test]
    public void TitleShouldReturnHelloWorldWhenSetToHelloWorld()
    {
        // Arrange
        const string expected = "Hello, World!";

        this.viewModel.Title = expected;

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TitleShouldReturnStringEmptyWhenInvoked()
    {
        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.Empty);
    }
}
