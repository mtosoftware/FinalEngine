// <copyright file="IMainView.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Views
{
    using System;
    using FinalEngine.Editor.ViewModels.Documents;
    using FinalEngine.Editor.ViewModels.Tools;

    public interface IMainView
    {
        event EventHandler? OnEditMenuOpening;

        event EventHandler? OnEditRedo;

        event EventHandler? OnEditUndo;

        event EventHandler? OnLoaded;

        event EventHandler? OnMenuEditDelete;

        ConsoleViewModel Console { get; }

        EntityInspectorViewModel EntityInspector { get; }

        EntitySystemsViewModel EntitySystems { get; }

        SceneViewModel Scene { get; }

        SceneHierarchyViewModel SceneHierarchy { get; }
    }
}
