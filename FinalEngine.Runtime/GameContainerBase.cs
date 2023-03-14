// <copyright file="GameContainerBase.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.IO;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Resources;
using FinalEngine.Runtime.Rendering;
using Resources = FinalEngine.Resources.ResourceManager;

#nullable disable warnings

public abstract class GameContainerBase : IDisposable
{
    private readonly IEventsProcessor eventsProcessor;

    private IRenderContext renderContext;

    protected GameContainerBase(IRuntimeFactory factory)
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        factory.InitializeRuntime(
            out var window,
            out this.eventsProcessor,
            out var keyboardDevice,
            out var mouseDevice,
            out var fileSystem,
            out this.renderContext,
            out var renderDevice);

        this.Window = window;
        this.FileSystem = fileSystem;
        this.RenderDevice = renderDevice;

        this.Keyboard = new Keyboard(keyboardDevice);
        this.Mouse = new Mouse(mouseDevice);
        this.DisplayManager = new DisplayManager(this.RenderDevice.Rasterizer, this.Window);
        this.ResourceManager = Resources.Instance;

        this.DisplayManager.ChangeResolution(DisplayResolution.HighDefinition);
    }

    ~GameContainerBase()
    {
        this.Dispose(false);
    }

    protected IDisplayManager DisplayManager { get; }

    protected IFileSystem FileSystem { get; }

    protected bool IsDisposed { get; private set; }

    protected bool IsRunning { get; private set; }

    protected IKeyboard Keyboard { get; private set; }

    protected IMouse Mouse { get; private set; }

    protected IRenderDevice RenderDevice { get; }

    protected IResourceManager ResourceManager { get; private set; }

    protected IWindow Window { get; private set; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Exit()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(GameContainerBase));
        }

        this.IsRunning = false;
    }

    [ExcludeFromCodeCoverage]
    public void Run(double frameCap)
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(GameContainerBase));
        }

        this.Run(new GameTime(frameCap));
    }

    public void Run(IGameTime gameTime)
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(GameContainerBase));
        }

        this.IsRunning = true;

        this.Initialize();
        this.LoadResources();

        while (this.IsRunning)
        {
            if (!gameTime.CanProcessNextFrame())
            {
                continue;
            }

            this.Update();

            this.Keyboard.Update();
            this.Mouse.Update();

            this.Render();

            this.renderContext.SwapBuffers();
            this.eventsProcessor.ProcessEvents();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.Window != null)
            {
                this.Window.Dispose();
                this.Window = null;
            }

            if (this.Keyboard is Keyboard keyboard)
            {
                keyboard.Dispose();
                this.Keyboard = null;
            }

            if (this.Mouse is Mouse mouse)
            {
                mouse.Dispose();
                this.Mouse = null;
            }

            if (this.renderContext != null)
            {
                this.renderContext.Dispose();
                this.renderContext = null;
            }

            if (this.ResourceManager != null)
            {
                this.ResourceManager.Dispose();
                this.ResourceManager = null;
            }
        }

        this.IsDisposed = true;
    }

    protected virtual void Initialize()
    {
    }

    protected virtual void LoadResources()
    {
    }

    protected virtual void Render()
    {
    }

    protected virtual void Update()
    {
    }
}

#nullable enable warnings
