// <copyright file="ISceneHierarchyToolWindowView.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views.Tools
{
    using System;
    using System.ComponentModel;

    public interface ISceneHierarchyToolWindowView
    {
        event EventHandler OnClick;

        event EventHandler<CancelEventArgs> OnContextMenuOpening;
    }
}
