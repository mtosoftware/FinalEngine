// <copyright file="EntityInspectorToolWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Tools
{
    using System;
    using DarkUI.Docking;
    using FinalEngine.Editor.Presenters;

    /// <summary>
    ///   Provides an entity inspector tool window used to show the collection of components and their respective properties to be modified by the user.
    /// </summary>
    /// <seealso cref="DarkUI.Docking.DarkToolWindow"/>
    public partial class EntityInspectorToolWindow : DarkToolWindow
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="EntityInspectorToolWindow"/> class.
        /// </summary>
        /// <param name="presenterFactory">
        ///   The presenter factory.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="presenterFactory"/> parameter cannot be null.
        /// </exception>
        public EntityInspectorToolWindow(IPresenterFactory presenterFactory)
        {
            if (presenterFactory == null)
            {
                throw new ArgumentNullException(nameof(presenterFactory));
            }

            this.InitializeComponent();
        }
    }
}
