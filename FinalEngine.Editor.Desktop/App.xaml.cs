// <copyright file="App.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop;

using System.Diagnostics;
using System.IO.Abstractions;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Extensions;
using FinalEngine.Editor.Common.Models.Scenes;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Common.Services.Environment;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.Desktop.Services.Actions;
using FinalEngine.Editor.Desktop.Services.Layout;
using FinalEngine.Editor.Desktop.Views;
using FinalEngine.Editor.Desktop.Views.Dialogs.Entities;
using FinalEngine.Editor.Desktop.Views.Dialogs.Layout;
using FinalEngine.Editor.ViewModels;
using FinalEngine.Editor.ViewModels.Dialogs.Entities;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Inspectors;
using FinalEngine.Editor.ViewModels.Projects;
using FinalEngine.Editor.ViewModels.Scenes;
using FinalEngine.Editor.ViewModels.Services;
using FinalEngine.Editor.ViewModels.Services.Actions;
using FinalEngine.Editor.ViewModels.Services.Entities;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using FinalEngine.Editor.ViewModels.Services.Layout;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.Renderers;
using FinalEngine.Rendering.Systems.Queues;
using FinalEngine.Resources;
using FinalEngine.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public partial class App : Application
{
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .Build();
    }

    private static IHost? AppHost { get; set; }

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
        services.AddLogging(builder =>
        {
            builder.AddConsole().SetMinimumLevel(Debugger.IsAttached ? LogLevel.Debug : LogLevel.Information);
        });

        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        services.AddSingleton<IResourceManager>(ResourceManager.Instance);

        services.AddTransient<IEntityWorld>(x =>
        {
            var world = new EntityWorld();

            world.AddSystem(new RenderModelRenderQueueSystem(x.GetRequiredService<IRenderQueue<RenderModel>>()));

            return world;
        });

        services.AddSingleton<IRenderPipeline, OpenGLRenderPipeline>();
        services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();

        services.AddSingleton<IGeometryRenderer, GeometryRenderer>();
        services.AddSingleton<ISceneRenderer, SceneRenderer>();
        services.AddSingleton<ILightRenderer, LightRenderer>();
        services.AddSingleton<IRenderCoordinator, RenderCoordinator>();
        services.AddSingleton<IRenderingEngine, RenderingEngine>();

        //// TODO: ILightRenderer and IGeometryRenderer should just implement IRenderQueue directly.
        services.AddSingleton<IRenderQueue<Model>>(x => (IRenderQueue<Model>)x.GetRequiredService<IGeometryRenderer>());
        services.AddSingleton<IRenderQueue<RenderModel>>(x => (IRenderQueue<RenderModel>)x.GetRequiredService<IGeometryRenderer>());
        services.AddSingleton<IRenderQueue<Light>>(x => (IRenderQueue<Light>)x.GetRequiredService<ILightRenderer>());

        services.AddSingleton<IEngineInitializer, EngineInitializer>();

        services.AddSingleton<IFileSystem, FileSystem>();

        services.AddTransient<IScene, Scene>();

        services.AddSingleton<IApplicationContext, ApplicationContext>();
        services.AddSingleton<IEnvironmentContext, EnvironmentContext>();
        services.AddSingleton<IViewRenderer, ViewRenderer>();
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
        services.AddFactory<ICreateEntityViewModel, CreateEntityViewModel>();
        services.AddSingleton<IMainViewModel, MainViewModel>();

        services.AddTransient<IViewable<ISaveWindowLayoutViewModel>, SaveWindowLayoutView>();
        services.AddTransient<IViewable<IManageWindowLayoutsViewModel>, ManageWindowLayoutsView>();
        services.AddTransient<IViewable<ICreateEntityViewModel>, CreateEntityView>();

        services.AddSingleton<IEntityComponentTypeResolver, EntityComponentTypeResolver>();

        services.AddSingleton<IUserActionRequester, UserActionRequester>();
        services.AddSingleton<ILayoutManager, LayoutManager>();

        services.AddSingleton<IViewPresenter>(x =>
        {
            return new ViewPresenter(x.GetRequiredService<ILogger<ViewPresenter>>(), x);
        });
    }
}
