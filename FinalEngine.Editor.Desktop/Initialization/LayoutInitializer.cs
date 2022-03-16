// <copyright file="LayoutInitializer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Initialization
{
    using System;
    using System.Linq;
    using AvalonDock.Layout;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    public class LayoutInitializer : ILayoutUpdateStrategy
    {
        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {
        }

        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {
        }

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
            anchorableToShow.CanShowOnHover = false; // TODO: What?

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

        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            return false;
        }
    }
}