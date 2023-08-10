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
using FinalEngine.Editor.ViewModels.Factories;

public sealed class DockViewModel : ObservableObject, IDockViewModel
{
    //// TODO: Check if I can just create the layout manager in the constructor.
    private readonly ILayoutManagerFactory layoutManagerFactory;

    private ICommand? loadCommand;

    private ICommand? unloadCommand;

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

    public ICommand LoadCommand
    {
        get { return this.loadCommand ??= new RelayCommand(this.Load); }
    }

    public IEnumerable<IPaneViewModel> Panes { get; }

    public IEnumerable<IToolViewModel> Tools { get; }

    public ICommand UnloadCommand
    {
        get { return this.unloadCommand ??= new RelayCommand(this.Unload); }
    }

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

    private void Unload()
    {
        this.layoutManagerFactory.CreateManager().SaveLayout("startup");
    }
}
