// <copyright file="PaneStyleSelector.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Styles.Docking.Panes;

using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Tools;

/// <summary>
/// Provides a style selector for panes and tool views.
/// </summary>
/// <seealso cref="StyleSelector" />
public sealed class PaneStyleSelector : StyleSelector
{
    /// <summary>
    /// Gets or sets the pane style.
    /// </summary>
    /// <value>
    /// The pane style.
    /// </value>
    public Style? PaneStyle { get; set; }

    /// <summary>
    /// Gets or sets the tool style.
    /// </summary>
    /// <value>
    /// The tool style.
    /// </value>
    public Style? ToolStyle { get; set; }

    /// <summary>
    /// Selects the style based on the specified <paramref name="item"/>.
    /// </summary>
    /// <param name="item">The item, this refers to the view model.
    /// </param>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <returns>
    /// The <see cref="Style"/> to use or <c>null</c> if one could not be found.
    /// </returns>
    public override Style? SelectStyle(object item, DependencyObject container)
    {
        if (item is IToolViewModel)
        {
            return this.ToolStyle;
        }

        if (item is IPaneViewModel)
        {
            return this.PaneStyle;
        }

        return base.SelectStyle(item, container);
    }
}
