// <copyright file="LayoutInitializer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Initialization
{
    using System;
    using System.Linq;
    using AvalonDock.Layout;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    /// <summary>
    ///   Provides a layout initializer for a dockable view.
    /// </summary>
    /// <seealso cref="ILayoutUpdateStrategy"/>
    public class LayoutInitializer : ILayoutUpdateStrategy
    {
        /// <summary>
        ///   Occurs after anchorables have been inserted.
        /// </summary>
        /// <param name="layout">
        ///   The layout.
        /// </param>
        /// <param name="anchorableShown">
        ///   The anchorable shown.
        /// </param>
        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {
            // Not required.
        }

        /// <summary>
        ///   Occurs after documents have been inserted.
        /// </summary>
        /// <param name="layout">
        ///   The layout.
        /// </param>
        /// <param name="anchorableShown">
        ///   The anchorable shown.
        /// </param>
        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {
            // Not required.
        }

        /// <summary>
        ///   Occurs before anchorables have been inserted.
        /// </summary>
        /// <param name="layout">
        ///   The layout.
        /// </param>
        /// <param name="anchorableToShow">
        ///   The anchorable to show.
        /// </param>
        /// <param name="destinationContainer">
        ///   The destination container.
        /// </param>
        /// <returns>
        ///   Returns <c>false</c> if an anchorable has not been added, otherwise <c>true</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="anchorableToShow"/> parametr cannot be null.
        /// </exception>
        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
        {
            if (anchorableToShow == null)
            {
                throw new ArgumentNullException(nameof(anchorableToShow));
            }

            if (destinationContainer != null && destinationContainer.FindParent<LayoutFloatingWindow>() != null)
            {
                return false;
            }

            anchorableToShow.AutoHideWidth = 256;
            anchorableToShow.AutoHideHeight = 128;
            anchorableToShow.CanShowOnHover = false;

            if (anchorableToShow.Content is IProjectExplorerViewModel)
            {
                LayoutAnchorablePane? projectExplorerTool = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == "ProjectExplorerTool");

                if (projectExplorerTool != null)
                {
                    projectExplorerTool.Children.Add(anchorableToShow);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///   Occurs before documents have been inserted.
        /// </summary>
        /// <param name="layout">
        ///   The layout.
        /// </param>
        /// <param name="anchorableToShow">
        ///   The anchorable to show.
        /// </param>
        /// <param name="destinationContainer">
        ///   The destination container.
        /// </param>
        /// <returns>
        ///   Returns <c>false</c>.
        /// </returns>
        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            return false;
        }
    }
}