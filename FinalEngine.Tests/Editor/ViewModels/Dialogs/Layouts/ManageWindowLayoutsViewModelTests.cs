// <copyright file="ManageWindowLayoutsViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Dialogs.Layouts;

using System;
using System.Collections.Generic;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Services.Actions;
using FinalEngine.Editor.ViewModels.Services.Factories.Layout;
using FinalEngine.Editor.ViewModels.Services.Layout;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class ManageWindowLayoutsViewModelTests
{
    private Mock<ILayoutManager> layoutManager;

    private Mock<ILayoutManagerFactory> layoutManagerFactory;

    private IList<string> layoutNames;

    private Mock<ILogger<ManageWindowLayoutsViewModel>> logger;

    private Mock<IUserActionRequester> userActionRequester;

    private ManageWindowLayoutsViewModel viewModel;

    [Test]
    public void ApplyCommandCanExecuteShouldReturnFalseWhenSelectedItemIsNull()
    {
        // Arrange
        this.viewModel.SelectedItem = null;

        // Act
        bool actual = this.viewModel.ApplyCommand.CanExecute(this.viewModel.SelectedItem);

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void ApplyCommandCanExecuteShouldReturnTrueWhenSelectedItemNotNull()
    {
        // Arrange
        this.viewModel.SelectedItem = "Test";

        // Act
        bool actual = this.viewModel.ApplyCommand.CanExecute(this.viewModel.SelectedItem);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void ApplyCommandExecuteShouldInvokeLayoutManagerLoadLayoutWhenInvoked()
    {
        // Arrange
        this.viewModel.SelectedItem = "Layout 1";

        // Act
        this.viewModel.ApplyCommand.Execute(this.viewModel.SelectedItem);

        // Assert
        this.layoutManager.Verify(x => x.LoadLayout("Layout 1"));
    }

    [Test]
    public void ConstructorShouldSetLayoutNamesToGetLayoutNamesWhenInvoked()
    {
        // Arrange
        var expected = this.layoutNames;

        // Act
        var actual = this.viewModel.LayoutNames;

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLayoutManagerFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ManageWindowLayoutsViewModel(this.logger.Object, this.userActionRequester.Object, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ManageWindowLayoutsViewModel(null, this.userActionRequester.Object, this.layoutManagerFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenUserActionRequesterIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ManageWindowLayoutsViewModel(this.logger.Object, null, this.layoutManagerFactory.Object);
        });
    }

    [Test]
    public void DeleteCommandCanExecuteShouldReturnFalseWhenSelectedItemIsNull()
    {
        // Arrange
        this.viewModel.SelectedItem = null;

        // Act
        bool actual = this.viewModel.DeleteCommand.CanExecute(this.viewModel.SelectedItem);

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void DeleteCommandCanExecuteShouldReturnTrueWhenSelectedItemNotNull()
    {
        // Arrange
        this.viewModel.SelectedItem = "Test";

        // Act
        bool actual = this.viewModel.DeleteCommand.CanExecute(this.viewModel.SelectedItem);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void DeleteCommandExecuteShouldInvokeLayoutManagerDeleteLayoutWhenUserActionRequesterYesNoReturnsTrue()
    {
        // Arrange
        this.viewModel.SelectedItem = "Layout 1";
        this.userActionRequester.Setup(x => x.RequestYesNo(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        // Act
        this.viewModel.DeleteCommand.Execute(this.viewModel.SelectedItem);

        // Assert
        this.layoutManager.Verify(x => x.DeleteLayout("Layout 1"), Times.Once);
    }

    [Test]
    public void DeleteCommandExecuteShouldInvokeLayoutManagerLoadLayoutNamesWhenUserActionRequesterYesNoReturnsTrue()
    {
        // Arrange
        this.viewModel.SelectedItem = "Layout 1";
        this.userActionRequester.Setup(x => x.RequestYesNo(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        this.layoutManager.Reset();

        // Act
        this.viewModel.DeleteCommand.Execute(this.viewModel.SelectedItem);

        // Assert
        this.layoutManager.Verify(x => x.LoadLayoutNames(), Times.Once);
    }

    [Test]
    public void DeleteCommandExecuteShouldInvokeUserActionRequesterYesNoWhenInvoked()
    {
        // Arrange
        this.viewModel.SelectedItem = "Layout 1";

        // Act
        this.viewModel.DeleteCommand.Execute(this.viewModel.SelectedItem);

        // Assert
        this.userActionRequester.Verify(x => x.RequestYesNo(this.viewModel.Title, $"Are you sure you want to do delete the '{this.viewModel.SelectedItem}' window layout?"));
    }

    [Test]
    public void DeleteCommandExecuteShouldNotInvokeLayoutManagerDeleteLayoutWhenUserActionRequesterYesNoReturnsFalse()
    {
        // Arrange
        this.viewModel.SelectedItem = "Layout 1";
        this.userActionRequester.Setup(x => x.RequestYesNo(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        // Act
        this.viewModel.DeleteCommand.Execute(this.viewModel.SelectedItem);

        // Assert
        this.layoutManager.Verify(x => x.SaveLayout("Layout 1"), Times.Never);
    }

    [Test]
    public void DeleteCommandExecuteShouldNotInvokeLayoutManagerLoadLayoutNamesWhenUserActionRequesterYesNoReturnsFalse()
    {
        // Arrange
        this.viewModel.SelectedItem = "Layout 1";
        this.userActionRequester.Setup(x => x.RequestYesNo(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        this.layoutManager.Reset();

        // Act
        this.viewModel.DeleteCommand.Execute(this.viewModel.SelectedItem);

        // Assert
        this.layoutManager.Verify(x => x.LoadLayoutNames(), Times.Never);
    }

    [Test]
    public void LayoutNamesShouldReturnEmptyArrayWhenLayoutManagerLoadLayoutNamesReturnsEmptyArray()
    {
        // Arrange
        this.layoutManager.Setup(x => x.LoadLayoutNames()).Returns<IEnumerable<string>>(null);
        var viewModel = new ManageWindowLayoutsViewModel(this.logger.Object, this.userActionRequester.Object, this.layoutManagerFactory.Object);

        // Act
        var actual = viewModel.LayoutNames;

        // Assert
        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void SelectedItemShouldReturnHelloWorldWhenSetToHelloWorld()
    {
        // Arrange
        const string expected = "Hello, World!";
        this.viewModel.SelectedItem = expected;

        // Act
        string actual = this.viewModel.SelectedItem;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SelectedItemShouldReturnNullWhenNotSet()
    {
        // Act
        string actual = this.viewModel.SelectedItem;

        // Assert
        Assert.That(actual, Is.Null);
    }

    [SetUp]
    public void Setup()
    {
        this.userActionRequester = new Mock<IUserActionRequester>();
        this.layoutManagerFactory = new Mock<ILayoutManagerFactory>();
        this.layoutManager = new Mock<ILayoutManager>();
        this.logger = new Mock<ILogger<ManageWindowLayoutsViewModel>>();

        this.layoutNames = new List<string>()
        {
            "Layout 1",
            "Layout 2",
            "Layout 3",
        };

        this.layoutManagerFactory.Setup(x => x.CreateManager()).Returns(this.layoutManager.Object);

        this.layoutManager.Setup(x => x.LoadLayoutNames()).Returns(this.layoutNames);

        this.viewModel = new ManageWindowLayoutsViewModel(this.logger.Object, this.userActionRequester.Object, this.layoutManagerFactory.Object);
    }

    [Test]
    public void TitleShouldReturnManageWindowLayoutsWhenInvoked()
    {
        // Arrange
        const string expected = "Manage Window Layouts";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
