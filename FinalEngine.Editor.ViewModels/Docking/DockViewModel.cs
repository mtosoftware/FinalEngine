// <copyright file="DockViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking;

using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;
using FinalEngine.Editor.ViewModels.Docking.Tools.Projects;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;
using FinalEngine.Editor.ViewModels.Messages.Layout;

public sealed class DockViewModel : ObservableObject, IDockViewModel
{
    private readonly IApplicationDataContext context;

    private readonly IMessenger messenger;

    private ICommand? loadCommand;

    private ICommand? unloadCommand;

    public DockViewModel(
        IMessenger messenger,
        IApplicationDataContext context,
        IFactory<IProjectExplorerToolViewModel> projectExplorerFactory,
        IFactory<ISceneHierarchyToolViewModel> sceneHierarchyFactory,
        IFactory<IPropertiesToolViewModel> propertiesFactory,
        IFactory<IConsoleToolViewModel> consoleFactory,
        IFactory<IEntitySystemsToolViewModel> entitySystemsFactory,
        IFactory<ISceneViewPaneViewModel> sceneViewFactory)
    {
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.context = context ?? throw new ArgumentNullException(nameof(context));

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
        // If it's the first time startup, use the default window layout.
        if (!this.context.ContainsLayout("startup"))
        {
            this.messenger.Send<ResetWindowLayoutMessage>();
            return;
        }

        // Otherwise, let's get the last known layout.
        this.messenger.Send(new LoadWindowLayoutMessage(this.context.GetLayoutPath("startup")));
    }

    private void Unload()
    {
        this.messenger.Send(new SaveWindowLayoutMessage(this.context.GetLayoutPath("startup")));
    }
}
