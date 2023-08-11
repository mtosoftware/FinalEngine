// <copyright file="ToolViewModelBaseTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Tools;

using FinalEngine.Tests.Editor.ViewModels.Docking.Tools.Mocks;
using NUnit.Framework;

[TestFixture]
public sealed class ToolViewModelBaseTests
{
    private MockToolViewModel viewModel;

    [Test]
    public void IsVisibleShouldReturnFalseWhenSetToFalse()
    {
        // Arrange
        this.viewModel.IsVisible = false;

        // Act
        bool actual = this.viewModel.IsVisible;

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void IsVisibleShouldReturnTrueWhenInvoked()
    {
        // Assert
        Assert.That(this.viewModel.IsVisible, Is.True);
    }

    [SetUp]
    public void Setup()
    {
        this.viewModel = new MockToolViewModel();
    }
}
