// <copyright file="PanesTemplateSelector.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Docking
{
    using System.Windows;
    using System.Windows.Controls;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    public class PanesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ProjectExplorerTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IProjectExplorerViewModel)
            {
                return this.ProjectExplorerTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}