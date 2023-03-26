// <copyright file="ToolViewModelBaseTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking.Tools;

using FinalEngine.Editor.ViewModels.Docking.Tools;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class ToolViewModelBaseTests
{
    private Mock<ToolViewModelBase> viewModel;

    [Test]
    public void ConstructorShouldNotThrowExceptionWhenInvoked()
    {
        // Act and assert
        Assert.DoesNotThrow(() =>
        {
            new Mock<ToolViewModelBase>();
        });
    }

    [Test]
    public void IsVisibleShouldReturnFalseWhenSetToFalse()
    {
        // Act
        this.viewModel.Object.IsVisible = false;

        // Assert
        Assert.That(this.viewModel.Object.IsVisible, Is.False);
    }

    [Test]
    public void IsVisibleShouldReturnTrueWhenNotSet()
    {
        // Act
        bool actual = this.viewModel.Object.IsVisible;

        // Assert
        Assert.That(actual, Is.True);
    }

    [SetUp]
    public void Setup()
    {
        this.viewModel = new Mock<ToolViewModelBase>();
    }
}
