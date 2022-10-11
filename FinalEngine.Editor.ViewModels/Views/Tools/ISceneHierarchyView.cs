// <copyright file="ISceneHierarchyView.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Views.Tools
{
    using System;

    public interface ISceneHierarchyView
    {
        event EventHandler OnContextDelete;

        event EventHandler OnContextOpening;
    }
}
