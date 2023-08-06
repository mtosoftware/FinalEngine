// <copyright file="DockViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking;

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;
using FinalEngine.Editor.ViewModels.Docking.Tools.Projects;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;
using FinalEngine.Editor.ViewModels.Interactions;
using FinalEngine.IO;

public sealed class DockViewModel : ObservableObject, IDockViewModel
{
    private const string LayoutFileName = "layout.config";

    private readonly IFileSystem fileSystem;

    private readonly IMessenger messenger;

    private ICommand? loadLayoutCommand;

    private ICommand? saveLayoutCommand;

    public DockViewModel(
        IMessenger messenger,
        IFileSystem fileSystem,
        IFactory<IProjectExplorerToolViewModel> projectExplorerFactory,
        IFactory<ISceneHierarchyToolViewModel> sceneHierarchyFactory,
        IFactory<IPropertiesToolViewModel> propertiesFactory,
        IFactory<IConsoleToolViewModel> consoleFactory,
        IFactory<IEntitySystemsToolViewModel> entitySystemsFactory,
        IFactory<ISceneViewPaneViewModel> sceneViewFactory)
    {
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

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

    public ICommand LoadLayoutCommand
    {
        get { return this.loadLayoutCommand ??= new RelayCommand<ILayoutSerializable>(this.LoadLayout); }
    }

    public IEnumerable<IPaneViewModel> Panes { get; }

    public ICommand SaveLayoutCommand
    {
        get
        {
            return this.saveLayoutCommand ??= new RelayCommand<ILayoutSerializable>(this.SaveLayout);
        }
    }

    public IEnumerable<IToolViewModel> Tools { get; }

    private void LoadLayout(ILayoutSerializable? serializable)
    {
        if (serializable == null)
        {
            throw new ArgumentNullException(nameof(serializable));
        }

        // Hmm... this could be an issue...
        if (!this.fileSystem.FileExists(LayoutFileName))
        {
            return;
        }

        using (var stream = this.fileSystem.OpenFile(LayoutFileName, FileAccessMode.Read))
        {
            using (var reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                serializable.Deserialize(content);
            }
        }
    }

    private void SaveLayout(ILayoutSerializable? serializable)
    {
        if (serializable == null)
        {
            throw new ArgumentNullException(nameof(serializable));
        }

        using (var stream = this.fileSystem.OpenFile(LayoutFileName, FileAccessMode.ReadAndWrite))
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(serializable.Serialize());
            }
        }
    }
}
