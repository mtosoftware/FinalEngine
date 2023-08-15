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
using FinalEngine.Editor.ViewModels.Services.Layout;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="IDockViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
/// <seealso cref="IDockViewModel" />
public sealed class DockViewModel : ObservableObject, IDockViewModel
{
    /// <summary>
    /// The name of the layout used when the user starts up the application after the first time.
    /// </summary>
    private const string StartupLayoutName = "startup";

    /// <summary>
    /// The layout manager, used to load and save the window layout when the application starts and shuts down.
    /// </summary>
    private readonly ILayoutManager layoutManager;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<DockViewModel> logger;

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
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="layoutManager">
    /// The layout manager, used to load and save layouts upon startup and shutdown of the application.
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
    /// The specified <paramref name="layoutManager"/>, <paramref name="projectExplorerFactory"/>, <paramref name="sceneHierarchyFactory"/>, <paramref name="propertiesFactory"/>, <paramref name="consoleFactory"/>, <paramref name="entitySystemsFactory"/> or <paramref name="sceneViewFactory"/> parameter cannot be null.
    /// </exception>
    public DockViewModel(
        ILogger<DockViewModel> logger,
        ILayoutManager layoutManager,
        IFactory<IProjectExplorerToolViewModel> projectExplorerFactory,
        IFactory<ISceneHierarchyToolViewModel> sceneHierarchyFactory,
        IFactory<IPropertiesToolViewModel> propertiesFactory,
        IFactory<IConsoleToolViewModel> consoleFactory,
        IFactory<IEntitySystemsToolViewModel> entitySystemsFactory,
        IFactory<ISceneViewPaneViewModel> sceneViewFactory)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.layoutManager = layoutManager ?? throw new ArgumentNullException(nameof(layoutManager));

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

        this.logger.LogInformation("Creating tool views...");

        this.Tools = new List<IToolViewModel>()
        {
            projectExplorerFactory.Create(),
            sceneHierarchyFactory.Create(),
            propertiesFactory.Create(),
            consoleFactory.Create(),
            entitySystemsFactory.Create(),
        };

        this.logger.LogInformation("Creating pane views...");

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
        this.logger.LogInformation("Loading the window layout...");

        if (!this.layoutManager.ContainsLayout(StartupLayoutName))
        {
            this.logger.LogInformation("No startup window layout was found, resolving to default layout...");
            this.layoutManager.ResetLayout();

            return;
        }

        this.layoutManager.LoadLayout(StartupLayoutName);
    }

    /// <summary>
    /// Saves the current layout to the application local data to be used when the application is started once again.
    /// </summary>
    private void Unload()
    {
        this.logger.LogInformation("Saving the startup window layout...");
        this.layoutManager.SaveLayout(StartupLayoutName);
    }
}
