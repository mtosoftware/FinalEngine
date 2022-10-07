// <copyright file="GameContainerBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime
{
    using System;
    using System.Diagnostics;
    using FinalEngine.Input;
    using FinalEngine.IO;
    using FinalEngine.Platform;
    using FinalEngine.Rendering;
    using FinalEngine.Resources;
    using FinalEngine.Runtime.Invocation;
    using FinalEngine.Runtime.Settings;

    public abstract class GameContainerBase : IDisposable
    {
        private readonly IEventsProcessor eventsProcessor;

        private bool isRunning;

        private IRenderContext? renderContext;

        private IWindow? window;

        protected GameContainerBase(GameSettings settings, IRuntimeFactory factory)
        {
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            factory.InitializeRuntime(
                settings.WindowSettings.Size.Width,
                settings.WindowSettings.Size.Height,
                settings.WindowSettings.Title,
                out this.window,
                out this.eventsProcessor,
                out var fileSystem,
                out var keyboard,
                out var mouse,
                out this.renderContext,
                out var renderDevice);

            this.FileSystem = fileSystem;
            this.Keyboard = keyboard;
            this.Mouse = mouse;
            this.RenderDevice = renderDevice;
        }

        ~GameContainerBase()
        {
            this.Dispose(false);
        }

        protected IFileSystem FileSystem { get; private set; }

        protected bool IsDisposed { get; private set; }

        protected IKeyboard Keyboard { get; private set; }

        protected IMouse Mouse { get; private set; }

        protected IRenderDevice RenderDevice { get; private set; }

        protected GameSettings Settings { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Exit()
        {
            this.isRunning = false;
        }

        public void Run()
        {
            var watch = new Stopwatch();
            var watchInvoker = new StopwatchInvoker(watch);
            var gameTime = new GameTime(watchInvoker, this.Settings.FrameCap);

            if (this.isRunning)
            {
                return;
            }

            this.isRunning = true;

            this.Initialize();

            while (this.isRunning && (!this.window?.IsExiting ?? false))
            {
                if (gameTime.CanProcessNextFrame())
                {
                    this.Update();

                    this.Keyboard.Update();
                    this.Mouse.Update();

                    this.Render();

                    this.renderContext?.SwapBuffers();
                    this.eventsProcessor.ProcessEvents();
                }
            }

            ResourceManager.Instance.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.renderContext != null)
                {
                    this.renderContext.Dispose();
                    this.renderContext = null;
                }

                if (this.window != null)
                {
                    this.window.Dispose();
                    this.window = null;
                }
            }

            this.IsDisposed = true;
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void Render()
        {
        }

        protected virtual void Update()
        {
        }
    }
}
