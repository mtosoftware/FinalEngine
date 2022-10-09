// <copyright file="ApplicationContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Interactions
{
    using System.Windows.Forms;
    using FinalEngine.Editor.Presenters.Interactions;

    /// <summary>
    ///   Provides a Windows Forms implemenation of an <see cref="IApplicationContext"/>.
    /// </summary>
    /// <seealso cref="IApplicationContext"/>
    public class ApplicationContext : IApplicationContext
    {
        /// <summary>
        ///   Exits the main application.
        /// </summary>
        public void ExitApplication()
        {
            Application.Exit();
        }
    }
}
