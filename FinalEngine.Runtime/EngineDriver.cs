// <copyright file="EngineDriver.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System;
using FinalEngine.Input;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Runtime.Services;
using Microsoft.Extensions.DependencyInjection;

internal sealed class EngineDriver : IEngineDriver
{
    private readonly IEventsProcessor eventsProcessor;

    private readonly GameContainerBase game;

    private readonly IGameTime gameTime;

    private readonly IInputDriver inputDriver;

    private readonly IRenderContext renderContext;

    private bool isRunning;

    public EngineDriver(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));
        ServiceLocator.SetServiceProvider(serviceProvider);

        this.eventsProcessor = serviceProvider.GetRequiredService<IEventsProcessor>();
        this.gameTime = serviceProvider.GetRequiredService<IGameTime>();
        this.inputDriver = serviceProvider.GetRequiredService<IInputDriver>();
        this.renderContext = serviceProvider.GetRequiredService<IRenderContext>();
        this.game = serviceProvider.GetRequiredService<GameContainerBase>();
    }

    public void Start()
    {
        if (this.isRunning)
        {
            return;
        }

        this.Run();
    }

    private void Run()
    {
        this.isRunning = true;

        this.game.Initialize();
        this.game.LoadContent();

        while (this.isRunning && this.eventsProcessor.CanProcessEvents)
        {
            if (!this.gameTime.CanProcessNextFrame())
            {
                continue;
            }

            this.game.Update(GameTime.Delta);

            this.inputDriver.Update();

            this.game.Render(GameTime.Delta);

            this.renderContext.SwapBuffers();
            this.eventsProcessor.ProcessEvents();
        }

        this.game.UnloadContent();
        this.game.Dispose();
    }
}
