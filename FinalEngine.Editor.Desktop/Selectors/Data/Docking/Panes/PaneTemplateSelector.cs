// <copyright file="PaneTemplateSelector.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Data.Docking.Panes;

using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;
using FinalEngine.Editor.ViewModels.Docking.Tools.Projects;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

public sealed class PaneTemplateSelector : DataTemplateSelector
{
    public DataTemplate? ConsoleTemplate { get; set; }

    public DataTemplate? EntitySystemsTemplate { get; set; }

    public DataTemplate? ProjectExplorerTemplate { get; set; }

    public DataTemplate? PropertiesTemplate { get; set; }

    public DataTemplate? SceneHierarchyTemplate { get; set; }

    public DataTemplate? SceneViewTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        if (item is IConsoleToolViewModel)
        {
            return this.ConsoleTemplate;
        }

        if (item is IEntitySystemsToolViewModel)
        {
            return this.EntitySystemsTemplate;
        }

        if (item is IProjectExplorerToolViewModel)
        {
            return this.ProjectExplorerTemplate;
        }

        if (item is IPropertiesToolViewModel)
        {
            return this.PropertiesTemplate;
        }

        if (item is ISceneHierarchyToolViewModel)
        {
            return this.SceneHierarchyTemplate;
        }

        if (item is ISceneViewPaneViewModel)
        {
            return this.SceneViewTemplate;
        }

        return base.SelectTemplate(item, container);
    }
}
