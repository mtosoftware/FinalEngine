// <copyright file="App.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop;

using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using FinalEngine.Editor.Common.Services.Rendering;
using FinalEngine.Editor.Desktop.Views;
using FinalEngine.Editor.ViewModels;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Extensions;
using FinalEngine.Rendering;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTK.Windowing.Desktop;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Starts up the main application on the current platform.
    /// </summary>
    /// <param name="e">
    ///   A <see cref="StartupEventArgs"/> that contains the event data.
    /// </param>
    protected override void OnStartup(StartupEventArgs e)
    {
        var viewModel = ConfigureServices().GetRequiredService<IMainViewModel>();

        var view = new MainView()
        {
            DataContext = viewModel,
        };

        view.ShowDialog();
    }

    /// <summary>
    /// Builds the configuration used throughout the lifetime of the application.
    /// </summary>
    /// <returns>
    /// The newly created <see cref="IConfiguration"/> to be used throughout the lifetime of the application.
    /// </returns>
    private static IConfiguration BuildConfiguration()
    {
        string environment = Debugger.IsAttached ? "Development" : "Production";

        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{environment}.json")
            .Build();
    }

    /// <summary>
    ///   Configures the services to be consumed by the application.
    /// </summary>
    /// <returns>
    ///   The newly configured <see cref="IServiceProvider"/>.
    /// </returns>
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration();

        services.AddLogging(builder =>
        {
            builder
            .AddConsole()
            .AddFile(configuration.GetSection("LoggingOptions"));
        });

        services.AddSingleton<GLFWGraphicsContext>();

        services.AddSingleton<IOpenGLInvoker, OpenGLInvoker>();
        services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();

        services.AddSingleton<ISceneRenderer, SceneRenderer>();

        services.AddViewModelFactory<SceneViewModel>();
        services.AddSingleton<IMainViewModel, MainViewModel>();

        return services.BuildServiceProvider();
    }
}
