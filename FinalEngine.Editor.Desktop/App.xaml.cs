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
using FinalEngine.Rendering.OpenGL.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(ConfigureAppConfiguration)
            .ConfigureServices(ConfigureServices)
            .Build();
    }

    private static IHost? AppHost { get; set; }

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

    private static void ConfigureAppConfiguration(IConfigurationBuilder builder)
    {
        string environment = Debugger.IsAttached ? "Development" : "Production";

        builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{environment}.json");
    }

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        var configuration = context.Configuration;

        services.AddLogging(builder =>
        {
            builder
            .AddConsole()
            .AddFile(configuration.GetSection("LoggingOptions"));
        });

        services.AddCommon();
        services.AddRendering();

        services.AddViewModelFactory<SceneViewModel>();
        services.AddSingleton<IMainViewModel, MainViewModel>();
    }
}
