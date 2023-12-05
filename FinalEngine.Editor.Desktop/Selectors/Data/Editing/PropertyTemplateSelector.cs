// <copyright file="PropertyTemplateSelector.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Data.Editing;

using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.ViewModels.Editing.DataTypes;

public sealed class PropertyTemplateSelector : DataTemplateSelector
{
    public DataTemplate? BoolPropertyTemplate { get; set; }

    public DataTemplate? DoublePropertyTemplate { get; set; }

    public DataTemplate? FloatPropertyTemplate { get; set; }

    public DataTemplate? IntPropertyTemplate { get; set; }

    public DataTemplate? QuaternionPropertyTemplate { get; set; }

    public DataTemplate? StringPropertyTemplate { get; set; }

    public DataTemplate? Vector2PropertyTemplate { get; set; }

    public DataTemplate? Vector3PropertyTemplate { get; set; }

    public DataTemplate? Vector4PropertyTemplate { get; set; }

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
