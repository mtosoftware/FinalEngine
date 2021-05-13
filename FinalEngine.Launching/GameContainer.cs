// <copyright file="GameContainer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Input.Mouse;
    using FinalEngine.IO;
    using FinalEngine.Platform;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;

    public abstract class GameContainer : IDisposable
    {
        private bool isRunning;

        protected internal GameContainer(IPlatformResolver resolver)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException(nameof(resolver), $"The specified {nameof(resolver)} parameter cannot be null.");
            }

            IGamePlatformFactory factory = resolver.Resolve();

            factory.InitializePlatform(
                default,
                out IWindow window,
                out IEventsProcessor eventsProcessor,
                out IFileSystem fileSystem,
                out IKeyboard keyboard,
                out IMouse mouse,
                out IRenderContext renderContext,
                out IRenderDevice renderDevice,
                out ITexture2DLoader textureLoader);

            this.Window = window;
            this.EventsProcessor = eventsProcessor;

            this.FileSystem = fileSystem;

            this.Keyboard = keyboard;
            this.Mouse = mouse;

            this.RenderContext = renderContext;
            this.RenderDevice = renderDevice;

            this.TextureLoader = textureLoader;
        }

        protected GameContainer()
            : this(PlatformResolver.Instance)
        {
        }

        ~GameContainer()
        {
            this.Dispose(false);
        }

        protected IFileSystem FileSystem { get; }

        protected bool IsDisposed { get; private set; }

        protected IKeyboard Keyboard { get; }

        protected IMouse Mouse { get; }

        protected IRenderDevice RenderDevice { get; }

        protected ITexture2DLoader TextureLoader { get; }

        protected IWindow? Window { get; private set; }

        private IEventsProcessor EventsProcessor { get; }

        private IRenderContext? RenderContext { get; set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Exit()
        {
            this.isRunning = false;
        }

        public void Launch(double frameCap)
        {
            if (this.isRunning)
            {
                return;
            }

            this.isRunning = true;

            var gameTime = new GameTime(frameCap);

            while (this.isRunning && !(this.Window?.IsExiting ?? false))
            {
                this.Update(gameTime);

                this.Keyboard.Update();
                this.Mouse.Update();

                this.Render(gameTime);

                this.RenderContext?.SwapBuffers();
                this.EventsProcessor.ProcessEvents();
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
                if (this.RenderContext != null)
                {
                    this.RenderContext.Dispose();
                    this.RenderContext = null;
                }

                if (this.Window != null)
                {
                    this.Window.Dispose();
                    this.Window = null;
                }
            }

            this.IsDisposed = true;
        }

        protected virtual void Render(IGameTime gameTime)
        {
        }

        protected virtual void Update(IGameTime gameTime)
        {
        }
    }
}