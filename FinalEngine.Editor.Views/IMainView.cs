// <copyright file="IMainView.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views
{
    using System;

    /// <summary>
    ///   Defines an interface that represents the main application view.
    /// </summary>
    public interface IMainView
    {
        /// <summary>
        ///   Gets or sets the action to perform when the application is exiting.
        /// </summary>
        /// <value>
        ///   The action to perform when the application is exiting.
        /// </value>
        Action? OnExit { get; set; }

        /// <summary>
        ///   Gets or sets the action to perform when the application has loaded.
        /// </summary>
        /// <value>
        ///   The action to perform when the application has loaded.
        /// </value>
        Action? OnLoad { get; set; }
    }
}
