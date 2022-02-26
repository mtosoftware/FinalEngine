// <copyright file="App.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop
{
    using System.Windows;
    using FinalEngine.Editor.Desktop.Views;

    /// <summary>
    ///   Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///   Raises the <see cref="Application.Startup"/> event.
        /// </summary>
        /// <param name="e">
        ///   A <see cref="StartupEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var view = new MainView();
            view.Show();
        }
    }
}