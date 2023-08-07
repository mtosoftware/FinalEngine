// <copyright file="DockViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking;

using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;
using FinalEngine.Editor.ViewModels.Docking.Tools.Projects;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

public sealed class DockViewModel : ObservableObject, IDockViewModel
{
    public DockViewModel(
        IFactory<IProjectExplorerToolViewModel> projectExplorerFactory,
        IFactory<ISceneHierarchyToolViewModel> sceneHierarchyFactory,
        IFactory<IPropertiesToolViewModel> propertiesFactory,
        IFactory<IConsoleToolViewModel> consoleFactory,
        IFactory<IEntitySystemsToolViewModel> entitySystemsFactory,
        IFactory<ISceneViewPaneViewModel> sceneViewFactory)
    {
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

    public IEnumerable<IPaneViewModel> Panes { get; }

    public IEnumerable<IToolViewModel> Tools { get; }
}
