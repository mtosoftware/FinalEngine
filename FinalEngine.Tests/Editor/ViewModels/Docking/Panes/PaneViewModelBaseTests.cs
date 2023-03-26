// <copyright file="PaneViewModelBaseTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Panes;

using FinalEngine.Editor.ViewModels.Docking.Panes;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class PaneViewModelBaseTests
{
    private Mock<PaneViewModelBase> viewModel;

    [Test]
    public void ConstructorShouldNotThrowExceptionWhenInvoked()
    {
        // Act and assert
        Assert.DoesNotThrow(() =>
        {
            this.viewModel = new Mock<PaneViewModelBase>();
        });
    }

    [Test]
    public void ContentIDShouldReturnEmptyWhenNotSet()
    {
        // Act
        string actual = this.viewModel.Object.ContentID;

        // Assert
        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void ContentIDShouldReturnHelloWorldWhenSet()
    {
        // Arrange
        string expected = "Hello, World!";

        // Act
        this.viewModel.Object.ContentID = expected;

        // Assert
        Assert.That(this.viewModel.Object.ContentID, Is.EqualTo(expected));
    }

    [Test]
    public void IsActiveShouldReturnFalseWhenNotSet()
    {
        // Act
        bool actual = this.viewModel.Object.IsActive;

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void IsActiveShouldReturnTrueWhenSetToTrue()
    {
        // Act
        this.viewModel.Object.IsActive = true;

        // Assert
        Assert.That(this.viewModel.Object.IsActive, Is.True);
    }

    [Test]
    public void IsSelectedeShouldReturnTrueWhenSetToTrue()
    {
        // Act
        this.viewModel.Object.IsSelected = true;

        // Assert
        Assert.That(this.viewModel.Object.IsSelected, Is.True);
    }

    [Test]
    public void IsSelectedShouldReturnFalseWhenNotSet()
    {
        // Act
        bool actual = this.viewModel.Object.IsSelected;

        // Assert
        Assert.That(actual, Is.False);
    }

    [SetUp]
    public void Setup()
    {
        this.viewModel = new Mock<PaneViewModelBase>();
    }

    [Test]
    public void TitleShouldReturnEmptyWhenNotSet()
    {
        // Act
        string actual = this.viewModel.Object.Title;

        // Assert
        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void TitleShouldReturnHelloWorldWhenSet()
    {
        // Arrange
        string expected = "Hello, World!";

        // Act
        this.viewModel.Object.Title = expected;

        // Assert
        Assert.That(this.viewModel.Object.Title, Is.EqualTo(expected));
    }
}
