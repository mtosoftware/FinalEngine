// <copyright file="PanesTemplateSelector.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Docking;

using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.ViewModels.Docking.Panes;

/// <summary>
///   Provides a pane template style selector.
/// </summary>
/// <seealso cref="DataTemplateSelector"/>
public class PanesTemplateSelector : DataTemplateSelector
{
    public DataTemplate? CodeTemplate { get; set; }

    /// <summary>
    /// Gets or sets the scene template.
    /// </summary>
    /// <value>
    /// The scene template.
    /// </value>
    public DataTemplate? SceneTemplate { get; set; }

    /// <summary>
    ///   When overridden in a derived class, returns a <see cref="DataTemplate"/> based on custom logic.
    /// </summary>
    /// <param name="item">
    ///   The data object for which to select the template.
    /// </param>
    /// <param name="container">
    ///   The data-bound object.
    /// </param>
    /// <returns>
    ///   Returns a <see cref="DataTemplate"/> or <c>null</c>. The default value is <c>null</c>.
    /// </returns>
    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        if (item is ISceneViewModel)
        {
            return this.SceneTemplate;
        }
        else if (item is ICodeViewModel)
        {
            return this.CodeTemplate;
        }

        return base.SelectTemplate(item, container);
    }
}
