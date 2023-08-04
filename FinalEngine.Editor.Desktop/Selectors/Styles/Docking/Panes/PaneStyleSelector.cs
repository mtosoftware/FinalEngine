// <copyright file="PaneStyleSelector.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Styles.Docking.Panes;

using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Tools;

public sealed class PaneStyleSelector : StyleSelector
{
    public Style? PaneStyle { get; set; }

    public Style? ToolStyle { get; set; }

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
