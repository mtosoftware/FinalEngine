// <copyright file="DockViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking;

using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;
using FinalEngine.Editor.ViewModels.Docking.Tools.Projects;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;
using FinalEngine.Editor.ViewModels.Services.Factories.Layout;

/// <summary>
/// Provides a standard implementation of an <see cref="IDockViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IDockViewModel" />
public sealed class DockViewModel : ObservableObject, IDockViewModel
{
    /// <summary>
    /// The layout manager factory, used to load and save the window layout when the application starts and shuts down.
    /// </summary>
    private readonly ILayoutManagerFactory layoutManagerFactory;

    /// <summary>
    /// The load command.
    /// </summary>
    private ICommand? loadCommand;

    /// <summary>
    /// The unload command.
    /// </summary>
    private ICommand? unloadCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="DockViewModel"/> class.
    /// </summary>
    /// <param name="layoutManagerFactory">
    /// The layout manager factory, used to load and save layouts upon startup and shutdown of the application.
    /// </param>
    /// <param name="projectExplorerFactory">
    /// The project explorer factory.
    /// </param>
    /// <param name="sceneHierarchyFactory">
    /// The scene hierarchy factory.
    /// </param>
    /// <param name="propertiesFactory">
    /// The properties factory.
    /// </param>
    /// <param name="consoleFactory">
    /// The console factory.
    /// </param>
    /// <param name="entitySystemsFactory">
    /// The entity systems factory.
    /// </param>
    /// <param name="sceneViewFactory">
    /// The scene view factory.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="layoutManagerFactory"/>, <paramref name="projectExplorerFactory"/>, <paramref name="sceneHierarchyFactory"/>, <paramref name="propertiesFactory"/>, <paramref name="consoleFactory"/>, <paramref name="entitySystemsFactory"/> or <paramref name="sceneViewFactory"/> parameter cannot be null.
    /// </exception>
    public DockViewModel(
        ILayoutManagerFactory layoutManagerFactory,
        IFactory<IProjectExplorerToolViewModel> projectExplorerFactory,
        IFactory<ISceneHierarchyToolViewModel> sceneHierarchyFactory,
        IFactory<IPropertiesToolViewModel> propertiesFactory,
        IFactory<IConsoleToolViewModel> consoleFactory,
        IFactory<IEntitySystemsToolViewModel> entitySystemsFactory,
        IFactory<ISceneViewPaneViewModel> sceneViewFactory)
    {
        this.layoutManagerFactory = layoutManagerFactory ?? throw new ArgumentNullException(nameof(layoutManagerFactory));

        if (projectExplorerFactory == null)
        {
            throw new ArgumentNullException(nameof(projectExplorerFactory));
        }

        if (sceneHierarchyFactory == null)
        {
            throw new ArgumentNullException(nameof(sceneHierarchyFactory));
        }

        if (propertiesFactory == null)
        {
            throw new ArgumentNullException(nameof(propertiesFactory));
        }

        if (consoleFactory == null)
        {
            throw new ArgumentNullException(nameof(consoleFactory));
        }

        if (entitySystemsFactory == null)
        {
            throw new ArgumentNullException(nameof(entitySystemsFactory));
        }

        if (sceneViewFactory == null)
        {
            throw new ArgumentNullException(nameof(sceneViewFactory));
        }

        this.Tools = new List<IToolViewModel>()
        {
            projectExplorerFactory.Create(),
            sceneHierarchyFactory.Create(),
            propertiesFactory.Create(),
            consoleFactory.Create(),
            entitySystemsFactory.Create(),
        };

        this.Panes = new List<IPaneViewModel>()
        {
            sceneViewFactory.Create(),
        };
    }

    /// <inheritdoc/>
    public ICommand LoadCommand
    {
        get { return this.loadCommand ??= new RelayCommand(this.Load); }
    }

    /// <inheritdoc/>
    public IEnumerable<IPaneViewModel> Panes { get; }

    /// <inheritdoc/>
    public IEnumerable<IToolViewModel> Tools { get; }

    /// <inheritdoc/>
    public ICommand UnloadCommand
    {
        get { return this.unloadCommand ??= new RelayCommand(this.Unload); }
    }

    /// <summary>
    /// Loads the last state of the window layout, or; resets and loads the default layout.
    /// </summary>
    private void Load()
    {
        var layoutManager = this.layoutManagerFactory.CreateManager();

        if (!layoutManager.ContainsLayout("startup"))
        {
            layoutManager.ResetLayout();
            return;
        }

        layoutManager.LoadLayout("startup");
    }

    /// <summary>
    /// Saves the current layout to the application roaming data to be used when the application is started once again.
    /// </summary>
    private void Unload()
    {
        this.layoutManagerFactory.CreateManager().SaveLayout("startup");
    }
}
