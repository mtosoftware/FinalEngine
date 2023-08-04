// <copyright file="LayoutInitializer.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Layout;

using System;
using AvalonDock.Layout;
using FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;
using FinalEngine.Editor.ViewModels.Docking.Tools.Projects;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;

public sealed class LayoutInitializer : ILayoutUpdateStrategy
{
    public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
    {
        // Not required.
    }

    public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
    {
        // Not required.
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

        anchorableToShow.CanShowOnHover = false;

        if (anchorableToShow.Content is IProjectExplorerToolViewModel or IConsoleToolViewModel)
        {
            var group = new LayoutAnchorGroup();
            group.Children.Add(anchorableToShow);
            layout.BottomSide.Children.Add(group);

            anchorableToShow.ToggleAutoHide();

            return true;
        }

        if (anchorableToShow.Content is ISceneHierarchyToolViewModel)
        {
            var group = new LayoutAnchorGroup();
            group.Children.Add(anchorableToShow);
            layout.LeftSide.Children.Add(group);

            anchorableToShow.ToggleAutoHide();

            return true;
        }

        if (anchorableToShow.Content is IPropertiesToolViewModel or IEntitySystemsToolViewModel)
        {
            var group = new LayoutAnchorGroup();
            group.Children.Add(anchorableToShow);
            layout.RightSide.Children.Add(group);

            anchorableToShow.ToggleAutoHide();

            return true;
        }

        return false;
    }

    public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
    {
        return false;
    }
}
