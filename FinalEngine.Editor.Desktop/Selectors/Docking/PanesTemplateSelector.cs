// <copyright file="PanesTemplateSelector.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Docking
{
    using System.Windows;
    using System.Windows.Controls;
    using FinalEngine.Editor.ViewModels.Docking.Panes;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    /// <summary>
    ///   Provides a pane template style selector.
    /// </summary>
    /// <seealso cref="DataTemplateSelector"/>
    public class PanesTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        ///   Gets or sets the project explorer template.
        /// </summary>
        /// <value>
        ///   The project explorer template.
        /// </value>
        public DataTemplate? ProjectExplorerTemplate { get; set; }

        public DataTemplate? SceneTemplate { get; set; }

        /// <summary>
        ///   When overridden in a derived class, returns a <see cref="System.Windows.DataTemplate"/> based on custom logic.
        /// </summary>
        /// <param name="item">
        ///   The data object for which to select the template.
        /// </param>
        /// <param name="container">
        ///   The data-bound object.
        /// </param>
        /// <returns>
        ///   Returns a <see cref="System.Windows.DataTemplate"/> or <c>null</c>. The default value is <c>null</c>.
        /// </returns>
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is IProjectExplorerViewModel)
            {
                return this.ProjectExplorerTemplate;
            }

            if (item is ISceneViewModel)
            {
                return this.SceneTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}