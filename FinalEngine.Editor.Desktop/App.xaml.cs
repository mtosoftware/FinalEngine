// <copyright file="App.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop
{
    using System;
    using System.Windows;
    using FinalEngine.Editor.Desktop.Interaction;
    using FinalEngine.Editor.Desktop.Views;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.ViewModels.Interaction;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

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
            IMainViewModel viewModel = ConfigureServices().GetRequiredService<IMainViewModel>();

            var view = new MainView()
            {
                DataContext = viewModel,
            };

            view.ShowDialog();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(x => x.AddConsole());

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<IViewPresenter, ViewPresenter>();
            services.AddSingleton<IMainViewModel, MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}