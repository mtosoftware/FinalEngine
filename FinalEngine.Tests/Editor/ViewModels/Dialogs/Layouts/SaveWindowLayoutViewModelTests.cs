// <copyright file="SaveWindowLayoutViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Dialogs.Layouts;

using System;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Factories;
using FinalEngine.Editor.ViewModels.Interactions;
using FinalEngine.Editor.ViewModels.Services.Actions;
using FinalEngine.Editor.ViewModels.Services.Layout;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SaveWindowLayoutViewModelTests
{
    private Mock<ICloseable> closeable;

    private Mock<ILayoutManager> layoutManager;

    private Mock<ILayoutManagerFactory> layoutManagerFactory;

    private Mock<IUserActionRequester> userActionRequester;

    private SaveWindowLayoutViewModel viewModel;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenlayoutManagerFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SaveWindowLayoutViewModel(null, this.userActionRequester.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenUserActionRequesterIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SaveWindowLayoutViewModel(this.layoutManagerFactory.Object, null);
        });
    }

    [Test]
    public void LayoutNameShouldReturnLayoutWhenSetToLayout()
    {
        // Arrange
        const string expected = "Layout";

        this.viewModel.LayoutName = expected;

        // Act
        string actual = this.viewModel.LayoutName;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void LayoutNameShouldReturnStringEmptyWhenSetToNull()
    {
        // Arrange
        string expected = string.Empty;

        this.viewModel.LayoutName = null;

        // Act
        string actual = this.viewModel.LayoutName;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SaveCommandCanExecuteShouldReturnFalseWhenLayoutNameIsNotValidFileName()
    {
        // Arraange
        this.viewModel.LayoutName = "<invalidFileName$>";

        // Act
        bool actual = this.viewModel.SaveCommand.CanExecute(this.closeable.Object);

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void SaveCommandCanExecuteShouldReturnTrueWhenLayoutNameIValidFileName()
    {
        // Arraange
        this.viewModel.LayoutName = "ValidFileName";

        // Act
        bool actual = this.viewModel.SaveCommand.CanExecute(this.closeable.Object);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void SaveCommandExecuteShouldInvokeCloseableCloseWhenInvoked()
    {
        // Arrange
        this.viewModel.LayoutName = "Layout";

        // Act
        this.viewModel.SaveCommand.Execute(this.closeable.Object);

        // Assert
        this.closeable.Verify(x => x.Close(), Times.Once);
    }

    [Test]
    public void SaveCommandExecuteShouldInvokeLayoutManagerContainsLayoutWhenInvoked()
    {
        // Arrange
        this.viewModel.LayoutName = "Layout";

        // Act
        this.viewModel.SaveCommand.Execute(this.closeable.Object);

        // Assert
        this.layoutManager.Verify(x => x.ContainsLayout("Layout"), Times.Once);
    }

    [Test]
    public void SaveCommandExecuteShouldInvokeLayoutManagerSaveLayoutWhenInvoked()
    {
        // Arrange
        this.viewModel.LayoutName = "Layout";

        // Act
        this.viewModel.SaveCommand.Execute(this.closeable.Object);

        // Assert
        this.layoutManager.Verify(x => x.SaveLayout("Layout"), Times.Once);
    }

    [Test]
    public void SaveCommandExecuteShouldInvokeSaveLayoutWhenUserActionRequesterYesNoReturnsTrue()
    {
        // Arrange
        this.layoutManager.Setup(x => x.ContainsLayout("Layout")).Returns(true);
        this.userActionRequester.Setup(x => x.RequestYesNo(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        this.viewModel.LayoutName = "Layout";

        // Act
        this.viewModel.SaveCommand.Execute(this.closeable.Object);

        // Assert
        this.layoutManager.Verify(x => x.SaveLayout("Layout"), Times.Once);
    }

    [Test]
    public void SaveCommandExecuteShouldInvokeUserActionRequesterYesNoWhenContainsLayoutReturnsTrue()
    {
        // Arrange
        this.layoutManager.Setup(x => x.ContainsLayout("Layout")).Returns(true);

        this.viewModel.LayoutName = "Layout";

        // Act
        this.viewModel.SaveCommand.Execute(this.closeable.Object);

        // Assert
        this.userActionRequester.Verify(
            x => x.RequestYesNo(
                this.viewModel.Title,
                $"A window layout named '{this.viewModel.LayoutName}' already exists. Do you want to replace it?"), Times.Once);
    }

    [Test]
    public void SaveCommandExecuteShouldNotInvokeCloseWhenUserActionRequesterReturnsFalse()
    {
        // Arrange
        this.layoutManager.Setup(x => x.ContainsLayout("Layout")).Returns(true);

        this.viewModel.LayoutName = "Layout";

        // Act
        this.viewModel.SaveCommand.Execute(this.closeable.Object);

        // Assert
        this.closeable.Verify(x => x.Close(), Times.Never);
    }

    [Test]
    public void SaveCommandExecuteShouldNotInvokeSaveLayoutWhenUserActionRequesterReturnsFalse()
    {
        // Arrange
        this.layoutManager.Setup(x => x.ContainsLayout("Layout")).Returns(true);

        this.viewModel.LayoutName = "Layout";

        // Act
        this.viewModel.SaveCommand.Execute(this.closeable.Object);

        // Assert
        this.layoutManager.Verify(x => x.SaveLayout("Layout"), Times.Never);
    }

    [Test]
    public void SaveCommandExecuteShouldThrowArgumentNullExceptionWhenCloseableIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.viewModel.SaveCommand.Execute(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.userActionRequester = new Mock<IUserActionRequester>();
        this.layoutManagerFactory = new Mock<ILayoutManagerFactory>();
        this.layoutManager = new Mock<ILayoutManager>();
        this.closeable = new Mock<ICloseable>();

        this.layoutManagerFactory.Setup(x => x.CreateManager()).Returns(this.layoutManager.Object);

        this.viewModel = new SaveWindowLayoutViewModel(this.layoutManagerFactory.Object, this.userActionRequester.Object);
    }

    [Test]
    public void TitleShouldReturnSaveWindowLayoutWhenInvoked()
    {
        // Arrange
        const string expected = "Save Window Layout";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
