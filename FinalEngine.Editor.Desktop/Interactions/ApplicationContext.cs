// <copyright file="ApplicationContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Interactions
{
    using System.Windows.Forms;
    using FinalEngine.Editor.Views.Interactions;

    public class ApplicationContext : IApplicationContext
    {
        public void ExitApplication()
        {
            Application.Exit();
        }
    }
}
