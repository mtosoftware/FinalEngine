// <copyright file="ConsoleToolWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Tools
{
    using System;
    using DarkUI.Docking;
    using FinalEngine.Editor.Presenters;

    /// <summary>
    ///   Provides a console tool window used to show the current state of the application and game to the user.
    /// </summary>
    /// <seealso cref="DarkToolWindow"/>
    public partial class ConsoleToolWindow : DarkToolWindow
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="ConsoleToolWindow"/> class.
        /// </summary>
        /// <param name="presenterFactory">
        ///   The presenter factory.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="presenterFactory"/> parameter cannot be null.
        /// </exception>
        public ConsoleToolWindow(IPresenterFactory presenterFactory)
        {
            if (presenterFactory == null)
            {
                throw new ArgumentNullException(nameof(presenterFactory));
            }

            this.InitializeComponent();
        }
    }
}
