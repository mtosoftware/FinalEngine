// <copyright file="PaneTemplateSelector.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Data.Docking.Panes;

using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.ViewModels.Inspectors;
using FinalEngine.Editor.ViewModels.Projects;
using FinalEngine.Editor.ViewModels.Scenes;

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
