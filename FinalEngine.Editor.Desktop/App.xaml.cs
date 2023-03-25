// <copyright file="App.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop;

using System.Diagnostics;
using System.IO;
using System.Windows;
using FinalEngine.Editor.Common.Extensions;
using FinalEngine.Editor.Desktop.Views;
using FinalEngine.Editor.ViewModels;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Extensions;
using FinalEngine.Rendering;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(ConfigureAppConfiguration)
            .ConfigureServices(ConfigureServices)
            .Build();
    }

    /// <summary>
    /// Gets or sets the application host.
    /// </summary>
    /// <value>
    /// The application host.
    /// </value>
    private static IHost? AppHost { get; set; }

    /// <summary>
    /// Exits the main application, disposing of any existing resources.
    /// </summary>
    /// <param name="e">
    /// The <see cref="ExitEventArgs"/> instance containing the event data.
    /// </param>
    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }

    /// <summary>
    /// Starts up the main application on the current platform.
    /// </summary>
    /// <param name="e">
    ///   A <see cref="StartupEventArgs"/> that contains the event data.
    /// </param>
    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var viewModel = AppHost.Services.GetRequiredService<IMainViewModel>();

        var view = new MainView()
        {
            DataContext = viewModel,
        };

        view.ShowDialog();
    }

    /// <summary>
    /// Configures the applications configuration.
    /// </summary>
    /// <param name="builder">
    /// The builder.
    /// </param>
    private static void ConfigureAppConfiguration(IConfigurationBuilder builder)
    {
        string environment = Debugger.IsAttached ? "Development" : "Production";

        builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{environment}.json");
    }

    /// <summary>
    /// Configures the services to be consumed by the application.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <param name="services">
    /// The services.
    /// </param>
    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        var configuration = context.Configuration;

        services.AddLogging(builder =>
        {
            builder
            .AddConsole()
            .AddFile(configuration.GetSection("LoggingOptions"));
        });

        services.AddSingleton<IOpenGLInvoker, OpenGLInvoker>();
        services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();

        services.AddCommon();

        services.AddViewModelFactory<SceneViewModel>();
        services.AddSingleton<IMainViewModel, MainViewModel>();
    }
}
