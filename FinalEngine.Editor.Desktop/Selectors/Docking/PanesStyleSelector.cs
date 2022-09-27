// <copyright file="PanesStyleSelector.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Selectors.Docking
{
    using System.Windows;
    using System.Windows.Controls;
    using FinalEngine.Editor.ViewModels.Docking.Panes;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    /// <summary>
    ///   Provides a style selector for an <see cref="IPaneViewModel"/>.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.StyleSelector"/>
    public class PanesStyleSelector : StyleSelector
    {
        /// <summary>
        ///   Gets or sets the document style.
        /// </summary>
        /// <value>
        ///   The document style.
        /// </value>
        public Style? DocumentStyle { get; set; }

        /// <summary>
        ///   Gets or sets the tool style.
        /// </summary>
        /// <value>
        ///   The tool style.
        /// </value>
        public Style? ToolStyle { get; set; }

        /// <summary>
        ///   When overridden in a derived class, returns a <see cref="Style"/> based on custom logic.
        /// </summary>
        /// <param name="item">
        ///   The content.
        /// </param>
        /// <param name="container">
        ///   The element to which the style will be applied.
        /// </param>
        /// <returns>
        ///   Returns an application-specific style to apply; otherwise, <see langword="null"/>.
        /// </returns>
        public override Style? SelectStyle(object item, DependencyObject container)
        {
            if (item is IToolViewModel)
            {
                return this.ToolStyle;
            }

            if (item is IPaneViewModel)
            {
                return this.DocumentStyle;
            }

            return base.SelectStyle(item, container);
        }
    }
}