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
    /// Gets or sets the double property template.
    /// </summary>
    /// <value>
    /// The double property template.
    /// </value>
    public DataTemplate? DoublePropertyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the float property template.
    /// </summary>
    /// <value>
    /// The float property template.
    /// </value>
    public DataTemplate? FloatPropertyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the int property template.
    /// </summary>
    /// <value>
    /// The int property template.
    /// </value>
    public DataTemplate? IntPropertyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the quaternion property template.
    /// </summary>
    /// <value>
    /// The quaternion property template.
    /// </value>
    public DataTemplate? QuaternionPropertyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the string property template.
    /// </summary>
    /// <value>
    /// The string property template.
    /// </value>
    public DataTemplate? StringPropertyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the vector2 property template.
    /// </summary>
    /// <value>
    /// The vector2 property template.
    /// </value>
    public DataTemplate? Vector2PropertyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the vector3 property template.
    /// </summary>
    /// <value>
    /// The vector3 property template.
    /// </value>
    public DataTemplate? Vector3PropertyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the vector4 property template.
    /// </summary>
    /// <value>
    /// The vector4 property template.
    /// </value>
    public DataTemplate? Vector4PropertyTemplate { get; set; }

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
            DoublePropertyViewModel => this.DoublePropertyTemplate,
            FloatPropertyViewModel => this.FloatPropertyTemplate,
            Vector2PropertyViewModel => this.Vector2PropertyTemplate,
            Vector3PropertyViewModel => this.Vector3PropertyTemplate,
            Vector4PropertyViewModel => this.Vector4PropertyTemplate,
            QuaternionPropertyViewModel => this.QuaternionPropertyTemplate,
            _ => base.SelectTemplate(item, container),
        };
    }
}
