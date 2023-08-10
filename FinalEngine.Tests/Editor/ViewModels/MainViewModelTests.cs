// <copyright file="MainViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels;

using System;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Factories;
using FinalEngine.Editor.ViewModels.Interactions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class MainViewModelTests
{
    private Mock<IApplicationContext> applicationContext;

    private Mock<ICloseable> closeable;

    private Mock<IDockViewModel> dockViewModel;

    private Mock<IFactory<IDockViewModel>> dockViewModelFactory;

    private Mock<ILayoutManager> layoutManager;

    private Mock<ILayoutManagerFactory> layoutManagerFactory;

    private Mock<ILogger<MainViewModel>> logger;

    private MainViewModel viewModel;

    private Mock<IViewPresenter> viewPresenter;

    [Test]
    public void ConstructorShouldInvokeApplicationWhenInvoked()
    {
        // Assert
        this.applicationContext.Verify(x => x.Title, Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeDockViewModelFactoryCreateWhenInvoked()
    {
        // Assert
        this.dockViewModelFactory.Verify(x => x.Create(), Times.Once);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenApplicationContextIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MainViewModel(
                this.logger.Object,
                this.viewPresenter.Object,
                null,
                this.layoutManagerFactory.Object,
                this.dockViewModelFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenDockViewModelFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MainViewModel(
                this.logger.Object,
                this.viewPresenter.Object,
                this.applicationContext.Object,
                this.layoutManagerFactory.Object,
                null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLayoutManagerFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MainViewModel(
                this.logger.Object,
                this.viewPresenter.Object,
                this.applicationContext.Object,
                null,
                this.dockViewModelFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MainViewModel(
                null,
                this.viewPresenter.Object,
                this.applicationContext.Object,
                this.layoutManagerFactory.Object,
                this.dockViewModelFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenViewPresenterIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MainViewModel(
                this.logger.Object,
                null,
                this.applicationContext.Object,
                this.layoutManagerFactory.Object,
                this.dockViewModelFactory.Object);
        });
    }

    [Test]
    public void DockViewModelShouldReturnDockViewModelObjectWhenInvoked()
    {
        // Act
        var actual = this.viewModel.DockViewModel;

        // Assert
        Assert.That(actual, Is.EqualTo(this.dockViewModel.Object));
    }

    [Test]
    public void ExitCommandExecuteShouldInvokeCloseableWhenInvoked()
    {
        // Act
        this.viewModel.ExitCommand.Execute(this.closeable.Object);

        // Assert
        this.closeable.Verify(x => x.Close(), Times.Once);
    }

    [Test]
    public void ExitCommandExecuteShouldThrowArgumentNullExceptionWhenCloseableIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.viewModel.ExitCommand.Execute(null);
        });
    }

    [Test]
    public void ManageWindowLayoutCommandExecuteShouldInvokeViewPresenterShowViewWhenInvoked()
    {
        // Act
        this.viewModel.ManageWindowLayoutsCommand.Execute(null);

        // Assert
        this.viewPresenter.Verify(x => x.ShowView<IManageWindowLayoutsViewModel>(), Times.Once);
    }

    [Test]
    public void ResetWindowLayoutCommandExecuteShouldInvokeLayoutManagerFactoryCreateManagerWhenInvoked()
    {
        // Act
        this.viewModel.ResetWindowLayoutCommand.Execute(null);

        // Assert
        this.layoutManagerFactory.Verify(x => x.CreateManager(), Times.Once);
    }

    [Test]
    public void ResetWindowLayoutCommandExecuteShouldInvokeLayoutManagerResetLayoutWhenInvoked()
    {
        // Act
        this.viewModel.ResetWindowLayoutCommand.Execute(null);

        // Assert
        this.layoutManager.Verify(x => x.ResetLayout(), Times.Once);
    }

    [Test]
    public void SaveWindowLayoutCommandExecuteShouldInvokeViewPresenterShowViewWhenInvoked()
    {
        // Act
        this.viewModel.SaveWindowLayoutCommand.Execute(null);

        // Assert
        this.viewPresenter.Verify(x => x.ShowView<ISaveWindowLayoutViewModel>(), Times.Once);
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<MainViewModel>>();
        this.viewPresenter = new Mock<IViewPresenter>();
        this.applicationContext = new Mock<IApplicationContext>();
        this.layoutManagerFactory = new Mock<ILayoutManagerFactory>();
        this.dockViewModelFactory = new Mock<IFactory<IDockViewModel>>();
        this.dockViewModel = new Mock<IDockViewModel>();
        this.layoutManager = new Mock<ILayoutManager>();
        this.closeable = new Mock<ICloseable>();

        this.applicationContext.Setup(x => x.Title).Returns("Final Engine");
        this.dockViewModelFactory.Setup(x => x.Create()).Returns(this.dockViewModel.Object);
        this.layoutManagerFactory.Setup(x => x.CreateManager()).Returns(this.layoutManager.Object);

        this.viewModel = new MainViewModel(
            this.logger.Object,
            this.viewPresenter.Object,
            this.applicationContext.Object,
            this.layoutManagerFactory.Object,
            this.dockViewModelFactory.Object);
    }

    [Test]
    public void TitleShouldReturnFinalEngineWhenInvoked()
    {
        // Arrange
        const string expected = "Final Engine";

        // Act
        string actual = this.viewModel.Title;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToggleToolWindowCommandExecuteShouldThrowArgumentExceptionWhenContentIDIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.viewModel.ToggleToolWindowCommand.Execute(string.Empty);
        });
    }

    [Test]
    public void ToggleToolWindowCommandExecuteShouldThrowArgumentExceptionWhenContentIDIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.viewModel.ToggleToolWindowCommand.Execute(null);
        });
    }

    [Test]
    public void ToggleToolWindowCommandExecuteShouldThrowArgumentExceptionWhenContentIDIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.viewModel.ToggleToolWindowCommand.Execute("\t\r\n ");
        });
    }

    [Test]
    public void ToggleWindowCommandExecuteShouldInvokeLayoutManagerFactoryCreateManagerWhenInvoked()
    {
        // Act
        this.viewModel.ToggleToolWindowCommand.Execute("Layout");

        // Assert
        this.layoutManagerFactory.Verify(x => x.CreateManager(), Times.Once);
    }
}
