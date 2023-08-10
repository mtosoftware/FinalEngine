// <copyright file="DockViewModelTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Docking;

using System;
using System.Linq;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;
using FinalEngine.Editor.ViewModels.Docking.Tools.Projects;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;
using FinalEngine.Editor.ViewModels.Factories;
using FinalEngine.Editor.ViewModels.Services.Layout;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class DockViewModelTests
{
    private Mock<IFactory<IConsoleToolViewModel>> consoleFactory;

    private Mock<IConsoleToolViewModel> consoleToolViewModel;

    private Mock<IFactory<IEntitySystemsToolViewModel>> entitySystemsFactory;

    private Mock<IEntitySystemsToolViewModel> entitySystemsToolViewModel;

    private Mock<ILayoutManager> layoutManager;

    private Mock<ILayoutManagerFactory> layoutManagerFactory;

    private Mock<IFactory<IProjectExplorerToolViewModel>> projectExplorerFactory;

    private Mock<IProjectExplorerToolViewModel> projectExplorerToolViewModel;

    private Mock<IFactory<IPropertiesToolViewModel>> propertiesFactory;

    private Mock<IPropertiesToolViewModel> propertiesToolViewModel;

    private Mock<IFactory<ISceneHierarchyToolViewModel>> sceneHierarchyFactory;

    private Mock<ISceneHierarchyToolViewModel> sceneHierarchyToolViewModel;

    private Mock<IFactory<ISceneViewPaneViewModel>> sceneViewFactory;

    private Mock<ISceneViewPaneViewModel> sceneViewPaneViewModel;

    private DockViewModel viewModel;

    [Test]
    public void ConstructorShouldInvokeConsoleFactoryCreateWhenInvoked()
    {
        // Assert
        this.consoleFactory.Verify(x => x.Create(), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeEntitySystemsFactoryCreateWhenInvoked()
    {
        // Assert
        this.entitySystemsFactory.Verify(x => x.Create(), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeProjectExplorerFactoryCreateWhenInvoked()
    {
        // Assert
        this.projectExplorerFactory.Verify(x => x.Create(), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokePropertiesFactoryCreateWhenInvoked()
    {
        // Assert
        this.propertiesFactory.Verify(x => x.Create(), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeSceneHierarchyFactoryCreateWhenInvoked()
    {
        // Assert
        this.sceneHierarchyFactory.Verify(x => x.Create(), Times.Once);
    }

    [Test]
    public void ConstructorShouldInvokeSceneViewFactoryCreateWhenInvoked()
    {
        // Assert
        this.sceneViewFactory.Verify(x => x.Create(), Times.Once);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenConsoleFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new DockViewModel(
                this.layoutManagerFactory.Object,
                this.projectExplorerFactory.Object,
                this.sceneHierarchyFactory.Object,
                this.propertiesFactory.Object,
                null,
                this.entitySystemsFactory.Object,
                this.sceneViewFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenEntitySystemsFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new DockViewModel(
                this.layoutManagerFactory.Object,
                this.projectExplorerFactory.Object,
                this.sceneHierarchyFactory.Object,
                this.propertiesFactory.Object,
                this.consoleFactory.Object,
                null,
                this.sceneViewFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLayoutManagerFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new DockViewModel(
                null,
                this.projectExplorerFactory.Object,
                this.sceneHierarchyFactory.Object,
                this.propertiesFactory.Object,
                this.consoleFactory.Object,
                this.entitySystemsFactory.Object,
                this.sceneViewFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenProjectExplorerFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new DockViewModel(
                this.layoutManagerFactory.Object,
                null,
                this.sceneHierarchyFactory.Object,
                this.propertiesFactory.Object,
                this.consoleFactory.Object,
                this.entitySystemsFactory.Object,
                this.sceneViewFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenPropertiesFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new DockViewModel(
                this.layoutManagerFactory.Object,
                this.projectExplorerFactory.Object,
                this.sceneHierarchyFactory.Object,
                null,
                this.consoleFactory.Object,
                this.entitySystemsFactory.Object,
                this.sceneViewFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenSceneHierarchyFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new DockViewModel(
                this.layoutManagerFactory.Object,
                this.projectExplorerFactory.Object,
                null,
                this.propertiesFactory.Object,
                this.consoleFactory.Object,
                this.entitySystemsFactory.Object,
                this.sceneViewFactory.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenSceneViewFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new DockViewModel(
                this.layoutManagerFactory.Object,
                this.projectExplorerFactory.Object,
                this.sceneHierarchyFactory.Object,
                this.propertiesFactory.Object,
                this.consoleFactory.Object,
                this.entitySystemsFactory.Object,
                null);
        });
    }

    [Test]
    public void LoadCommandExecuteShouldInvokeLayoutManagerContainsLayoutWhenInvoked()
    {
        // Act
        this.viewModel.LoadCommand.Execute(null);

        // Assert
        this.layoutManager.Verify(x => x.ContainsLayout("startup"), Times.Once);
    }

    [Test]
    public void LoadCommandExecuteShouldInvokeLayoutManagerFactoryCreateManagerWhenInvoked()
    {
        // Act
        this.viewModel.LoadCommand.Execute(null);

        // Assert
        this.layoutManagerFactory.Verify(x => x.CreateManager(), Times.Once);
    }

    [Test]
    public void LoadCommandExecuteShouldInvokeLayoutManagerResetLayoutWhenDoesNotContainStartupLayout()
    {
        // Act
        this.viewModel.LoadCommand.Execute(null);

        // Assert
        this.layoutManager.Verify(x => x.ResetLayout(), Times.Once);
    }

    [Test]
    public void LoadCommandExecuteShouldInvokeLoadLayoutWhenDoesContainStartupLayout()
    {
        // Arrange
        this.layoutManager.Setup(x => x.ContainsLayout("startup")).Returns(true);

        // Act
        this.viewModel.LoadCommand.Execute(null);

        // Assert
        this.layoutManager.Verify(x => x.LoadLayout("startup"), Times.Once);
    }

    [Test]
    public void LoadCommandExecuteShouldNotInvokeLoadLayoutWhenDoesNotContainStartupLayout()
    {
        // Act
        this.viewModel.LoadCommand.Execute(null);

        // Assert
        this.layoutManager.Verify(x => x.LoadLayout("startup"), Times.Never);
    }

    [Test]
    public void PanesShouldContainSceneViewWhenInvoked()
    {
        // Assert
        Assert.That(this.viewModel.Panes.ToList().Contains(this.sceneViewPaneViewModel.Object), Is.True);
    }

    [SetUp]
    public void Setup()
    {
        this.consoleFactory = new Mock<IFactory<IConsoleToolViewModel>>();
        this.entitySystemsFactory = new Mock<IFactory<IEntitySystemsToolViewModel>>();
        this.propertiesFactory = new Mock<IFactory<IPropertiesToolViewModel>>();
        this.projectExplorerFactory = new Mock<IFactory<IProjectExplorerToolViewModel>>();
        this.sceneViewFactory = new Mock<IFactory<ISceneViewPaneViewModel>>();
        this.sceneHierarchyFactory = new Mock<IFactory<ISceneHierarchyToolViewModel>>();

        this.sceneViewPaneViewModel = new Mock<ISceneViewPaneViewModel>();
        this.projectExplorerToolViewModel = new Mock<IProjectExplorerToolViewModel>();
        this.propertiesToolViewModel = new Mock<IPropertiesToolViewModel>();
        this.sceneHierarchyToolViewModel = new Mock<ISceneHierarchyToolViewModel>();
        this.consoleToolViewModel = new Mock<IConsoleToolViewModel>();
        this.entitySystemsToolViewModel = new Mock<IEntitySystemsToolViewModel>();

        this.layoutManagerFactory = new Mock<ILayoutManagerFactory>();
        this.layoutManager = new Mock<ILayoutManager>();

        this.consoleFactory.Setup(x => x.Create()).Returns(this.consoleToolViewModel.Object);
        this.entitySystemsFactory.Setup(x => x.Create()).Returns(this.entitySystemsToolViewModel.Object);
        this.propertiesFactory.Setup(x => x.Create()).Returns(this.propertiesToolViewModel.Object);
        this.projectExplorerFactory.Setup(x => x.Create()).Returns(this.projectExplorerToolViewModel.Object);
        this.sceneViewFactory.Setup(x => x.Create()).Returns(this.sceneViewPaneViewModel.Object);
        this.sceneHierarchyFactory.Setup(x => x.Create()).Returns(this.sceneHierarchyToolViewModel.Object);
        this.entitySystemsFactory.Setup(x => x.Create()).Returns(this.entitySystemsToolViewModel.Object);
        this.layoutManagerFactory.Setup(x => x.CreateManager()).Returns(this.layoutManager.Object);

        this.viewModel = new DockViewModel(
            this.layoutManagerFactory.Object,
            this.projectExplorerFactory.Object,
            this.sceneHierarchyFactory.Object,
            this.propertiesFactory.Object,
            this.consoleFactory.Object,
            this.entitySystemsFactory.Object,
            this.sceneViewFactory.Object);
    }

    [Test]
    public void ToolsShouldContainConsoleWhenInvoked()
    {
        // Assert
        Assert.That(this.viewModel.Tools.ToList().Contains(this.consoleToolViewModel.Object), Is.True);
    }

    [Test]
    public void ToolsShouldContainEntitySystemsWhenInvoked()
    {
        // Assert
        Assert.That(this.viewModel.Tools.ToList().Contains(this.entitySystemsToolViewModel.Object), Is.True);
    }

    [Test]
    public void ToolsShouldContainProjectExplorerWhenInvoked()
    {
        // Assert
        Assert.That(this.viewModel.Tools.ToList().Contains(this.projectExplorerToolViewModel.Object), Is.True);
    }

    [Test]
    public void ToolsShouldContainPropertiesWhenInvoked()
    {
        // Assert
        Assert.That(this.viewModel.Tools.ToList().Contains(this.propertiesToolViewModel.Object), Is.True);
    }

    [Test]
    public void ToolsShouldContainSceneHierarchyWhenInvoked()
    {
        // Assert
        Assert.That(this.viewModel.Tools.ToList().Contains(this.sceneHierarchyToolViewModel.Object), Is.True);
    }

    [Test]
    public void UnloadCommandExecuteShouldInvokeLayoutManagerFactoryCreateManagerWhenInvoked()
    {
        // Act
        this.viewModel.UnloadCommand.Execute(null);

        // Assert
        this.layoutManagerFactory.Verify(x => x.CreateManager(), Times.Once);
    }

    [Test]
    public void UnloadCommandExecuteShouldInvokeLayoutManagerSaveLayoutWhenInvoked()
    {
        // Act
        this.viewModel.UnloadCommand.Execute(null);

        // Assert
        this.layoutManager.Verify(x => x.SaveLayout("startup"), Times.Once);
    }
}
