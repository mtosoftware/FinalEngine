// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop
{
    using System;
    using System.Windows.Forms;
    using DarkUI.Win32;
    using FinalEngine.Editor.Desktop.Views;
    using FinalEngine.Editor.Presenters;
    using FinalEngine.Editor.Presenters.Interactions;
    using FinalEngine.Editor.Services.Resources;
    using FinalEngine.Editor.Views;
    using FinalEngine.IO;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Resources;
    using Microsoft.Extensions.DependencyInjection;
    using ApplicationContext = FinalEngine.Editor.Desktop.Interactions.ApplicationContext;

    /// <summary>
    ///   The main application running on Windows.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///   Configures the applications services and returns an <see cref="IServiceProvider"/>.
        /// </summary>
        /// <returns>
        ///   The newly created <see cref="IServiceProvider"/> used to run the main application.
        /// </returns>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddSingleton<IOpenGLInvoker, OpenGLInvoker>();
            services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();
            services.AddSingleton<IResourceManager>(ResourceManager.Instance);
            services.AddSingleton<IResourceLoaderRegistrar, ResourceLoaderRegistrar>();

            services.AddSingleton<IApplicationContext, ApplicationContext>();
            services.AddSingleton<IApplicationStarter, MainForm>();
            services.AddSingleton<IPresenterFactory, PresenterFactory>();
            services.AddSingleton<IMainView, MainForm>();

            return services.BuildServiceProvider();
        }

        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.AddMessageFilter(new ControlScrollFilter());
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConfigureServices().GetRequiredService<IApplicationStarter>().StartApplication();
        }
    }
}
