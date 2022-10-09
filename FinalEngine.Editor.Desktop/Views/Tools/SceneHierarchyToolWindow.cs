// <copyright file="SceneHierarchyToolWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Tools
{
    using System;
    using DarkUI.Docking;
    using FinalEngine.Editor.Presenters;

    /// <summary>
    ///   Provides a scene hierarchy tool window used to show the entities within the currently loaded scene.
    /// </summary>
    /// <seealso cref="DarkUI.Docking.DarkToolWindow"/>
    public partial class SceneHierarchyToolWindow : DarkToolWindow
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="SceneHierarchyToolWindow"/> class.
        /// </summary>
        /// <param name="presenterFactory">
        ///   The presenter factory.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="presenterFactory"/> parameter cannot be null.
        /// </exception>
        public SceneHierarchyToolWindow(IPresenterFactory presenterFactory)
        {
            if (presenterFactory == null)
            {
                throw new ArgumentNullException(nameof(presenterFactory));
            }

            this.InitializeComponent();
        }
    }
}
