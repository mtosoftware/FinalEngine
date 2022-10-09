// <copyright file="EntitySystemsToolWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Tools
{
    using System;
    using DarkUI.Docking;
    using FinalEngine.Editor.Presenters;

    /// <summary>
    ///   Provides an entity systems tool window used to add, remove and modify systems.
    /// </summary>
    /// <seealso cref="DarkUI.Docking.DarkToolWindow"/>
    public partial class EntitySystemsToolWindow : DarkToolWindow
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="EntitySystemsToolWindow"/> class.
        /// </summary>
        /// <param name="presenterFactory">
        ///   The presenter factory.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="presenterFactory"/> parameter cannot be null.
        /// </exception>
        public EntitySystemsToolWindow(IPresenterFactory presenterFactory)
        {
            if (presenterFactory == null)
            {
                throw new ArgumentNullException(nameof(presenterFactory));
            }

            this.InitializeComponent();
        }
    }
}
