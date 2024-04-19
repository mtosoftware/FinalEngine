// <copyright file="EngineDriver.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System;
using FinalEngine.Input;
using FinalEngine.Platform;
using FinalEngine.Rendering;

internal sealed class EngineDriver : IEngineDriver
{
    private readonly IEventsProcessor eventsProcessor;

    private readonly GameContainerBase game;

    private readonly IGameTime gameTime;

    private readonly IInputDriver inputDriver;

    private readonly IRenderContext renderContext;

    private bool isRunning;

    public EngineDriver(
        GameContainerBase game,
        IEventsProcessor eventsProcessor,
        IGameTime gameTime,
        IInputDriver inputDriver,
        IRenderContext renderContext)
    {
        this.game = game ?? throw new ArgumentNullException(nameof(game));
        this.eventsProcessor = eventsProcessor ?? throw new ArgumentNullException(nameof(eventsProcessor));
        this.gameTime = gameTime ?? throw new ArgumentNullException(nameof(gameTime));
        this.inputDriver = inputDriver ?? throw new ArgumentNullException(nameof(inputDriver));
        this.renderContext = renderContext ?? throw new ArgumentNullException(nameof(renderContext));
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
