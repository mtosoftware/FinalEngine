// <copyright file="App.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop;

using System.Diagnostics;
using System.Windows;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Desktop.Views;
using FinalEngine.Editor.ViewModels;
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

    private static bool IsDebugMode
    {
        get { return Debugger.IsAttached; }
    }

    /// <summary>
    /// Exits the main application, disposing of any existing resources.
    /// </summary>
    /// <param name="e">
    /// The <see cref="ExitEventArgs"/> instance containing the event data.
    /// </param>
    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync().ConfigureAwait(false);
        base.OnExit(e);
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync().ConfigureAwait(false);

        var viewModel = AppHost.Services.GetRequiredService<IMainViewModel>();

        var view = new MainView()
        {
            DataContext = viewModel,
        };

        view.ShowDialog();

        base.OnStartup(e);
    }

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole().SetMinimumLevel(IsDebugMode ? LogLevel.Debug : LogLevel.Information);
        });

        services.AddSingleton<IApplicationContext, ApplicationContext>();
        services.AddTransient<IMainViewModel, MainViewModel>();
    }
}
