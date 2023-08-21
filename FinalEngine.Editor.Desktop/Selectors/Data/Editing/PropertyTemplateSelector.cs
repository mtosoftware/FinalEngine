// <copyright file="PropertyTemplateSelector.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Data.Editing;

using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.ViewModels.Editing.DataTypes;

/// <summary>
/// Temp.
/// </summary>
/// <seealso cref="System.Windows.Controls.DataTemplateSelector" />
public sealed class PropertyTemplateSelector : DataTemplateSelector
{
    /// <summary>
    /// Gets or sets the bool property template.
    /// </summary>
    /// <value>
    /// The bool property template.
    /// </value>
    public DataTemplate? BoolPropertyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the int property template.
    /// </summary>
    /// <value>
    /// The int property template.
    /// </value>
    public DataTemplate? IntPropertyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the string property template.
    /// </summary>
    /// <value>
    /// The string property template.
    /// </value>
    public DataTemplate? StringPropertyTemplate { get; set; }

    /// <summary>
    /// Selects the data template.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <returns>
    /// The data template.
    /// </returns>
    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        return item switch
        {
            StringPropertyViewModel => this.StringPropertyTemplate,
            BoolPropertyViewModel => this.BoolPropertyTemplate,
            IntPropertyViewModel => this.IntPropertyTemplate,
            _ => base.SelectTemplate(item, container),
        };
    }
}
