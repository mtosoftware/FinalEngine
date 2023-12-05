// <copyright file="PaneTemplateSelector.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Data.Docking.Panes;

using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.ViewModels.Inspectors;
using FinalEngine.Editor.ViewModels.Projects;
using FinalEngine.Editor.ViewModels.Scenes;

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
        return item switch
        {
            IConsoleToolViewModel => this.ConsoleTemplate,
            IEntitySystemsToolViewModel => this.EntitySystemsTemplate,
            IProjectExplorerToolViewModel => this.ProjectExplorerTemplate,
            IPropertiesToolViewModel => this.PropertiesTemplate,
            ISceneHierarchyToolViewModel => this.SceneHierarchyTemplate,
            ISceneViewPaneViewModel => this.SceneViewTemplate,
            _ => base.SelectTemplate(item, container)
        };
    }
}
