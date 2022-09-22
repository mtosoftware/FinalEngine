﻿// <copyright file="App.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop
{
    using System;
    using System.Windows;
    using FinalEngine.Editor.Common.Services.Factories;
    using FinalEngine.Editor.Common.Services.Projects;
    using FinalEngine.Editor.Common.Services.Scenes;
    using FinalEngine.Editor.Desktop.Interaction;
    using FinalEngine.Editor.Desktop.Views;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.ViewModels.Interaction;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Microsoft.Toolkit.Mvvm.Messaging;

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

        /// <summary>
        ///   Configures the services to be consumed by the application.
        /// </summary>
        /// <returns>
        ///   The newly configuerd <see cref="IServiceProvider"/>.
        /// </returns>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(x => x.AddConsole());
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            services.AddSingleton<IFileInvoker, FileInvoker>();
            services.AddSingleton<IDirectoryInvoker, DirectoryInvoker>();
            services.AddSingleton<IPathInvoker, PathInvoker>();
            services.AddSingleton<IFileSystem, FileSystem>();

            services.AddSingleton<IOpenGLInvoker, OpenGLInvoker>();
            services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();

            services.AddSingleton<ISceneRenderer, SceneRenderer>();

            services.AddSingleton<IProjectFileHandler, ProjectFileHandler>();
            services.AddSingleton<IGameTimeFactory, GameTimeFactory>();

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<IViewPresenter, ViewPresenter>();
            services.AddSingleton<IUserActionRequester, UserActionRequester>();
            services.AddSingleton<IMainViewModel, MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}