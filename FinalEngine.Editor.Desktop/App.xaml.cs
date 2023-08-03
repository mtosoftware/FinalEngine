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

    /// <summary>
    /// Starts up the main application on the current platform.
    /// </summary>
    /// <param name="e">
    ///   A <see cref="StartupEventArgs"/> that contains the event data.
    /// </param>
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

    /// <summary>
    /// Configures the services to be consumed by the application.
    /// </summary>
    /// <param name="context">
    /// The application host builder context.
    /// </param>
    /// <param name="services">
    /// The services to be consumed by the application.
    /// </param>
    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddConsole().SetMinimumLevel(Debugger.IsAttached ? LogLevel.Debug : LogLevel.Information);
        });

        services.AddSingleton<IApplicationContext, ApplicationContext>();
        services.AddTransient<IMainViewModel, MainViewModel>();
    }
}
