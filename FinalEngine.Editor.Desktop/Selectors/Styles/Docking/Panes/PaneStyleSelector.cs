// <copyright file="PaneStyleSelector.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
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
        return item switch
        {
            IToolViewModel => this.ToolStyle,
            IPaneViewModel => this.PaneStyle,
            _ => base.SelectStyle(item, container)
        };
    }
}
