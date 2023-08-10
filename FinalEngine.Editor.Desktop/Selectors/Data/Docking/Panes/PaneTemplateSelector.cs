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

/// <summary>
/// Provides a data template selectors for panes and tool views.
/// </summary>
/// <seealso cref="System.Windows.Controls.DataTemplateSelector" />
public sealed class PaneTemplateSelector : DataTemplateSelector
{
    /// <summary>
    /// Gets or sets the console template.
    /// </summary>
    /// <value>
    /// The console template.
    /// </value>
    public DataTemplate? ConsoleTemplate { get; set; }

    /// <summary>
    /// Gets or sets the entity systems template.
    /// </summary>
    /// <value>
    /// The entity systems template.
    /// </value>
    public DataTemplate? EntitySystemsTemplate { get; set; }

    /// <summary>
    /// Gets or sets the project explorer template.
    /// </summary>
    /// <value>
    /// The project explorer template.
    /// </value>
    public DataTemplate? ProjectExplorerTemplate { get; set; }

    /// <summary>
    /// Gets or sets the properties template.
    /// </summary>
    /// <value>
    /// The properties template.
    /// </value>
    public DataTemplate? PropertiesTemplate { get; set; }

    /// <summary>
    /// Gets or sets the scene hierarchy template.
    /// </summary>
    /// <value>
    /// The scene hierarchy template.
    /// </value>
    public DataTemplate? SceneHierarchyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the scene view template.
    /// </summary>
    /// <value>
    /// The scene view template.
    /// </value>
    public DataTemplate? SceneViewTemplate { get; set; }

    /// <summary>
    /// Selects the template to be used based on the specified <paramref name="item"/>.
    /// </summary>
    /// <param name="item">
    /// The item, this refers to the view model.
    /// </param>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <returns>
    /// The <see cref="DataTemplate"/> to use, or <c>null</c> if one could not be found.
    /// </returns>
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
