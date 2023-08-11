// <copyright file="ViewPresenterTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels;

using System;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Interactions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class ViewPresenterTests
{
    private Mock<ILogger<ViewPresenter>> logger;

    private ViewPresenter presenter;

    private Mock<IServiceProvider> provider;

    private Mock<IViewable<IMainViewModel>> viewable;

    private Mock<IMainViewModel> viewModel;

    private Mock<IFactory<IMainViewModel>> viewModelFactory;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ViewPresenter(null, this.provider.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenProviderIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ViewPresenter(this.logger.Object, null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.logger = new Mock<ILogger<ViewPresenter>>();
        this.provider = new Mock<IServiceProvider>();
        this.viewable = new Mock<IViewable<IMainViewModel>>();
        this.viewModel = new Mock<IMainViewModel>();
        this.viewModelFactory = new Mock<IFactory<IMainViewModel>>();

        this.viewModelFactory.Setup(x => x.Create()).Returns(this.viewModel.Object);
        this.provider.Setup(x => x.GetService(typeof(IViewable<IMainViewModel>))).Returns(this.viewable.Object);
        this.provider.Setup(x => x.GetService(typeof(IFactory<IMainViewModel>))).Returns(this.viewModelFactory.Object);

        this.presenter = new ViewPresenter(this.logger.Object, this.provider.Object);
    }

    [Test]
    public void ShowViewShouldInvokeProviderGetServiceWhenInvoked()
    {
        // Act
        this.presenter.ShowView(this.viewModel.Object);

        // Assert
        this.provider.Verify(x => x.GetService(typeof(IViewable<IMainViewModel>)), Times.Once);
    }

    [Test]
    public void ShowViewShouldInvokeShowDialogWhenInvoked()
    {
        // Act
        this.presenter.ShowView(this.viewModel.Object);

        // Assert
        this.viewable.Verify(x => x.ShowDialog(), Times.Once);
    }

    [Test]
    public void ShowViewShouldInvokeViewModelFactoryCreateWhenInvoked()
    {
        // Act
        this.presenter.ShowView<IMainViewModel>();

        // Assert
        this.viewModelFactory.Verify(x => x.Create(), Times.Once);
    }

    [Test]
    public void ShowViewShouldThrowArgumentExceptionWhenViewableCannotBeMatchedToViewModelType()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.presenter.ShowView(this.viewable.Object);
        });
    }

    [Test]
    public void ShowViewShouldThrowArgumentExceptionWhenViewModelHasNotBeenRegisteredAsFactory()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(this.presenter.ShowView<IDockViewModel>);
    }

    [Test]
    public void ShowViewShouldThrowArgumentNullExceptionWhenViewModelIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.presenter.ShowView<IMainViewModel>(null);
        });
    }
}
