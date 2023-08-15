// <copyright file="App.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop;

using System.Diagnostics;
using System.IO.Abstractions;
using System.Windows;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Extensions;
using FinalEngine.Editor.Common.Models.Scenes;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Common.Services.Environment;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.Desktop.Services.Actions;
using FinalEngine.Editor.Desktop.Services.Layout;
using FinalEngine.Editor.Desktop.Views;
using FinalEngine.Editor.Desktop.Views.Dialogs.Layout;
using FinalEngine.Editor.ViewModels;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Docking.Panes.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;
using FinalEngine.Editor.ViewModels.Docking.Tools.Projects;
using FinalEngine.Editor.ViewModels.Docking.Tools.Scenes;
using FinalEngine.Editor.ViewModels.Interactions;
using FinalEngine.Editor.ViewModels.Services.Actions;
using FinalEngine.Editor.ViewModels.Services.Layout;
using FinalEngine.Rendering;
using FinalEngine.Rendering.OpenGL;
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

        services.AddTransient<IEntityWorld, EntityWorld>();
        services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();

        services.AddSingleton<IFileSystem, FileSystem>();

        services.AddTransient<IScene, Scene>();

        services.AddSingleton<IApplicationContext, ApplicationContext>();
        services.AddSingleton<IEnvironmentContext, EnvironmentContext>();
        services.AddSingleton<ISceneRenderer, SceneRenderer>();
        services.AddSingleton<ISceneManager, SceneManager>();

        services.AddFactory<IProjectExplorerToolViewModel, ProjectExplorerToolViewModel>();
        services.AddFactory<ISceneHierarchyToolViewModel, SceneHierarchyToolViewModel>();
        services.AddFactory<IPropertiesToolViewModel, PropertiesToolViewModel>();
        services.AddFactory<IConsoleToolViewModel, ConsoleToolViewModel>();
        services.AddFactory<IEntitySystemsToolViewModel, EntitySystemsToolViewModel>();
        services.AddFactory<ISceneViewPaneViewModel, SceneViewPaneViewModel>();
        services.AddFactory<IDockViewModel, DockViewModel>();
        services.AddFactory<ISaveWindowLayoutViewModel, SaveWindowLayoutViewModel>();
        services.AddFactory<IManageWindowLayoutsViewModel, ManageWindowLayoutsViewModel>();
        services.AddSingleton<IMainViewModel, MainViewModel>();

        services.AddTransient<IViewable<ISaveWindowLayoutViewModel>, SaveWindowLayoutView>();
        services.AddTransient<IViewable<IManageWindowLayoutsViewModel>, ManageWindowLayoutsView>();

        services.AddSingleton<IUserActionRequester, UserActionRequester>();
        services.AddSingleton<ILayoutManager, LayoutManager>();

        services.AddSingleton<IViewPresenter>(x =>
        {
            return new ViewPresenter(x.GetRequiredService<ILogger<ViewPresenter>>(), x);
        });
    }
}
