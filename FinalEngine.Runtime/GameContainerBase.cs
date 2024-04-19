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

public abstract class GameContainerBase : IDisposable
{
    protected GameContainerBase(
        IWindow window,
        IKeyboard keyboard,
        IMouse mouse,
        IRenderDevice renderDevice,
        IResourceManager resourceManager,
        IResourceLoaderFetcher fetcher)
    {
        ArgumentNullException.ThrowIfNull(fetcher, nameof(fetcher));

        this.Window = window ?? throw new ArgumentNullException(nameof(window));
        this.Keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
        this.Mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
        this.RenderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.ResourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));

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
