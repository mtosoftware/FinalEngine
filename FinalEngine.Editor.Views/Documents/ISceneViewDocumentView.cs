// <copyright file="ISceneViewDocumentView.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views.Documents
{
    using System;

    /// <summary>
    ///   Defines an interface that represents a scene view document.
    /// </summary>
    public interface ISceneViewDocumentView
    {
        /// <summary>
        ///   Gets or sets the action to perform when the scene view is rendered.
        /// </summary>
        /// <value>
        ///   The action to perform when the scene view is rendered.
        /// </value>
        Action? OnRender { get; set; }

        /// <summary>
        ///   Gets or sets the action to perform when the scene view has been resized.
        /// </summary>
        /// <value>
        ///   The action to perform when the scene view has been resized.
        /// </value>
        Action? OnResize { get; set; }
    }
}
