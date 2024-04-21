// <copyright file="GameContainerBase.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Resources;
using FinalEngine.Runtime.Services;
using Microsoft.Extensions.DependencyInjection;

//// TODO: Dispose of resources and also engine driver dispose lmao

public abstract class GameContainerBase : IDisposable
{
    protected GameContainerBase()
    {
        this.Window = ServiceLocator.Provider.GetRequiredService<IWindow>();
        this.Keyboard = ServiceLocator.Provider.GetRequiredService<IKeyboard>();
        this.Mouse = ServiceLocator.Provider.GetRequiredService<IMouse>();
        this.RenderDevice = ServiceLocator.Provider.GetRequiredService<IRenderDevice>();
        this.ResourceManager = ServiceLocator.Provider.GetRequiredService<IResourceManager>();

        var fetcher = ServiceLocator.Provider.GetRequiredService<IResourceLoaderFetcher>();
        var loaders = fetcher.GetResourceLoaders();

        foreach (var loader in loaders)
        {
            this.ResourceManager.RegisterLoader(loader.GetResourceType(), loader);
        }
    }

    ~GameContainerBase()
    {
        this.Dispose(false);
    }

    protected bool IsDisposed { get; private set; }

    protected IKeyboard Keyboard { get; }

    protected IMouse Mouse { get; }

    protected IRenderDevice RenderDevice { get; }

    protected IResourceManager ResourceManager { get; }

    protected IWindow Window { get; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Exit()
    {
        this.Window.Close();
    }

    public virtual void Initialize()
    {
    }

    public virtual void LoadContent()
    {
    }

    public virtual void Render(float delta)
    {
    }

    public virtual void UnloadContent()
    {
    }

    public virtual void Update(float delta)
    {
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing)
        {
        }

        this.IsDisposed = true;
    }
}
