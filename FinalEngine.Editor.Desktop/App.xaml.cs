// <copyright file="App.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop;

using System;
using System.IO;
using System.Windows;
using FinalEngine.Editor.Desktop.Views;
using FinalEngine.Editor.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var viewModel = ConfigureServices().GetRequiredService<IMainViewModel>();

        var view = new MainView()
        {
            DataContext = viewModel,
        };

        view.ShowDialog();
    }

    private static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration();

        services.AddLogging(x =>
        {
            x.AddConsole().AddFile(configuration.GetSection("LoggingOptions"));
        });

        services.AddSingleton<IMainViewModel, MainViewModel>();

        return services.BuildServiceProvider();
    }
}
