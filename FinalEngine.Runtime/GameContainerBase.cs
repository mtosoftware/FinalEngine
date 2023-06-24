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

/// <summary>
/// Provides a standard game container abstraction used to run your game.
/// </summary>
/// <seealso cref="IDisposable" />
public abstract class GameContainerBase : IDisposable
{
    /// <summary>
    /// The events processor.
    /// </summary>
    private readonly IEventsProcessor eventsProcessor;

    /// <summary>
    /// The render context.
    /// </summary>
    private IRenderContext renderContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameContainerBase"/> class.
    /// </summary>
    /// <param name="factory">
    /// The runtime factory used to initialize the engine runtime.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="factory"/> parameter cannot be null.
    /// </exception>
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

    /// <summary>
    /// Finalizes an instance of the <see cref="GameContainerBase"/> class.
    /// </summary>
    ~GameContainerBase()
    {
        this.Dispose(false);
    }

    /// <summary>
    /// Gets the display manager.
    /// </summary>
    /// <value>
    /// The display manager.
    /// </value>
    protected IDisplayManager DisplayManager { get; }

    /// <summary>
    /// Gets the file system.
    /// </summary>
    /// <value>
    /// The file system.
    /// </value>
    protected IFileSystem FileSystem { get; }

    /// <summary>
    /// Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDisposed { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this instance is running.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is running; otherwise, <c>false</c>.
    /// </value>
    protected bool IsRunning { get; private set; }

    /// <summary>
    /// Gets the keyboard.
    /// </summary>
    /// <value>
    /// The keyboard.
    /// </value>
    protected IKeyboard Keyboard { get; private set; }

    /// <summary>
    /// Gets the mouse.
    /// </summary>
    /// <value>
    /// The mouse.
    /// </value>
    protected IMouse Mouse { get; private set; }

    /// <summary>
    /// Gets the render device.
    /// </summary>
    /// <value>
    /// The render device.
    /// </value>
    protected IRenderDevice RenderDevice { get; }

    /// <summary>
    /// Gets the resource manager.
    /// </summary>
    /// <value>
    /// The resource manager.
    /// </value>
    protected IResourceManager ResourceManager { get; private set; }

    /// <summary>
    /// Gets the window.
    /// </summary>
    /// <value>
    /// The window.
    /// </value>
    protected IWindow Window { get; private set; }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Exits the game, disposing of all resources.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="GameContainerBase"/> has been disposed.
    /// </exception>
    public void Exit()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(GameContainerBase));
        }

        this.IsRunning = false;
    }

    /// <summary>
    /// Runs the game at the soecified <paramref name="frameCap"/>.
    /// </summary>
    /// <param name="frameCap">
    /// The frame cap.
    /// </param>
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="GameContainerBase"/> has been disposed.
    /// </exception>
    [ExcludeFromCodeCoverage]
    public void Run(double frameCap = 120.0d)
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(GameContainerBase));
        }

        this.Run(new GameTime(frameCap));
    }

    /// <summary>
    /// Runs the game using the specified <paramref name="gameTime"/>.
    /// </summary>
    /// <param name="gameTime">
    /// The game time.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="gameTime"/> parameter cannot be null.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="GameContainerBase"/> has been disposed.
    /// </exception>
    public void Run(IGameTime gameTime)
    {
        if (gameTime == null)
        {
            throw new ArgumentNullException(nameof(gameTime));
        }

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

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
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

    /// <summary>
    /// Initializes the game.
    /// </summary>
    /// <remarks>
    /// Provide any initialization logic here, all resources should be loaded using the <see cref="LoadResources"/> function.
    /// </remarks>
    protected virtual void Initialize()
    {
    }

    /// <summary>
    /// Loads the resources to be used by the game.
    /// </summary>
    protected virtual void LoadResources()
    {
    }

    /// <summary>
    /// Renders the game.
    /// </summary>
    protected virtual void Render()
    {
    }

    /// <summary>
    /// Processes the game logic.
    /// </summary>
    protected virtual void Update()
    {
    }
}

#nullable enable warnings
